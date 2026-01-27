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
       
        private void OnEnable()
        {
            DPSGlobalManager.OnDpsUpgrade += IncreaseDamage;
        }

        private void OnDisable()
        {
            DPSGlobalManager.OnDpsUpgrade -= IncreaseDamage;
        }

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

            destructible.ApplyDamage(damage, this);
        }

        public override void IncreaseDamage()
        {
             damage =
     baseDamage += DPSGlobalManager.CurrentUpgrade * 10;

        }

        public override void OnEnemyKilled()
        {
            TDPlayer.Instance.ChangeKilledCount(DPSGlobalManager.TotalKillsByDPSTowers);
        }

        public override void UseSpecificMechanic(TurretProperties turretProperties)
        {
            baseDamage = turretProperties.Damage;
            damage = baseDamage + DPSGlobalManager.CurrentUpgrade * 10;
        }
    }
}