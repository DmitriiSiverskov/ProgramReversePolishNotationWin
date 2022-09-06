using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using EventGlobal;
using UnityEngine;
using UnityEngine.UI;

namespace InputText
{
    public class HandlingExpressionsForComputation : MonoBehaviour
    {
        [SerializeField] private Text _inputText;

        private void Awake()
        {
            MyEvent.ListInputText.AddListener(MethodAcceptingGeneratedSheet);
        }
        private void MethodAcceptingGeneratedSheet(List<string> list)
        {
            StackDefinitionMethod(list,_inputText) ;
        }
        private void StackDefinitionMethod(List<string> list, Text inputText)
        {
            List<string> newList = new List<string>();
            newList.AddRange(list.ToArray());
            
            Stack<string> numberStack = new Stack<string>();
            Stack<string> stringStack = new Stack<string>();
            MethodForHandlingCalculationAndWritingToStack(newList,inputText,numberStack,stringStack);
        }
        private void MethodForHandlingCalculationAndWritingToStack(List<string> list, Text inputText, Stack<string> numberStack, Stack<string> characterStack)
        {
            float result = 0.0f;
            float numberOne = 0.0f;
            float numberTwo = 0.0f;
            int i = 0;
            while (list.Count != 0)
            {
                if (char.IsNumber(list[i][0]))
                {
                    numberStack.Push(list[i]);
                    list.RemoveAt(i);
                    if (list.Count == 0)
                    {
                        break;
                    }
                }
                if (string.Equals(list[i],"/",StringComparison.Ordinal) || 
                    string.Equals(list[i],"*",StringComparison.Ordinal))
                {
                    numberOne = float.Parse(numberStack.Pop());
                    char character = list[i][0];
                    list.RemoveAt(i);
                    numberTwo = float.Parse(list[i]);
                    list.RemoveAt(i);
                    switch (character)
                    {
                        case '/': result = numberOne / numberTwo;
                            break;
                        case '*': result = numberOne * numberTwo;
                            break;
                    }
                    numberStack.Push(result.ToString(CultureInfo.InvariantCulture));
                }
                else
                {
                    characterStack.Push(list[i]);
                    list.RemoveAt(i);
                }
            }
            MethodForDisplayingTheresultAndWritingToInputText(numberStack,characterStack,inputText);
        }
        private void MethodForDisplayingTheresultAndWritingToInputText(Stack<string> numberStack, Stack<string> characterStack,Text inputText)
        {
            double result = 0.0;
            string[] resultString = new string[numberStack.Count + characterStack.Count];
            int i = 0;
            while (numberStack.Count !=0)
            {
                if (numberStack.Count != 1)
                {
                    resultString[i] = numberStack.Pop();
                    i++;
                    resultString[i] = characterStack.Pop();
                    i++;
                }
                else
                {
                    resultString[i] = numberStack.Pop();
                    i = 0;
                }
            }
            string temp = String.Empty;
            for (int j = resultString.Length - 1; j != i; j--)
            {
                temp = resultString[i];
                resultString[i] = resultString[j];
                resultString[j] = temp;
                i++;
                temp = String.Empty;
            }

            foreach (var item in resultString)
            {
                temp += item;
            }
            result = Convert.ToDouble(new DataTable().Compute(temp, null));
            inputText.text = result.ToString(CultureInfo.InvariantCulture);
        }
        
    }
}