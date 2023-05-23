using System;
using _1.Scripts.Buildings;
using _1.Scripts.Characters;
using _1.Scripts.Inventory;
using _1.Scripts.Islands;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _1.Scripts.Ui
{
    public class BuildButton : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private GameObject buildingPrefab;
        [SerializeField] private GameObject buildingView;
        [SerializeField] private PreviewBuildBuildings previewBuildBuildings;
        [SerializeField] private BaseResources baseResources;
        [SerializeField] private PlayerBuildingInventory buildingInventory;
        [SerializeField] private bool isHead;
        [SerializeField] private TextMeshProUGUI counter;

        private bool _isClicked;
        private GameObject _building;
        private BuildingPrice _buildingPrice;
        private PreviewBuildingTrigger _buildingTrigger;
        private int _buildingCount;

        

        private void Awake()
        {
            this._buildingPrice = GetComponent<BuildingPrice>();
        }

        private void Update()
        {
            if (this._isClicked && Input.GetMouseButtonUp(0))
            {
                this._isClicked = false;
            
                foreach (var hideBuildingUi in FindObjectsOfType<HideBuildingUi>())
                {
                    if (hideBuildingUi.gameObject.activeInHierarchy && hideBuildingUi.TryGetComponent<BuildButton>(out var button) && button.enabled)
                    {
                        hideBuildingUi.StartShow();
                    }
                }
                
                this.previewBuildBuildings.StopBuildingProcess();

                if (this._building.transform.position.z >= -10f && !this._buildingTrigger.IsTrigger)
                {
                    FindObjectOfType<IslandBuildings>().ApplyBuilding(this.buildingPrefab, true, this._building.transform.position);
                    
                    if (!ReferenceEquals(this.baseResources, null))
                    {
                        this.baseResources.AddGears(this._buildingPrice.Price * -1);
                    }

                    if (this.buildingInventory)
                    {
                        this.counter.text = Left().ToString();
                        
                        if (HaveToFinish())
                        {
                            GetComponent<HideBuildingUi>().StartHide();
                            this.enabled = false;
                        }
                    }
                }

                Destroy(this._building);
            }
        }

        

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!ReferenceEquals(this.baseResources, null) && this._buildingPrice.Price > this.baseResources.Gear)
            {
                return;
            }
            
            foreach (var hideBuildingUi in FindObjectsOfType<HideBuildingUi>())
            {
                if (hideBuildingUi.gameObject.activeInHierarchy)
                {
                    hideBuildingUi.StartHide();
                }
            }
            
            this._isClicked = true;
            this._building = Instantiate(this.buildingView);
            this._buildingTrigger = this._building.GetComponent<PreviewBuildingTrigger>();
            
            this.previewBuildBuildings.StartBuildingProcess(this._building);
        }



        private bool HaveToFinish()
        {
            return this.isHead ? this.buildingInventory.IsHeadquartersInit : this.buildingInventory.IsRocketInit;
        }

        private int Left()
        {
            return this.isHead ? this.buildingInventory.CurrentHeadquarterCount : this.buildingInventory.CurrentRocketCount;
        }
    }
}