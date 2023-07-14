using TMPro;
using UnityEngine;

public class CellInfo : MonoBehaviour
{
    public int Index;
    public string word;
    public GameObject Text, CategoryImageBox;
    public int CollectedCategoryWords;
    public int CountWordsInCategory;
    public Sprite CategoryImage;
    public bool IsEmpty;
    // ����� ����� ��������� ��� ����.

    private void Start()
    {
        CollectedCategoryWords = 1;
    }

    private void Update()
    {
        if (IsEmpty)
        {
            GetComponent<CapsuleCollider>().enabled = false;
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Empty Cell");
            Index = 0;
            word = "";
            Text.GetComponent<TextMeshPro>().text = "";
            CollectedCategoryWords = 1;
            CategoryImageBox.SetActive(false);
        }
        else
        {
            GetComponent<CapsuleCollider>().enabled = true;
            if (CollectedCategoryWords != 1)
            {
                CategoryImageBox.SetActive(true);
                CategoryImageBox.GetComponent<SpriteRenderer>().sprite = CategoryImage;
                Text.GetComponent<TextMeshPro>().text = CollectedCategoryWords.ToString() + "/" + CountWordsInCategory.ToString();
            }
        }
    }
}
