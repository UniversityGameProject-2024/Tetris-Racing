using UnityEngine;
using System.Collections;
using System.Threading.Tasks;

/**
*This component spawns the given object at fixed time ntervals at its object position
*/

public class EnemySpawnerControl : MonoBehaviour
{
    [SerializeField] GameObject prefabToSpawn;
    [SerializeField] float minVelocityOfSpawnedObject;
    [SerializeField] float secondsBetweenSpawns = 5f;

    
    const int MIN_OFFSET_X = -5;
    const int MAX_OFFSET_X = 6;

    const int MIN_OFFSET_VELOCITY = 0;
    const int MAX_OFFSET_VELOCITY = 4;

    void Start()
    {
        SpawnRoutine();
        Debug.Log("Start finished");
    }

    async void SpawnRoutine()
    {
        while (true)
        {
            // Generates a random integer in the range -5 to +5
            int randomOffsetX = Random.Range(MIN_OFFSET_X, MAX_OFFSET_X);

            Vector3 prefabPos = new Vector3(transform.position.x + randomOffsetX, transform.position.y, transform.position.z);
            GameObject newObject = Instantiate(prefabToSpawn.gameObject, prefabPos, Quaternion.identity);

            int randomOffsetVelocity = Random.Range(MIN_OFFSET_VELOCITY, MAX_OFFSET_VELOCITY);
            newObject.GetComponent<EnemyControl>().velocity = minVelocityOfSpawnedObject + randomOffsetVelocity;
            await Awaitable.WaitForSecondsAsync(secondsBetweenSpawns);
     
       }
    }
}
