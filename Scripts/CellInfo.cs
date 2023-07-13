using TMPro;
using UnityEngine;

public class CellInfo : MonoBehaviour
{
    public int Index;
    public string word;
    public GameObject Text;
    public int CollectedCategoryWords;
    public bool IsEmpty;
    // Потом будут добавлены еще поля.

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
        }
        else
        {
            GetComponent<CapsuleCollider>().enabled = true;
            if (CollectedCategoryWords != 1)
                Text.GetComponent<TextMeshPro>().text = CollectedCategoryWords.ToString() + "/8";
        }
    }
}
