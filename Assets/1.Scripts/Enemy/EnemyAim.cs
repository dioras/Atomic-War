using System.Linq;
using _1.Scripts.Buildings;
using _1.Scripts.GameEvents;
using _1.Scripts.Games;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _1.Scripts.Enemy
{
    public class EnemyAim : MonoBehaviour
    {
        [SerializeField] private EnemySearcher enemySearcher;
        
        private Transform _target;
        private Vector3 _targetPosition;

        

        public void Aim()
        {
            SetTarget();
            this._target.position = this._targetPosition;
        }

        
        
        private void Awake()
        {
            this._target = GetComponent<Transform>();
        }



        private void SetTarget()
        {
            var visibleStates = FindObjectsOfType<BuildingVisibleState>()
                .Where(vs => vs.isVisible)
                .ToList();

            if (visibleStates.Any())
            {
                var visibleState = visibleStates.First();
                this._targetPosition = visibleState.transform.position;

                return;
            }

            var target = this.enemySearcher.AimSettings.GetTarget();

            if (target != null)
            {
                this._targetPosition = target.position;

                return;
            }

            var x = Random.Range(-2.57f, 2.57f);
            var z = Random.Range(-9.92f, -4.97f);
            var y = 0f;

            this._targetPosition = new Vector3(x, y, z);
        }
    }
}