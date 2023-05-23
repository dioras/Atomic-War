using _1.Scripts.Buildings;
using _1.Scripts.GameEvents;
using _1.Scripts.Games;
using UnityEngine;

namespace _1.Scripts.Vfxs
{
    public class WinVfx : MonoBehaviour
    {
        [SerializeField] private GameObject vfx;
        [SerializeField] private BaseBuilding building;
        
        

        private void Awake()
        {
            EventRepository.GameProcessStateChanged.AddListener(OnGameProcessStateChanged);
        }

        private void OnDestroy()
        {
            EventRepository.GameProcessStateChanged.RemoveListener(OnGameProcessStateChanged);
        }

        
        
        private void OnGameProcessStateChanged(GameProcessStateEnum state)
        {
            if (state == GameProcessStateEnum.Win && this.building.isPlayer)
            {
                this.vfx.SetActive(true);
            }
        }
    }
}