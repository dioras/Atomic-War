using UnityEngine;

namespace _1.Scripts.Ui
{
    public class DebugPanelUi : MonoBehaviour
    {
        private void Awake()
        {
            if (!Debug.isDebugBuild)
            {
                Destroy(this.gameObject);
            }
        }
    }
}