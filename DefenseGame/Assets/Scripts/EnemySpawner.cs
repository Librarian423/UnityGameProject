using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Dragon[] dragonPrefabs;
    public Transform[] spawnPoints;
    public DragonData[] dragonDatas;
    public WaveData[] waveDatas;

    private List<Dragon> dragons = new List<Dragon>();
    private int wave = 0;
    private int waveCount = 0;

    public float waitTimer = 1f;
    private float timer = 1f;

    public enum DragonType
    {
        Gold = 0,
        Red = 1,
    }

    // Start is called before the first frame update
    void Start()
    {
        timer = waitTimer;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= waitTimer && dragons.Count <= 0 && wave < waveDatas.Length) 
        {
            
            SpawnWave();
        }
        if (wave >= waveDatas.Length) 
        {
            wave = 0;

        }
    }

    private void SpawnWave()
    {
        waveCount++;
        int gdCount = waveDatas[wave].goldDragonCount;
        int rdCount = waveDatas[wave].redDragonCount;

        MakeGoldDragon(gdCount);
        MakeRedDragon(rdCount);
        UIManager.instance.UpdateWaveText(waveCount);
        wave++;
    }

    private void MakeGoldDragon(int count)
    {
        for (int i = 0; i < count; i++)
        {
            CreateDragon(DragonType.Gold);
        }
    }

    private void MakeRedDragon(int count)
    {
        for (int i = 0; i < count; i++)
        {
            CreateDragon(DragonType.Red);
        }
    }

    private void CreateDragon(DragonType type)
    {

        float health = dragonDatas[(int)type].health;
        float speed = dragonDatas[(int)type].speed;
        float damage = dragonDatas[(int)type].damage;
        float moveDistance = dragonDatas[(int)type].moveDistance;
        float fireDelay = dragonDatas[(int)type].fireDelay;

        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        // 적 프리팹으로부터 적 생성
        Dragon dragon = Instantiate(dragonPrefabs[(int)type], spawnPoint.position, Quaternion.identity);

        // 생성한 적의 능력치와 추적 대상 설정
        dragon.SetStates(health, speed, damage, moveDistance, fireDelay);

        // 생성된 적을 리스트에 추가
        dragons.Add(dragon);

        // 적의 onDeath 이벤트에 익명 메서드 등록
        // 사망한 적을 리스트에서 제거
        dragon.onDeath += () => dragons.Remove(dragon);
        dragon.onDeath += () => timer = 0f;

        // 사망한 적을 10 초 뒤에 파괴
        //enemy.onDeath += () => Destroy(enemy.gameObject, 10f);
        // 적 사망시 점수 상승
        //enemy.onDeath += () => GameManager.instance.AddScore(100);

    }
}
