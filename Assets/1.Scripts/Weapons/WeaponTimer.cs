using System;
using _1.Scripts.Characters;
using _1.Scripts.GameEvents;
using _1.Scripts.Games;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

namespace _1.Scripts.Weapons
{
    public class WeaponTimer : MonoBehaviour
    {
        public float CurrentReload => this._currentReload;
        public float ReloadDuration => this.reloadDuration;
        
        [SerializeField] private float reloadDuration;
        [SerializeField] private GameProcess gameState;
        [SerializeField] private BaseResources resources;

        private float _currentReload;
        private bool _isRocket;
        private WeaponShooter _shooter;
        private RocketFlight _rocketFlight;


        
        private void Awake()
        {
            this._shooter = GetComponent<WeaponShooter>();
            
            EventRepository.RocketStarted.AddListener(OnRocketStarted);
            EventRepository.RocketFinished.AddListener(OnRocketFinished);
        }

        private void Update()
        {
            return;
            
            if (this.gameState.GameProcessState.CurrentGameProcessState != GameProcessStateEnum.Battle)
            {
                return;
            }

            if (this.resources.Rockets == 0)
            {
                return;
            }

            if (this._isRocket)
            {
                return;
            }
            
            this._currentReload += Time.deltaTime;

            if (this._currentReload < this.reloadDuration)
            {
                return;
            }

            this._currentReload = 0f;
            EventRepository.OnWeaponReloaded.Invoke(this.resources.IsPlayer);
        }

        private void OnDestroy()
        {
            EventRepository.RocketStarted.RemoveListener(OnRocketStarted);
            EventRepository.RocketFinished.RemoveListener(OnRocketFinished);
        }



        private void OnRocketStarted(RocketFlight rocketFlight, Transform arg1, bool isPlayer)
        {
            if (this._shooter.IsPlayer == isPlayer)
            {
                this._isRocket = true;
                this._rocketFlight = rocketFlight;
            }
        }
        private void OnRocketFinished(RocketFlight rocketFlight)
        {
            if (this._rocketFlight == rocketFlight)
            {
                this._isRocket = false;
                this._rocketFlight = null;
            }
        }
    }
}