using _1.Scripts.Buildings;
using _1.Scripts.GameEvents;
using _1.Scripts.Ui;
using UnityEngine;

namespace _1.Scripts.Inventory
{
    public class PlayerBuildingInventory : MonoBehaviour
    {
        public delegate void CurrentHeadquartersCountChanged(int count);
        public event CurrentHeadquartersCountChanged CurrentHeadquartersCountChangedEvent;
        public event CurrentHeadquartersCountChanged CurrentRocketCountChangedEvent;
        
        public bool IsHeadquartersInit => this._currentHeadquartersCount == 0;
        public bool IsRocketInit => this._currentRocketCount == 0;

        public int CurrentHeadquarterCount => this._currentHeadquartersCount;
        public int CurrentRocketCount => this._currentRocketCount;
        
        [SerializeField] private int initHeadquartersCount;
        [SerializeField] private int initRocketCount;

        private int _currentHeadquartersCount;
        private int _currentRocketCount;

        

        private void Awake()
        {
            this._currentHeadquartersCount = this.initHeadquartersCount;
            this._currentRocketCount = this.initRocketCount;

            EventRepository.BuildingBuilt.AddListener(OnBuildingBuilt);
        }

        
        
        private void OnBuildingBuilt(BaseBuilding building)
        {
            if (building is HeadquartersBuilding headquartersBuilding && headquartersBuilding.isPlayer)
            {
                if (this._currentHeadquartersCount <= 0)
                {
                    return;
                }
                
                --this._currentHeadquartersCount;
                CurrentHeadquartersCountChangedEvent?.Invoke(this._currentHeadquartersCount);
            }
            else if (building is RocketBuilding rocketBuilding && rocketBuilding.isPlayer)
            {
                if (this._currentRocketCount <= 0)
                {
                    return;
                }

                --this._currentRocketCount;
                CurrentRocketCountChangedEvent?.Invoke(this._currentRocketCount);
            }

            if (this._currentHeadquartersCount <= 0 && this._currentRocketCount <= 0)
            {
                EventRepository.StartInventoryEmpty.Invoke();
                EventRepository.BuildingBuilt.RemoveListener(OnBuildingBuilt);
            }
        }
    }
}