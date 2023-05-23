using System.Collections.Generic;
using System.Linq;
using _1.Scripts.Buildings;
using _1.Scripts.GameEvents;
using UnityEngine;

namespace _1.Scripts.Islands
{
    public class IslandPartBuildings : MonoBehaviour
    {
        [SerializeField] private Transform center;
        [SerializeField] private Transform buildingRoot;
        
        private readonly List<GameObject> _buildings;

        
        
        public IslandPartBuildings()
        {
            this._buildings = new List<GameObject>();
        }

        
        
        public void ApplyBuilding(GameObject buildingPrefab, bool isPlayer, Vector3 pos)
        {
            if (this._buildings.Any(b => b.TryGetComponent<BaseBuilding>(out var baseBuilding)))
            {
                return;
            }
            
            var position = isPlayer ? pos : this.center.position;
            position.y = 0.11f;
            
            var building = Instantiate(buildingPrefab, position, Quaternion.identity, this.buildingRoot);
            building.GetComponent<BaseBuilding>().isPlayer = isPlayer;
            
            this._buildings.Add(building);
            
            EventRepository.BuildingBuilt.Invoke(building.GetComponent<BaseBuilding>());
        }
    }
}