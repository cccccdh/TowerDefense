using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private Transform[] wayPoints;          // ���� ���������� �̵� ���
    [SerializeField]
    private PlayerHP playerHP;              // �÷��̾� ü�� ������Ʈ
    [SerializeField]
    private PlayerGold playerGold;          // �÷��̾� ��� ������Ʈ
    private Wave currentWave;               // ���� ���̺� ����
    private int currentEnemyCount;          // ���� ���̺꿡 �����ִ� �� ����
    private List<Enemy> enemyList;          // ���� �ʿ� �����ϴ� ��� ���� ����

    public List<Enemy> EnemyList => enemyList;

    // ���� ���̺��� �����ִ� ��, �ִ� �� ����
    public int CurrentEnemyCount => currentEnemyCount;
    public int MaxEnemyCount => currentWave.maxEnemyCount;

    private void Awake()
    {
        // �� ����Ʈ �޸� �Ҵ�
        enemyList = new List<Enemy>();        
    }

    public void StartWave(Wave wave)
    {
        // �Ű������� �޾ƿ� ���̺� ���� ����
        currentWave = wave;
        // ���� ���̺��� �ִ� �� ���ڸ� ����
        currentEnemyCount = currentWave.maxEnemyCount;
        // ���� ���̺� ����
        StartCoroutine("SpawnEnemy");
    }

    private IEnumerator SpawnEnemy()
    {
        // ���� ���̺꿡�� ������ �� ����
        int spawnEnemyCount = 0;

        // ���� ���̺��� enemySpawnCounts �迭
        int[] enemySpawnCounts = currentWave.enemySpawnCounts;

        // ���� ���̺꿡�� �����Ǿ���ϴ� ���� ���ڸ�ŭ ���� �����ϰ� �ڷ�ƾ ����
        for (int i = 0; i < currentWave.enemyPrefabs.Length; i++)
        {
            for (int j = 0; j < enemySpawnCounts[i]; j++)
            {
                GameObject clone = Instantiate(currentWave.enemyPrefabs[i]);
                Enemy enemy = clone.GetComponent<Enemy>();                      // ��� ������ ���� Enemy ������Ʈ

                enemy.Setup(this, wayPoints);                                   // wayPoint ������ �Ű������� SetUp() ȣ��
                enemyList.Add(enemy);                                           // ����Ʈ�� ��� ������ �� ���� ����

                // ���� ���̺꿡 ������ ���� ���� +1
                spawnEnemyCount++;

                // �� ���̺긶�� spawnTime�� �ٸ� �� �ֱ� ������ ���� ���̺��� spawnTime ���
                yield return new WaitForSeconds(currentWave.spawnTime);
            }
        }
    }

    public void DestroyEnemy(EnemyDestroyType type,Enemy enemy, int gold)
    {
        // ���� ��ǥ�������� �������� ��
        if(type == EnemyDestroyType.Arrive)
        {
            // �÷��̾� ü�� ����
            playerHP.TakeDamage(1);
        }
        // ���� �÷��̾��� �߻�ü���� ������� ��
        else if(type == EnemyDestroyType.kill)
        {
            // ���� ������ ���� ��� �� ��� ȹ��
            playerGold.CurrentGold += gold;
        }

        // ���� ����� ������ ���� ���̺��� ���� �� ���� ����(UI ǥ�ÿ�)
        currentEnemyCount--;

        // ����Ʈ���� ����ϴ� �� ���� ����
        enemyList.Remove(enemy);

        // �� ������Ʈ ����
        Destroy(enemy.gameObject);
    }
}