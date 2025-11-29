using SpaceShooter;
using System.Collections;
using System.Collections.Generic;
using TowerDefense;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    [SerializeField] private Projectile projectile;
    [SerializeField] private TowerAsset towerAsset;
    private void Start()
    {
        Use();
    }


    private void Use()
    {
        var e = Instantiate(projectile);
        e.Use(towerAsset);
    }
}
