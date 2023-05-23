using UnityEngine;
using UnityEngine.UI;

namespace _1.Scripts.Ui
{
    public class FpsUi : MonoBehaviour
    {
        private Text _fpsText;
        private float _deltaTime;


        private void Awake()
        {
            this._fpsText = GetComponent<Text>();
            
            QualitySettings.vSyncCount = 0;
        }

        private void Update()
        {
            this._deltaTime += (Time.unscaledDeltaTime - this._deltaTime) * 0.1f;
            this._fpsText.text = $"FPS: {1.0f / this._deltaTime:0} ";
        }
    }
}