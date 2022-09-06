using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace EventGlobal
{
    public class MyEvent : MonoBehaviour
    {
        public static UnityEvent<string> OnEnemyText = new UnityEvent<string>();
        public static UnityEvent<bool,string> BoolAndText = new UnityEvent<bool,string>();
        public static UnityEvent<List<string>> ListInputText = new UnityEvent<List<string>>();
        
    }
}