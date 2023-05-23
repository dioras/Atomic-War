using System.Collections.Generic;
using System.Linq;
using _1.Scripts.Characters;
using _1.Scripts.GameEvents;
using _1.Scripts.Games;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _1.Scripts.Enemy
{
    public class EnemyBuildSystem : MonoBehaviour
    {
        [SerializeField] private GameObject headquartersBuildingPrefab;
        [SerializeField] private GameObject rocketBuildingPrefab;
        [SerializeField] private EnemySettings enemySettings;
        
        private List<BuildingPoint> _buildingPoints;
        private BaseResources _baseResources;
        
        

        public void NewMethod()
        {
            while (true)
            {
                var (prefab, price) = this.enemySettings.GetBuilding();

                if (prefab == null)
                {
                    break;
                }

                var buildingPoint = BuildingPoint.FindFree(false);

                if (ReferenceEquals(buildingPoint, null))
                {
                    return;
                }
                
                buildingPoint.ApplyBuilding(prefab);
                this._baseResources.AddGears(price * -1);
            }
        }

        
        
        private void Awake()
        {
            this._baseResources = GetComponent<BaseResources>();
        }

        private void Start()
        {
            EventRepository.GameProcessStateChanged.AddListener(BuildBases);
            
            this._buildingPoints = FindObjectsOfType<BuildingPoint>()
                                   .Where(g => !g.IsPlayer)
                                   .ToList();
        }

        private void OnDestroy()
        {
            EventRepository.GameProcessStateChanged.RemoveListener(BuildBases);
        }


        private void BuildBases(GameProcessStateEnum gameProcess)
        {
            if (gameProcess != GameProcessStateEnum.Init)
            {
                return;
            }
            
            var built = 0;

            while (built < 3)
            {
                var freePoints = this._buildingPoints.Where(p => p.IsEmpty).ToList();
                var point = freePoints[Random.Range(0, freePoints.Count)];

                if (point.IsEmpty)
                {
                    point.ApplyBuilding(this.headquartersBuildingPrefab);
                    built++;
                }
            }

            built = 0;

            while (built < 1)
            {
                var freePoints = this._buildingPoints.Where(p => p.IsEmpty).ToList();
                var point = freePoints[Random.Range(0, freePoints.Count)];

                if (point.IsEmpty)
                {
                    point.ApplyBuilding(this.rocketBuildingPrefab);
                    built++;
                }
            }
        }
    }
}