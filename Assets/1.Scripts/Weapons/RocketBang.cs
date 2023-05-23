using System;
using System.Collections.Generic;
using System.Linq;
using _1.Scripts.Buildings;
using _1.Scripts.GameEvents;
using _1.Scripts.Vfxs.RocketVfxs;
using UnityEditor;
using UnityEngine;

namespace _1.Scripts.Weapons
{
    public class RocketBang : MonoBehaviour
    {
        [SerializeField] private float bangRadius;
        
        
        
        public void Bang(bool isPlayer)
        {
            var buildings = FindNearbyBuildings(isPlayer);
            
            foreach (var building in buildings)
            {
                DestroyBuilding(building);
            }
            
            GetComponent<RocketBangVfx>().StartVfx();
        }


#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Handles.DrawWireDisc(this.transform.position, Vector3.up, this.bangRadius);
        }
#endif

        

        private List<BaseBuilding> FindNearbyBuildings(bool isPlayer)
        {
            return FindObjectsOfType<BaseBuilding>()
                   .Where(b => b.isPlayer != isPlayer 
                               && CheckDistance(b.transform.position, this.transform.position))
                   .ToList();
        }

        private bool CheckDistance(Vector3 building, Vector3 rocket)
        {
            building.y = 0f;
            rocket.y = 0f;

            return Vector3.Distance(building, rocket) <= this.bangRadius;
        }

        private void DestroyBuilding(BaseBuilding building)
        {
            EventRepository.BuildingDestroyed.Invoke(building, building.isPlayer);
            
            building.Destroy();
        }
    }
}