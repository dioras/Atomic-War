using System;
using System.Linq;
using _1.Scripts.Buildings;
using _1.Scripts.Characters;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _1.Scripts.Enemy
{
    public class EnemySettings : MonoBehaviour
    {
        [SerializeField] private EnemyBuildSettings buildSettings;
        
        [Space, Header("Building Prefabs")] 
        [SerializeField] private GameObject factoryPrefab;
        [SerializeField] private GameObject rocketPrefab;
        [SerializeField] private GameObject abmPrefab;
        
        [Space, Header("Building Price")]
        [SerializeField] private BuildingPrice factoryPrice;
        [SerializeField] private BuildingPrice rocketPrice;
        [SerializeField] private BuildingPrice abmPrice;

        private BaseResources _baseResources;



        public (GameObject, int) GetBuilding()
        {
            var bound1 = 0;
            var bound2 = 0;
            var bound3 = 0;

            if (FindObjectsOfType<RocketBuilding>().Count(b => !b.isPlayer) == 0 && this._baseResources.Gear >= this.rocketPrice.Price)
            {
                return (this.rocketPrefab, this.rocketPrice.Price);
            }

            if (this._baseResources.Gear >= this.rocketPrice.Price)
            {
                bound1 = this.buildSettings.Rocket; // 30
            }
            if (this._baseResources.Gear >= this.factoryPrice.Price)
            {
                bound2 = this.buildSettings.Factory; // 60
            }
            if (this._baseResources.Gear >= this.abmPrice.Price)
            {
                bound3 = this.buildSettings.Abm; // 10
            }

            if (bound1 + bound2 + bound3 == 0)
            {
                return (null, 0);
            }
            
            var range = Random.Range(0, bound1 + bound2 + bound3 + 1);

            if (range < bound1) // < 30
            {
                return (this.rocketPrefab, this.rocketPrice.Price);
            }
            
            if (range >= bound1 && range < bound1 + bound2) // >= 30 && < 90
            {
                return (this.factoryPrefab, this.factoryPrice.Price);
            }
            
            if (range >= bound1 + bound2) // >= 90
            {
                return (this.abmPrefab, this.abmPrice.Price);
            }

            return (null, 0);
        }


        
        private void Awake()
        {
            this._baseResources = GetComponent<BaseResources>();
        }
    }
}