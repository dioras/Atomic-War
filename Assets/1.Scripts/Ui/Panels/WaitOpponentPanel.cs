using System.Collections;
using _1.Scripts.Games;
using UnityEngine;

namespace _1.Scripts.Ui.Panels
{
    public class WaitOpponentPanel : MonoBehaviour
    {
        [SerializeField] private float delayMin = 2;
        [SerializeField] private float delayMax = 5;
        
        
        
        private void Start()
        {
            StartCoroutine(GoNext());
        }

        
        
        private IEnumerator GoNext()
        {
            var random = Random.Range(this.delayMin, this.delayMax);

            yield return new WaitForSeconds(random);
            
            FindObjectOfType<GameProcess>().GameProcessState.ApplyGameProcessState(GameProcessStateEnum.Vs);
        }
    }
}