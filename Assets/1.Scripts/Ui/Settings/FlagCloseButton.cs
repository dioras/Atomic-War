using UnityEngine;
using UnityEngine.EventSystems;

namespace _1.Scripts.Ui.Settings
{
    public class FlagCloseButton : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private GameObject chooseFlagPanel;

        
        
        public void OnPointerClick(PointerEventData eventData)
        {
            this.chooseFlagPanel.SetActive(false);
        }
    }
}