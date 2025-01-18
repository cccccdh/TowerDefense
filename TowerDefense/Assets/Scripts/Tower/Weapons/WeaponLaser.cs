using System.Collections;
using UnityEngine;

public class WeaponLaser : TowerWeapon
{
    [Header("Laser")]
    [SerializeField]
    private LineRenderer lineRenderer;                              // �������� ���Ǵ� ��
    [SerializeField]
    private Transform hitEffect;                                    // Ÿ�� ȿ��
    [SerializeField]
    private LayerMask targetLayer;                                  // ������ �ε����� ���̾� ����

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
        // ������, ������ Ÿ�� ȿ�� Ȱ��ȭ
        EnableLaser();

        while (true)
        {
            // target�� �����ϴ°� �������� �˻�
            if (IsPossibleToAttackTarget() == false)
            {
                // ������, ������ Ÿ�� ȿ�� ��Ȱ��ȭ
                DisableLaser();
                ChangeState(WeaponState.SearchTarget);
                break;
            }

            // ������ ����
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

        // ���� �������� ���� ���� ������ ���� �� �� ���� attackTarget�� ������ ������Ʈ�� ����
        for (int i = 0; i < hit.Length; ++i)
        {
            if (hit[i].transform == attackTarget)
            {
                // ���� ���� ����
                lineRenderer.SetPosition(0, spawnPoint.position);

                // ���� ��ǥ ����
                //lineRenderer.SetPosition(1, new Vector3(hit[i].point.x, hit[i].point.y, 0) + Vector3.back);
                lineRenderer.SetPosition(1, new Vector3(attackTarget.position.x, attackTarget.position.y, 0) + Vector3.back);

                // Ÿ�� ȿ�� ��ġ ����
                //hitEffect.position = hit[i].point;
                hitEffect.position = attackTarget.position;

                // �� ü�� ���� ( 1�ʿ� damage��ŭ ����)
                attackTarget.GetComponent<EnemyHP>().TakeDamage(towerTemplate.weapon[level].damage * Time.deltaTime);

            }
        }
    }

    public override bool Upgrade()
    {
        base.Upgrade();

        float LaserWidth = 0.05f + level * 0.05f;

        // ������ ���� ������ ���� ����
        lineRenderer.startWidth = LaserWidth;
        lineRenderer.endWidth = 0.05f;

        return true;
    }
}
