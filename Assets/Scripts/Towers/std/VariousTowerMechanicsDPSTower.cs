using SpaceShooter;
using System;
using TowerDefense;
using UnityEngine;
using UnityEngine.XR;

namespace Towers.std
{
    public class VariousTowerMechanicsDPSTower : VariousMech
    {
        protected Turret[] turrets;
        private bool isDead = false;
        private void Start()
        {
            turrets = GetComponentsInChildren<Turret>();
        }
        public override void UseSpecificMechanic(Destructible target, float towerRadius)
        {
            target.EventOnDeath.AddListener(OnEnemyDeath);
            
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
        private void OnEnemyDeath()
        {
            isDead = true;
        }
    }
}