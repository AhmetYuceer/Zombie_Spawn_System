using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ZombieSpawnManager : MonoBehaviour
{
    [Header("Control Panel")]
    [SerializeField] private bool randomPos;
    [SerializeField] private bool specificPosition;

    [Header("Player")]
    [SerializeField] private GameObject Player;

    [Header("Zombie Spawn Specific Position")]
    [SerializeField] private List<GameObject> zombieList = new List<GameObject>();
    [SerializeField] private List<Transform> zombieSpawnPosList = new List<Transform>();

    [Header("Zombie Spawn Random Position")]
    [SerializeField] private float spawnTime;
    [SerializeField] private int maxZombieCount;
    [SerializeField] private float maxZombieSpawnDistance;
    [SerializeField] private float minZombieSpawnDistance;
    [SerializeField] private float spawnDistance;
    public int spawnnedZombieCount;    

    private void Start() 
    {
        if (specificPosition)
        {
            SpawnZombieSpecificPosition();
        }
        if(randomPos)
        {
            StartCoroutine(spawnZombie());
        }
    }

    private void SpawnZombieSpecificPosition()
    {
        for (int i = 0; i < zombieSpawnPosList.Count; i++)
        {
            int zombieIndex = Random.Range(0,zombieList.Count);
            var spawnZombiePos = zombieSpawnPosList[i].position;
            GameObject spawnedZombie = Instantiate(zombieList[zombieIndex],spawnZombiePos,Quaternion.identity);
        }
    }
   
    private void SpawnZombieRandomPosition()
    {
        var playerPos = Player.transform.position;
        var posX = Random.Range(minZombieSpawnDistance,maxZombieSpawnDistance);
        var posY = playerPos.y;
        var posZ = Random.Range(minZombieSpawnDistance,maxZombieSpawnDistance);

        if (posX < 0)
        {
            posX += -spawnDistance;
        }
        else
        {
            posX += spawnDistance;
        }
        if (posZ < 0)
        {
            posZ += -spawnDistance;
        }
        else
        {
            posZ += spawnDistance;
        }

        Vector3 spawnPos = new Vector3(posX,posY,posZ);  
        var index = Random.Range(0,zombieList.Count);
        GameObject zombie = Instantiate(zombieList[index],spawnPos,Quaternion.identity);
        zombie.tag = "zombie";
        zombie.layer = LayerMask.NameToLayer("zombie");
        spawnnedZombieCount++;   
    }

    IEnumerator spawnZombie()
    {
        while (spawnnedZombieCount < maxZombieCount)
        {
            SpawnZombieRandomPosition();
            yield return new WaitForSeconds(spawnTime);  
        }
    }
}