using System;
using UnityEditor;
using UnityEngine;

namespace _1.Scripts.Fogs
{
    public class FogInit : MonoBehaviour
    {
        [SerializeField] private GameObject fogCloudPrefab;
        [SerializeField] private float stepX;
        [SerializeField] private float stepY;

        private Transform _root;
        
        

        private void Start()
        {
            this._root = GameObject.Find("Fog Cloud Root").transform;
            
            InitFogClouds();
        }


        private void InitFogClouds()
        {
            for (var j = 1.33f; j >= -3.43f; j -= this.stepY)
            {
                for (var i = -2.3f; i <= 2.4f; i += this.stepX)
                {
                    var origin = new Vector3(i, 1, j);
                    
                    if (Physics.Raycast(origin, Vector3.down, out var hit, 2, 1 << 9))
                    {
                        var point = hit.point;
                        point.y = .1f;
                        Instantiate(this.fogCloudPrefab, point, Quaternion.Euler(-90f, 0f, 0f), this._root);
                    }
                }
                
                for (var i = -2.3f - this.stepX / 2f; i <= 2.4f + this.stepX; i += this.stepX)
                {
                    var origin = new Vector3(i, 1, j - this.stepY / 2f);
                    
                    if (Physics.Raycast(origin, Vector3.down, out var hit, 2, 1 << 9))
                    {
                        var point = hit.point;
                        point.y = .1f;
                        var fog = Instantiate(this.fogCloudPrefab, point, Quaternion.Euler(-90f, 0f, 0f), this._root);
                        fog.name += " s";
                    }
                }
            }
        }
    }
}