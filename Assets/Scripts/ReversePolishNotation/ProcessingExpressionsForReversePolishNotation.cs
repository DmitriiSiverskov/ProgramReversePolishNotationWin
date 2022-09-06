using System;
using System.Collections.Generic;
using EventGlobal;
using UnityEngine;
using UnityEngine.UI;

namespace ReversePolishNotation
{
    public class ProcessingExpressionsForReversePolishNotation : MonoBehaviour
    {
        [SerializeField] private Text _inputText;
        [SerializeField] private Text _textNotation;

        private string _number = String.Empty;
        private string _symbol = String.Empty;
        private List<string> _listString = new List<string>();
        private void Awake()
        {
            MyEvent.BoolAndText.AddListener(SetFlag);
        }
        private void SetFlag(bool flag, string incomeText)
        {
            if (flag == false & incomeText == "=" & _inputText.text != "0")
                ExpressionEvaluation(_inputText.text);
        }
        private void ExpressionEvaluation(string inputText)
        {
            if (!inputText.Contains("("))
            {
                DataSortingMethodWithoutBrackets(inputText, _textNotation);
            }
        }
        private void DataSortingMethodWithoutBrackets(string inputText, Text textNotation)
        {
            TheMethodToFillTheLeafToPassOntoTheStack(inputText, _number, _symbol, _listString, textNotation);
        }
        private void TheMethodToFillTheLeafToPassOntoTheStack(string inputText, string number, string symbol, List<string> listString,Text textNotation)
        {
            for (int i = 0; i < inputText.Length; i++)
            {
                if (char.IsNumber(inputText[i]) | inputText[i] == '.')
                {
                    number += inputText[i].ToString();
                    if (i == inputText.Length - 1)
                    {
                        listString.Add(number);
                    }
                }
                else if (!char.IsNumber(inputText[i]))
                {
                    symbol = inputText[i].ToString();
                    listString.Add(number);
                    listString.Add(symbol);
                    number = String.Empty;
                }
            }
            MyEvent.ListInputText.Invoke(listString);
            textNotation.text = OutputMethodOfReversePolishNotationResult(listString);
        }
        private string OutputMethodOfReversePolishNotationResult(List<string> list)
        {
            string textNotation = String.Empty;
            string[] array = list.ToArray();
            if (list.Contains("*") || list.Contains("/"))
            {
                for (int i = 0; i < array.Length; i++)
                {
                    if (string.Equals(array[i], "*", StringComparison.Ordinal) ||
                        string.Equals(array[i], "/", StringComparison.Ordinal))
                    {
                        string temp = array[i + 1];
                        array[i + 1] = array[i];
                        array[i] = temp;
                        i++;
                    }
                }
            }
            else if (list.Contains("+") || list.Contains("-"))
            {
                for (int i = 0; i < array.Length; i++)
                {
                    if (string.Equals(array[i], "+", StringComparison.Ordinal) ||
                        string.Equals(array[i], "-", StringComparison.Ordinal))
                    {
                        if (char.IsNumber(array[i - 1][0]) & char.IsNumber(array[i + 1][0]))
                        {
                            string temp = array[i + 1];
                            array[i + 1] = array[i];
                            array[i] = temp;
                            i++;
                        }
                        else if (i == array.Length - 2 & char.IsNumber(array[i + 1][0]))
                        {
                            string temp = array[i + 1];
                            array[i + 1] = array[i];
                            array[i] = temp;
                            break;
                        }
                    }
                }
            }
            for (int i = 0; i < array.Length; i++)
            {
                textNotation += array[i];
                if (i < array.Length - 1)
                {
                    string number = array[i + 1];
                    if (char.IsNumber(number[0]))
                    {
                        textNotation += " ";
                    }
                }
            }
            list.Clear();
            return textNotation;
        }
    }
}