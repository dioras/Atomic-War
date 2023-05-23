using System.Collections;
using UnityEngine;

namespace _1.Scripts.Ui.Panels
{
    public class WinPanel : MonoBehaviour
    {
        [SerializeField] private float duration;
        [SerializeField] private GameObject root;


        
        private void OnEnable()
        {
            StartCoroutine(EnableWinPanel());
        }

        
        
        private IEnumerator EnableWinPanel()
        {
            yield return new WaitForSeconds(this.duration);
            
            this.root.SetActive(true);
        }
    }
}