using SpaceShooter;
using System.Collections.Generic;
using System.Linq;
using Towers;
using Towers.std;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace TowerDefense
{
    public class Tower : MonoBehaviour
    {
        private float m_Radius;
        public float Radius { get { return m_Radius; } set { m_Radius = value; } }
        public Destructible target = null;
        public List<Destructible> allTargets = new List<Destructible>(); 
        public Turret[] turrets;

        public float timer = 0f;
        public VariousMech _variousMech;
        private bool isSingleTower;
        private void Awake()
        {
            turrets = GetComponentsInChildren<Turret>();
            _variousMech = GetComponent<VariousMech>();
        }

        public void InitVariousMech(VariousMech mech)
        {
            _variousMech = mech;
            isSingleTower = _variousMech.TryGetComponent<VariousTowerMechanicsSingleTower>(out _);
        }

        public void InitTurretSpecificSettings(EVariousMech variousType, float towerRadius)
        {
            foreach (var turret in turrets)
            {
                turret.InitTurretSpecificSettings(variousType, towerRadius);
            }
        }

        private Destructible EnemyForSingleTower(List<Destructible> allTargets)
        {
            Destructible best = null;
            float maxProgress = -1f;
            foreach (var e in allTargets)
            {
                if (e.IsPoisoned) continue;
                if (Vector2.Distance(transform.position, e.transform.position) > m_Radius) continue;
                if (e.PathProgress > maxProgress)
                {
                    maxProgress = e.PathProgress;
                    best = e;
                }
            }
            return best;
        }

        public Projectile projectile;

        private void Update()
        {
            timer += Time.deltaTime;

            if (target != null)
            {
                Vector2 targetVector = target.transform.position - transform.position;

                if (target.IsPoisoned || targetVector.magnitude > m_Radius)
                {
                    // Сбрасываем цель, если она помечена или вышла за радиус
                    target = null;
                }
                else
                {
                    // Стреляем в текущую цель
                    foreach (var turret in turrets)
                    {
                        turret.transform.up = targetVector;
                        turret.Init(this);
                        turret.Fire();
                    }

                    // Всё ок, не ищем новую цель
                    return;
                }
            }

            // --- Если target == null или был сброшен, ищем нового врага ---
            if (isSingleTower)
            {
                Collider2D[] enters = Physics2D.OverlapCircleAll(transform.position, m_Radius);
                allTargets.Clear();

                if (enters != null)
                {
                    foreach (var destruct in enters)
                    {
                        var d = destruct.GetComponentInParent<Destructible>();
                        if (d != null)
                            allTargets.Add(d);
                    }
                }
                target = EnemyForSingleTower(allTargets);
            }
            else
            {
                var enter = Physics2D.OverlapCircle(transform.position, m_Radius);
                if(enter != null)target = enter.GetComponentInParent<Destructible>();
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, m_Radius);
        }
    }
}

