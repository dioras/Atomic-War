using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace _1.Scripts.Ui.Settings
{
    public class FlagFiller : MonoBehaviour
    {
        [SerializeField] private GameObject flagButtonPrefab;
        [SerializeField] private List<Sprite> flags;

        

        private void Awake()
        {
            foreach (var flag in this.flags)
            {
                var flagButton = Instantiate(this.flagButtonPrefab, this.transform);
                flagButton.GetComponent<Image>().sprite = flag;
            }
        }
    }
}