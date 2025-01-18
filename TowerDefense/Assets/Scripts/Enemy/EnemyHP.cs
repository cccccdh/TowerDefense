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

    // ���� ������ ���� ��ųʸ�
    private readonly Color32[] colorMapping = new Color32[]
    {
        new Color32(255, 0, 0, 255),       // ����
        new Color32(255, 165, 0, 255),     // ��Ȳ
        new Color32(255, 255, 0, 255),     // ���
        new Color32(0, 255, 0, 255),       // �ʷ�
        new Color32(0, 0, 255, 255),       // �Ķ�
        new Color32(180, 85, 162, 255),    // ����
        new Color32(0, 0, 0, 0),           // ����
        new Color32(255, 255, 255, 255),   // ���
    };


    private void Awake()
    {
        // ���� ü���� �ִ� ü�°� ���� ����
        currentHP = maxHP;

        enemy = GetComponent<Enemy>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float damage)
    {
        // ���� ���� ���°� ��� �����̸� ���� X
        if (isDie) return;

        // ���� ü���� damage��ŭ ����
        currentHP -= damage;

        ChangeColor();

        // ü���� 0 ����
        if(currentHP <= 0)
        {
            isDie = true;
            // �� ĳ���� ���
            enemy.OnDie(EnemyDestroyType.kill);
        }
    }

    private void ChangeColor()
    {
        // ü�¿� ���� ���� ���� (������ �°� ���� ����)
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
