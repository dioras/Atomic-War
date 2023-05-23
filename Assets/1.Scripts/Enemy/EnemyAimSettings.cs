using System;
using System.Linq;
using _1.Scripts.Buildings;
using UnityEngine;

namespace _1.Scripts.Enemy
{
    [CreateAssetMenu(fileName = "Enemy Aim Settings", menuName = "Settings/Enemy Aim Settings", order = 10)]
    public class EnemyAimSettings : ScriptableObject
    {
        [field: SerializeField] public int HeadquartersPercent { get; private set; }
        [field: SerializeField] public int BuildingPercent { get; private set; }
        [field: SerializeField] public int Chance { get; private set; }



        public Transform GetTarget()
        {
            var random = new System.Random(Guid.NewGuid().GetHashCode());
            var next = random.Next(0, 101);

            if (next > this.Chance)
            {
                return null;
            }

            next = random.Next(0, 101);
            if (next > this.HeadquartersPercent)
            {
                var building = FindObjectsOfType<BaseBuilding>().FirstOrDefault(b => !(b is HeadquartersBuilding) && b.isPlayer);

                if (building != null)
                {
                    return building.transform;
                }

                if (random.Next(0, 101) > 50)
                {
                    return FindObjectsOfType<HeadquartersBuilding>().FirstOrDefault(b => b.isPlayer)?.transform;
                }
            }
            
            return FindObjectsOfType<HeadquartersBuilding>().FirstOrDefault(b => b.isPlayer)?.transform;
        }
    }
}