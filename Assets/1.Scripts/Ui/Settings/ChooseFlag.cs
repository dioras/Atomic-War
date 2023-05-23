using _1.Scripts.Flags;
using _1.Scripts.GameEvents;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _1.Scripts.Ui.Settings
{
    public class ChooseFlag : MonoBehaviour, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            var sprite = GetComponent<Image>().sprite;
            FindObjectOfType<FlagsRepository>().ApplyFlag(sprite.name);
            
            EventRepository.FlagChanged.Invoke(sprite);
            
            this.transform.parent.parent.parent.parent.gameObject.SetActive(false);
        }
    }
}