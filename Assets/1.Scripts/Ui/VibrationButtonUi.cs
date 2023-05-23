using _1.Scripts.Settings;
using UnityEngine;
using UnityEngine.UI;

namespace _1.Scripts.Ui
{
    public class VibrationButtonUi : MonoBehaviour
    {
        [SerializeField] private VibrationSetting vibrationSetting;

        [SerializeField] private Image bg;
        [SerializeField] private Image image;
        
        [SerializeField] private Color activeBgColor;
        [SerializeField] private Color activeImageColor;
        [SerializeField] private Color disableBgColor;
        [SerializeField] private Color disableImageColor;
        
        private Button _button;
        
        
        
        private void Awake()
        {
            this._button = GetComponent<Button>();

            ApplyButtonState(this.vibrationSetting.IsVibration);
            
            this._button.onClick.AddListener(SwitchSetting);
        }

        private void OnDestroy()
        {
            this._button.onClick.RemoveListener(SwitchSetting);
        }

        
        
        private void SwitchSetting()
        {
            this.vibrationSetting.SetVibrationState();
            ApplyButtonState(this.vibrationSetting.IsVibration);
        }

        private void ApplyButtonState(bool isVibration)
        {
            if (isVibration)
            {
                this.bg.color = this.activeBgColor;
                this.image.color = this.activeImageColor;
            }
            else
            {
                this.bg.color = this.disableBgColor;
                this.image.color = this.disableImageColor;
            }
        }
    }
}