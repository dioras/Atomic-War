using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _1.Scripts.Games
{
    public class CreateComponentCloud : MonoBehaviour
    {
        [SerializeField] private Transform[] allChildren;
        [SerializeField] private GameObject PCloud;
        // Start is called before the first frame update
        void Start()
        {

            allChildren = GetComponentsInChildren<Transform>();
            foreach (Transform child in allChildren)
            {

                GameObject PCloudO = Instantiate(PCloud, transform.position, transform.rotation) as GameObject;
                PCloudO.transform.parent = this.gameObject.transform;
                //Copy Position/Rotation/Scale Child
                PCloudO.transform.localPosition = child.transform.localPosition;
                PCloudO.transform.localRotation = child.transform.localRotation;
                PCloudO.transform.localScale = child.transform.localScale;
                //Set New Parent to Child
                child.parent = PCloudO.transform;
            }
        }


    }
}
