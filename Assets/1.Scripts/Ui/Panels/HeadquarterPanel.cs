using _1.Scripts.Buildings;
using _1.Scripts.GameEvents;
using UnityEngine;
using UnityEngine.UI;

namespace _1.Scripts.Ui.Panels
{
    public class HeadquarterPanel : MonoBehaviour
    {
        [SerializeField] private Sprite graySprite;

        [SerializeField] private Image headquarter1;
        [SerializeField] private Image headquarter2;
        [SerializeField] private Image headquarter3;

        private Image _nextImage;

        
        
        private void Awake()
        {
            this._nextImage = this.headquarter1;
            
            EventRepository.BuildingDestroyed.AddListener(OnBuildingDestroyed);
        }

        private void OnDestroy()
        {
            EventRepository.BuildingDestroyed.RemoveListener(OnBuildingDestroyed);
        }

        
        
        private void OnBuildingDestroyed(BaseBuilding baseBuilding, bool isPlayer)
        {
            if (baseBuilding is HeadquartersBuilding && !isPlayer)
            {
                this._nextImage.sprite = this.graySprite;
                this._nextImage.color = new Color(0.6603774f, 0.6603774f, 0.6603774f);
                
                if (this._nextImage == this.headquarter1)
                {
                    this._nextImage = this.headquarter2;
                }
                else if (this._nextImage == this.headquarter2)
                {
                    this._nextImage = this.headquarter3;
                } 
            }
        }
    }
}