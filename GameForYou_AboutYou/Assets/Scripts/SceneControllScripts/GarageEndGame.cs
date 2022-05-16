using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarageEndGame : MonoBehaviour
{
    public GameObject EndGamePanel;
    public GameObject WallObj;


    [SerializeField] public AudioSource LevelCompleteSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent<CarMove>(out CarMove car))
        {
            EndGamePanel.SetActive(true);
            LevelCompleteSound.Play();
            WallObj.SetActive(true);
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
