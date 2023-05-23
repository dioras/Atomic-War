using _1.Scripts.Inventory;
using UnityEngine;
using UnityEngine.UI;

namespace _1.Scripts.Ui
{
    public class InitBaseCounterUi : MonoBehaviour
    {
        [SerializeField] private PlayerBuildingInventory inventory;
        
        private Text _text;

        

        private void Awake()
        {
            this._text = GetComponent<Text>();
            this.inventory.CurrentHeadquartersCountChangedEvent += ChangeLeftBase;
        }

        
        
        private void ChangeLeftBase(int count)
        {
            this._text.text = count.ToString();
        }
    }
}