using System;
using _1.Scripts.GameEvents;
using _1.Scripts.Games;
using _1.Scripts.Ui.Panels;
using UnityEngine;

namespace _1.Scripts.Ui
{
    public class UiState : MonoBehaviour
    {
        public UiStateEnum CurrentUiState { get; private set; }
        
        [SerializeField] private GameObject initPanel;
        [SerializeField] private GameObject tapToPlayPanel;
        [SerializeField] private GameObject battleReadyPanel;
        [SerializeField] private GameObject battlePanel;
        [SerializeField] private GameObject buildingPanel;
        [SerializeField] private GameObject winPanel;
        [SerializeField] private GameObject losePanel;
        [SerializeField] private GameObject resourcesPanel;
        [SerializeField] private GameObject waitOpponentPanel;
        [SerializeField] private GameObject vsPanel;
        
        [SerializeField] private GameObject headquarterPanel;
        

        
        private void Awake()
        {
            Input.multiTouchEnabled = false;
            
            EventRepository.GameProcessStateChanged.AddListener(SetUiState);
        }

        private void OnDestroy()
        {
            EventRepository.GameProcessStateChanged.RemoveListener(SetUiState);
        }

        

        private void ApplyPanel(UiStateEnum state)
        {
            if (this.CurrentUiState == state)
            {
                return;
            }
            
            this.tapToPlayPanel.SetActive(false);
            this.initPanel.SetActive(false);
            this.battleReadyPanel.SetActive(false);
            this.battlePanel.SetActive(false);
            this.buildingPanel.SetActive(false);
            this.winPanel.SetActive(false);
            this.losePanel.SetActive(false);
            this.waitOpponentPanel.SetActive(false);
            this.vsPanel.SetActive(false);

            this.CurrentUiState = state;
            
            switch (state)
            {
                case UiStateEnum.TapToPlay:
                    this.tapToPlayPanel.SetActive(true);
                    break;
                
                case UiStateEnum.Init:
                    this.initPanel.SetActive(true);
                    this.resourcesPanel.SetActive(true);
                    break;
                
                case UiStateEnum.BattleReady:
                    this.battleReadyPanel.SetActive(true);
                    break;
                
                case UiStateEnum.Battle:
                    this.headquarterPanel.SetActive(true);
                    this.battlePanel.SetActive(true);
                    break;
                
                case UiStateEnum.Build:
                    this.buildingPanel.SetActive(true);
                    break;
                
                case UiStateEnum.WaitOpponent:
                    this.waitOpponentPanel.SetActive(true);
                    break;
                
                case UiStateEnum.Vs:
                    this.vsPanel.SetActive(true);
                    break;
                
                case UiStateEnum.Win:
                    this.headquarterPanel.SetActive(false);
                    this.resourcesPanel.SetActive(false);
                    this.winPanel.SetActive(true);
                    break;
                
                case UiStateEnum.Lose:
                    this.headquarterPanel.SetActive(false);
                    this.resourcesPanel.SetActive(false);
                    this.losePanel.SetActive(true);
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }

        private void SetUiState(GameProcessStateEnum gameState)
        {
            switch (gameState)
            {
                case GameProcessStateEnum.Init:
                    ApplyPanel(UiStateEnum.Init);
                    break;
                
                case GameProcessStateEnum.TapToPlay:
                    ApplyPanel(UiStateEnum.TapToPlay);
                    break;
                
                case GameProcessStateEnum.BattleReady:
                    ApplyPanel(UiStateEnum.BattleReady);
                    break;
                
                case GameProcessStateEnum.Battle:
                    ApplyPanel(UiStateEnum.Battle);
                    break;
                
                case GameProcessStateEnum.Build:
                    ApplyPanel(UiStateEnum.Build);
                    break;
                
                case GameProcessStateEnum.Win:
                    ApplyPanel(UiStateEnum.Win);
                    break;
                
                case GameProcessStateEnum.Lose:
                    ApplyPanel(UiStateEnum.Lose);
                    break;
                
                case GameProcessStateEnum.WaitOpponent:
                    ApplyPanel(UiStateEnum.WaitOpponent);
                    break;
                
                case GameProcessStateEnum.Vs:
                    ApplyPanel(UiStateEnum.Vs);
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException(nameof(gameState), gameState, null);
            }
        }
    }
}