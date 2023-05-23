using TMPro;
using UnityEngine;

namespace _1.Scripts.Ui
{
    public class SettingNicknameUi : MonoBehaviour
    {
        [SerializeField] private TMP_InputField inputField;

        private string _nick;
        

        private void OnEnable()
        {
            this.inputField.text = PlayerPrefs.GetString("nickname", "PLAYER");
        }

        private void OnDisable()
        {
            if (string.IsNullOrWhiteSpace(this.inputField.text))
            {
                return;
            }
            
            PlayerPrefs.SetString("nickname", this.inputField.text);
            PlayerPrefs.Save();
        }
    }
}