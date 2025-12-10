using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShooter;
using System;
using Towers;

namespace TowerDefense
{
    public class Tower : MonoBehaviour
    {
        private float m_Radius;
        public float Radius { get { return m_Radius; } set { m_Radius = value; } }
        public Destructible target = null;
       
        public VariousTowerMechanics variousTowerMechanics;

        private void Start()
        {
            variousTowerMechanics = GetComponent<VariousTowerMechanics>();
        }
        private void Update()
        {
            if (target)
            {
                variousTowerMechanics.Tower_UseSpecificMechanic(target);
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

