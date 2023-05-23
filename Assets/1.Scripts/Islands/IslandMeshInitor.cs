using UnityEngine;

namespace _1.Scripts.Islands
{
    public class IslandMeshInitor : MonoBehaviour
    {
        [SerializeField] private MeshFilter meshFilter;

        

        private void Awake()
        {
            foreach (var meshCollider in GetComponents<MeshCollider>())
            {
                meshCollider.sharedMesh = this.meshFilter.mesh;
            }
        }
    }
}