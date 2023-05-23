using _1.Scripts.Buildings;
using _1.Scripts.GameEvents;
using UnityEngine;

namespace _1.Scripts.Ui
{
    public class DragTutorUi : MonoBehaviour
    {
        private void Awake()
        {
            EventRepository.BuildingBuilt.AddListener(OnBuildingBuilt);
        }

        private void OnDestroy()
        {
            EventRepository.BuildingBuilt.RemoveListener(OnBuildingBuilt);
        }

        
        
        private void OnBuildingBuilt(BaseBuilding building)
        {
            if (building.isPlayer)
            {
                this.gameObject.SetActive(false);
            }
        }
    }
}