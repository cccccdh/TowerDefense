using System.Collections;
using UnityEngine;

public class WeaponCannon : TowerWeapon
{
    [Header("Cannon")]
    [SerializeField]
    private GameObject projectilePrefabs;                           // 발사체 프리팹

    protected  IEnumerator SearchTarget()
    {
        while (true)
        {
            attackTarget = FindClosestAttackTarget();

            if (attackTarget != null)
            {
                ChangeState(WeaponState.TryAttackCannon);
            }

            yield return null;
        }
    }

    private IEnumerator TryAttackCannon()
    {
        while (true)
        {
            // target을 공격하는게 가능한지 검사
            if (IsPossibleToAttackTarget() == false)
            {
                ChangeState(WeaponState.SearchTarget);
                break;
            }

            // attackRate 시간만큼 대기
            yield return new WaitForSeconds(towerTemplate.weapon[level].rate);

            // 공격 (발사체 생성)
            SpawnProjectile();
        }
    }

    private void SpawnProjectile()
    {
        GameObject clone = Instantiate(projectilePrefabs, spawnPoint.position, Quaternion.identity);

        // 생성된 발사체에게 공격대상(attackTarget) 정보 제공
        clone.GetComponent<Projectile>().Setup(attackTarget, towerTemplate.weapon[level].damage);
    }
}
