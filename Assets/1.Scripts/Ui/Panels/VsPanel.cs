using System.Collections;
using _1.Scripts.Enemy;
using _1.Scripts.Flags;
using _1.Scripts.Games;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _1.Scripts.Ui.Panels
{
    public class VsPanel : MonoBehaviour
    {
        [SerializeField] private float waiting = 2f;
        
        [SerializeField] private TextMeshProUGUI playerNickname;
        [SerializeField] private Image playerFlag;
        
        [SerializeField] private TextMeshProUGUI enemyNickname;
        [SerializeField] private Image enemyFlag;
        


        
        private void Awake()
        {
            FillNicknames();
            FillFlags();
        }

        private void Start()
        {
            StartCoroutine(GoNext());
        }

        
        
        private IEnumerator GoNext()
        {
            yield return new WaitForSeconds(this.waiting);
            
            FindObjectOfType<GameProcess>().GameProcessState.ApplyGameProcessState(GameProcessStateEnum.Init);
        }

        private void FillNicknames()
        {
            var enemyNick = Nicknames.GetNickname();
            this.enemyNickname.text = enemyNick;

            var playerNick = PlayerPrefs.GetString("nickname", "PLAYER");
            this.playerNickname.text = playerNick;
        }

        private void FillFlags()
        {
            var flagsRepository = FindObjectOfType<FlagsRepository>();

            this.playerFlag.sprite = flagsRepository.GetDeviceFlag();
            this.enemyFlag.sprite = flagsRepository.GetRandomFlag();
        }
    }
}