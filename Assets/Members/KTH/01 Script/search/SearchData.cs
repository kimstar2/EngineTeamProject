using Assets.Members.KJY.Scripts.Program.Func.FuncInternet;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "SearchData", menuName = "KTH/SearchData")]
public class SearchData : ScriptableObject
{
    public string Keyword;
    public string Title;
    public string Description;
    public WebSite openWebsite; 
}