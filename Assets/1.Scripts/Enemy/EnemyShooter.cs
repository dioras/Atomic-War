using System.Collections;
using System.Linq;
using _1.Scripts.Buildings;
using _1.Scripts.GameEvents;
using _1.Scripts.Games;
using _1.Scripts.Weapons;
using UnityEngine;

namespace _1.Scripts.Enemy
{
    public class EnemyShooter : MonoBehaviour
    {
        [SerializeField] private GameObject bulletPrefab;

        private EnemySearcher _enemySearcher;
        
        
        
        private void Awake()
        {
            this._enemySearcher = GetComponent<EnemySearcher>();
            
            EventRepository.GameProcessStateChanged.AddListener(OnGameProcessStateChanged);
        }

        private void OnDestroy()
        {
            EventRepository.GameProcessStateChanged.RemoveListener(OnGameProcessStateChanged);
        }
        
        

        private void OnGameProcessStateChanged(GameProcessStateEnum state)
        {
            if (state != GameProcessStateEnum.Battle)
            {
                return;
            }

            StartCoroutine(StartShootProcess());
        }

        private IEnumerator StartShootProcess()
        {
            yield return new WaitForSeconds(.1f);
            
            while (AnyReadyToShoot())
            {
                yield return new WaitForSeconds(RocketFlight.FlightSpeed + .1f);
                
                this.transform.position = GetTarget();
                
                var rocketBuilding = GetRocketBuilding();

                if (ReferenceEquals(rocketBuilding, null))
                {
                    yield break;
                }
                
                Shoot(rocketBuilding, rocketBuilding.transform.position, this.transform.position);
            }
        }

        private bool AnyReadyToShoot()
        {
            return FindObjectsOfType<RocketBuilding>().Any(b => !b.isPlayer && b.IsShootReady);
        }

        private RocketBuilding GetRocketBuilding()
        {
            var rocketBuildings = FindObjectsOfType<RocketBuilding>().Where(b => !b.isPlayer && b.IsShootReady).ToList();

            return rocketBuildings.OrderByDescending(b => b.transform.position.z).FirstOrDefault();
        }
        
        private Vector3 GetTarget()
        {
            var visibleStates = FindObjectsOfType<BuildingVisibleState>()
                                .Where(vs => vs.isVisible)
                                .ToList();

            if (visibleStates.Any())
            {
                var visibleState = visibleStates.First();
                
                return visibleState.transform.position;
            }

            var target = this._enemySearcher.AimSettings.GetTarget();

            if (target != null)
            {
                return target.position;
            }

            var x = Random.Range(-2.57f, 2.57f);
            var z = Random.Range(-9.92f, -4.97f);
            var y = 0f;

            return new Vector3(x, y, z);
        }
        
        private void Shoot(RocketBuilding rocketBuilding, Vector3 barrel, Vector3 target)
        {
            var rocketFlight = InstantiateRocket(barrel, target);

            rocketBuilding.ApplyShootReady(false);
            
            EventRepository.RocketStarted.Invoke(rocketFlight, this.transform, false);
        }

        private RocketFlight InstantiateRocket(Vector3 barrel, Vector3 target)
        {
            var rocket = Instantiate(this.bulletPrefab, barrel, Quaternion.identity);
            rocket.transform.LookAt(target);
            var rocketFlight = rocket.GetComponent<RocketFlight>();
            rocketFlight.SetTarget(target, false);
            
            return rocketFlight;
        }
    }
}