using _1.Scripts.Games;
using UnityEngine;
using UnityEngine.UI;

namespace _1.Scripts.Ui.Panels
{
    public class TapToPlayPanel : MonoBehaviour
    {
        [SerializeField] private Button tapToPlayButton;
        
        
        
        private void Awake()
        {
            this.tapToPlayButton.onClick.AddListener(Play);
        }

        private void OnDestroy()
        {
            this.tapToPlayButton.onClick.RemoveListener(Play);
        }

        
        
        private void Play()
        {
            FindObjectOfType<GameProcess>().GameProcessState.ApplyGameProcessState(GameProcessStateEnum.WaitOpponent);
        }
    }
}