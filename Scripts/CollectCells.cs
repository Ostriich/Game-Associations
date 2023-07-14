using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectCells : MonoBehaviour
{
    [SerializeField] private List<GameObject> _selectedCells = new List<GameObject>();
    [SerializeField] private GameObject MainCamera;


    public void ClickCollect()
    {
        int categoryIndex = _selectedCells[0].GetComponent<CellInfo>().Index;

        for (int i = 1; i < _selectedCells.Count; i++)
        {
            if (_selectedCells[i].GetComponent<CellInfo>().Index != categoryIndex)
            {
                MainCamera.GetComponent<SelectionManager>().DeleteSelection(_selectedCells[0]);
                return;
            }
        }
        CollectSelectedCells();
    }

    private void Update()
    {
        _selectedCells = MainCamera.GetComponent<SelectionManager>().GetSelectedCells();

        if (_selectedCells.Count > 1)
        {
            GetComponent<Button>().enabled = true;
            GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
        else
        {
            GetComponent<Button>().enabled = false;
            GetComponent<Image>().color = new Color32(255, 255, 255, 100);
        }
    }


    private void CollectSelectedCells()
    {
        int countWordsCategory = 0;
        for (int i = 0; i < _selectedCells.Count-1; i++)
        {
            countWordsCategory += _selectedCells[i].GetComponent<CellInfo>().CollectedCategoryWords;
            _selectedCells[i].GetComponent<CellInfo>().IsEmpty = true;
        }

        _selectedCells[_selectedCells.Count-1].GetComponent<CellInfo>().CollectedCategoryWords += countWordsCategory;

        if (_selectedCells[_selectedCells.Count-1].GetComponent<CellInfo>().CollectedCategoryWords == _selectedCells[_selectedCells.Count - 1].GetComponent<CellInfo>().CountWordsInCategory)
            _selectedCells[_selectedCells.Count-1].GetComponent<CellInfo>().IsEmpty = true;

        MainCamera.GetComponent<SelectionManager>().DeleteSelection(_selectedCells[0]);
        MainCamera.GetComponent<LoadingLevel>().CheckEmptyLines();
    }
}
