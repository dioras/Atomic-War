using UnityEngine;

namespace _1.Scripts.Weapons
{
    public class RocketTargetPoint : MonoBehaviour
    {
        [SerializeField] private WeaponAimTrack weaponAimTrack;

        

        private void Update()
        {
            //this.weaponAimTrack.SetPath(this.transform.position);
        }
    }
}