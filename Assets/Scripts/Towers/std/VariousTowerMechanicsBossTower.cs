using System.Collections;
using UnityEngine;
using TowerDefense;
using SpaceShooter;

namespace Towers.std
{
    public class VariousTowerMechanicsBossTower : VariousMech
    {
        [Header("Boss Tower Settings")]
        public int baseDamage = 50;          // базовый урон обычному врагу
        public int bossDamageMultiplier = 5; // урон дл€ босса

        [Header("Visual Effect")]
        public float scaleMultiplier = 1.5f;
        public float scaleDuration = 2f;

        public override void TryApplyDamage(Destructible destructible)
        {
            if (destructible == null) return;
            if (destructible.IsPoisoned) return;
            int damageToApply = baseDamage;
            // ќпредел€ем, босс это или нет
            if (destructible.IsBoss == true)
            {
                damageToApply *= bossDamageMultiplier;
                destructible.ApplyDamage(damageToApply);
                destructible.IsPoisoned = false;
            }
            else
            {
                destructible.ApplyDamage(damageToApply);
                StartCoroutine(ScaleEnemyTemporary(destructible));
                destructible.IsPoisoned = true;
            }
        }

        private IEnumerator ScaleEnemyTemporary(Destructible destructible)
        {
            if (destructible == null) yield break;

            var sprite = destructible.GetComponentInChildren<SpriteRenderer>();
            if (sprite == null) yield break;

            Transform visual = sprite.transform;

            Vector3 originalScale = visual.localScale;
            visual.localScale = originalScale * scaleMultiplier;

            yield return new WaitForSeconds(scaleDuration);

            if (visual != null)
                visual.localScale = originalScale;
        }

        public override void UseSpecificMechanic(TurretProperties turretProperties)
        {
            // «десь можно добавить уникальную механику башни, например зар€д мощного выстрела
        }
    }
}
