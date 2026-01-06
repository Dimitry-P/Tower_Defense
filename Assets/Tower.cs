using UnityEngine;
using SpaceShooter;
using Towers;

namespace TowerDefense
{
    public class Tower : MonoBehaviour
    {
        private float m_Radius;
        public float Radius { get { return m_Radius; } set { m_Radius = value; } }
        public Destructible target = null;
        public Turret[] turrets;

        public float timer = 0f;

        private void Awake()
        {
            turrets = GetComponentsInChildren<Turret>();   
        }

        public void InitTurretSpecificSettings(EVariousMech variousType, float towerRadius)
        {
            foreach (var turret in turrets)
            {
                turret.InitTurretSpecificSettings(variousType, towerRadius);
            }
        }

        private void Update()
        {
            timer += Time.deltaTime;

            if (target != null)
            {
                Vector2 targetVector = target.transform.position - transform.position;
                Enemy enemy = target.GetComponent<Enemy>();
                
                if (targetVector.magnitude <= Radius)
                {
                    foreach (var turret in turrets)
                    {
                        turret.transform.up = targetVector;
                        turret.Init(this);
                        turret.Fire();
                    }
                }
            }
            else
            {
                var enter = Physics2D.OverlapCircle(transform.position, m_Radius);
                if (enter != null)
                {
                    target = enter.GetComponentInParent<Destructible>();
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

