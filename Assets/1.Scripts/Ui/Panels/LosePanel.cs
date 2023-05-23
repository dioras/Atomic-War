using System.Collections;
using _1.Scripts.Games;
using UnityEngine;
using UnityEngine.UI;

namespace _1.Scripts.Ui.Panels
{
    public class LosePanel : MonoBehaviour
    {
        [SerializeField] private float duration;
        [SerializeField] private GameObject root;
        [SerializeField] private Button restart;
        

        
        private void OnEnable()
        {
            StartCoroutine(EnableLosePanel());
            
            this.restart.onClick.AddListener(RestartLevelClicked);
        }

        private void OnDisable()
        {
            this.restart.onClick.RemoveListener(RestartLevelClicked);
        }


        
        private IEnumerator EnableLosePanel()
        {
            yield return new WaitForSeconds(this.duration);
            
            this.root.SetActive(true);
        }

        private void RestartLevelClicked()
        {
            FindObjectOfType<GameProcess>().ReloadLevel();
        }
    }
}