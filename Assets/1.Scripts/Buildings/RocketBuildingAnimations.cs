using System;
using System.Collections;
using _1.Scripts.GameEvents;
using _1.Scripts.Games;
using UnityEngine;

namespace _1.Scripts.Buildings
{
    public class RocketBuildingAnimations : MonoBehaviour
    {
        [SerializeField] private GameObject view;
        
        private static readonly int IsIdle = Animator.StringToHash("IsIdle");
        
        private Animator _animator;


        
        private void Awake()
        {
            this._animator = GetComponent<Animator>();
            
            EventRepository.GameProcessStateChanged.AddListener(OnGameProcessStateChanged);
        }

        private void OnDestroy()
        {
            EventRepository.GameProcessStateChanged.RemoveListener(OnGameProcessStateChanged);
        }

        
        
        private void Recharge()
        {
            StartCoroutine(RechargeProcess());
        }
        
        private void OnGameProcessStateChanged(GameProcessStateEnum state)
        {
            if (state == GameProcessStateEnum.Build)
            {
                Recharge();
            }
        }

        private IEnumerator RechargeProcess()
        {
            this.view.SetActive(true);
            this._animator.SetBool(IsIdle, false);
            
            yield return new WaitForSeconds(1f);
            
            this._animator.SetBool(IsIdle, true);
        }
    }
}