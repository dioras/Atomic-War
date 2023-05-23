using System.Linq;
using _1.Scripts.Islands;
using UnityEngine;

namespace _1.Scripts.Vfxs.RocketVfxs
{
    public class RocketBangVfx : MonoBehaviour
    {
        [SerializeField] private Transform bangVfx;
        [SerializeField] private Transform waterBangVfx;
        [SerializeField] private GameObject fireEarth;
        



        public void StartVfx()
        {
            var isIsland = IsIsland(this.transform.position);
            var vfx = isIsland ? this.bangVfx : this.waterBangVfx;
            
            ActivateVfx(vfx);
            
            if (isIsland)
            {
                Instantiate(this.fireEarth, this.transform.position, Quaternion.Euler(0f, 0f, 0f));
            }
        }

        
        
        private void ActivateVfx(Transform vfx)
        {
            vfx.parent = null;
            vfx.gameObject.SetActive(true);
            vfx.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }

        private bool IsIsland(Vector3 position)
        {
            var ray = new Ray(position + Vector3.up * 5, Vector3.down);
            var raycastHits = Physics.RaycastAll(ray, 10f);

            return raycastHits.Any(c => c.transform.TryGetComponent<IslandPart>(out _));
        }
    }
}