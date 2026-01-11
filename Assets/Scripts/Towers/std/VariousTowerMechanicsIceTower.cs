using SpaceShooter;
using UnityEngine;

namespace Towers.std
{
    public class VariousTowerMechanicsIceTower : VariousMech
    {
        private int baseDamage;

        private int enemyLayerMask;

        private void Awake()
        {
            enemyLayerMask = 1 << LayerMask.NameToLayer("Enemy");
        }

      

        public override void TryApplyDamage(Destructible destructible)
        {
            Collider2D[] enemies = Physics2D.OverlapCircleAll(destructible.transform.position, tower.Radius, enemyLayerMask);
            foreach (Collider2D col in enemies)
            {
                SpaceShip spaceShip = col.GetComponentInParent<SpaceShip>();
                if (spaceShip != null)
                {
                    spaceShip.IsFrozen = true;
                }
            }
        }
        public override void UseSpecificMechanic(TurretProperties turretProperties)
        {
            baseDamage = turretProperties.Damage;
        }
    }
}
