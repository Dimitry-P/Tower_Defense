using System;
using System.Collections;
using System.Collections.Generic;
using TowerDefense;
using Towers;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

namespace SpaceShooter
{
    /// <summary>
    /// Уничтожаемый объект на сцене. То что может иметь хит поинты.
    /// </summary>
    public class Destructible : Entity
    {
        #region Properties

        /// <summary>
        /// Объект игнорирует повреждения.
        /// </summary>
        [SerializeField] private bool m_Indestructible;
        public bool IsIndestructible => m_Indestructible;

        /// <summary>
        /// Стартовое кол-во хитпоинтов.
        /// </summary>
        [SerializeField] private int m_HitPoints;
        
        /// <summary>
        /// Текущие хит поинты
        /// </summary>
        private int m_CurrentHitPoints;
        public int HitPoints => m_CurrentHitPoints;

        // Прогресс по пути от 0 (старт) до 1 (конец)
        public float PathProgress { get; set; }

        #endregion

        #region Unity events

        //public VariousTowerMechanics variousTowerMechanics;

        protected virtual void Start()
        {
            m_CurrentHitPoints = m_HitPoints;
        }
       
        #region Безтеговая коллекция скриптов на сцене

        private static HashSet<Destructible> m_AllDestructibles;

        public static IReadOnlyCollection<Destructible> AllDestructibles => m_AllDestructibles;

        protected virtual void OnEnable()
        {
            if (m_AllDestructibles == null)
                m_AllDestructibles = new HashSet<Destructible>();

            m_AllDestructibles.Add(this);
        }

        protected virtual void OnDestroy()
        {
            m_AllDestructibles.Remove(this);
        }

        #endregion 

        #endregion

        #region Public API

        public bool isDead = false;

        /// <summary>
        /// Применение дамага к объекту.
        /// </summary>
        /// <param name="damage"></param>
        public void ApplyDamage(int damage)
        {
            if (m_Indestructible)
                return;
            Debug.Log("DAMAGE;;;;;$$$$$$$$$$$$$$$$$$$$$$$$$$$: " + damage);
            Debug.Log("было$$$$$$$$$$$$$: " + m_CurrentHitPoints);

            m_CurrentHitPoints -= damage;

            Debug.Log("стало$$$$$$$$$$$$$: " + m_CurrentHitPoints);

            if (m_CurrentHitPoints <= 0)
                OnDeath();
        }

        public void AddHitPoints(float hp)
        {
            m_CurrentHitPoints = (int)Mathf.Clamp(m_CurrentHitPoints + hp, 0, m_HitPoints);
        }

        #endregion

        /// <summary>
        /// Перепоределяемое событие уничтожения объекта, когда хит поинты ниже нуля.
        /// </summary>
        /// 

        public void SetColorTemporary(Color color, float duration)
        {
            var sprite = GetComponentInChildren<SpriteRenderer>();
            if (sprite == null) return;

            sprite.color = color;

            CancelInvoke(nameof(ResetColor));
            Invoke(nameof(ResetColor), duration);
        }

        private void ResetColor()
        {
            var sprite = GetComponentInChildren<SpriteRenderer>();
            if (sprite != null)
                sprite.color = Color.white;
           
        }

        private Coroutine poisonCoroutine;

        public void ApplyPoison(int damagePerSecond, float duration)
        {
            if (poisonCoroutine != null)
                StopCoroutine(poisonCoroutine);

            poisonCoroutine = StartCoroutine(PoisonCoroutine(damagePerSecond, duration));
            IsPoisoned = false;
        }
        private bool isPoinsoned;
        public bool IsPoisoned
        {
            get { return isPoinsoned;  }
            set { isPoinsoned = value; }
        }

        private IEnumerator PoisonCoroutine(int damage, float duration)
        {
            float elapsed = 0f;

            while (elapsed < duration)
            {
                ApplyDamage(damage);
                yield return new WaitForSeconds(1f);
                elapsed += 1f;
            }
                poisonCoroutine = null;
        }

        protected virtual void OnDeath()
        {
            if(m_ExplosionPrefab != null)
            {
                var explosion = Instantiate(m_ExplosionPrefab.gameObject);
                explosion.transform.position = transform.position;
            }

            var enemyName = gameObject.GetComponent<Enemy>();
            string nm = enemyName.enemyName;
            if (nm == "boss")
            {
                Debug.Log("You are the winner");
            } 
                Destroy(gameObject);

            m_EventOnDeath?.Invoke();
        }

        [SerializeField] private UnityEvent m_EventOnDeath;
        public UnityEvent EventOnDeath => m_EventOnDeath;

        [SerializeField] private ImpactEffect m_ExplosionPrefab;

        #region Teams

        /// <summary>
        /// Полностью нейтральный тим ид. Боты будут игнорировать такие объекты.
        /// </summary>
        public const int TeamIdNeutral = 0;

        /// <summary>
        /// ИД стороны. Боты будут атаковать всех кто не свой.
        /// </summary>
        [SerializeField] private int m_TeamId;
        public int TeamId => m_TeamId;

        #endregion

        #region Score

        /// <summary>
        /// Кол-во очков за уничтожение.
        /// </summary>
        [SerializeField] private int m_ScoreValue;

        public int ScoreValue => m_ScoreValue;

        #endregion

        protected void Use(EnemyAsset asset)
        {
            m_HitPoints = asset.hp;
            m_ScoreValue = asset.score;
        }
    }
}