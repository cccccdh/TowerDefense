using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private Transform[] wayPoints;          // 현재 스테이지의 이동 경로
    [SerializeField]
    private PlayerHP playerHP;              // 플레이어 체력 컴포넌트
    [SerializeField]
    private PlayerGold playerGold;          // 플레이어 골드 컴포넌트
    private Wave currentWave;               // 현재 웨이브 정보
    private int currentEnemyCount;          // 현재 웨이브에 남아있는 적 숫자
    private List<Enemy> enemyList;          // 현재 맵에 존재하는 모든 적의 정보

    public List<Enemy> EnemyList => enemyList;

    // 현재 웨이브의 남아있는 적, 최대 적 숫자
    public int CurrentEnemyCount => currentEnemyCount;
    public int MaxEnemyCount => currentWave.maxEnemyCount;

    private void Awake()
    {
        // 적 리스트 메모리 할당
        enemyList = new List<Enemy>();        
    }

    public void StartWave(Wave wave)
    {
        // 매개변수로 받아온 웨이브 정보 저장
        currentWave = wave;
        // 현재 웨이브의 최대 적 숫자를 저장
        currentEnemyCount = currentWave.maxEnemyCount;
        // 현재 웨이브 시작
        StartCoroutine("SpawnEnemy");
    }

    private IEnumerator SpawnEnemy()
    {
        // 현재 웨이브에서 생성한 적 숫자
        int spawnEnemyCount = 0;

        // 현재 웨이브의 enemySpawnCounts 배열
        int[] enemySpawnCounts = currentWave.enemySpawnCounts;

        // 현재 웨이브에서 생성되어야하는 적의 숫자만큼 적을 생성하고 코루틴 종료
        for (int i = 0; i < currentWave.enemyPrefabs.Length; i++)
        {
            for (int j = 0; j < enemySpawnCounts[i]; j++)
            {
                GameObject clone = Instantiate(currentWave.enemyPrefabs[i]);
                Enemy enemy = clone.GetComponent<Enemy>();                      // 방금 생성된 적의 Enemy 컴포넌트

                enemy.Setup(this, wayPoints);                                   // wayPoint 정보를 매개변수로 SetUp() 호출
                enemyList.Add(enemy);                                           // 리스트에 방금 생성된 적 정보 저장

                // 현재 웨이브에 생성한 적의 숫자 +1
                spawnEnemyCount++;

                // 각 웨이브마다 spawnTime이 다를 수 있기 때문에 현재 웨이브의 spawnTime 사용
                yield return new WaitForSeconds(currentWave.spawnTime);
            }
        }
    }

    public void DestroyEnemy(EnemyDestroyType type,Enemy enemy, int gold)
    {
        // 적이 목표지점까지 도착했을 때
        if(type == EnemyDestroyType.Arrive)
        {
            // 플레이어 체력 감소
            playerHP.TakeDamage(1);
        }
        // 적이 플레이어의 발사체에게 사망했을 때
        else if(type == EnemyDestroyType.kill)
        {
            // 적의 종류에 따라 사망 시 골드 획득
            playerGold.CurrentGold += gold;
        }

        // 적이 사망할 때마다 현재 웨이브의 생존 적 숫자 감소(UI 표시용)
        currentEnemyCount--;

        // 리스트에서 사망하는 적 정보 삭제
        enemyList.Remove(enemy);

        // 적 오브젝트 삭제
        Destroy(enemy.gameObject);
    }
}