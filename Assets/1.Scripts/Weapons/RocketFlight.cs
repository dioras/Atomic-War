using System.Collections;
using _1.Scripts.GameEvents;
using UnityEngine;

namespace _1.Scripts.Weapons
{
    public class RocketFlight : MonoBehaviour
    {
        public static float FlightSpeed = 1.9f;

        public bool isTarget;
        
        [SerializeField] private GameObject visibleArea;
        [SerializeField] private AnimationCurve curve;
        [SerializeField] private float yFactor;
        [SerializeField] private float angle;

        [SerializeField] private GameObject viewsRoot;
        
        private RocketBang _rocketBang;
        
        
        
        public void SetTarget(Vector3 point, bool isPlayer)
        {
            StartCoroutine(FlightProcess(point, isPlayer));
        }


        
        private void Awake()
        {
            this._rocketBang = GetComponent<RocketBang>();
        }

        

        private IEnumerator FlightProcess(Vector3 point, bool isPlayer)
        {
            if (isPlayer)
            {
                Vibration.Vibrate(150);
            }
            else
            {
                this.viewsRoot.SetActive(false);
            }
            
            var original = this.transform.position;
            
            for (var i = 0f; i < 1; i += Time.deltaTime / FlightSpeed)
            {
                var pos = Vector3.Lerp(original, point, i);
                pos.y = this.curve.Evaluate(i) * this.yFactor;
                this.transform.position = pos;
                
                var rotationX = Mathf.Lerp(this.angle, -this.angle, i);
                var angles = this.transform.rotation.eulerAngles;
                angles.x = rotationX;
                
                this.transform.rotation = Quaternion.Euler(angles);

                if (i >= .5f && !this.viewsRoot.activeSelf)
                {
                    this.viewsRoot.SetActive(true);
                }

                yield return null;
            }

            this.transform.position = point;
            yield return null;
            
            this._rocketBang.Bang(isPlayer);
            
            Instantiate(this.visibleArea, point, Quaternion.identity);
            
            if (isPlayer)
            {
                Vibration.Vibrate(350);
            }
            
            EventRepository.RocketFinished.Invoke(this);
            Destroy(this.gameObject);
        }
    }
}