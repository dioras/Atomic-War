using _1.Scripts.Buildings;
using UnityEngine;

namespace _1.Scripts.Vfxs.RocketVfxs
{
    public class GearVfx : MonoBehaviour
    {
        [SerializeField] private GameObject gearVfx;
        
        
        
        private void Start()
        {
            if (GetComponent<BaseBuilding>().isPlayer)
            {
                this.gearVfx.SetActive(true);
            }
        }
    }
}