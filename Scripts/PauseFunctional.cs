using UnityEngine;

public class PauseFunctional : MonoBehaviour
{
    [SerializeField] GameObject PanelPause;

    public void OpenPause()
    {
        PanelPause.SetActive(true);
    }

    public void ClosePause()
    {
        PanelPause.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PanelPause.activeSelf)
                ClosePause();
            else
                OpenPause();
        }
    }
}
