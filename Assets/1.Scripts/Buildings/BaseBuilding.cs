using UnityEngine;

namespace _1.Scripts.Buildings
{
    public abstract class BaseBuilding : MonoBehaviour
    {
        public bool isPlayer;

        [SerializeField] private GameObject destroyedBuilding;
        



        public void Destroy()
        {
            if (!this.isPlayer)
            {
                Vibration.Vibrate(700);
            }
            
            Instantiate(this.destroyedBuilding, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}