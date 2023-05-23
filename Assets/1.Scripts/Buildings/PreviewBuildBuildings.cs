using System.Collections;
using _1.Scripts.Islands;
using UnityEngine;

namespace _1.Scripts.Buildings
{
    public class PreviewBuildBuildings : MonoBehaviour
    {
        public bool IsBuilding { get; private set; }
        
        private Camera _camera;

        

        public void StartBuildingProcess(GameObject building)
        {
            this.IsBuilding = true;
            StartCoroutine(BuildingProcess(building));
        }

        public void StopBuildingProcess()
        {
            this.IsBuilding = false;
        }

        

        private void Awake()
        {
            this._camera = Camera.main;
        }

        

        private IEnumerator BuildingProcess(GameObject building)
        {
            while (this.IsBuilding)
            {
                var ray = this._camera.ScreenPointToRay(Input.mousePosition + new Vector3(-1, 1, 0) * 125);

                if (Physics.Raycast(ray, out var hit, 100, 1 << 9) &&
                    hit.transform.gameObject.TryGetComponent<IslandPart>(out var islandPart) && islandPart.IsPlayer) 
                {
                    var position = hit.point;
                    position.y = 0f;
                    building.transform.position = position;
                }
                else 
                {
                    building.transform.position = Vector3.back * 100;
                }

                yield return null;
            }
        }
    }
}