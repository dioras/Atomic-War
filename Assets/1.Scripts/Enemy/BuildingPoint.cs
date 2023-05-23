using System.Collections;
using System.Linq;
using _1.Scripts.Buildings;
using _1.Scripts.GameEvents;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _1.Scripts.Enemy
{
    public class BuildingPoint : MonoBehaviour
    {
        [field: SerializeField] public bool IsEmpty { get; set; } = true;
        [field:SerializeField] public bool IsPlayer { get; private set; } = true;
        [field:SerializeField] public bool IsVisible { get; private set; }
        
        private BaseBuilding _building;



        public BuildingPoint()
        {
            this.IsEmpty = true;
        }



        public static BuildingPoint FindFree(bool isPlayer)
        {
            var points = FindObjectsOfType<BuildingPoint>()
                .Where(g => g.IsEmpty && g.IsPlayer == isPlayer).ToList();

            if (points.Count == 0)
            {
                return null;
            }

            return points.ElementAt(Random.Range(0, points.Count));
        }

        

        public void ApplyBuilding(GameObject buildingPrefab)
        {
            var parent = GameObject.Find("Buildings").transform;
            var building = Instantiate(buildingPrefab, this.transform.position, Quaternion.identity, parent);
            this._building = building.GetComponent<BaseBuilding>();
            this._building.isPlayer = this.IsPlayer;

            this.IsEmpty = false;

            if (!this.IsPlayer)
            {
                StartCoroutine(SetVisibleState(building));
            }
        }

        

        private void Awake()
        {
            EventRepository.BuildingDestroyed.AddListener(OnBuildingDestroyed);
        }

        private void OnDestroy()
        {
            EventRepository.BuildingDestroyed.RemoveListener(OnBuildingDestroyed);
        }

        
        
        private void OnBuildingDestroyed(BaseBuilding arg0, bool arg1)
        {
            if (arg0 != this._building)
            {
                return;
            }

            if (!this.IsVisible)
            {
                this.IsVisible = arg0.GetComponent<EnemyBuildingVisibleState>().IsVisible;
            }
        }

        private IEnumerator SetVisibleState(GameObject building)
        {
            yield return null;
            yield return null;
            
            if (this.IsVisible)
            {
                building.GetComponent<EnemyBuildingVisibleState>().SetVisibleState();
            }
        }
    }
}