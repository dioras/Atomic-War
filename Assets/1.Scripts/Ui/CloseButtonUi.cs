﻿using UnityEngine;
using UnityEngine.UI;

namespace _1.Scripts.Ui
{
    public class CloseButtonUi : MonoBehaviour
    {
        [SerializeField] private GameObject panel;
        
        private Button _button;


        
        private void Awake()
        {
            this._button = GetComponent<Button>();
            this._button.onClick.AddListener(OpenSettings);
        }

        private void OnDestroy()
        {
            this._button.onClick.RemoveListener(OpenSettings);
        }

        
        
        private void OpenSettings()
        {
            this.panel.SetActive(false);
        }
    }
}