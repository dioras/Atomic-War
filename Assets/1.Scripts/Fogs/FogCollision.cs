using _1.Scripts.Buildings;
using _1.Scripts.Enemy;
using UnityEngine;

namespace _1.Scripts.Fogs
{
    public class FogCollision : MonoBehaviour
    {
        private EnemyBuildingVisibleState _go;
        
        
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<VisibleArea>(out var visibleArea))
            {
                Destroy(this.gameObject);
                if (!ReferenceEquals(this._go, null))
                {
                    this._go.SetVisibleState();
                }
            }
            else if (other.TryGetComponent<EnemyBuildingVisibleState>(out var enemyBuildingVisibleState))
            {
                this._go = enemyBuildingVisibleState;
            }
        }
    }
}