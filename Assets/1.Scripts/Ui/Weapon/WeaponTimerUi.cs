using _1.Scripts.Weapons;
using UnityEngine;
using UnityEngine.UI;

namespace _1.Scripts.Ui.Weapon
{
    public class WeaponTimerUi : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private WeaponTimer weaponTimer;
        


        private void Update()
        {
            //this.image.fillAmount = this.weaponTimer.CurrentReload / this.weaponTimer.ReloadDuration;
        }
    }
}