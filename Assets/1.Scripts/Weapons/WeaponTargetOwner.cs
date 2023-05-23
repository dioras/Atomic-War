using UnityEngine;

namespace _1.Scripts.Weapons
{
    public class WeaponTargetOwner : MonoBehaviour
    {
        public bool IsPlayer => this.isPlayer;
        
        [SerializeField] private bool isPlayer;
    }
}