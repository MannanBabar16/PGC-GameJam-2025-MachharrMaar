using UnityEngine;
using System.Collections.Generic;

public class MosquitoSwarmManager : MonoBehaviour
{
    public static MosquitoSwarmManager instance;

    [Header("Swarm Settings")]
    public Transform player;
    public Vector3 biteOffset = new Vector3(0, 1.5f, 0.5f); // front of chest
    public float slotRadius = 0.7f;    // orbit radius around bite point
    public int slotCount = 6;          // how many slots around center

    private List<Transform> slots = new List<Transform>();
    private Mosquito occupant; // mosquito at bite point

    void Awake()
    {
        instance = this;
        CreateSlots();
    }

    void CreateSlots()
    {
        // Arrange slots in circle around bite point
        for (int i = 0; i < slotCount; i++)
        {
            GameObject slot = new GameObject("MosquitoSlot_" + i);
            slot.transform.parent = transform;
            slots.Add(slot.transform);
        }
    }

    public Vector3 GetBitePoint()
    {
        return player.position + player.forward * biteOffset.z + Vector3.up * biteOffset.y;
    }

    public Mosquito GetOccupant()
    {
        return occupant;
    }

    public void SetOccupant(Mosquito m)
    {
        occupant = m;
    }

    public Transform GetRandomSlot()
    {
        // Pick random position around bite point
        int i = Random.Range(0, slots.Count);
        float angle = (360f / slotCount) * i;
        Vector3 offset = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * slotRadius;

        slots[i].position = GetBitePoint() + offset;
        return slots[i];
    }
}