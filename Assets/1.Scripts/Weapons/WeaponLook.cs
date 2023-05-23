using UnityEngine;

namespace _1.Scripts.Weapons
{
    public class WeaponLook : MonoBehaviour
    {
        [SerializeField] private Transform target;

        

        private void Update()
        {
            this.transform.LookAt(this.target);
        }
    }
}