using _1.Scripts.Games;
using UnityEngine;
using UnityEngine.UI;

namespace _1.Scripts.Ui
{
    public class BattleButtonUi : MonoBehaviour
    {
        private void OnEnable()
        {
            GetComponent<Button>().onClick.AddListener(ActivateBattleState);
        }

        private void ActivateBattleState()
        {
            GetComponent<Button>().onClick.RemoveListener(ActivateBattleState);
            
            FindObjectOfType<GameProcess>().GameProcessState.ApplyGameProcessState(GameProcessStateEnum.Battle);
        }
    }
}