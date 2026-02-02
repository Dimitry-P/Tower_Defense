using SpaceShooter;
using System;
using TowerDefense;
using Towers.std;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.XR;
using static UnityEngine.EventSystems.EventTrigger;

namespace SpaceShooter
{
    public class EnemySpawner : Spawner
    {
        [SerializeField] private Enemy m_EnemyPrefab;
        [SerializeField] private Path m_path;
        [SerializeField] private EnemyAsset[] m_EnemyAssets;
        public Transform spawnPoint;   // точка появления врагов
        public Transform baseTransform; // база игрока


        protected override GameObject GenerateSpawnedEntity()
        {
            // Логируем сразу перед спавном
            Debug.Log($"spawnPoint={spawnPoint}, baseTransform={baseTransform}");
            var e = Instantiate(m_EnemyPrefab, spawnPoint.position, Quaternion.identity);
            e.GetComponent<SpaceShip>().SetTargetPoint(baseTransform);
            e.Use(m_EnemyAssets[UnityEngine.Random.Range(0, m_EnemyAssets.Length)]);
            e.GetComponent<TDController>().SetPath(m_path);
            return e.gameObject;
        }
    }
}



