using System;
using _1.Scripts.GameEvents;
using _1.Scripts.Games;
using UnityEditor;
using UnityEngine;

namespace _1.Scripts.Enemy
{
    public class EnemySearcher : MonoBehaviour
    {
        public float SearchRadius => this.searchRadius;
        public EnemyAimSettings AimSettings => this._aimSettings;
        
        [SerializeField] private float searchRadius;
        [SerializeField] private Transform target;
        [SerializeField] private EnemyAimSettings easySettings;
        [SerializeField] private EnemyAimSettings middleSettings;
        [SerializeField] private EnemyAimSettings hardSettings;
        
        private EnemyAimSettings _aimSettings;

        private static int _index;


        
        private void Awake()
        {
            this._aimSettings = GetAimSettings();
            
            EventRepository.GameProcessStateChanged.AddListener(OnGameProcessStateChanged);
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Handles.color = Color.red;
            Handles.DrawWireDisc(this.target.position, Vector3.up, this.searchRadius);
        }
#endif

        private void OnDestroy()
        {
            EventRepository.GameProcessStateChanged.RemoveListener(OnGameProcessStateChanged);
        }
        


        private EnemyAimSettings GetAimSettings()
        {
            switch (_index)
            {
                case 0:
                    return this.easySettings;
                case 1:
                    return this.middleSettings;
                case 2:
                    return this.hardSettings;
                
                default:
                    throw new IndexOutOfRangeException();
            }
        }

        private void OnGameProcessStateChanged(GameProcessStateEnum state)
        {
            if (state == GameProcessStateEnum.Win && _index < 2)
            {
                ++_index;
            }
            else if (state == GameProcessStateEnum.Lose && _index > 0)
            {
                --_index;
            }
        }
    }
}