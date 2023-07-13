using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public void SetVolume(GameObject soundButton)
    {
        if (AudioListener.volume == 0)
        {
            soundButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("SoundOn");
            AudioListener.volume = 1;
        }
        else
        {
            soundButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("SoundOff"); ;
            AudioListener.volume = 0;
        }
    }
}
