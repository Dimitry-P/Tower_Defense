using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShooter;
using System;

namespace TowerDefense
{
    public class Tower : MonoBehaviour
    {
        private float m_Radius;
        public float Radius { get { return m_Radius; } set { m_Radius = value; } }
        public Destructible target = null;
       
        public event Action<Destructible> UseSpecificMechanic;
        public VariousTowerMechanics variousTowerMechanics;

        private void Start()
        {
            variousTowerMechanics = GetComponent<VariousTowerMechanics>();
            UseSpecificMechanic += variousTowerMechanics.Tower_UseSpecificMechanic;
        }
        private void Update()
        {
            if (target)
            {
                UseSpecificMechanic?.Invoke(target);
            }
            else
            {
                var enter = Physics2D.OverlapCircle(transform.position, m_Radius);
                if (enter)
                {
                    target = enter.transform.root.GetComponent<Destructible>();
                }
            }
        }

       

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;

            Gizmos.DrawWireSphere(transform.position, m_Radius);
        }
    }
}

