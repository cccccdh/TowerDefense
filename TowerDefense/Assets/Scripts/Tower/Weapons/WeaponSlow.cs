using UnityEngine;

public class WeaponSlow : MonoBehaviour
{
    private TowerWeapon towerWeapon;

    private void Awake()
    {
        towerWeapon = GetComponentInParent<TowerWeapon>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy"))
        {
            return;
        }

        Movement2D movement2D = collision.GetComponent<Movement2D>();

        // 이동속도 = 이동속도 - 이동속도 * 감속률
        movement2D.MoveSpeed -= movement2D.MoveSpeed * towerWeapon.Slow;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy"))
        {
            return;
        }

        // 속도를 기본 이동속도로 설정
        collision.GetComponent<Movement2D>().ResetMoveSpeed();
    }
}
