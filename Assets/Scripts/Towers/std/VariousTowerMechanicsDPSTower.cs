using SpaceShooter;
using System;
using TowerDefense;
using UnityEngine;
using UnityEngine.XR;
using Towers;

namespace Towers.std
{
    public class VariousTowerMechanicsDPSTower : VariousMech
    {
        
        public override void TryApplyDamage(Destructible destructible)
        {
            destructible.ApplyDamage(_ammoUs);
        }

        public override void UseSpecificMechanic(TurretProperties turretProperties)
        {
            _ammoUs = turretProperties.AmmoUsage;
            _ammoUs *= 2;
           




        }
        //public override void TryApplyDamage(Destructible destructible)
        //{
        //    Debug.Log("???????????????????????????");
        //    int damage = 0;
        //    damage *= 2;

        //    destructible.ApplyDamage(damage);
        //}
    }
}