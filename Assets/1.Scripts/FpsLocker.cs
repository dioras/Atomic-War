using UnityEngine;

namespace _1.Scripts
{
    public class FpsLocker : MonoBehaviour
    {
        public int target = 30;

        private void Awake()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = this.target;
        }
        
        private void Update()
        {
            if(Application.targetFrameRate != this.target)
            {
                Application.targetFrameRate = this.target;
            }
        }
    }
}