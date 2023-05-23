using System;
using UnityEngine;

namespace _1.Scripts.Settings
{
    public class VibrationSetting : MonoBehaviour
    {
        public bool IsVibration { get; private set; }

        

        public void SetVibrationState()
        {
            this.IsVibration = !this.IsVibration;
            
            PlayerPrefs.SetInt("vibration", Convert.ToInt32(this.IsVibration));
            PlayerPrefs.Save();
        }
        
        
        
        private void Awake()
        {
            this.IsVibration = Convert.ToBoolean(PlayerPrefs.GetInt("vibration", 1));
        }
    }
}