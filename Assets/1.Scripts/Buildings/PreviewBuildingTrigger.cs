using UnityEngine;

namespace _1.Scripts.Buildings
{
    public class PreviewBuildingTrigger : MonoBehaviour
    {
        public bool IsTrigger => this._triggersCount != 0;

        [SerializeField] private GameObject preview;
        [SerializeField] private GameObject forbiddenUi;
        [SerializeField] private Material[] stopMaterials;
        
        private int _triggersCount;
        private Renderer _baseRenderer;
        private Material[] _origMaterials;
        private bool _activeState;



        private void Awake()
        {
            this._baseRenderer = this.preview.GetComponent<Renderer>();
            this._origMaterials = this._baseRenderer.materials;
            this._activeState = true;
        }

        private void Update()
        {
            if (this._triggersCount == 0 && !this._activeState)
            {
                this._activeState = true;
                this.forbiddenUi.SetActive(false);
                this._baseRenderer.materials = this._origMaterials;
            }
            else if (this._triggersCount != 0 && this._activeState)
            {
                this._activeState = false;
                this.forbiddenUi.SetActive(true);
                this._baseRenderer.materials = this.stopMaterials;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<BuildingVisibleState>(out _) || other.TryGetComponent<DisallowBuild>(out _))
            {
                ++this._triggersCount;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<BuildingVisibleState>(out _) || other.TryGetComponent<DisallowBuild>(out _))
            {
                --this._triggersCount;
            }
        }
    }
}