using UnityEngine;

namespace _1.Scripts.Enemy
{
    [CreateAssetMenu(fileName = "Enemy Build Settings", menuName = "Settings/Enemy Build Settings", order = 0)]
    public class EnemyBuildSettings : ScriptableObject
    {
        [field: SerializeField] public int Rocket { get; private set; }
        [field: SerializeField] public int Factory { get; private set; }
        [field: SerializeField] public int Abm { get; private set; }
    }
}