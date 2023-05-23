using System.Linq;
using UnityEngine;

namespace _1.Scripts.Buildings
{
    public class FireEarthBuildRange : MonoBehaviour
    {
        [SerializeField] private float disallowRadius = .5f;

        

        private void Awake()
        {
            if (FindObjectsOfType<FireEarthBuildRange>().Any(e =>
                e != this && Vector3.Distance(this.transform.position, e.transform.position) <= this.disallowRadius))
            {
                Destroy(this.gameObject);
            }
        }
    }
}