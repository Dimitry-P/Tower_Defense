using SpaceShooter;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TowerDefense;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace Towers.std
{
    public class VariousTowerMechanicsSlowDownTower : VariousMech
    {
        private int baseDamage;

        public float duration = 5f;     // на 2 секунды

        public override void TryApplyDamage(Destructible destructible)
        {
            if (destructible == null) return;
            if (destructible.IsPoisoned) return;

            var ship = destructible.GetComponent<SpaceShip>();
            //float initialSpeed = ship.MaxLinearVelocity;
            //ship.MaxLinearVelocity *= 0.1f;
            //StartCoroutine(RemoveAfterTime(ship, duration, destructible, initialSpeed));
            //destructible.IsPoisoned = true;
            StartCoroutine(SlowCoroutine(ship, duration, destructible));

            destructible.IsPoisoned = true;
        }

        private IEnumerator SlowCoroutine(SpaceShip ship, float duration, Destructible destructible)
        {
            float originalSpeed = ship.MaxLinearVelocity;

            // Замедляем лимит
            ship.MaxLinearVelocity /= 4f;

            try
            {
                yield return new WaitForSecondsRealtime(duration);
            }
            finally
            {
                if (ship != null)
                {
                    ship.MaxLinearVelocity = originalSpeed;

                    if (ship.Rigid != null && ship.Rigid.velocity.magnitude > 0f)
                    {
                        ship.Rigid.velocity =
                            ship.Rigid.velocity.normalized * ship.MaxLinearVelocity;
                    }
                }
                if (destructible != null)
                    destructible.IsPoisoned = false;
            }
        }


        //private IEnumerator SlowCoroutine(SpaceShip ship, float duration, Destructible destructible)
        //{
        //    float originalSpeed = ship.MaxLinearVelocity;
        //    ship.MaxLinearVelocity *= 0.1f;

        //    yield return new WaitForSeconds(duration);

        //    if (ship != null)
        //        ship.MaxLinearVelocity = originalSpeed;

        //    if (destructible != null)
        //        destructible.IsPoisoned = false;
        //}

        //private IEnumerator RemoveAfterTime(SpaceShip ship, float duration, Destructible destructible, float initialSpeed)
        //{
        //    yield return new WaitForSeconds(duration);
        //    if (ship != null) ship.MaxLinearVelocity = initialSpeed;
        //    destructible.IsPoisoned = false;
        //}

        public override void UseSpecificMechanic(TurretProperties turretProperties)
        {
           
        }
    }
}
