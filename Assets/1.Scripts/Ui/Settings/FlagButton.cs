using System;
using _1.Scripts.Flags;
using _1.Scripts.GameEvents;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _1.Scripts.Ui.Settings
{
    public class FlagButton : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private GameObject chooseFlagPanel;
        [SerializeField] private Image flagView;
        
        
        
        public void OnPointerClick(PointerEventData eventData)
        {
            this.chooseFlagPanel.SetActive(true);
        }


        
        private void Awake()
        {
            this.flagView.sprite = FindObjectOfType<FlagsRepository>().GetDeviceFlag();
            
            EventRepository.FlagChanged.AddListener(OnFlagChanged);
        }

        private void OnDestroy()
        {
            EventRepository.FlagChanged.RemoveListener(OnFlagChanged);
        }

        
        
        private void OnFlagChanged(Sprite sprite)
        {
            this.flagView.sprite = sprite;
        }
    }
}