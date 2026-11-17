using Assets.Members.KJY.Scripts.Program.Entry;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SearchSystem : MonoBehaviour
{
    [SerializeField] private TMP_InputField _searchInput;
    [SerializeField] private SearchResult _resultPrefab;
    [SerializeField] private Transform _resultContent;
    [SerializeField] private Transform websiteParent;
    private List<SearchData> _searchDatas = new();
    private List<SearchResult> searchResults = new();

    public void SetSearchData(InternetEntry Internetentry)
    {
        _searchDatas.Clear();
        foreach (SearchData data in Internetentry.SearchList)
            _searchDatas.Add(data);
    }

    public void Search()
    {
        ClearResults();

        string keyword = _searchInput.text;
        bool found = false;

        foreach (SearchData data in _searchDatas)
        {
            if (data.Keyword != keyword) continue;

            SearchResult result = Instantiate(_resultPrefab, _resultContent);
            result.SetData(data);
            result.OnClicked += HandleClickedResult;
            searchResults.Add(result);
            found = true;
        }

        if (!found)
        {
            SearchResult result = Instantiate(_resultPrefab, _resultContent);
            result.SetEmpty();
        }
    }

    private void HandleClickedResult(SearchData data)
    {
        var website = Instantiate(data.openWebsite, websiteParent);
    }

    private void ClearResults()
    {
        foreach (SearchResult result in searchResults)
        {
            result.OnClicked -= HandleClickedResult;
            Destroy(result);
        }
    }

    private void OnDestroy()
    {
        ClearResults() ;
    }
}