using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Movement2D movement2D;
    private Transform target;
    private float damage;

    public void Setup(Transform target, float damage)
    {
        movement2D = GetComponent<Movement2D>();
        this.target = target;                       // Ÿ���� �������� target
        this.damage = damage;
    }

    void Update()
    {
        // Ÿ���� �����ϸ�
        if(target != null)
        {
            // �߻�ü�� target�� ��ġ�� �̵�
            Vector3 direction = (target.position - transform.position).normalized;
            movement2D.MoveTo(direction);
        }
        // Ÿ���� �������
        else
        {
            // �߻�ü ������Ʈ ����
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ���� �ƴ� ���� �ε�����
        if (!collision.CompareTag("Enemy")) return;

        // ���� Ÿ���� ���� �ƴϸ�
        if (collision.transform != target) return;

        //collision.GetComponent<Enemy>().OnDie();

        // �� ü���� damage��ŭ ����
        collision.GetComponent<EnemyHP>().TakeDamage(damage);

        // �߻�ü ������Ʈ ����
        Destroy(gameObject);
    }
}
