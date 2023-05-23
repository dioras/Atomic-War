using System;
using TMPro;
using UnityEngine;

namespace _1.Scripts.Ui
{
    public class WaitingOpponentUi : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private float delay;

        private float _timer;


        private void Update()
        {
            this._timer += Time.deltaTime;
            if (this._timer > this.delay * 4)
            {
                this._timer = 0f;
            }
            
            var dots = "<color=#00000000>...</color>";

            if (this._timer > this.delay && this._timer <= this.delay * 2)
            {
                dots = ".<color=#00000000>..</color>";
            }
            else if (this._timer > this.delay * 2 && this._timer <= this.delay * 3)
            {
                dots = "..<color=#00000000>.</color>";
            }
            else if (this._timer > this.delay * 3 && this._timer <= this.delay * 4)
            {
                dots = "...";
            }

            this.text.text = $"FINDING OPPONENT{dots}";
        }
    }
}