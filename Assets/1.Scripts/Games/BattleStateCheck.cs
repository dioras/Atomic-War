using System.Linq;
using _1.Scripts.Buildings;
using _1.Scripts.Weapons;
using UnityEngine;

namespace _1.Scripts.Games
{
    public class BattleStateCheck
    {
        private readonly GameProcess _gameProcess;

        
        
        public BattleStateCheck()
        {
            this._gameProcess = Object.FindObjectOfType<GameProcess>();
        }

        
        
        public void ReloadAllRockets()
        {
            foreach (var rocketBuilding in Object.FindObjectsOfType<RocketBuilding>())
            {
                rocketBuilding.ApplyShootReady(true);
            }
        }

        public void CheckRoundState()
        {
            var rocketBuildings = Object.FindObjectsOfType<RocketBuilding>();
            var playerBuildings = rocketBuildings.Where(b => b.isPlayer).ToList();
            var enemyBuildings = rocketBuildings.Where(b => !b.isPlayer).ToList();
            
            if (playerBuildings.All(b => !b.IsShootReady) && enemyBuildings.All(b => !b.IsShootReady))
            {
                if (!Object.FindObjectsOfType<RocketFlight>().Any())
                {
                    this._gameProcess.GameProcessState.ApplyGameProcessState(GameProcessStateEnum.Build);
                }
            }
        }
    }
}