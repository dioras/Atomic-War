using System;
using System.Collections.Generic;
using _1.Scripts.Fogs;
using _1.Scripts.GameEvents;
using _1.Scripts.Games;
using _1.Scripts.Islands;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _1.Scripts.Levels
{
    public class LevelRepository : MonoBehaviour
    {
        public int CurrentLevelIndex => this._currentLevelIndex;

        public LevelPair LevelPair { get; private set; }
        
        [SerializeField] private Transform parent;
        [SerializeField] private Vector3 playerIslandPosition;
        [SerializeField] private Vector3 enemyIslandPosition;
        [SerializeField] private List<LevelPair> levelPairs;

        private int _currentLevelIndex;
        
        

        private void Awake()
        {
            EventRepository.GameProcessStateChanged.AddListener(OnGameProcessStateChanged);

            this._currentLevelIndex = GetIndex();

            if (this._currentLevelIndex > this.levelPairs.Count - 1)
            {
                this.LevelPair = this.levelPairs[Random.Range(0, this.levelPairs.Count)];
            }
            else
            {
                this.LevelPair = this.levelPairs[this._currentLevelIndex];
            }
            
            SpawnIslands(this.LevelPair);
        }

        private void OnDestroy()
        {
            EventRepository.GameProcessStateChanged.RemoveListener(OnGameProcessStateChanged);
        }

        
        
        private void OnGameProcessStateChanged(GameProcessStateEnum arg0)
        {
            if (arg0 != GameProcessStateEnum.Win)
            {
                return;
            }
            
            ++this._currentLevelIndex;
            PlayerPrefs.SetInt("level_index", this._currentLevelIndex);
            PlayerPrefs.Save();
        }

        private int GetIndex()
        {
            return PlayerPrefs.GetInt("level_index", 0);
        }
        
        private void SpawnIslands(LevelPair levelPair)
        {
            SpanIsland(levelPair.playerIsland, this.playerIslandPosition, true);
            SpanIsland(levelPair.enemyIsland, this.enemyIslandPosition, false);
        }

        private void SpanIsland(GameObject islandPrefab, Vector3 position, bool isPlayer)
        {
            var island = Instantiate(islandPrefab, position, Quaternion.identity, this.parent);
            island.GetComponentInChildren<IslandPart>().ApplyPlayerState(isPlayer);
        }
    }
}