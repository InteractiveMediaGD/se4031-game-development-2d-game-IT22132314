using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShoot : MonoBehaviour
{
    private BossLvlThreeController bossLvlThreeController;
    public GameObject FireballPrefab;
    public Transform FireballGizmod;
    public int FireballDamage;

    void Start()
    {
        bossLvlThreeController = GetComponent<BossLvlThreeController>();        
    }

    public void ShootFireball()
    {
        GameObject fireball = Instantiate(FireballPrefab, FireballGizmod.position, Quaternion.identity);
        EnemyProjectile fireballController = fireball.GetComponent<EnemyProjectile>();
        Vector2 shootDirection = bossLvlThreeController.GetIsTurnedLeft() ? Vector2.left : Vector2.right;
        fireballController.SetShootDirection(shootDirection);
        fireballController.Damage = FireballDamage;
        fireballController.Shoot();
    }
}
