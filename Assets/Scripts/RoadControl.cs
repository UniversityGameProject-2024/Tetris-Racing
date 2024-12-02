using UnityEngine;
/**
*This component moves its object in a fixed velocity
*NOTE: velocity is defined as speed+direction
*speed is a number; velocity is a vector
*/

public class RoadControl : MonoBehaviour
{
    [Tooltip("Movement vector in meters per second")]
    [SerializeField] public float velocity;

    void Start()
    {
        Destroy(gameObject, 8f);
    }

    void Update()
    {
        transform.position += Vector3.down * velocity * Time.deltaTime;
    }

}
