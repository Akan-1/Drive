using UnityEngine;
using UnityEngine.UI;
public class PackagePicker : MonoBehaviour
{
    [SerializeField] private AudioSource pickUpSound;
    
    private int packageCount = 0;
    
    public Text PackCounter;
    public GameObject garageWall;
    public GameObject AllPackagesPickedup;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pack"))
        {
            packageCount++;
            pickUpSound.Play();
            PackCounter.text = packageCount.ToString();
            Destroy(collision.gameObject);
            if (packageCount == 8)
            {
                garageWall.SetActive(false);
                AllPackagesPickedup.SetActive(true);
            }
        }
    }
}
