using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Dragon[] dragonPrefabs;
    public DragonBoss[] dragonBossePrefabs;
    public EyeBall eyeBallPrefab;
    public Transform[] spawnPoints;
    public WaveData[] waveDatas;

    private List<Enemy> enemies = new List<Enemy>();
    private int wave = 0;
    private int waveCount = 0;

    public float waitTimer = 1f;
    private float timer = 1f;
    private bool isBossAlive = false;
    public float summonTimer = 3f;

    public enum EnemyType
    {
        Gold = 0,
        Red = 1,
        BossDragon,
        Eye,
        
    }

    // Start is called before the first frame update
    void Start()
    {
        timer = waitTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.IsPause) 
        {
            return;
        }

        timer += Time.deltaTime;

        //In case of Boss dragon pattern
        //summons Eyeballs
        if (isBossAlive && timer >= summonTimer) 
        {
            timer = 0;
            SpawnEyeBalls();
        }

        if (timer >= waitTimer && enemies.Count <= 0 && wave < waveDatas.Length) 
        {
            if (waveCount > 0 )
            {
                UIManager.instance.OpenSkillTree();
                //timer = 0;
            }
            //wave pattern
            if (waveDatas[wave].bossWave)
            {
                waveCount++;
                SpawnBoss();
            }
            else
            {
                waveCount++;
                SpawnDragons();
            }
        }
        if (wave >= waveDatas.Length) 
        {
            wave = 0;
        }
    }

    private void SpawnDragons()
    {
        //waveCount++;
        int gdCount = waveDatas[wave].goldDragonCount;
        int rdCount = waveDatas[wave].redDragonCount;

        MakeGoldDragon(gdCount);
        MakeRedDragon(rdCount);
        UIManager.instance.UpdateWaveText(waveCount);
        SetWaveCount();
        //wave++;
    }

    private void SpawnBoss()
    {
        isBossAlive = true;
        //waveCount++;
        int bossCount = waveDatas[wave].bossDragonCount;

        MakeBossDragon(bossCount);
        UIManager.instance.UpdateWaveText(waveCount);
        SetWaveCount();
        //wave++;
    }

    private void SpawnEyeBalls()
    {
        int eyeCount = Random.Range(waveDatas[wave].eyeBallMinCount, waveDatas[wave].eyeBallMaxCount);

        MakeEyeBall(eyeCount);
        //Debug.Log("eyeball");
    }

    private void MakeGoldDragon(int count)
    {
        for (int i = 0; i < count; i++)
        {
            SetStatus(EnemyType.Gold);
        }
    }

    private void MakeRedDragon(int count)
    {
        for (int i = 0; i < count; i++)
        {
            SetStatus(EnemyType.Red);
        }
    }

    private void MakeBossDragon(int count)
    {
        for (int i = 0; i < count; i++)
        {
            SetStatus(EnemyType.BossDragon);
        }
    }

    private void MakeEyeBall(int count)
    {
        for (int i = 0; i < count; i++)
        {
            SetStatus(EnemyType.Eye);
        }
    }

    private void SetStatus(EnemyType type)
    {
        //float health = dragonDatas[(int)type].health;
        //float speed = dragonDatas[(int)type].speed;
        //float damage = dragonDatas[(int)type].damage;
        //float moveDistance = dragonDatas[(int)type].movePos;
        //float fireDelay = dragonDatas[(int)type].fireDelay;
        //int gold = dragonDatas[(int)type].dropGold;

        switch (type)
        {
            case EnemyType.Gold:
            case EnemyType.Red:
                CreateDragon(type);//, health, speed, damage, moveDistance, fireDelay, gold);
                break;
            case EnemyType.BossDragon:
                CreateBossDragon();
                break;
            case EnemyType.Eye:
                CreateEyeBall();
                break;
        }
        
    }

    private void CreateDragon(EnemyType type)//,float health, float speed, float damage, float moveDistance, float fireDelay, int gold)
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length - 1)];
        
        Dragon dragon = Instantiate(dragonPrefabs[(int)type], spawnPoint.position, Quaternion.identity);

        // 생성한 적의 능력치와 추적 대상 설정
        //dragon.SetStates(health, speed, damage, moveDistance, fireDelay);

        // 생성된 적을 리스트에 추가
        enemies.Add(dragon);

        // 적의 onDeath 이벤트에 익명 메서드 등록
        // 사망한 적을 리스트에서 제거
        dragon.onDeath += () => PropertySpawner.instance.CreateCorp((int)type, dragon.transform.position);
        dragon.onDeath += () => enemies.Remove(dragon);
        dragon.onDeath += () => timer = 0f;
        //dragon.onDeath += () => PropertyManager.instance.SetMoney(gold);
        dragon.onDeath += () => GotoNextWave();

        // 사망한 적을 10 초 뒤에 파괴
        //enemy.onDeath += () => Destroy(enemy.gameObject, 10f);
        // 적 사망시 점수 상승
        //enemy.onDeath += () => GameManager.instance.AddScore(100);

    }

    private void CreateBossDragon()
    {
        int index = spawnPoints.Length - 1;
        Transform spawnPoint = spawnPoints[index];

        DragonBoss dragonBoss = Instantiate(dragonBossePrefabs[0], spawnPoint.position, Quaternion.identity);

        // 생성한 적의 능력치와 추적 대상 설정
        //dragonBoss.SetStates(health, speed, damage, moveDistance, fireDelay);

        // 생성된 적을 리스트에 추가
        enemies.Add(dragonBoss);

        // 적의 onDeath 이벤트에 익명 메서드 등록
        // 사망한 적을 리스트에서 제거
        //dragonBoss.onDeath += () => PropertySpawner.instance.CreateCorp((int)type, dragon.transform.position);
        dragonBoss.onDeath += () => PropertySpawner.instance.CreateCorp((int)EnemyType.BossDragon, dragonBoss.transform.position);
        dragonBoss.onDeath += () => enemies.Remove(dragonBoss);
        dragonBoss.onDeath += () => timer = 0f;
        //dragonBoss.onDeath += () => PropertyManager.instance.SetMoney(gold);
        dragonBoss.onDeath += () => StopSpawnEyeBalls();
        dragonBoss.onDeath += () => GotoNextWave();
    }

    private void CreateEyeBall()
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length - 1)];

        EyeBall eyeBall = Instantiate(eyeBallPrefab, spawnPoint.position, Quaternion.identity);

        // 생성한 적의 능력치와 추적 대상 설정
        //eyeBall.SetStates(health, speed, damage, moveDistance, fireDelay);

        // 생성된 적을 리스트에 추가
        enemies.Add(eyeBall);
 
        eyeBall.onDeath += () => enemies.Remove(eyeBall);
        eyeBall.onDeath += () => GotoNextWave();

    }

    private void StopSpawnEyeBalls()
    {
        foreach (var enemy in enemies)
        {
            if (enemy.gameObject.layer == LayerMask.NameToLayer("BossDragon")) 
            {
                isBossAlive = true;
                return;
            }
        }
        isBossAlive = false;
    }

    private void GotoNextWave()
    {
        if (enemies.Count <= 0) 
        {
            wave++;
        }
    }

    private void SetWaveCount()
    {
        GameManager.wave = waveCount;
    }
}
