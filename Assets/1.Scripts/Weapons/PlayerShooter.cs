using System.Linq;
using _1.Scripts.Buildings;
using _1.Scripts.GameEvents;
using _1.Scripts.Games;
using UnityEngine;

namespace _1.Scripts.Weapons
{
    public class PlayerShooter : MonoBehaviour
    {
        [SerializeField] private WeaponAimTrack weaponAim;
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private GameObject view;
        [SerializeField] private float shootDelay;

        private bool _isBattleProcessState;
        private BattleStateCheck _battleStateCheck;
        private float _d;

        

        private void Awake()
        {
            this._battleStateCheck = new BattleStateCheck();
            
            EventRepository.GameProcessStateChanged.AddListener(OnGameProcessStateChanged);
        }

        private void Update()
        {
            if (!this._isBattleProcessState)
            {
                return;
            }
            
            var rocketBuilding = GetRocketBuilding();

            if (ReferenceEquals(rocketBuilding, null))
            {
                this._battleStateCheck.CheckRoundState();
                
                return;
            }

            if (Input.GetMouseButton(0))
            {
                this._d += Time.deltaTime;
            }
            
            ShowTrail(rocketBuilding);
            Shoot(rocketBuilding, rocketBuilding.transform.position, this.transform.position);

            if (Input.GetMouseButtonUp(0))
            {
                this._d = 0f;
            }
        }

        private void OnDestroy()
        {
            this._battleStateCheck = null;
            
            EventRepository.GameProcessStateChanged.RemoveListener(OnGameProcessStateChanged);
        }

        
        
        private void OnGameProcessStateChanged(GameProcessStateEnum state)
        {
            if (state != GameProcessStateEnum.Battle)
            {
                this._isBattleProcessState = false;
                
                return;
            }
            
            this._battleStateCheck.ReloadAllRockets();
            
            this._isBattleProcessState = true;

            if (!FindObjectsOfType<RocketBuilding>().Any(b => b.isPlayer))
            {
                this.view.SetActive(false);
                this.weaponAim.gameObject.SetActive(false);
            }
        }

        private void ShowTrail(RocketBuilding rocketBuilding)
        {
            if (!Input.GetMouseButton(0))
            {
                this.weaponAim.gameObject.SetActive(false);
                this.view.SetActive(false);
                
                return;
            }
            
            this.weaponAim.gameObject.SetActive(true);
            this.view.SetActive(true);
            
            this.weaponAim.SetPath(rocketBuilding.transform.position);
        }

        private RocketBuilding GetRocketBuilding()
        {
            var rocketBuildings = FindObjectsOfType<RocketBuilding>().Where(b => b.isPlayer && b.IsShootReady).ToList();
            
            return rocketBuildings.OrderByDescending(b => b.transform.position.z).FirstOrDefault();
        }

        private void Shoot(RocketBuilding rocketBuilding, Vector3 barrel, Vector3 target)
        {
            if (!Input.GetMouseButtonUp(0))
            {
                return;
            }

            if (this._d <= this.shootDelay)
            {
                return;
            }
            
            var rocketFlight = InstantiateRocket(barrel, target);

            rocketBuilding.ApplyShootReady(false);
            
            EventRepository.RocketStarted.Invoke(rocketFlight, this.transform, true);
        }

        private RocketFlight InstantiateRocket(Vector3 barrel, Vector3 target)
        {
            var rocket = Instantiate(this.bulletPrefab, barrel, Quaternion.identity);
            rocket.transform.LookAt(target);
            var rocketFlight = rocket.GetComponent<RocketFlight>();
            rocketFlight.SetTarget(target, true);
            
            return rocketFlight;
        }
    }
}