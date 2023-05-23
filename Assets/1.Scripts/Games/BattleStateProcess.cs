using System.Collections;
using _1.Scripts.Characters;
using UnityEngine;

namespace _1.Scripts.Games
{
    public class BattleStateProcess : MonoBehaviour
    {
        [SerializeField] private float delay;
        [SerializeField] private BaseResources playerResources;
        [SerializeField] private BaseResources enemyResources;

        private GameProcess _gameProcess;

        

        private void Awake()
        {
            this._gameProcess = FindObjectOfType<GameProcess>();
        }

        private void Start()
        {
            this.enemyResources.RocketsChanged += RocketsResourcesChanged;
            this.playerResources.RocketsChanged += RocketsResourcesChanged;
        }

        private void OnDestroy()
        {
            this.enemyResources.RocketsChanged -= RocketsResourcesChanged;
            this.playerResources.RocketsChanged -= RocketsResourcesChanged;
        }
        
        

        private void RocketsResourcesChanged(int newValue)
        {
            if (newValue == 0)
            {
                CheckBattleState();
            }
        }

        private void CheckBattleState()
        {
            if (this.playerResources.Rockets == 0 && this.enemyResources.Rockets == 0)
            {
                StartCoroutine(ChangeStateWithDelay());
            }
        }

        private IEnumerator ChangeStateWithDelay()
        {
            yield return new WaitForSeconds(this.delay);
            
            this._gameProcess.GameProcessState.ApplyGameProcessState(GameProcessStateEnum.Build);
        }
    }
}