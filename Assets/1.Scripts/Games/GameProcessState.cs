using _1.Scripts.GameEvents;
using UnityEngine;

namespace _1.Scripts.Games
{
    public class GameProcessState
    {
        public GameProcessStateEnum CurrentGameProcessState { get; private set; }
        
        
        
        public void ApplyGameProcessState(GameProcessStateEnum state)
        {
            if (this.CurrentGameProcessState == GameProcessStateEnum.Win ||
                this.CurrentGameProcessState == GameProcessStateEnum.Lose)
            {
                return;
            }
            
            if (this.CurrentGameProcessState == state)
            {
                return;
            }
            
            this.CurrentGameProcessState = state;
            EventRepository.GameProcessStateChanged.Invoke(this.CurrentGameProcessState);

            Debug.Log($"<color>GameProcess: {this.CurrentGameProcessState}</color>");
        }
    }
}