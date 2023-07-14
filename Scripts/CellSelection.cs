using UnityEngine;

public class CellSelection : MonoBehaviour
{
    [SerializeField] private GameObject MainCamera;
    [SerializeField] private Sprite _imageCellFull;

    private void Start()
    {
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        _imageCellFull = Resources.Load<Sprite>("Full Cell");
    }

    private void OnMouseDown()
    {
        if (GetComponent<SpriteRenderer>().sprite == _imageCellFull)
            MainCamera.GetComponent<SelectionManager>().SelectNewCell(MainCamera.GetComponent<SelectionManager>().GetLastSelectedCell(),gameObject);
        else
        {
            GetComponent<SpriteRenderer>().sprite = _imageCellFull;
            MainCamera.GetComponent<SelectionManager>().DeleteSelection(gameObject);
        }
    }
}
