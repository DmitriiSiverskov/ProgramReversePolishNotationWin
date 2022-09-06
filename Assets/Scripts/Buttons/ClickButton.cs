using EventGlobal;
using UnityEngine;
using UnityEngine.UI;

namespace Buttons
{
    public class ClickButton : MonoBehaviour
    {
        [SerializeField] private Text _textNameButton;
        [SerializeField] private string _nameButton;
        private void Awake()
        {
            SetupNameButton(_nameButton,_textNameButton);
            
        }
        private void SetupNameButton(string text, Text nameButton)
        {
            nameButton.text = text;

        }
        public void Click()
        {
           // MyEvent.SendEnemyText(_nameButton);
            MyEvent.OnEnemyText.Invoke(_nameButton);
        }
    }
}