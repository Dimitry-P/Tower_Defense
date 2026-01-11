using SpaceShooter;
using System;
using TowerDefense;
using Towers;
using UnityEngine;
using UnityEngine.XR;
using static UnityEngine.GraphicsBuffer;

namespace Towers.std
{
    public class VariousTowerMechanicsDPSTower : VariousMech
    {
        private int baseDamage;
        private int damage;

     

        public override void TryApplyDamage(Destructible destructible)
        {
            if (destructible == null)
            {
                Debug.Log("destructible   null");
                return;
            }
               
            if (tower == null)
            {
                Debug.Log("tower   null");
                return;
            }
               
            float time = tower.timer;
            int finalDamage;

            if (time < 5f)
                finalDamage = 1;
            else if (time < 10f)
                finalDamage = 5;
            else if (time < 15f)
                finalDamage = 10;
            else if (time < 20f)
                finalDamage = 20;
            else
                finalDamage = 30;

            destructible.ApplyDamage(finalDamage);
        }

        public override void UseSpecificMechanic(TurretProperties turretProperties)
        {
            baseDamage = turretProperties.AmmoUsage;
            Debug.Log("BaseDamage = " + baseDamage);
        }
    }
}