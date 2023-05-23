using System.Collections;
using _1.Scripts.GameEvents;
using _1.Scripts.Vfxs;
using UnityEngine;

namespace _1.Scripts.Weapons
{
    public class AbmMissile : MonoBehaviour
    {
        [SerializeField] private float radius;
        [SerializeField] private Transform bangVfx;
        
        
        
        public void SetTargetTransform(Transform rocket)
        {
            StartCoroutine(ShootDownRocket(rocket));
        }

        
        
        private IEnumerator ShootDownRocket(Transform rocket)
        {
            var originalPosition = this.transform.position;
            
            for (var i = 0f; i < 1; i += Time.deltaTime / (RocketFlight.FlightSpeed / 2f))
            {
                if (!this.transform || !rocket.transform)
                {
                    break;
                }
                
                this.transform.position = Vector3.Lerp(originalPosition, rocket.transform.position, i);
                this.transform.LookAt(rocket);

                if (Vector3.Distance(this.transform.position, rocket.position) <= this.radius)
                {
                    break;
                }
                
                yield return null;
            }

            this.bangVfx.parent = null;
            this.bangVfx.gameObject.SetActive(true);

            EventRepository.RocketFinished.Invoke(rocket.GetComponent<RocketFlight>());
            Destroy(rocket.gameObject);
            Destroy(this.gameObject);
        }
    }
}