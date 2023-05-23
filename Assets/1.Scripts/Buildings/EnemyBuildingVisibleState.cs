using _1.Scripts.Fogs;
using UnityEngine;

namespace _1.Scripts.Buildings
{
    public class EnemyBuildingVisibleState : MonoBehaviour
    {
        public bool IsVisible { get; private set; }

        [SerializeField] private MeshRenderer meshRenderer;
        [SerializeField] private MeshRenderer meshRenderer2;
        [SerializeField] private ParticleSystem particleSystem;
        


        
        private void Start()
        {
            if (GetComponent<BaseBuilding>().isPlayer)
            {
                Destroy(this);
            }
            else
            { 
                this.meshRenderer.enabled = false;
                
                if (this.meshRenderer2)
                {
                    this.meshRenderer2.enabled = false;
                }
                
                if (this.particleSystem)
                {
                    this.particleSystem.gameObject.SetActive(false);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<FogCollision>(out var fog))
            {
                SetVisibleState();
            }
        }


        public void SetVisibleState()
        {
            this.IsVisible = true;
            if (this.meshRenderer)
            {
                this.meshRenderer.enabled = true;
            }
            
            if (this.meshRenderer2)
            {
                this.meshRenderer2.enabled = true;
            }
            
            if (this.particleSystem)
            {
                this.particleSystem.gameObject.SetActive(true);
            }
        }
    }
}