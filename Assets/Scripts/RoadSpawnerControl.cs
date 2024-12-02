using UnityEngine;
using System.Collections;
using System.Threading.Tasks;

/**
*This component spawns the given object at fixed time ntervals at its object position
*/

public class RoadSpawnerControl : MonoBehaviour
{
    [SerializeField] GameObject prefabToSpawn;
    [SerializeField] float velocityOfSpawnedObject;
    [SerializeField] float secondsBetweenSpawns = 1f;
    void Start()
    {
        SpawnRoutine();
        Debug.Log("Start finished");
    }

    async void SpawnRoutine()
    {
        while (true)
        {
            GameObject newObject = Instantiate(prefabToSpawn.gameObject, transform.position, Quaternion.identity);
            newObject.GetComponent<RoadControl>().velocity = velocityOfSpawnedObject;
            await Awaitable.WaitForSecondsAsync(secondsBetweenSpawns);
       }
    }
}
