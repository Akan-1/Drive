using UnityEngine;

public class ButtonFXUI : MonoBehaviour
{
    public AudioSource myFX;
    public AudioClip HoverButtonSound;
    public AudioClip ClickButton;

    public void HoverSound()
    {
        myFX.PlayOneShot(HoverButtonSound);
    }

    public void ClickSound()
    {
        myFX.PlayOneShot(ClickButton);
    }
}
