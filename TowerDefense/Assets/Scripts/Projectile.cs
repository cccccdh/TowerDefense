using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Movement2D movement2D;
    private Transform target;
    private float damage;

    public void Setup(Transform target, float damage)
    {
        movement2D = GetComponent<Movement2D>();
        this.target = target;                       // 타워가 설정해준 target
        this.damage = damage;
    }

    void Update()
    {
        // 타겟이 존재하면
        if(target != null)
        {
            // 발사체를 target의 위치로 이동
            Vector3 direction = (target.position - transform.position).normalized;
            movement2D.MoveTo(direction);
        }
        // 타겟이 사라지면
        else
        {
            // 발사체 오브젝트 삭제
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 적이 아닌 대상과 부딪히면
        if (!collision.CompareTag("Enemy")) return;

        // 현재 타겟이 적이 아니면
        if (collision.transform != target) return;

        //collision.GetComponent<Enemy>().OnDie();

        // 적 체력을 damage만큼 감소
        collision.GetComponent<EnemyHP>().TakeDamage(damage);

        // 발사체 오브젝트 삭제
        Destroy(gameObject);
    }
}
