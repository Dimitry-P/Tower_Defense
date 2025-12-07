using SpaceShooter;
using TowerDefense;
using Towers.std;
using UnityEngine;

namespace Towers
{
    public class VariousTowerMechanics : MonoBehaviour
    {
        public Destructible target;
        protected Tower tower;
        protected string nameOfTower;
        protected Turret[] turrets;
        private Projectile projectile;
        private VariousMech _variousMech;
        private EVariousMech _typeMech;

        private void Start()
        {
            turrets = GetComponentsInChildren<Turret>();

        }

        public void ApplyTowersMechanics(Tower specificTower, string towerName, EVariousMech type)
        {
            tower = specificTower;
            nameOfTower = towerName;
            InitVariousMech(type);
        }

        public void Tower_UseSpecificMechanic(Destructible destructible)
        {
            target = destructible;
            
            if (_variousMech != null)
            {
                _variousMech.UseSpecificMechanic(target);
            }
        }
        
        public void EnemyIsDead()
        {
            target.isDead = true;
        }
        
        public void EnemyIsPoisoned()
        {

        }
        
        private void InitVariousMech(EVariousMech type)
        {
            _typeMech = type;
            
            switch (_typeMech)
            {
                case EVariousMech.Poison:
                    _variousMech = new VariousTowerMechanicsPoison( );
                    break;
                case EVariousMech.Dps:
                    _variousMech = new VariousTowerMechanicsDPSTower( );
                    break;
            }
        }
    }
}
