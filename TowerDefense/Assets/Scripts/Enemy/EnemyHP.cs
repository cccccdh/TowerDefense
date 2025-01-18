using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    [SerializeField]
    private float maxHP;
    [SerializeField]
    private float currentHP;
    private bool isDie = false;
    private Enemy enemy;
    private SpriteRenderer spriteRenderer;

    public float MaxHP => maxHP;
    public float CurrentHP => currentHP;

    // 색상 매핑을 위한 딕셔너리
    private readonly Color32[] colorMapping = new Color32[]
    {
        new Color32(255, 0, 0, 255),       // 빨강
        new Color32(255, 165, 0, 255),     // 주황
        new Color32(255, 255, 0, 255),     // 노랑
        new Color32(0, 255, 0, 255),       // 초록
        new Color32(0, 0, 255, 255),       // 파랑
        new Color32(180, 85, 162, 255),    // 보라
        new Color32(0, 0, 0, 0),           // 검정
        new Color32(255, 255, 255, 255),   // 흰색
    };


    private void Awake()
    {
        // 현재 체력을 최대 체력과 같게 설정
        currentHP = maxHP;

        enemy = GetComponent<Enemy>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float damage)
    {
        // 현재 적의 상태가 사망 상태이면 실행 X
        if (isDie) return;

        // 현재 체력을 damage만큼 감소
        currentHP -= damage;

        ChangeColor();

        // 체력이 0 이하
        if(currentHP <= 0)
        {
            isDie = true;
            // 적 캐릭터 사망
            enemy.OnDie(EnemyDestroyType.kill);
        }
    }

    private void ChangeColor()
    {
        // 체력에 따라 색상 변경 (범위에 맞게 색상 결정)
        if (currentHP > 0 && currentHP <= 1)
        {
            spriteRenderer.color = colorMapping[0];
        }
        else if (currentHP > 1 && currentHP <= 2)
        {
            spriteRenderer.color = colorMapping[1];
        }
        else if (currentHP > 2 && currentHP <= 3)
        {
            spriteRenderer.color = colorMapping[2];
        }
        else if (currentHP > 3 && currentHP <= 4)
        {
            spriteRenderer.color = colorMapping[3];
        }
        else if (currentHP > 4 && currentHP <= 5)
        {
            spriteRenderer.color = colorMapping[4];
        }
        else if (currentHP > 5 && currentHP <= 6)
        {
            spriteRenderer.color = colorMapping[5];
        }
        else
        {
            spriteRenderer.color = colorMapping[6];
        }
    }
}
