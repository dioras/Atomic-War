using System.Linq;
using _1.Scripts.Buildings;
using _1.Scripts.Enemy;
using _1.Scripts.GameEvents;
using _1.Scripts.Games;
using UnityEngine;

namespace _1.Scripts.Characters
{
    public class BaseResources : MonoBehaviour
    {
        public delegate void ResourceCountChanged(int newValue);

        public event ResourceCountChanged GearsChanged;
        public event ResourceCountChanged RocketsChanged;
        
        public int Gear { get; private set; }
        public int Rockets { get; private set; }

        public bool IsPlayer => this.isPlayer;

        [SerializeField] private bool isPlayer;
        [SerializeField] private int initGearsCount;
        [SerializeField] private int gearsPerRoundGearsCount;
        [SerializeField] private int gearsPerFactoryBuilding;
        [SerializeField] private int gearsPerBuilding;
        [SerializeField] private int initRocketsCount;
        
        private EnemyBuildSystem _enemyBuildSystem;
        private int _round;
        private int _additionalGear;
        


        public int AddGears(int count)
        {
            this.Gear += count;

            if (this.Gear < 0)
            {
                this.Gear = 0;
            }
            
            GearsChanged?.Invoke(this.Gear);

            return this.Gear;
        }

        public int AddRockets(int count)
        {
            this.Rockets += count;

            if (this.Rockets < 0)
            {
                this.Rockets = 0;
            }
            
            RocketsChanged?.Invoke(this.Rockets);

            return this.Rockets;
        }


        
        private void Awake()
        {
            this._enemyBuildSystem = GetComponent<EnemyBuildSystem>();
            
            EventRepository.GameProcessStateChanged.AddListener(OnGameProcessChanged);
            EventRepository.BuildingDestroyed.AddListener(OnBuildingDestroyed);
        }

        private void OnDestroy()
        {
            EventRepository.GameProcessStateChanged.RemoveListener(OnGameProcessChanged);
            EventRepository.BuildingDestroyed.RemoveListener(OnBuildingDestroyed);
        }

        
        
        private void OnGameProcessChanged(GameProcessStateEnum state)
        {
            if (state == GameProcessStateEnum.Build)
            {
                ++this._round;
                AddGears(GetGearsCount());
                this._additionalGear = 0;
                
                if (this._enemyBuildSystem)
                {
                    this._enemyBuildSystem.NewMethod();
                }
            }

            if (state == GameProcessStateEnum.Battle)
            {
                AddRockets(GetRocketsCount());
            }
        }

        private int GetRocketsCount()
        {
            var count = FindObjectsOfType<RocketBuilding>().Count(b => b.isPlayer == this.isPlayer);
            
            return count + this.initRocketsCount;
        }

        private int GetGearsCount()
        {
            var count = FindObjectsOfType<FactoryBuilding>().Count(b => b.isPlayer == this.isPlayer);

            var result = count * this.gearsPerFactoryBuilding + this.gearsPerRoundGearsCount + this._additionalGear;
            
            if (this._round == 1)
            {
                result += this.initGearsCount - this.gearsPerRoundGearsCount;
            }
            
            return result;
        }

        private void OnBuildingDestroyed(BaseBuilding building, bool arg)
        {
            if (arg == this.isPlayer)
            {
                return;
            }

            this._additionalGear += this.gearsPerBuilding;
        }
    }
}