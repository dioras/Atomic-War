using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _1.Scripts.Buildings
{
    public class DestroyedBuilding : MonoBehaviour
    {
        [SerializeField] private GameObject fireEarthPrefab;
        [SerializeField] private float duration;
        [SerializeField] private List<Rigidbody> _rbs;
        
        
        
        private void Start()
        {
            StartCoroutine(DestroyProcess());
        }

        
        
        private IEnumerator DestroyProcess()
        {
            yield return new WaitForSeconds(this.duration);

            foreach (var rb in this._rbs)
            {
                rb.useGravity = true;
                rb.isKinematic = false;
            }
            
            yield return new WaitForSeconds(this.duration);

            Instantiate(this.fireEarthPrefab, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}