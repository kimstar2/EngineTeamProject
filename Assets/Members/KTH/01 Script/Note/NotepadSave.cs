using TMPro;
using UnityEngine;

public class NotepadSave : MonoBehaviour
{
    [SerializeField] private TMP_InputField _inputField;

    private const string SaveKey = "NotepadText";

    public void Save()
    {
        PlayerPrefs.SetString(SaveKey, _inputField.text);
        PlayerPrefs.Save();

        Debug.Log("메모 저장");
    }

    public void Load()
    {
        _inputField.text = PlayerPrefs.GetString(SaveKey, "");

        Debug.Log("메모 불러오기");
    }
}
