using SpaceShooter;
using System.Collections.Generic;
using System.Linq;
using Towers;
using Towers.std;
using UnityEngine;
using static SpaceShooter.Destructible;
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
                //if (e.IsPoisoned && _variousMech) continue;
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

        public VariousMech thisTowerHitEnemy;

        public void WasHitByThisTower(VariousMech d)
        {
            thisTowerHitEnemy = d;
        }

        private void Update()
        {
            timer += Time.deltaTime;

            if (target != null)
            {
                Vector2 targetVector = target.transform.position - transform.position;

                //Основная логика не выбирать врага, которого эта башня уже била.
                if (targetVector.magnitude > m_Radius ||
                    target.WasAlreadyHitBy(this))     // ключевое условие)
                {
                    // Сбрасываем цель
                    target = null;
                }
            }

            // 2. Если цели нет ищем новую
            if (target == null)
            {
                // --- Если target == null или был сброшен, ищем нового врага ---
                if (isSingleTower)
                {
                    Collider2D[] enters = Physics2D.OverlapCircleAll(transform.position, m_Radius);
                    allTargets.Clear();

                    foreach (var col in enters)
                    {
                        var dest = col.GetComponentInParent<Destructible>();
                        if (dest != null && !dest.WasAlreadyHitBy(this))  // не берём тех, кого уже били
                        {
                            allTargets.Add(dest);
                        }
                    }

                    target = EnemyForSingleTower(allTargets);
                }
                else
                {
                    var enter = Physics2D.OverlapCircle(transform.position, m_Radius);
                    if (enter != null)
                    {
                        var candidate = enter.GetComponentInParent<Destructible>();
                        if (candidate != null && !candidate.WasAlreadyHitBy(this))
                        {
                            target = candidate;
                        }
                    }
                }
            }

            // 3. Если цель найдена поворачиваем и стреляем
            if (target != null)
            {
                Vector2 direction = (target.transform.position - transform.position).normalized;

                foreach (var turret in turrets)
                {
                    turret.transform.up = direction;
                    turret.Init(this);
                    turret.Fire();               // здесь или внутри Turret должна быть логика попадания
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

