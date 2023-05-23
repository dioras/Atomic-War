using System;
using System.IO;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _1.Scripts.Enemy
{
    public static class Nicknames
    {
        public static string GetNickname()
        {
            var logins = Resources.Load<TextAsset>("logins");
            if (logins == null)
            {
                return "PLAYER#" + Random.Range(10000, 99999);
            }

            using (StreamReader sr = new StreamReader(new MemoryStream(logins.bytes)))
            {
                var nicknames = sr.ReadToEnd().Split(new[]{"\n"}, StringSplitOptions.RemoveEmptyEntries);
                
                return nicknames[Random.Range(0, nicknames.Length)];
            }
        }
    }
}