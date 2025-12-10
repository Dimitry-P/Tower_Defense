using SpaceShooter;
using System;
using TowerDefense;
using Towers.std;
using UnityEngine;

namespace Towers
{
    public class VariousTowerMechanics : MonoBehaviour
    {
        public Destructible target;
        protected TowerAsset towerAsset;
        private Projectile projectile;
        private VariousMech _variousMech;
        private EVariousMech _typeMech;
        private float towerRadius;

        private void Start()
        {
            _variousMech = GetComponent<VariousMech>();
        }

        public void ApplyTowersMechanics(EVariousMech typeOfEnum, float radius)
        {
            _typeMech = typeOfEnum;
            towerRadius = radius;   
        }

        public void Tower_UseSpecificMechanic(Destructible destructible)
        {
            target = destructible;

            if (target != null)
            {
                switch (_typeMech)
                {
                    case EVariousMech.Poison:
                        _variousMech = GetComponent<VariousTowerMechanicsPoison>();
                        break;
                    case EVariousMech.Dps:
                        _variousMech = GetComponent<VariousTowerMechanicsDPSTower>();
                        break;
                }
                _variousMech?.UseSpecificMechanic(target, towerRadius);
            }
        }
    }
}
