using System.Linq;
using _1.Scripts.Buildings;
using _1.Scripts.GameEvents;
using _1.Scripts.Games;
using UnityEngine;

namespace _1.Scripts.Animations
{
    public class RocketsEmptyUi : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        
        private static readonly int IsIdle = Animator.StringToHash("IsIdle");

        private bool _isWork;
        


        private void Awake()
        {
            EventRepository.GameProcessStateChanged.AddListener(OnGameProcessStateChanged);
        }

        private void Update()
        {
            if (!this._isWork)
            {
                return;
            }
            
            var any = FindObjectsOfType<RocketBuilding>().Any(b => b.isPlayer);

            this.animator.SetBool(IsIdle, any);
        }

        private void OnDestroy()
        {
            EventRepository.GameProcessStateChanged.RemoveListener(OnGameProcessStateChanged);
        }

        
        
        private void OnGameProcessStateChanged(GameProcessStateEnum state)
        {
            if (state == GameProcessStateEnum.Battle || state == GameProcessStateEnum.Build)
            {
                this._isWork = true;
            }
            else
            {
                this._isWork = false;
            }
        }
    }
}