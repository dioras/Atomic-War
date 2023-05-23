using _1.Scripts.Weapons;
using UnityEngine;
using UnityEngine.Events;

namespace _1.Scripts.GameEvents.Events
{
    public class RocketStarted : UnityEvent<RocketFlight, Transform, bool>
    {
        
    }
}