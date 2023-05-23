using _1.Scripts.Characters;
using _1.Scripts.Ui;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace _1.Scripts.Buildings
{
    public class BuildingPrice : MonoBehaviour
    {
        public int Price => this.price;
        
        [SerializeField] private int price;
        [SerializeField] private BaseResources baseResources;
        [SerializeField] private GameObject overlay;
        [SerializeField] private TMP_Text priceText;



        private void Awake()
        {
            if (this.priceText && this.price > 0)
            {
                this.priceText.text = this.price.ToString();
            }
            
            OnGearsChanged(this.baseResources.Gear);
            
            this.baseResources.GearsChanged += OnGearsChanged;
        }

        private void OnDestroy()
        {
            this.baseResources.GearsChanged -= OnGearsChanged;
        }

        
        
        private void OnGearsChanged(int newValue)
        {
            if (this.overlay)
            {
                this.overlay.SetActive(this.price > this.baseResources.Gear);
            }
        }
    }
}