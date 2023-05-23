using System;
using System.Linq;
using _1.Scripts.Buildings;
using _1.Scripts.Characters;
using _1.Scripts.Enemy;
using _1.Scripts.GameEvents;
using UnityEngine;

namespace _1.Scripts.Weapons
{
    public class WeaponShooter : MonoBehaviour
    {
        public bool IsPlayer => this._isPlayer;
        
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Transform barrel;
        [SerializeField] private BaseResources resources;
        [SerializeField] private EnemyAim aim;
        
        private int _count = 0;
        private bool _isPlayer;
        private Transform _target;


        
        private void Start()
        {
            this._isPlayer = GetComponent<BaseBuilding>().isPlayer;
            this._target = FindObjectsOfType<WeaponTargetOwner>().Single(w => w.IsPlayer).transform;
        }

        private void OnEnable()
        {
            EventRepository.OnWeaponReloaded.AddListener(Shoot);
        }

        private void OnDisable()
        {
            EventRepository.OnWeaponReloaded.RemoveListener(Shoot);
        }

        
        
        private void Shoot(bool isPlayer)
        {
            if (this._isPlayer != isPlayer)
            {
                return;
            }
            
            if (this.resources.Rockets <= 0)
            {
                return;
            }
            
            if (!this._isPlayer)
            {
                this.aim.Aim();
            }
            
            var rocket = Instantiate(this.bulletPrefab, this.barrel.position, Quaternion.identity);
            rocket.transform.LookAt(this._target);
            var rocketFlight = rocket.GetComponent<RocketFlight>();
            rocketFlight.SetTarget(this._target.position, this._isPlayer);
            
            foreach (var abmBuilding in FindObjectsOfType<AbmBuilding>().Where(b => b.isPlayer != this.IsPlayer))
            {
                abmBuilding.ApplyRocket(rocket);
            }

            this.resources.AddRockets(-1);
            
            EventRepository.RocketStarted.Invoke(rocketFlight, this._target, this._isPlayer);
        }
    }
}