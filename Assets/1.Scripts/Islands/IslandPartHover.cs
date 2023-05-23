using System;
using _1.Scripts.Buildings;
using _1.Scripts.GameEvents;
using _1.Scripts.Games;
using _1.Scripts.Islands.PartHovers;
using UnityEngine;

namespace _1.Scripts.Islands
{
    public class IslandPartHover : MonoBehaviour
    {
        [field:SerializeField] public bool IsHover { get; private set; }
        
        [SerializeField] private Color hoverColor;

        private IslandPart _islandPart;
        private IPartHover _partHover;
        private BuildPartHover _buildPartHover;
        private AttackPartHover _attackPartHover;
        private InitPartHover _initPartHover;
        private FalsePartHover _falsePartHover;
        private MeshRenderer _meshRenderer;
        private Color _defaultColor;
        private PreviewBuildBuildings _previewBuildBuildings;
        
        

        private void Awake()
        {
            this._previewBuildBuildings = FindObjectOfType<PreviewBuildBuildings>();
            this._islandPart = GetComponent<IslandPart>();
            this._buildPartHover = new BuildPartHover(this._islandPart);
            this._attackPartHover = new AttackPartHover(this._islandPart);
            this._initPartHover = new InitPartHover(this._islandPart, this._previewBuildBuildings);
            this._falsePartHover = new FalsePartHover();
            this._meshRenderer = GetComponent<MeshRenderer>();
            this._defaultColor = this._meshRenderer.materials[3].color;
            
            EventRepository.GameProcessStateChanged.AddListener(SetPartHover);
        }

        //private void OnMouseEnter()
        //{
        //    if (!this._partHover.CanHover())
        //    {
        //        return;
        //    }
        //    
        //    this.IsHover = true;
        //    ApplyColor(this.hoverColor);
        //}

        //private void OnMouseExit()
        //{
        //    this.IsHover = false;
        //    ApplyColor(this._defaultColor);
        //}

        private void OnDestroy()
        {
            EventRepository.GameProcessStateChanged.RemoveListener(SetPartHover);
        }

        

        private void ApplyColor(Color color)
        {
            this._meshRenderer.materials[3].color = color;
        }

        private void SetPartHover(GameProcessStateEnum state)
        {
            switch (state)
            {
                case GameProcessStateEnum.Init:
                    this._partHover = this._initPartHover;
                    break;
                
                case GameProcessStateEnum.BattleReady:
                    this._partHover = this._falsePartHover;
                    break;
                
                case GameProcessStateEnum.Battle:
                    this._partHover = this._attackPartHover;
                    break;
                
                case GameProcessStateEnum.Build:
                    this._partHover = this._buildPartHover;
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }
    }
}