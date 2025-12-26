using SpaceShooter;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TowerDefense;
using Unity.VisualScripting;

namespace Towers.std
{
    public abstract class VariousMech : MonoBehaviour
    {
       
        public int _ammoUs;
       

        public abstract void UseSpecificMechanic(TurretProperties turretProperties);


        public abstract void TryApplyDamage(Destructible destructible);


        //public abstract void TryCreateParticle(Transform target);
        
    }
}
