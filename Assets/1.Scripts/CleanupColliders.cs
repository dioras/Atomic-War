using UnityEngine;

namespace _1.Scripts
{
    public class CleanupColliders : MonoBehaviour
    {
#if UNITY_EDITOR
        [ContextMenu("Check state")]
        public void Colliders()
        {
            var i = 0;
            var j = 0;

            foreach (var boxCollider in GetComponents<BoxCollider>())
            {
                if (boxCollider.center.z < 0)
                {
                    DestroyImmediate(boxCollider);
                    i++;
                }
                else
                {
                    j++;
                }
            }

            Debug.Log($"<color=red>Destroy: {i}</color>");
            Debug.Log($"<color=red>Left: {j}</color>");
        }
#endif
    }
}