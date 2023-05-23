using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _1.Scripts.Games
{
    public class ScaleCloudy : MonoBehaviour
    {
        private float SizeMax = 0f;
        private float SizeMin = 1f;
        private bool Y = true;
        [SerializeField] private float ScaleCoef = 5f;
        private bool Z = false;
        [SerializeField] private float SpeedRand;

        private void Start()
        {
            //StartCoroutine(waiter());
            Randomach();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
                if (Y == true)
                {

                    if (SizeMax < SpeedRand && SizeMin > 0f)
                    {
                        this.gameObject.transform.localScale = new Vector3(this.gameObject.transform.localScale.x + Time.deltaTime * ScaleCoef, this.gameObject.transform.localScale.y + Time.deltaTime * ScaleCoef, this.gameObject.transform.localScale.z + Time.deltaTime * ScaleCoef);
                        SizeMax = SizeMax + Time.deltaTime;
                        SizeMin = SizeMin - Time.deltaTime;
                    }
                    else
                    {
                        Randomach();
                        Y = false;
                    }
                }
                else
                {
                    if (SizeMax > 0 && SizeMin < SpeedRand)
                    {
                        this.gameObject.transform.localScale = new Vector3(this.gameObject.transform.localScale.x - Time.deltaTime * ScaleCoef, this.gameObject.transform.localScale.y - Time.deltaTime * ScaleCoef, this.gameObject.transform.localScale.z - Time.deltaTime * ScaleCoef);
                        SizeMax = SizeMax - Time.deltaTime;
                        SizeMin = SizeMin + Time.deltaTime;
                    }
                    else
                    {
                        Randomach();
                        Y = true;
                    }
                }

        }

        private float Randomach()
        {
            return SpeedRand = Random.Range(0.5f, 2f);
        }
    }
}