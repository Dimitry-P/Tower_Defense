using SpaceShooter;
using TowerDefense;
using UnityEngine;

namespace Towers.std
{
    public class VariousTowerMechanicsPoison : VariousMech
    {
        public override void UseSpecificMechanic(Destructible target)
        {
            //     if (target != null)
            //     {
            //         if (target.TargetWasHitWithPoison)
            //         {
            //             target.isDead = false;
            //             target = null;
            //             return;
            //         }
            //         else
            //         {
            //             Vector2 targetVector = target.transform.position - transform.position;
            //             Enemy enemy = target.GetComponent<Enemy>();
            //             Debug.Log(enemy.enemyName);
            //             if (targetVector.magnitude <= tower.Radius)
            //             {
            //                 foreach (var turret in turrets)
            //                 {
            //                     turret.transform.up = targetVector;
            //                     turret.Fire();
            //                 }
            //             }
            //         }
            //     }
        }
    }
}