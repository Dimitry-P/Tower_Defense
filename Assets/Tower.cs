using SpaceShooter;
using System.Collections.Generic;
using System.Linq;
using Towers;
using Towers.std;
using UnityEngine;
using static SpaceShooter.Destructible;
using static UnityEngine.GraphicsBuffer;

//if (targetVector.magnitude > m_Radius ||
//            target.WasAlreadyHitBy(this))
//            ключевое условие.  В данной ситуации проверять экземпляр башни
//            на предмет её попадания по врагу лучше всего с помощью метода, а не свойства!
//            И как раз из класса Tower мы передаём this,
//            что очевидно будет означать данный конкретный экземпляр
// Дальше получается, что мы проверяем !dest.WasAlreadyHitBy(this) для конкретного экземпляра врага dest
// - у него через точку вызываем метод WasAlreadyHitBy(Tower tower), который проверяет, не находится ли
// данный экземпляр башни в Хэшсете уже стрелявших башен, и если Contains возвращает false,
// то тогда данный экземпляр врага добавляем в allTargets - то есть в список врагов.  

//Вопрос: почему мы у экземпляра врага вызываем метод, которым проверяем наличие Башни в хэшсете  dest.WasAlreadyHitBy(this) ? 
//Ответ: Мы вызываем WasAlreadyHitBy(this) у врага, потому что логично, чтобы именно враг помнил:
//«меня уже била вот эта башня, вот эта и вот эта». У башен хранить такие списки обычно хуже по памяти и удобству.

//Вопрос: Скажите, пожалуйста,  в этой строке: var candidate = hit.GetComponentInParent<Destructible>();
//принципиально ли класть Destructible в локальную переменную var ?
//Просто в моей прежней версии я клал его в публичное поле public Destructible target.
//Ответ: var candidate = … хорошая практика: код становится читаемее, легче дебажить,
//и мы не вызываем GetComponentInParent несколько раз подряд. Это не обязательно, но сильно рекомендуется.

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
            Debug.Log("Check targets: " + allTargets.Count);
            Destructible best = null;
            float maxProgress = -1f;
            foreach (var e in allTargets)
            {
                float dist = Vector3.Distance(transform.position, e.transform.position);
                Debug.Log($"Enemy dist={dist}, progress={e.PathProgress}");
                //if (e.IsPoisoned && _variousMech) continue;
                if (Vector2.Distance(transform.position, e.transform.position) > m_Radius) continue;
                if (e.PathProgress > maxProgress)
                {
                    maxProgress = e.PathProgress;
                    best = e;
                }
            }
            Debug.Log("Selected target: " + best);
            return best;
        }

        public Projectile projectile;

        public VariousMech thisTowerHitEnemy;

        public void WasHitByThisTower(VariousMech d)
        {
            thisTowerHitEnemy = d;
        }

        //Что по сути делает мой метод  Update в классе Tower(находит цель и делает выстрел):
        //1. Если цель найдена, проверяю не вышла ли она за пределы радиуса башни и не попали ли уже в эту цель.
        //Если да, то обнуляем цель
        //2. Если цели нет,  то с помощью Physics2D.OverlapCircle смотрим кто внутри радиуса.
        //И для найденного врага вызываем метод, проверяющий не находится ли данная башня,
        //которая попала в данного врага, уже в нашем хэшсете (этот метод мы добавили в Destructible).
        //И если нет - то кладём в target  нашу жертву.
        //3. Если враг уже найден  if (target != null), то проходимся foreach по массиву турелей,
        //и фактически единственной турелью из массива делаем выстрел.
        //4. После смерти жертвы, внутри класса Destructible очищаем наш хэшсет от помеченных башен:
        //private void OnDestroy()
        //{
        //    _hitByTowers.Clear();
        //}

        //Update в 80+ строк это почти всегда плохой знак.
        //В идеале разбить на маленькие методы (FindBestTarget, HasValidTarget, RotateToTarget, Fire)
        //что уже сильно улучшит читаемость.

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
                    // <-- Вставляем новый код выбора цели -->
                    Destructible bestTarget = EnemyForSingleTower(allTargets);
                    if (bestTarget != null)
                    {
                        target = bestTarget;
                    }
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

                Debug.Log($"[Tower] Shooting at enemy: {target.name}, progress={target.PathProgress}, dist={Vector2.Distance(transform.position, target.transform.position)}");

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

