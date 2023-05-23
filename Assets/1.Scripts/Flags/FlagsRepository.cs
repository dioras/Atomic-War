using System.Collections.Generic;
using System.Linq;
using _1.Scripts.GameEvents;
using UnityEngine;

namespace _1.Scripts.Flags
{
    public class FlagsRepository : MonoBehaviour
    {
        [SerializeField] private List<Sprite> flags;
        [SerializeField] private List<Sprite> allFags;




        public Sprite GetRandomFlag()
        {
            return this.allFags[Random.Range(0, this.allFags.Count)];
        }

        public Sprite GetDeviceFlag()
        {
            var flagName = PlayerPrefs.GetString("flag_name", "");
            if (!string.IsNullOrWhiteSpace(flagName))
            {
                var flag = this.allFags.SingleOrDefault(f => f.name == flagName);
                if (!ReferenceEquals(flag, null))
                {
                    return flag;
                }
            }
            
            var index = PlayerPrefs.GetInt("flag_index", (int)Application.systemLanguage - 1);
            
            return this.flags[index];
        }

        public void ApplyFlag(string flagName)
        {
            PlayerPrefs.SetString("flag_name", flagName);
            PlayerPrefs.Save();
        }
    }
}