using System;
using System.Collections;
using _1.Scripts.Buildings;
using _1.Scripts.Characters;
using _1.Scripts.Games;
using UnityEngine;

namespace _1.Scripts.Ui
{
    public class HideBuildingUi : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private Canvas canvas;
        [SerializeField] private BaseResources resources;
        [SerializeField] private GameObject dragTutorial;

        private Coroutine _activeCoroutine;
        private Vector3 _originPos;
        private Vector3 _hidePos;
        private GameProcess _gameProcess;
        private BuildingPrice _buildingPrice;

        

        public void StartShow()
        {
            if (!this.isActiveAndEnabled)
            {
                return;
            }
            
            if (!ReferenceEquals(this._activeCoroutine, null))
            {
                StopCoroutine(this._activeCoroutine);
            }
            
            this._activeCoroutine = StartCoroutine(ShowProcess());
        }
        
        public void StartHide()
        {
            if (this.dragTutorial)
            {
                this.dragTutorial.SetActive(false);
            }
            
            if (!this.isActiveAndEnabled)
            {
                return;
            }
            
            if (!ReferenceEquals(this._activeCoroutine, null))
            {
                StopCoroutine(this._activeCoroutine);
            }

            this._activeCoroutine = StartCoroutine(HideProcess());
        }

        

        private void Awake()
        {
            this._originPos = this.transform.position;
            this._hidePos = this.transform.position - new Vector3(0f, 415f, 0f) * this.canvas.scaleFactor;
            this._gameProcess = FindObjectOfType<GameProcess>();
            this._buildingPrice = GetComponent<BuildingPrice>();
        }

        private void OnEnable()
        {
            StartShow();
        }

        private void Update()
        {
            if (this._gameProcess.GameProcessState.CurrentGameProcessState != GameProcessStateEnum.Build)
            {
                return;
            }
            
            if (this.resources.IsPlayer && this.resources.Gear < this._buildingPrice.Price)
            {
                StartHide();
            }
        }

        private IEnumerator ShowProcess()
        {
            while (this.transform.position.y < this._originPos.y)
            {
                this.transform.position = Vector3.MoveTowards(this.transform.position, this._originPos, this.speed * Time.deltaTime);
                
                yield return null;
            }
            
            this.transform.position = this._originPos;
        }
        
        private IEnumerator HideProcess()
        {
            while (this.transform.position.y > this._hidePos.y)
            {
                this.transform.position = Vector3.MoveTowards(this.transform.position, this._hidePos, this.speed * Time.deltaTime);
                
                yield return null;
            }
            
            this.transform.position = this._hidePos;
        }
    }
}