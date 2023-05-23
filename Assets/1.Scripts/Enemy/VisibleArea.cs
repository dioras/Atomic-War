using _1.Scripts.Buildings;
using UnityEngine;

namespace _1.Scripts.Enemy
{
    public class VisibleArea : MonoBehaviour
    {
        private SphereCollider _sphereCollider;



        private void Awake()
        {
            this._sphereCollider = GetComponent<SphereCollider>();
            this._sphereCollider.radius = FindObjectOfType<EnemySearcher>().SearchRadius;
            this.transform.parent = GameObject.Find("Visible Areas").transform;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<BuildingVisibleState>(out var visibleState))
            {
                visibleState.isVisible = true;
            }
        }
    }
}