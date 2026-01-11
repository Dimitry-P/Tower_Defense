using SpaceShooter;
using UnityEngine;

namespace Towers.std
{
    public class VariousTowerMechanicsAoETower : VariousMech
    {
        private float radiusOfDamage = 8f;
        private int baseDamage;

        private int enemyLayerMask;

        private void Awake()
        {
            enemyLayerMask = 1 << LayerMask.NameToLayer("Enemy");
        }

       

        public override void TryApplyDamage(Destructible destructible)
        {
            //Поиск всех враго в ращдиусе от точки попадания
            Debug.Log("Enemy layer mask: " + enemyLayerMask);
            Collider2D[] enemies = Physics2D.OverlapCircleAll(
    destructible.transform.position,
    radiusOfDamage,
    enemyLayerMask
);
            
            foreach (Collider2D col in enemies)
            {
                Destructible destr = col.GetComponentInParent<Destructible>();
                if (destr != null)
                {
                    destr.ApplyDamage(baseDamage);
                }
            }
        }
        public override void UseSpecificMechanic(TurretProperties turretProperties)
        {
            baseDamage = turretProperties.Damage;
        }
    }
}
