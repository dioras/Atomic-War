using UnityEngine;

namespace _1.Scripts.Cameras
{
    public class CameraScaler : MonoBehaviour
    {
        [SerializeField] private Canvas canvas;


        
        private void Start()
        {
            Camera.main.orthographicSize = 2.53125f / Screen.width * Screen.height;
        }
    }
}