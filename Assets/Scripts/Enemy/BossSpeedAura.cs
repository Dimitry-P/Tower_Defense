using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class BossSpeedAura : MonoBehaviour
    {
        public float speedMultiplier = 5f;

        private void Awake()
        {
            var col = GetComponent<CircleCollider2D>();
            col.isTrigger = true;
            var ship = GetComponent<SpaceShip>();
            if (ship != null)
            {
                ship.FreezeImmune = true;
            }
        }

        //private void OnTriggerEnter2D(Collider2D other)
        //{
        //    var ship = other.GetComponentInParent<SpaceShip>();
        //    if (ship == null) return;
        //    if (ship == GetComponent<SpaceShip>()) return;

        //    ship.AddSpeedAura(speedMultiplier);
        //}

        //private void OnTriggerExit2D(Collider2D other)
        //{
        //    var ship = other.GetComponentInParent<SpaceShip>();
        //    if (ship == null) return;
        //    if (ship == GetComponent<SpaceShip>()) return;

        //    ship.RemoveSpeedAura(speedMultiplier);
        //}
        private void OnTriggerEnter2D(Collider2D other)
        {
            var ship = other.GetComponentInParent<SpaceShip>();
            if (ship == null) return;
            if (ship == GetComponent<SpaceShip>()) return;

            ship.AddSpeedAura(speedMultiplier);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            var ship = other.GetComponentInParent<SpaceShip>();
            if (ship == null) return;
            if (ship == GetComponent<SpaceShip>()) return;

            ship.RemoveSpeedAura(speedMultiplier);
        }

    }
}