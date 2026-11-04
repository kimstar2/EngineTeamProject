using TMPro;
using UnityEngine;

public class Notepad : MonoBehaviour
{
    [SerializeField] private TMP_InputField _InputField;
    private string Text => _InputField.text;
    
        public void SetText(string Text)
        {
            _InputField.text = Text;
        }

        public void clear()
        {
            _InputField.text = "";
        }
}
