using System;
using System.Linq;
using _1.Scripts.Buildings;
using _1.Scripts.Characters;
using _1.Scripts.GameEvents;
using _1.Scripts.Games;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace _1.Scripts.Ui
{
    public class PlayerResourceUi : MonoBehaviour
    {
        [SerializeField] private TMP_Text gearsText;
        [SerializeField] private TMP_Text rocketsText;
        [SerializeField] private BaseResources playerResources;

        private GameProcessStateEnum _state;


        private void Awake()
        {
            EventRepository.GameProcessStateChanged.AddListener(OnGameProcessStateChanged);

            if (this._state == GameProcessStateEnum.None)
            {
                this._state = GameProcessStateEnum.Init;
            }
        }

        private void Start()
        {
            SetValues();
            
            this.playerResources.GearsChanged += PlayerResourcesOnGearsChanged;
            this.playerResources.RocketsChanged += PlayerResourcesOnRocketsChanged;
        }

        private void Update()
        {
            switch (this._state)
            {
                case GameProcessStateEnum.Battle:
                    this.rocketsText.text = FindObjectsOfType<RocketBuilding>().Count(c => c.IsShootReady && c.isPlayer).ToString();
                    break;
                
                case GameProcessStateEnum.Build:
                case GameProcessStateEnum.Init:
                    this.rocketsText.text = FindObjectsOfType<RocketBuilding>().Count(c => c.isPlayer).ToString();
                    break;
            }
        }

        private void OnDisable()
        {
            this.playerResources.GearsChanged -= PlayerResourcesOnGearsChanged;
            this.playerResources.RocketsChanged -= PlayerResourcesOnRocketsChanged;
            
        }

        private void OnDestroy()
        {
            EventRepository.GameProcessStateChanged.RemoveListener(OnGameProcessStateChanged);
        }


        private void SetValues()
        {
            this.gearsText.text = this.playerResources.Gear.ToString();
            this.rocketsText.text = this.playerResources.Rockets.ToString();
        }

        private void PlayerResourcesOnGearsChanged(int newValue)
        {
            this.gearsText.text = newValue.ToString();
        }

        private void PlayerResourcesOnRocketsChanged(int newValue)
        {
            this.rocketsText.text = newValue.ToString();
        }

        private void OnGameProcessStateChanged(GameProcessStateEnum state)
        {
            this._state = state;
        }
    }
}