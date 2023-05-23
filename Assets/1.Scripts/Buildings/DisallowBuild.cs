using System.Linq;
using _1.Scripts.Enemy;
using UnityEditor;
using UnityEngine;

namespace _1.Scripts.Buildings
{
    public class DisallowBuild : MonoBehaviour
    {
        [SerializeField] private float radius;

        

        private void OnEnable()
        {
            foreach (var buildingPoint in FindObjectsOfType<BuildingPoint>().Where(b => Vector3.Distance(this.transform.position, b.transform.position) <= this.radius))
            {
                buildingPoint.IsEmpty = false;
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Handles.color = Color.magenta;
            Handles.DrawWireDisc(this.transform.position, Vector3.up, this.radius);
        }
#endif
    }
}