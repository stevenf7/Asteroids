using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//This is a resource based spawner
public class Spawn : MonoBehaviour
{
    private float nextSpawnTime;

    [SerializeField]
    public GameObject[] prefabs;

    [SerializeField]
    private float spawnDelay = 10f;

    [SerializeField]
    private float LowerBoundPos = 15f;

    [SerializeField]
    private float UpperBoundPos = 25f;
    [SerializeField]
    private float Boundary = 5f;
    [SerializeField]
    private float GameUpdateFreq = 2;

    private float nextUpdateTime;

    private float view_angle_limiter = 10f;
    float interval = 1;

    [SerializeField]
    public GameObject Medkit;
    // Start is called before the first frame update
    private Vector3 Threshold = new Vector3();
    private Vector3 position = new Vector3();
    int secondsSinceGameStart;
    float speed = 0;

    int random_number = 0;
    // Update is called once per fram5
    private void Update()
    {

        if (ShouldSpawn())
        {
            SpawnObject();
        }
        if (ShouldUpdate())
        {
            SpawnUpdate();
        }
    }

    private void SpawnHealthPack()
    {
        do
        {
            position.Set(Random.Range(-UpperBoundPos, UpperBoundPos), Random.Range(0, UpperBoundPos), Random.Range(LowerBoundPos, UpperBoundPos));
        } while (Get_Distance(position) < 30 || Get_Distance(position) > 40);

        //spawn at the correct heading
        Threshold.Set(Random.Range(-Boundary, Boundary), Random.Range(-Boundary, Boundary), Random.Range(-Boundary, Boundary));
        var heading = Threshold - position;
        
        
        GameObject gameObject = Instantiate(Medkit, position, Random.rotation);
        Rigidbody thisrb = gameObject.GetComponent<Rigidbody>();
        thisrb.AddForce(heading * 1f, ForceMode.Impulse);
    }

    private void SpawnObject()
    {
        if (spawnDelay <= 0.5f)
        {
            spawnDelay = 0.5f;
        }
        nextSpawnTime = Time.time + spawnDelay;
        //spawn at correct distance away

        random_number = Random.Range(0, 20);
        if (random_number == 10)
        {
            SpawnHealthPack();
            return;
        }

        if (SceneManager.GetActiveScene().name == "Remastered") //if it's spacemode
        {
            do
            {
                position.Set(Random.Range(-UpperBoundPos, UpperBoundPos), Random.Range(-LowerBoundPos, UpperBoundPos), Random.Range(LowerBoundPos, UpperBoundPos));
            } while (Get_Distance(position) < 30 || Get_Distance(position) > 50);
        }
        else
        {
            do
            {
                position.Set(Random.Range(-UpperBoundPos/2, UpperBoundPos/2), Random.Range(0, UpperBoundPos/2), Random.Range(LowerBoundPos/2, UpperBoundPos/2));
            } while (Get_Distance(position) < 20 || Get_Distance(position) > 40);
        }

        //spawn at the correct heading
        Threshold.Set(Random.Range(-Boundary, Boundary), Random.Range(-Boundary, Boundary), Random.Range(-Boundary, Boundary));
        var heading = Threshold - position;
        speed = Random.Range(1, 5);
        speed = Speedupdate(speed);
        GameObject gameObject = Instantiate(prefabs[Random.Range(0, 2)], position, Random.rotation);
        Rigidbody thisrb = gameObject.GetComponent<Rigidbody>();
        thisrb.AddForce(heading * speed, ForceMode.Impulse);
    }
    private bool ShouldSpawn()
    {
        if (Time.time >= nextSpawnTime)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool ShouldUpdate()
    {
        if (Time.time >= nextUpdateTime)
        {
            nextUpdateTime = Time.time+ GameUpdateFreq;
            return true;
        }
        return false;
    }
    private float Get_Distance(Vector3 Position)
    {
        return Mathf.Sqrt(Mathf.Pow(Position.x, 2) + Mathf.Pow(Position.y, 2) + Mathf.Pow(Position.y, 2));
    }

    public static float _TimeSinceStart;
    private void Start()
    {
        gameObject.SetActive(true);
        _TimeSinceStart = Time.realtimeSinceStartup;
    }

    const float SPEED_INCREASE_INTERVAL = 10f;

    const float force_increase = 0.1f;

    private float Speedupdate(float startSpeed)
    {
        startSpeed += (0.05f);
        if (startSpeed > 7)
        {
            startSpeed = 7;
        }

        return startSpeed;
    }
    
    private void SpawnUpdate()
    {

        Boundary = Boundary - 0.05f;
        spawnDelay = spawnDelay -0.05f;

        if (spawnDelay <= 0.5f)
        {
            spawnDelay = 0.5f;
        }

        if (Boundary < 1)
        {
            Boundary = 1f;
        }
    }

    public void PauseSpawning()
    {
        gameObject.SetActive(false);
    }
}
