using TMPro;
using UnityEngine;

public class SearchResult : MonoBehaviour
{
    private TMP_Text _title;
    private TMP_Text _description;

    private void Awake()
    {
        _title = transform.Find("Title").GetComponent<TMP_Text>();
        _description = transform.Find("Description").GetComponent<TMP_Text>();
    }

    public void SetData(SearchData data)
    {
        _title.text = data.Title;
        _description.text = data.Description;
    }

    public void SetEmpty()
    {
        _title.text = "검색 결과가 없습니다.";
        _description.text = "";
    }
}