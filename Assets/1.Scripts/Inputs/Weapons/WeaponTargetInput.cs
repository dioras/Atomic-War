using System.Collections;
using System.Linq;
using _1.Scripts.Buildings;
using _1.Scripts.GameEvents;
using _1.Scripts.Games;
using UnityEngine;

namespace _1.Scripts.Inputs.Weapons
{
    public class WeaponTargetInput : MonoBehaviour
    {
        [SerializeField] private float sensitivityFactor;

        private Camera _camera;

        

        private void Awake()
        {
            this._camera = Camera.main;
            
            EventRepository.GameProcessStateChanged.AddListener(GameStateChanged);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && FindObjectsOfType<RocketBuilding>().Any(b => b.IsShootReady && b.isPlayer))
            {
                StartCoroutine(InputProcess());
            }
        }

        private void OnDestroy()
        {
            EventRepository.GameProcessStateChanged.RemoveListener(GameStateChanged);
        }

        
        
        private void GameStateChanged(GameProcessStateEnum state)
        {
            this.gameObject.SetActive(state == GameProcessStateEnum.Battle);
        }

        private IEnumerator InputProcess()
        {
            var originalPosition = this.transform.position;
            var originalCameraPoint = this._camera.ScreenToWorldPoint(Input.mousePosition);
            
            while (Input.GetMouseButton(0))
            {
                var currentCameraPoint = this._camera.ScreenToWorldPoint(Input.mousePosition);
                
                var position = originalPosition + currentCameraPoint - originalCameraPoint;
                position *= this.sensitivityFactor;
                position.x = Mathf.Clamp(position.x, -2.57f, 2.57f);
                position.z = Mathf.Clamp(position.z, -3.5f, 1.45f);
                position.y = 0f;
                
                this.transform.position = position;
                
                yield return null;
            }
        }
    }
}