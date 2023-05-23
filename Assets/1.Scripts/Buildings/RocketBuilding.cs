using UnityEngine;

namespace _1.Scripts.Buildings
{
    public class RocketBuilding : BaseBuilding
    {
        [field:SerializeField] public bool IsShootReady { get; private set; }

        [SerializeField] private GameObject rocket;


        
        public void ApplyShootReady(bool state)
        {
            this.IsShootReady = state;
            this.rocket.SetActive(this.IsShootReady);
        }
    }
}