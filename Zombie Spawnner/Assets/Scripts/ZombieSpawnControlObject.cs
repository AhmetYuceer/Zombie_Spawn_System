using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawnControlObject : MonoBehaviour
{
    private ZombieSpawnManager spawnManager;
    
    private void Start() 
    {
        spawnManager = FindObjectOfType<ZombieSpawnManager>();    
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("wall") || other.gameObject.layer == LayerMask.NameToLayer("wall"))
        {   
            removeZombie();
        }
        else if(other.gameObject.CompareTag("zombie") || other.gameObject.layer == LayerMask.NameToLayer("zombie"))
        {
           removeZombie();
        }   
        else
        {
            Destroy(this.gameObject);       
        }
    }
    private void removeZombie()
    {
        Destroy(this.gameObject.transform.parent.gameObject);
        spawnManager.spawnnedZombieCount--;
    }
}