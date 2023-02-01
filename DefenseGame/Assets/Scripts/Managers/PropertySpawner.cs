using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropertySpawner : MonoBehaviour
{
    public Resource[] resourcePrefabs;

    private List<Resource> resources = new List<Resource>();

    private static PropertySpawner m_instance;
    public static PropertySpawner instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<PropertySpawner>();
            }

            return m_instance;
        }
    }

    private void Awake()
    {
        // 씬에 싱글톤 오브젝트가 된 다른 GameManager 오브젝트가 있다면
        if (instance != this)
        {
            // 자신을 파괴
            Destroy(gameObject);
        }
    }

    public int GetResourcesCount()
    {
        if (resources != null)
        {
            return resources.Count;

        }

        return 0;
    }

    public List<Resource> GetResources()
    {
        return resources;
    }

    public Vector2[] GetResourcesPosition()
    {
        if (resources == null)
        {
            return null;
        }

        Vector2[] pos = new Vector2[resources.Count];

        for (int i = 0; i < resources.Count; i++)
        {
            pos[i] = resources[i].transform.position;
        }

        return pos;
    }

    public void CreateCorp(int type, Vector2 pos)
    {

        // 적 프리팹으로부터 적 생성
        Resource resource = Instantiate(resourcePrefabs[(int)type], pos, Quaternion.identity);

        // 생성된 적을 리스트에 추가
        resources.Add(resource);

        // 적의 onDeath 이벤트에 익명 메서드 등록
        // 사망한 적을 리스트에서 제거
        resource.onComplete += () => resources.Remove(resource);
        resource.onComplete += () => Destroy(resource.gameObject);
    }
}
