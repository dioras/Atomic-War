using _1.Scripts.Buildings;
using _1.Scripts.GameEvents;
using UnityEngine;

namespace _1.Scripts.Islands
{
    public class IslandBuildings : MonoBehaviour
    {
        [SerializeField] private Transform buildingsRoot;
        
        
        
        public void ApplyBuilding(GameObject buildingPrefab, bool isPlayer, Vector3 pos)
        {
            var position = pos;
            
            var building = Instantiate(buildingPrefab, position, Quaternion.identity, this.buildingsRoot);
            building.GetComponent<BaseBuilding>().isPlayer = isPlayer;

            if (isPlayer)
            {
                Vibration.Vibrate(100);
            }
            
            EventRepository.BuildingBuilt.Invoke(building.GetComponent<BaseBuilding>());
        }
    }
}