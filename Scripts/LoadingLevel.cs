using UnityEngine;
using System.IO;
using System.Collections.Generic;
using TMPro;

public class LoadingLevel : MonoBehaviour
{
    [SerializeField] private WordsField _wordsField;
    [SerializeField] private JsonFile _jsonFile = new JsonFile();
    [SerializeField] private string _filePath;
    [SerializeField] private int _indexRandomNumber = 0, _wordRandomNumber = 0;


    private void Start()
    {
        _filePath = Path.Combine(Application.streamingAssetsPath, "words.json"); 

        WWW reader = new WWW(_filePath);

        string _realPath = Application.persistentDataPath + "/words";
        File.WriteAllBytes(_realPath, reader.bytes);

        _jsonFile = JsonUtility.FromJson<JsonFile>(File.ReadAllText(_realPath));

        SetStartWords();
    }

    private void SetStartWords()
    {
        for (int i = 0; i < _wordsField.line.Count; i++)
        {
            for (int j = 0; j < _wordsField.line[0].cell.Count; j++)
            {
                _indexRandomNumber = Random.Range(0, _jsonFile.categories.Count);
                _wordRandomNumber = Random.Range(0, _jsonFile.categories[_indexRandomNumber].words.Count);

                _wordsField.line[i].cell[j].Text.GetComponent<TextMeshPro>().text = _jsonFile.categories[_indexRandomNumber].words[_wordRandomNumber].word;
                _wordsField.line[i].cell[j].Image.GetComponent<CellInfo>().Index = _jsonFile.categories[_indexRandomNumber].index;
                _wordsField.line[i].cell[j].Image.GetComponent<CellInfo>().word = _jsonFile.categories[_indexRandomNumber].words[_wordRandomNumber].word;

                _jsonFile.categories[_indexRandomNumber].words.RemoveAt(_wordRandomNumber);

                if (_jsonFile.categories[_indexRandomNumber].words.Count == 0)
                    _jsonFile.categories.RemoveAt(_indexRandomNumber);
            }
        }
    }

    public void CheckEmptyLines()
    {
        for (int i = 0; i < _wordsField.line.Count; i++)
        {
            int counterEmptyCells = 0;

            for (int j = 0; j < _wordsField.line[i].cell.Count; j++)
            {
                if (_wordsField.line[i].cell[j].Image.GetComponent<CellInfo>().IsEmpty)
                    counterEmptyCells++;
            }
            if (_jsonFile.categories.Count > 0)
                if (counterEmptyCells == 4)
                    ShiftCells(i);
        }
    }

    private void ShiftCells(int line)
    {
        for (int i = line; i > 0; i--)
        {
            for (int j = 0; j < _wordsField.line[i].cell.Count; j++)
            {
                _wordsField.line[i].cell[j].Image.GetComponent<CellInfo>().IsEmpty =
                   _wordsField.line[i - 1].cell[j].Image.GetComponent<CellInfo>().IsEmpty;
                _wordsField.line[i].cell[j].Text.GetComponent<TextMeshPro>().text =
                    _wordsField.line[i - 1].cell[j].Text.GetComponent<TextMeshPro>().text;
                _wordsField.line[i].cell[j].Image.GetComponent<SpriteRenderer>().sprite =
                    _wordsField.line[i - 1].cell[j].Image.GetComponent<SpriteRenderer>().sprite;
                _wordsField.line[i].cell[j].Image.GetComponent<CellInfo>().Index = 
                    _wordsField.line[i - 1].cell[j].Image.GetComponent<CellInfo>().Index;
                _wordsField.line[i].cell[j].Image.GetComponent<CellInfo>().word = 
                    _wordsField.line[i - 1].cell[j].Image.GetComponent<CellInfo>().word;
                _wordsField.line[i].cell[j].Image.GetComponent<CellInfo>().CollectedCategoryWords =
                    _wordsField.line[i - 1].cell[j].Image.GetComponent<CellInfo>().CollectedCategoryWords;
            }
        }
        SetNewWords();
    }

    private void SetNewWords()
    {
        for (int j = 0; j < _wordsField.line[0].cell.Count; j++)
        {
            _indexRandomNumber = Random.Range(0, _jsonFile.categories.Count);
            _wordRandomNumber = Random.Range(0, _jsonFile.categories[_indexRandomNumber].words.Count);

            _wordsField.line[0].cell[j].Text.GetComponent<TextMeshPro>().text = _jsonFile.categories[_indexRandomNumber].words[_wordRandomNumber].word;
            _wordsField.line[0].cell[j].Image.GetComponent<CellInfo>().Index = _jsonFile.categories[_indexRandomNumber].index;
            _wordsField.line[0].cell[j].Image.GetComponent<CellInfo>().word = _jsonFile.categories[_indexRandomNumber].words[_wordRandomNumber].word;
            _wordsField.line[0].cell[j].Image.GetComponent<CellInfo>().CollectedCategoryWords = 1;
            _wordsField.line[0].cell[j].Image.GetComponent<CellInfo>().IsEmpty = false;

            _jsonFile.categories[_indexRandomNumber].words.RemoveAt(_wordRandomNumber);

            if (_jsonFile.categories[_indexRandomNumber].words.Count == 0)
                _jsonFile.categories.RemoveAt(_indexRandomNumber);
            if (_jsonFile.categories.Count == 0)
                return;
        }
    }
}

[System.Serializable]
public class JsonFile
{
    public List<Cathegory> categories;

    [System.Serializable]
    public class Cathegory
    {
        public int index;
        public string name;
        public string description;
        public string icon;
        public int level;
        public List<Word> words;

        [System.Serializable]
        public class Word
        {
            public string word;
            public string description;
        }
    }
}

// Информация записывается неполная, ввиду особенностей тестового задания

[System.Serializable]
public class WordsField
{
    public List<Line> line;

    [System.Serializable]
    public class Line
    {
        public List<Cell> cell;
    }

    [System.Serializable]
    public class Cell
    {
        public GameObject Image;
        public GameObject Text;
    }
}


