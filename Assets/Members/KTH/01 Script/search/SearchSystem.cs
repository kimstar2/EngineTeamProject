using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SearchSystem : MonoBehaviour
{
    [SerializeField] private TMP_InputField _searchInput;
    [SerializeField] private SearchResult _resultPrefab;
    [SerializeField] private Transform _resultContent;
    [SerializeField] private List<SearchData> _searchDatas;

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
            found = true;
        }

        if (!found)
        {
            SearchResult result = Instantiate(_resultPrefab, _resultContent);
            result.SetEmpty();
        }
    }

    private void ClearResults()
    {
        foreach (Transform child in _resultContent)
        {
            Destroy(child.gameObject);
        }
    }
}