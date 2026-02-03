using SpaceShooter;
using System.Collections.Generic;
using System.Linq;
using TowerDefense;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace Towers.std
{
    public class VariousTowerMechanicsSingleTower : VariousMech
    {
        private int baseDamage;

        private int enemyLayerMask;

        private void Awake()
        {
            enemyLayerMask = 1 << LayerMask.NameToLayer("Enemy");
        }

        public override void TryApplyDamage(Destructible destructible)
        {
            if (destructible == null) return;

            destructible.ApplyDamage(baseDamage, this);
        }

        public override void UseSpecificMechanic(TurretProperties turretProperties)
        {
            baseDamage = turretProperties.Damage;
        }
    }
}
