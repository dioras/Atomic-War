using _1.Scripts.GameEvents;
using _1.Scripts.Games;
using UnityEngine;

namespace _1.Scripts.Weapons
{
    public class WeaponAimTrack : MonoBehaviour
    {
        [SerializeField] private AnimationCurve easing;
        
        private GameProcess _gameProcess;
        private LineRenderer _lineRenderer;
        private Coroutine _delayCoroutine;



        public void SetPath(Vector3 point)
        {
            DrawPath(point);
        }
        
        
        
        private void Awake()
        {
            this._lineRenderer = GetComponent<LineRenderer>();
        }


        
        private void DrawPath(Vector3 endPosition)
        {
            var positionCount = this._lineRenderer.positionCount;
            var positions = new Vector3[positionCount];
            
            for (var i = 0; i < positionCount; i++)
            {
                positions[i] = Vector3.Lerp(this.transform.position, endPosition, (float)i / (positionCount - 1));
                positions[i].y = this.easing.Evaluate(i / (positionCount - 1f));
            }
            
            this._lineRenderer.SetPositions(positions);
        }
    }
}