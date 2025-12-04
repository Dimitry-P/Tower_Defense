using SpaceShooter;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;
using static UnityEngine.GraphicsBuffer;

namespace TowerDefense
{
    public class TDPlayer : Player
    {
        public static new TDPlayer Instance
        {
            get
            {
                return Player.Instance as TDPlayer;
            }
        }


        public static void Reset()
        {
            MonoSingleton<LevelController>.ResetInstance();
            MonoSingleton<LevelSequenceController>.ResetInstance();
            MonoSingleton<GameUI>.ResetInstance();
            MonoSingleton<LevelResultController>.ResetInstance();
            OnGoldUpdate = null;
            OnLifeUpdate = null;
        }


        private static event Action<int> OnGoldUpdate;
        public static void GoldUpdateSubscribe(Action<int> act)
        {
            OnGoldUpdate += act;
            act(Instance.m_gold);
        }

        public static void GoldUpdateUnsubscribe(Action<int> act)
        {
            OnGoldUpdate -= act;
        }
        

        private static event Action<int> OnLifeUpdate;
        public static void LifeUpdateSubscribe(Action<int> act)
        {
            OnLifeUpdate += act;
            act(Player.Instance.NumLives);
        }

        public static void LifeUpdateUnsubscribe(Action<int> act)
        {
            OnLifeUpdate -= act;
        }

        [SerializeField] private int m_gold = 10;


        public void ChangeGold(int change)
        {
            m_gold += change;
            OnGoldUpdate(m_gold);
        }

        public void ReduceLife(int change, string enemyName)
        {
            TakeDamage(change, enemyName);
            OnLifeUpdate(Player.Instance.NumLives);
        }

        // TODO: верим в то что золота на постройку достаточно
        [SerializeField] private Tower m_towerPrefab;

        public void TryBuild(TowerAsset towerAsset, Transform buildSite)
        {
            ChangeGold(-towerAsset.goldCost);
            var tower = Instantiate(m_towerPrefab, buildSite.position, Quaternion.identity);
            tower.GetComponentInChildren<SpriteRenderer>().sprite = towerAsset.sprite;
            foreach (var turret in tower.GetComponentsInChildren<Turret>())
            {
                turret.AssignLoadout2(towerAsset.turretProperties);
            }
            Destroy(buildSite.gameObject);
        }
    }
}

