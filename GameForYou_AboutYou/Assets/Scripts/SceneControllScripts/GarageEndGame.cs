using UnityEngine;

public class GarageEndGame : MonoBehaviour
{
    #region Public Fields
    public GameObject EndGamePanel;
    public GameObject WallObj;
    public GameObject child;
    public Transform parent;
    #endregion


    [SerializeField] private AudioSource LevelCompleteSound;
    [SerializeField] private AudioSource CarEngineOffSound;
    private CarMove carMove;
    private CarInputHandler carInputHandler;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent<CarMove>(out CarMove car))
        {
            EndGamePanel.SetActive(true);
            LevelCompleteSound.Play();
            WallObj.SetActive(true);
            SetParentAndOffScript();
        }
    }

    void SetParentAndOffScript()
    {
        child.transform.SetParent(parent);
        carMove = parent.GetComponentInChildren<CarMove>();
        carInputHandler = parent.GetComponentInChildren<CarInputHandler>();
        carMove.enabled = false;
        carMove.carRigidbody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY;
        carInputHandler.enabled = false;
        CarEngineOffSound.Play();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
