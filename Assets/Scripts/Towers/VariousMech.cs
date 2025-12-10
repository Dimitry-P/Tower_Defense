using SpaceShooter;
using System;
using UnityEngine;

namespace Towers
{
    public abstract class VariousMech : MonoBehaviour
    {
        public abstract void UseSpecificMechanic(Destructible target, float towerRadius);
    }
}