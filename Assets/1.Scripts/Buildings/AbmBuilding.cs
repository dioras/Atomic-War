using System.Collections;
using _1.Scripts.GameEvents;
using _1.Scripts.Games;
using _1.Scripts.Weapons;
using UnityEditor;
using UnityEngine;

namespace _1.Scripts.Buildings
{
    public class AbmBuilding : BaseBuilding
    {
        public float Radius => this.radius;
        
        [SerializeField] private float radius;
        [SerializeField] private int rocketsPerRound = 1;
        [SerializeField] private GameObject abmMissilePrefab;
        
        private GameObject _rocket;
        private Vector3 _position;
        private int _currentCharge;
        private GameProcess _gameProcess;



        public void ApplyRocket(GameObject rocket)
        {
            this._rocket = rocket;
        }


        
        private void Awake()
        {
            this._position = new Vector3(this.transform.position.x, 0f, this.transform.position.z);
            this._gameProcess = FindObjectOfType<GameProcess>();
            
            EventRepository.GameProcessStateChanged.AddListener(OnGameStateChanged);
            EventRepository.RocketStarted.AddListener(OnRocketStarted);
        }

        private void Update()
        {
            if (this._gameProcess.GameProcessState.CurrentGameProcessState != GameProcessStateEnum.Battle)
            {
                return;
            }
            
            if (!this._rocket)
            {
                return;
            }

            if (this._currentCharge == 0)
            {
                return;
            }

            var rocketPosition = new Vector3(this._rocket.transform.position.x, 0f, this._rocket.transform.position.z);
            
            if (Vector3.Distance(this._position, rocketPosition) <= this.Radius)
            {
                --this._currentCharge;
                this._rocket = null;
            }
        }

        private void OnDestroy()
        {
            EventRepository.GameProcessStateChanged.RemoveListener(OnGameStateChanged);
            EventRepository.RocketStarted.RemoveListener(OnRocketStarted);
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Handles.color = Color.blue;
            Handles.DrawWireDisc(this.transform.position, Vector3.up, this.radius);
        }
#endif


        private void OnGameStateChanged(GameProcessStateEnum state)
        {
            if (state == GameProcessStateEnum.Battle)
            {
                this._currentCharge = this.rocketsPerRound;
            }
        }

        private void OnRocketStarted(RocketFlight rocketFlight, Transform target, bool player)
        {
            if (this.isPlayer == player)
            {
                return;
            }

            if (rocketFlight.isTarget)
            {
                return;
            }

            if (this._currentCharge == 0)
            {
                return;
            }

            if (Vector3.Distance(this.transform.position, target.position) > this.Radius)
            {
                return;
            }

            StartCoroutine(ActivateRocketWithDelay(rocketFlight));
        }

        private void ActivateRocket(RocketFlight rocketFlight)
        {
            var abmMissile = Instantiate(this.abmMissilePrefab, this.transform.position, Quaternion.identity);
            abmMissile.GetComponent<AbmMissile>().SetTargetTransform(rocketFlight.transform);
        }

        private IEnumerator ActivateRocketWithDelay(RocketFlight rocketFlight)
        {
            rocketFlight.isTarget = true;
            --this._currentCharge;
            yield return new WaitForSeconds(1.9f / 2f);
            
            ActivateRocket(rocketFlight);
        }
    }
}