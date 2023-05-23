using System.Linq;
using _1.Scripts.Buildings;
using _1.Scripts.Characters;
using _1.Scripts.GameEvents;
using _1.Scripts.Games;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _1.Scripts.Ui
{
    public class RocketHintUi : MonoBehaviour
    {
        [SerializeField] private Image Arrow;
        [SerializeField] private BaseResources resources;
        [SerializeField] private BuildingPrice price;
        
        private GameProcessStateEnum _state;

        
        
        private void Awake()
        {
            EventRepository.GameProcessStateChanged.AddListener(OnGameProcessStateChanged);
        }

        private void Update()
        {
            if (this._state == GameProcessStateEnum.Build)
            {
                if (!FindObjectsOfType<RocketBuilding>().Any(b => b.isPlayer)
                    && !FindObjectsOfType<PreviewBuildingTrigger>().Any(b => b.name.Contains("Rocket Preview"))
                    && this.resources.Gear >= this.price.Price) 
                {
                    if (!this.Arrow.enabled)
                    {
                        this.Arrow.enabled = true;
                        this.Arrow.GetComponent<Animation>().Play();
                    }
                }
                else
                {
                    this.Arrow.enabled = false;
                    this.Arrow.GetComponent<Animation>().Stop();
                }
            }
            else if(this._state == GameProcessStateEnum.Battle)
            {
                if (this.Arrow.enabled)
                {
                    this.Arrow.enabled = false;
                    this.Arrow.GetComponent<Animation>().Stop();
                }
            }
        }

        private void OnDestroy()
        {
            EventRepository.GameProcessStateChanged.RemoveListener(OnGameProcessStateChanged);
        }

        
        
        private void OnGameProcessStateChanged(GameProcessStateEnum gameState)
        {
            this._state = gameState;
        }
    }
}