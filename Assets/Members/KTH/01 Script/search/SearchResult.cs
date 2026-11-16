using Members.KJY.Scripts.Utils.Mono;
using System;
using TMPro;
using UnityEngine;

public class SearchResult : MonoBehaviour
{
    [SerializeField] private MonoTMP_UGUI _title;
    [SerializeField] private MonoTMP_UGUI _description;
    public event Action<SearchData> OnClicked;
    private SearchData _searchData;

    public void SetData(SearchData data)
    {
        _title.SetText(data.Title);
        _description.SetText(data.Description);
    }

    public void SetEmpty()
    {
        _title.SetText("검색 결과가 없습니다.");
        _description.SetText("");
    }

    public void Clicked()
    {
        OnClicked?.Invoke(_searchData);
    }
}