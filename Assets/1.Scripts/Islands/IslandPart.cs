using System.Linq;
using UnityEngine;

namespace _1.Scripts.Islands
{
    public class IslandPart : MonoBehaviour
    {
        [field:SerializeField] public bool IsPlayer { get; private set; }
        
        [SerializeField] private GameObject clouds;
        [SerializeField] private GameObject buildPoints;
        [SerializeField] private Material playerMaterial;
        [SerializeField] private Material playerMaterial_border;
        [SerializeField] private Material enemyMaterial;
        [SerializeField] private Material enemyMaterial_border;
        
        [Space(3)] 
        [SerializeField] private float b = -.00003f;

        

        

#if UNITY_EDITOR
        [ContextMenu("Delete")]
        public void Colliders()
        {
            var i = 0;
            var j = 0;

            var orderedEnumerable = GetComponents<BoxCollider>().OrderBy(c => c.center.z).ToList();

            var delta = float.MaxValue;
            var index = 0;

            //Debug.Log($"<color=red>{orderedEnumerable[0].center.z:0.00000}</color>");
            //Debug.Log($"<color=red>{orderedEnumerable[75].center.z:0.00000}</color>");
            //Debug.Log($"<color=red>{orderedEnumerable[76].center.z:0.00000}</color>");
            Debug.Log($"<color=red>{orderedEnumerable.Last().center.z:0.00000}</color>");
            
            foreach (var boxCollider in orderedEnumerable)
            {
                //Debug.Log($"<color=red>{++index}: {boxCollider.center.z:0.000000}</color>");
            }

            foreach (var boxCollider in GetComponents<BoxCollider>())
            {
                //Debug.Log($"<color=red>{boxCollider.center.z}</color>");
                
                if (boxCollider.center.z < this.b)
                {
                    DestroyImmediate(boxCollider);
                    i++;
                }
                else
                {
                    j++;
                }
            }

            Debug.Log($"<color=red>Destroy: {i}</color>");
            Debug.Log($"<color=red>Left: {j}</color>");
        }

        public void PrintSizes()
        {
            var boxColliders = GetComponents<BoxCollider>().OrderBy(c => c.center.z).ToList();
            
            foreach (var boxCollider in boxColliders)
            {
                Debug.Log($"<color=red>{boxCollider.size.z}</color>");
            }
        }

        public void DeleteSize()
        {
            foreach (var boxCollider in GetComponents<BoxCollider>().OrderBy(c => c.center.z))
            {
                if (boxCollider.size.z >= .002f)
                {
                    DestroyImmediate(boxCollider);
                }
            }
        }

        public void ApplyZero()
        {
            foreach (var boxCollider in GetComponents<BoxCollider>().OrderBy(c => c.center.z))
            {
                var center = boxCollider.center;
                center.z = boxCollider.size.z / -2;

                boxCollider.center = center;
            }
        }

        public void CheckCollidersCount()
        {
            var i = 0;
            
            foreach (var boxCollider in GetComponents<BoxCollider>())
            {
                if (boxCollider.center.z >= this.b)
                {
                    i++;
                }
            }

            Debug.Log($"<color={(i == 0 ? "red" : "green")}>Left: {i}</color>");
        }

        public void MinMax()
        {
            var orderedEnumerable = GetComponents<BoxCollider>().OrderBy(c => c.center.z).ToList();

            Debug.Log($"<color=red>" +
                      $"Min: {orderedEnumerable.Min(c => c.center.z):0.000_00}; " +
                      $"Max: {orderedEnumerable.Max(c => c.center.z):0.000_00}</color>");
        }
#endif
        
        

        public void ApplyPlayerState(bool isPlayer)
        {
            this.IsPlayer = isPlayer;
            this.clouds.SetActive(!isPlayer);
            this.buildPoints.SetActive(!isPlayer);

            var material = this.IsPlayer ? this.playerMaterial : this.enemyMaterial;
            var border = this.IsPlayer ? this.playerMaterial_border : this.enemyMaterial_border;
            ApplyMaterial(material, border);
        }

        
        
        private void ApplyMaterial(Material material, Material border)
        {
            var meshRenderer = GetComponent<MeshRenderer>();
            var materials = meshRenderer.materials;
            materials[0] = border;
            materials[1] = material;
            meshRenderer.materials = materials;
        }
    }
}