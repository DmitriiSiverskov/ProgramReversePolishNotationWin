using System;
using System.Linq;
using EventGlobal;
using UnityEngine;
using UnityEngine.UI;

namespace InputText
{
    public class SendingTextToInputText : MonoBehaviour
    {
        [SerializeField] private Text _inputText;
        [SerializeField] private Text _textNotation;
        
        private void Awake()
        {
            _inputText = GetComponent<Text>();
            _inputText.text = "0";
            MyEvent.OnEnemyText.AddListener(SettingText);
        }
        private void SettingText(string text)
        {
            CheckingForRowStatus(text, _inputText, _textNotation);
        }
        private void CheckingForRowStatus(string text, Text inputText, Text textNotation)
        {
            switch (inputText.text)
            {
                case "0" :
                    if (text == "." || text == "+" || text == "/" || text == "*" || text == "-")
                    {
                        inputText.text += RestrictionMethodAddingOnlyNumbersAndMathematicalSymbols(text,inputText);
                        return;
                    }
                    if (text == "=" || text == "AC")
                    {
                        inputText.text = "0";
                        
                        return;
                    }
                    inputText.text = RestrictionMethodAddingOnlyNumbersAndMathematicalSymbols(text, inputText);
                    break;
                default:
                    if (text == "AC")
                    {
                        inputText.text = "0";
                        textNotation.text = String.Empty;
                        return;
                    }
                    inputText.text += RestrictionMethodAddingOnlyNumbersAndMathematicalSymbols(text, inputText);
                        break;
            }
        }
        private string RestrictionMethodAddingOnlyNumbersAndMathematicalSymbols(string text,Text inputText)
        {
            if (inputText.text.Length < 30)
            {
                if (text.Any(c => char.IsNumber(c)) ||
                    text == "+" || text == "-" || text == "*" || 
                    text == "/" || text == "." || text == "AC" ||
                    text == "(" || text == ")" )
                {
                    return text;
                }
            }
            return String.Empty;
        }
    }
}