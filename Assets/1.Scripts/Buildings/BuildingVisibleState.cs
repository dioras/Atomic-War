using UnityEngine;

namespace _1.Scripts.Buildings
{
    public class BuildingVisibleState : MonoBehaviour
    {
        public bool isVisible;


        
        private void Start()
        {
            if (!GetComponent<BaseBuilding>().isPlayer)
            {
                Destroy(this);
            }
        }
    }
}