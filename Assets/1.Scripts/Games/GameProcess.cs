using _1.Scripts.Buildings;
using _1.Scripts.GameEvents;
using _1.Scripts.Inventory;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _1.Scripts.Games
{
    public class GameProcess : MonoBehaviour
    {
        [field:SerializeField] public GameProcessState GameProcessState { get; private set; }
        
        private PlayerBuildingInventory _playerBuildingInventory;
        private int _enemyScore;
        private int _playerScore;
        private bool _isHeadquartersBuilt;
        private bool _isRocketBuilt;

        
        
        public void ReloadLevel()
        {
            SceneManager.LoadScene(0);
        }



        private void Awake()
        {
            this.GameProcessState = new GameProcessState();
            
            this._playerBuildingInventory = FindObjectOfType<PlayerBuildingInventory>();
            this._playerBuildingInventory.CurrentHeadquartersCountChangedEvent += InventoryBuildingCountChanged;
            this._playerBuildingInventory.CurrentRocketCountChangedEvent += InventoryRocketBuildingCountChanged;
            
            EventRepository.BuildingDestroyed.AddListener(OnBuildDestroy);
            EventRepository.GameProcessStateChanged.AddListener(OnGameStateChanged);
        }

        private void Start()
        {
            this.GameProcessState.ApplyGameProcessState(GameProcessStateEnum.TapToPlay);
        }

        private void OnDestroy()
        {
            EventRepository.BuildingDestroyed.RemoveListener(OnBuildDestroy);
            EventRepository.GameProcessStateChanged.RemoveListener(OnGameStateChanged);
        }



        private void InventoryBuildingCountChanged(int count)
        {
            if (count <= 0 && this.GameProcessState.CurrentGameProcessState == GameProcessStateEnum.Init)
            {
                this._isHeadquartersBuilt = true;
                this._playerBuildingInventory.CurrentHeadquartersCountChangedEvent -= InventoryBuildingCountChanged;
            }
            
            CheckBuildingState();
        }

        private void InventoryRocketBuildingCountChanged(int count)
        {
            if (count <= 0 && this.GameProcessState.CurrentGameProcessState == GameProcessStateEnum.Init)
            {
                this._isRocketBuilt = true;
                this._playerBuildingInventory.CurrentRocketCountChangedEvent -= InventoryRocketBuildingCountChanged;
            }

            CheckBuildingState();
        }

        private void CheckBuildingState()
        {
            if (this._isHeadquartersBuilt && this._isRocketBuilt)
            {
                this.GameProcessState.ApplyGameProcessState(GameProcessStateEnum.Battle);
            }
        }

        private void OnBuildDestroy(BaseBuilding building, bool isPlayer)
        {
            if (!(building is HeadquartersBuilding))
            {
                return;
            }
            
            if (building.isPlayer)
            {
                ++this._enemyScore;
            }
            else
            {
                ++this._playerScore;
            }

            if (this._enemyScore == 3)
            {
                this.GameProcessState.ApplyGameProcessState(GameProcessStateEnum.Lose);
            }
            else if (this._playerScore == 3)
            {
                this.GameProcessState.ApplyGameProcessState(GameProcessStateEnum.Win);
            }
        }
        
        private void OnGameStateChanged(GameProcessStateEnum state)
        {
            if (state == GameProcessStateEnum.Win)
            {
                Invoke(nameof(ReloadLevel), 3.75f);
            }
        }
    }
}