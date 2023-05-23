using _1.Scripts.Levels;
using UnityEngine;
using UnityEngine.UI;

namespace _1.Scripts.Ui.DebugUi
{
    public class IslandsUi : MonoBehaviour
    {
        [SerializeField] private Text enemyText;
        [SerializeField] private Text playerText;
        [SerializeField] private Text levelText;

        

        private void Start()
        {
            var levelRepository = FindObjectOfType<LevelRepository>();
            
            this.enemyText.text = $"E: {levelRepository.LevelPair.enemyIsland.name.Replace("Base Island ", "").Replace(" Variant", "")}";
            this.playerText.text = $"P: {levelRepository.LevelPair.playerIsland.name.Replace("Base Island ", "").Replace(" Variant", "")}";
            this.levelText.text = $"L: {levelRepository.CurrentLevelIndex}";
        }
    }
}