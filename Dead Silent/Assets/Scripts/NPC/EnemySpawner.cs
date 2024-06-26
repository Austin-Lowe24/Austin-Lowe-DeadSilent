using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Spawning;
    public List<PatrolWaypoint> PatrolPath;
    public bool SpawnOnFirstPoint;

    GameObject spawn;

    private void Awake()
    {
        if (SpawnOnFirstPoint && PatrolPath.Count > 0)
        {
            spawn = Instantiate(Spawning, PatrolPath[0].transform.position, transform.rotation);
        }
        else
        {
            spawn = Instantiate(Spawning, transform.position, transform.rotation);
        }

        EnemyAI ai = spawn.GetComponent<EnemyAI>();
        (Vector3 Position, int TimeInPosition)[] path = new (Vector3 Position, int TimeInPosition)[PatrolPath.Count];

        //Debug.Log(PatrolPath.Count);

        for (int i = 0; i < PatrolPath.Count; i++)
        {
            path[i] = new(PatrolPath[i].transform.position, PatrolPath[i].TimeInPosition);
        }

        ai.SetPatrolPath(path);
    }

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
