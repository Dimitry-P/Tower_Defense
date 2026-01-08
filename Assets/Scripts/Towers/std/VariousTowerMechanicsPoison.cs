using SpaceShooter;
using System.Threading;
using UnityEngine;
using UnityEngine.U2D;

namespace Towers.std
{
    public class VariousTowerMechanicsPoisonTower : VariousMech
    {
        private int baseDamage;

        private int enemyLayerMask;

        private void Awake()
        {
            enemyLayerMask = 1 << LayerMask.NameToLayer("Enemy");
        }

        public override void TryApplyDamage(Destructible destructible)
        {
            destructible.SetColorTemporary(Color.green, 7f);
        }

        public override void UseSpecificMechanic(TurretProperties turretProperties)
        {
            baseDamage = turretProperties.Damage;
        }
    }
}
