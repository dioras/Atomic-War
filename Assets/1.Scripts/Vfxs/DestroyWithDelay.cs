using UnityEngine;

namespace _1.Scripts.Vfxs
{
    public class DestroyWithDelay : MonoBehaviour
    {
        [SerializeField] private float delay;


        
        private void Start()
        {
            Invoke(nameof(Destroy), this.delay);
        }



        private void Destroy()
        {
            Destroy(this.gameObject);
        }
    }
}