using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    [SerializeField]
    private Image imageScreen;              // ��üȭ���� ���� ������ �̹���

    [SerializeField]
    private float maxHP = 20;               // �ִ� ü��
    private float currentHP;                // ���� ü��

    public float MaxHP => maxHP;
    public float CurrentHP => currentHP;


    private void Awake()
    {
        currentHP = maxHP;
    }

    public void TakeDamage(float damage)
    {
        // ���� ü���� damage��ŭ ����
        currentHP -= damage;

        StopCoroutine("HitAlphaAnimation");
        StartCoroutine("HitAlphaAnimation");

        // ü���� 0�� �Ǹ� ���� ����
        if (currentHP <= 0)
        {

        }
    }

    private IEnumerator HitAlphaAnimation()
    {
        // ��üȭ�� ũ��� ��ġ�� imageScreen�� ������ color ������ ����
        // imageScreen�� ������ 40%�� ����
        Color color = imageScreen.color;
        color.a = 0.4f;
        imageScreen.color = color;

        // ������ 0%�� �� ������ ����
        while( color.a >= 0.0f)
        {
            color.a -= Time.deltaTime;
            imageScreen.color = color;

            yield return null;
        }
    }
}
