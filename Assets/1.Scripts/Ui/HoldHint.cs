using System;
using System.Collections;
using System.Linq;
using _1.Scripts.Buildings;
using _1.Scripts.GameEvents;
using _1.Scripts.Games;
using _1.Scripts.Weapons;
using UnityEngine;

namespace _1.Scripts.Ui
{
    public class HoldHint : MonoBehaviour
    {
        [SerializeField] private float duration;

        private Coroutine _hintCoroutine;

        

        private void Awake()
        {
            EventRepository.RocketStarted.AddListener(OnRocketStarted);
            EventRepository.GameProcessStateChanged.AddListener(OnGameProcessStateChanged);

            if (this._hintCoroutine == null)
            {
                this._hintCoroutine = StartCoroutine(HintProcess());
            }
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && this._hintCoroutine != null)
            {
                StopCoroutine(this._hintCoroutine);
                
                this.gameObject.SetActive(false);
                this._hintCoroutine = null;
            }
        }

        private void OnDestroy()
        {
            EventRepository.RocketStarted.RemoveListener(OnRocketStarted);
            EventRepository.GameProcessStateChanged.RemoveListener(OnGameProcessStateChanged);
        }


        private IEnumerator HintProcess()
        {
            yield return new WaitForSeconds(this.duration);
            
            this.gameObject.SetActive(false);
            this._hintCoroutine = null;
        }

        private void OnGameProcessStateChanged(GameProcessStateEnum state)
        {
            if (state == GameProcessStateEnum.Battle)
            {
                if (!FindObjectsOfType<RocketBuilding>().Any(c => c.isPlayer))
                {
                    return;
                }
                
                this.gameObject.SetActive(true);
                this._hintCoroutine = StartCoroutine(HintProcess());
            }
        }

        private void OnRocketStarted(RocketFlight arg0, Transform arg1, bool arg2)
        {
            if (!arg2 || !FindObjectsOfType<RocketBuilding>().Any(c => c.isPlayer && c.IsShootReady))
            {
                return;
            }
            
            this.gameObject.SetActive(true);
            this._hintCoroutine = StartCoroutine(HintProcess());
        }
    }
}