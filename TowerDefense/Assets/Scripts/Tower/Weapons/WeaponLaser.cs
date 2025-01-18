using System.Collections;
using UnityEngine;

public class WeaponLaser : TowerWeapon
{
    [Header("Laser")]
    [SerializeField]
    private LineRenderer lineRenderer;                              // 레이저로 사용되는 선
    [SerializeField]
    private Transform hitEffect;                                    // 타격 효과
    [SerializeField]
    private LayerMask targetLayer;                                  // 광선에 부딪히는 레이어 설정

    protected IEnumerator SearchTarget()
    {
        while (true)
        {
            attackTarget = FindClosestAttackTarget();

            if (attackTarget != null)
            {
                ChangeState(WeaponState.TryAttackLaser);
            }

            yield return null;
        }
    }

    private IEnumerator TryAttackLaser()
    {
        // 레이저, 레이저 타격 효과 활성화
        EnableLaser();

        while (true)
        {
            // target을 공격하는게 가능한지 검사
            if (IsPossibleToAttackTarget() == false)
            {
                // 레이저, 레이저 타격 효과 비활성화
                DisableLaser();
                ChangeState(WeaponState.SearchTarget);
                break;
            }

            // 레이저 공격
            SpawnLaser();

            yield return null;
        }
    }

    private void EnableLaser()
    {
        lineRenderer.gameObject.SetActive(true);
        hitEffect.gameObject.SetActive(true);
    }

    private void DisableLaser()
    {
        lineRenderer.gameObject.SetActive(false);
        hitEffect.gameObject.SetActive(false);
    }

    private void SpawnLaser()
    {
        Vector3 direction = attackTarget.position - spawnPoint.position;
        RaycastHit2D[] hit = Physics2D.RaycastAll(spawnPoint.position, direction,
            towerTemplate.weapon[level].range, targetLayer);

        // 같은 방향으로 여러 개의 광선을 쏴서 그 중 현재 attackTarget과 동일한 오브젝트를 검출
        for (int i = 0; i < hit.Length; ++i)
        {
            if (hit[i].transform == attackTarget)
            {
                // 선의 시작 지점
                lineRenderer.SetPosition(0, spawnPoint.position);

                // 선의 목표 지점
                //lineRenderer.SetPosition(1, new Vector3(hit[i].point.x, hit[i].point.y, 0) + Vector3.back);
                lineRenderer.SetPosition(1, new Vector3(attackTarget.position.x, attackTarget.position.y, 0) + Vector3.back);

                // 타격 효과 위치 설정
                //hitEffect.position = hit[i].point;
                hitEffect.position = attackTarget.position;

                // 적 체력 감소 ( 1초에 damage만큼 감소)
                attackTarget.GetComponent<EnemyHP>().TakeDamage(towerTemplate.weapon[level].damage * Time.deltaTime);

            }
        }
    }

    public override bool Upgrade()
    {
        base.Upgrade();

        float LaserWidth = 0.05f + level * 0.05f;

        // 레벨에 따라 레이저 굵기 설정
        lineRenderer.startWidth = LaserWidth;
        lineRenderer.endWidth = 0.05f;

        return true;
    }
}
