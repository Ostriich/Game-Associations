using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _selectedCells = new List<GameObject>();
    [SerializeField] private GameObject _lineBetweenCells;
    [SerializeField] private Sprite _imageCellFull, _imageCellSelected;

    public void SelectNewCell(GameObject lastSelected, GameObject cell)
    {
        if (_selectedCells.Count == 0)
            _selectedCells.Add(cell);
        else
        {
            Ray ray = new Ray(cell.transform.position,
                lastSelected.transform.position - cell.transform.position);

            RaycastHit hitCell;

            if (Physics.Raycast(ray, out hitCell))
            {
                GameObject line = Instantiate(_lineBetweenCells);
                line.GetComponent<LineControl>().SetLinePositions(cell.transform.position, hitCell.transform.position);

                SelectNewCell(lastSelected, hitCell.collider.gameObject);
            }

            cell.GetComponent<SpriteRenderer>().sprite = _imageCellSelected;
        }
        cell.GetComponent<SpriteRenderer>().sprite = _imageCellSelected;
        
        if (!_selectedCells.Contains(cell))
            _selectedCells.Add(cell);
    }

    public void DeleteSelection(GameObject cell)
    {
        int index = _selectedCells.IndexOf(cell);

        for (int i = index; i < _selectedCells.Count; i++)
            _selectedCells[i].GetComponent<SpriteRenderer>().sprite = _imageCellFull;

        _selectedCells.RemoveRange(index, _selectedCells.Count - index);

        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("LineBetweenCells"))
            Destroy(obj);

        if (_selectedCells.Count > 1)
            for (int i = 0; i < _selectedCells.Count-1; i++)
                SelectNewCell(_selectedCells[i],_selectedCells[i+1]);
    }

    public GameObject GetLastSelectedCell()
    {
        if (_selectedCells.Count == 0)
            return null;
        return _selectedCells[_selectedCells.Count-1];
    }

    public List<GameObject> GetSelectedCells()
    {
        return _selectedCells;
    }
}
