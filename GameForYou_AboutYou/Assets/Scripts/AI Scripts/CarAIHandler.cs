using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CarAIHandler : MonoBehaviour
{
    public enum AIMode {followPlayer, followWaypoints, followMouse};

    [Header("AI settings")]
    public AIMode aiMode;
    public float MaxSpeed = 16;
    public bool isAvoidCars = true;
    [Range(0.0f, 1.0f)]
    public float skillLevel = 1.0f;


    //Local variables
    Vector3 targetPosition = Vector3.zero;
    Transform targetTransform = null;
    float originalMaximumSpeed = 0;

    Vector2 avoidanceVectorLerped = Vector3.zero;

    WaypointNode currentWaypoint = null;
    WaypointNode previousWaypoint = null;
    WaypointNode[] allWayPoints;

    //Components
    CarMove carMove;

    PolygonCollider2D polygonCollider2D;

    //Awake is called when the script instance is being loaded.
    void Awake()
    {
        carMove = GetComponent<CarMove>();
        allWayPoints = FindObjectsOfType<WaypointNode>();
        polygonCollider2D = GetComponentInChildren<PolygonCollider2D>();
        originalMaximumSpeed = MaxSpeed;
    }

   

    // Start is called before the first frame update
    void Start()
    {
        SetMaxSpeedBasedOnSkillLevel(MaxSpeed);
    }



    // Update is called once per frame and is frame dependent
    void FixedUpdate()
    {
        Vector2 inputVector = Vector2.zero;

        switch (aiMode)
        {
            case AIMode.followPlayer:
                FollowPlayer();
                break;
            case AIMode.followWaypoints:
                FollowWaypoints();
                break;
            case AIMode.followMouse:
                FollowMousePosition();
                break;
        }

        inputVector.x = TurnTowardTarget();
        inputVector.y = ApplyThrottleOrBrake(inputVector.x);

        //Send the input to the car controller.
        carMove.SetInputVector(inputVector);
    }

    //AI follows player
    void FollowPlayer()
    {
        if (targetTransform == null)
            targetTransform = GameObject.FindGameObjectWithTag("Player").transform;

        if (targetTransform != null)
            targetPosition = targetTransform.position;
    }

    void FollowMousePosition()
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        targetPosition = worldPosition;
    }

    //AI follows waypoints
    void FollowWaypoints()
    {
        //Pick the cloesest waypoint if we don't have a waypoint set.
        if (currentWaypoint == null)
        {
            currentWaypoint = FindClosestWayPoint();
            previousWaypoint = currentWaypoint;
        }
        //Set the target on the waypoints position
        if (currentWaypoint != null)
        {
            //Set the target position of for the AI. 
            targetPosition = currentWaypoint.transform.position;

            //Store how close we are to the target
            float distanceToWayPoint = (targetPosition - transform.position).magnitude;

            if (distanceToWayPoint > 20)
            {
                Vector3 nearestPointOnTheWaypointLine = FindNearesPointOnLine(previousWaypoint.transform.position, currentWaypoint.transform.position, transform.position);

                float segments = distanceToWayPoint / 20.0f;

                targetPosition = (targetPosition + nearestPointOnTheWaypointLine * segments) / (segments + 1);

                Debug.DrawRay(transform.position, targetPosition, Color.cyan);
            }
            //Check if we are close enough to consider that we have reached the waypoint
            if (distanceToWayPoint <= currentWaypoint.minDistanceToReachWaypoint)
            {
                if (currentWaypoint.maxSpeed > 0)
                    SetMaxSpeedBasedOnSkillLevel(currentWaypoint.maxSpeed);
                else SetMaxSpeedBasedOnSkillLevel(1000);

                previousWaypoint = currentWaypoint;

                //If we are close enough then follow to the next waypoint, if there are multiple waypoints then pick one at random.
                currentWaypoint = currentWaypoint.nextWaypointNode[Random.Range(0, currentWaypoint.nextWaypointNode.Length)];
            }
        }
    }

    WaypointNode FindClosestWayPoint()
    {
        return allWayPoints
            .OrderBy(t => Vector3.Distance(transform.position, t.transform.position))
            .FirstOrDefault();
    }

    float TurnTowardTarget()
    {
        Vector2 vectorToTarget = targetPosition - transform.position;
        vectorToTarget.Normalize();

        if (isAvoidCars)
            AvoidAICars(vectorToTarget, out vectorToTarget);


        //Calculate an angle towards the target 
        float angleToTarget = Vector2.SignedAngle(transform.up, vectorToTarget);
        angleToTarget *= -1;

        
        
        //We want the car to turn as much as possible if the angle is greater than 45 degrees and we wan't it to smooth out so if the angle is small we want the AI to make smaller corrections. 
        float steerAmount = angleToTarget / 45.0f;

        //Clamp steering to between -1 and 1.
        steerAmount = Mathf.Clamp(steerAmount, -1.0f, 1.0f);

        return steerAmount;
    }

    float ApplyThrottleOrBrake(float inputX)
    {
        //If we are going too fast then do not accelerate further. 
        if (carMove.GetVelocityMagnitude() > MaxSpeed)
            return 0;

        float reduceSeedDueToCornering = Mathf.Abs(inputX) / 1.0f;

        //Apply throttle forward based on how much the car wants to turn. If it's a sharp turn this will cause the car to apply less speed forward.
        return 1.05f - reduceSeedDueToCornering * skillLevel;
    }

    void SetMaxSpeedBasedOnSkillLevel(float newSpeed)
    {
        MaxSpeed = Mathf.Clamp(newSpeed, 0, originalMaximumSpeed);

        float skillBasedMaxSpeed = Mathf.Clamp(skillLevel, 0.3f, 1.0f);
        MaxSpeed = MaxSpeed * skillBasedMaxSpeed;
    }

    Vector2 FindNearesPointOnLine(Vector2 lineStartPosition, Vector2 lineEndPosition, Vector2 point)
    {
        Vector2 lineHeadingVector = (lineEndPosition - lineStartPosition);

        float maxDistance = lineHeadingVector.magnitude;
        lineHeadingVector.Normalize();

        Vector2 lineVectorStartToPoint = point - lineStartPosition;
        float dotProduct = Vector2.Dot(lineVectorStartToPoint, lineHeadingVector);

        dotProduct = Mathf.Clamp(dotProduct, 0f, maxDistance);

        return lineStartPosition + lineHeadingVector * dotProduct;
    }

    bool IsCarsInFrontOfAICar(out Vector3 position, out Vector3 otherCarRightVector)
    {
        polygonCollider2D.enabled = false;

        RaycastHit2D raycastHit2D = Physics2D.CircleCast(transform.position + transform.up * 0.5f, 1.2f, transform.up, 12, 1 << LayerMask.NameToLayer("Car") | 1 << LayerMask.NameToLayer("Tree"));

        polygonCollider2D.enabled = true;

        if(raycastHit2D.collider != null)
        {
            Debug.DrawRay(transform.position, transform.up * 12, Color.red);

            position = raycastHit2D.collider.transform.position;
            otherCarRightVector = raycastHit2D.collider.transform.right;

            return true;
        }
        else
        {
            Debug.DrawRay(transform.position, transform.up * 12, Color.black);
        }

        position = Vector3.zero;
        otherCarRightVector = Vector3.zero;

        return false;
    }

    void AvoidAICars(Vector2 vectorToTarget, out Vector2 newVectorToTarget)
    {
        if(IsCarsInFrontOfAICar(out Vector3 position, out Vector3 otherCarRightVector))
        {
            Vector2 avoidVector = Vector2.zero;

            avoidVector = Vector2.Reflect((position - transform.position).normalized, otherCarRightVector);

            float distanceToTarget = (targetPosition - transform.position).magnitude;

            float driveToTargetInfluence = 6.0f / distanceToTarget;

            driveToTargetInfluence = Mathf.Clamp(driveToTargetInfluence, 0.30f, 1.0f);

            float avoidInfluence = 1.0f - driveToTargetInfluence;

            avoidanceVectorLerped = Vector2.Lerp(avoidanceVectorLerped, avoidVector, Time.fixedDeltaTime * 4);

            newVectorToTarget = (vectorToTarget * driveToTargetInfluence + avoidVector * avoidInfluence);
            newVectorToTarget.Normalize();

            Debug.DrawRay(transform.position, transform.up * 10, Color.green);

            Debug.DrawRay(transform.position, transform.up * 10, Color.yellow);

            return;
        }
        newVectorToTarget = vectorToTarget;
    }
} 