using System;
using UnityEngine;

namespace Factory
{
    public class FactoryCreations : MonoBehaviour
    {
        [SerializeField] private GameObject _prefabProgram;

        private void Start()
        {
            Instantiate(_prefabProgram);
        }
    }
}