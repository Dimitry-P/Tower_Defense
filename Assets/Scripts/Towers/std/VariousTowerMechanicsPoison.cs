using SpaceShooter;
using TowerDefense;
using UnityEngine;

namespace Towers.std
{
    public class VariousTowerMechanicsPoison : VariousMech
    {
        protected Turret[] turrets;
        private bool isDead = false;
        private void Start()
        {
            turrets = GetComponentsInChildren<Turret>();
        }
        public override void UseSpecificMechanic(Destructible target, float towerRadius)
        {
            target.EventOnDeath.AddListener(TargetWasHitWithPoison);
            if (target != null)
            {
                if (isDead == true)
                {
                    isDead = false;
                    target = null;
                    return;
                }
                else
                {
                    Vector2 targetVector = target.transform.position - transform.position;
                    Enemy enemy = target.GetComponent<Enemy>();
                    Debug.Log(enemy.enemyName);
                    if (targetVector.magnitude <= towerRadius)
                    {
                        foreach (var turret in turrets)
                        {
                            turret.transform.up = targetVector;
                            turret.Fire();
                        }
                    }
                }
            }
        }
        private void TargetWasHitWithPoison()
        {
            isDead = true;
        }
    }
}