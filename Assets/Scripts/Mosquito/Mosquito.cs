using UnityEngine;

public class Mosquito : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 2f;
    public float hoverSpeed = 1.5f;
    public float buzzAmplitude = 0.2f;
    public float buzzFrequency = 5f;

    private Transform player;
    private MosquitoSwarmManager swarm;
    private Vector3 target;
    private bool isOccupant;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        swarm = MosquitoSwarmManager.instance;

        // Start by heading to a random slot
        target = swarm.GetRandomSlot().position;
    }

    void Update()
    {
        if (player == null) return;

        Vector3 bitePoint = swarm.GetBitePoint();

        // Check if bite point is free
        if (swarm.GetOccupant() == null && !isOccupant)
        {
            // Take over bite point
            swarm.SetOccupant(this);
            isOccupant = true;
        }

        if (isOccupant)
        {
            // Move into center bite point
            target = bitePoint;
        }
        else
        {
            // Circle around in slot
            if (Vector3.Distance(transform.position, target) < 0.2f)
            {
                target = swarm.GetRandomSlot().position;
            }
        }

        // Buzzing wiggle
        Vector3 buzz = new Vector3(
            Mathf.Sin(Time.time * buzzFrequency) * buzzAmplitude,
            Mathf.Cos(Time.time * buzzFrequency) * buzzAmplitude,
            0f
        );

        // Smooth movement
        float speed = isOccupant ? hoverSpeed : moveSpeed;
        transform.position = Vector3.Lerp(transform.position, target + buzz, speed * Time.deltaTime);

        // Look at player (but smoothly)
        Vector3 dir = (bitePoint - transform.position).normalized;
        if (dir.magnitude > 0.01f)
        {
            Quaternion lookRot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, 5f * Time.deltaTime);
        }
    }

    void OnDestroy()
    {
        // Free bite slot if killed
        if (isOccupant && swarm != null && swarm.GetOccupant() == this)
        {
            swarm.SetOccupant(null);
        }
    }
}
