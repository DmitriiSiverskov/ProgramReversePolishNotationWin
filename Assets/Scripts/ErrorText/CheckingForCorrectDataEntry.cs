using System;
using System.Collections;
using EventGlobal;
using UnityEngine;
using UnityEngine.UI;

namespace ErrorText
{
    public class CheckingForCorrectDataEntry : MonoBehaviour
    {
        [SerializeField] private GameObject _errorText;
        [SerializeField] private Text _textInput;
        private bool _flagOnCoroutine;
        private string nameButton = String.Empty;
        private void Awake()
        {
            _errorText.SetActive(false);
            MyEvent.OnEnemyText.AddListener(IncomingText);
        }
        private void Update()
        {
            if (_flagOnCoroutine)
            {
                StartCoroutine(StartOnOff());
            }
        }
        private IEnumerator StartOnOff()
        {
            yield return OnOffError(_errorText, _textInput);
        }
        private IEnumerator OnOffError(GameObject _errorText, Text textInput)
        {
            _errorText.SetActive(true);
            textInput.text = "0";
            var timer = 0f;
            while (timer < 2f)
            { 
                timer = Mathf.Min(timer + Time.deltaTime / 1, 2f);
                yield return null;
            }
            _errorText.SetActive(false);
            _flagOnCoroutine = false;
        }
        private void IncomingText(string textIncoming)
        {
            nameButton = textIncoming;
            CorrectDataEntry(textIncoming, _textInput.text);
        }
        private void CorrectDataEntry(string textIncoming, string textInput)
        {
            if (textIncoming == "-" || textIncoming == "+" || textIncoming == "*" || textIncoming == "/") 
                CheckingForTheArrangementOfMathematicalSigns(textInput);
            if (textIncoming == "(" || textIncoming == ")")
                CheckingForThePlacementOfMathematicalBrackets(textIncoming, textInput);
            if (textIncoming == ".") 
                PointCheck(textInput);
            if(textIncoming == "=")
                ValidationOfAMathematicalExpression(textInput);
        }
        private void CheckingForTheArrangementOfMathematicalSigns(string textInput)
        {
            if (textInput[^2] == '+' || textInput[^2] == '-' || textInput[^2] == '/' ||
                textInput[^2] == '*' || textInput[^2] == '.' || textInput[^2] == '(' )
            {
                _flagOnCoroutine = true;
            }
        }
        private void CheckingForThePlacementOfMathematicalBrackets(string textIncoming,string textInput)
        {
            if (textIncoming == "(")
            {
                if (textInput == "0")
                {
                    return;
                }
                if (char.IsNumber(textInput[^1]) || textInput[^1] == '(' || textInput[^1] == ')')
                {
                    _flagOnCoroutine = true;
                }
            }
            if (textIncoming == ")")
            {
                if (textInput.Contains("("))
                {
                    if (char.IsNumber(textInput[^1]))
                    {
                        for (int i = textInput.Length - 1; i > 1; i--)
                        {
                            if (textInput[i] == '+' || textInput[i] == '-' ||
                                textInput[i] == '*' || textInput[i] == '/')
                            {
                                _flagOnCoroutine = false;
                                return;
                            }
                        }
                        _flagOnCoroutine = true;
                    }
                    else
                    {
                        _flagOnCoroutine = true;
                    }
                }
                else
                {
                    _flagOnCoroutine = true;
                }
            }
        }
        private void PointCheck(string textInput)
        {
            
            if (!char.IsNumber(textInput[^2]))
            {
                _flagOnCoroutine = true;
            }
            if (textInput.Contains("."))
            {
                int indexPoint = 0;
                for (int i = textInput.Length - 1; i > -1; i--)
                {
                    if (textInput[i] == '.')
                    {
                        indexPoint = i;
                        break;
                    }
                }
                for (int i = indexPoint; i < textInput.Length; i++)
                {
                    if (textInput[i] != '+' || textInput[i] != '-' ||
                        textInput[i] != '*' || textInput[i] != '/')
                    {
                        return;
                    }
                }
                _flagOnCoroutine = true;
            }
        }
        private void ValidationOfAMathematicalExpression(string textInput)
        {
            if (textInput.Contains("("))
            {
                var countleftParenthesis = 0;
                var countRightParenthesis = 0;
                foreach (var item in textInput)
                {
                    if (item == ')') countleftParenthesis++;
                    if (item == '(') countRightParenthesis++;
                }
                if (countleftParenthesis != countRightParenthesis)
                {
                    _flagOnCoroutine = true;
                }
            }
            if (textInput[^1] == '+' || textInput[^1] == '-' ||
                textInput[^1] == '*' || textInput[^1] == '/')
            {
                _flagOnCoroutine = true;
            } 
            MyEvent.BoolAndText.Invoke(_flagOnCoroutine,nameButton);
        }
    }
}
