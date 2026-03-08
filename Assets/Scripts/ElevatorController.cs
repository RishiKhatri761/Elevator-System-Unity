using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ElevatorController : MonoBehaviour
{
    public float[] floorYPositions = { -3.48f, -0.98f, 1.52f, 4.02f };
    public float moveSpeed = 2f;
    public TextMeshProUGUI floorLabel;

    private Queue<int> queue = new Queue<int>();
    private int currentFloor = 0;
    private bool isMoving = false;
    private float targetY;

    public bool IsIdle => !isMoving && queue.Count == 0;
    public int CurrentFloor => currentFloor;

    // Each elevator runs its OWN Update — fully independent
    void Update()
    {
        if (!isMoving && queue.Count > 0)
        {
            int next = queue.Dequeue();
            if (next != currentFloor)
            {
                targetY = floorYPositions[next];
                currentFloor = next;
                isMoving = true;
            }
        }

        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                new Vector3(transform.position.x, targetY, transform.position.z),
                moveSpeed * Time.deltaTime
            );

            if (Mathf.Abs(transform.position.y - targetY) < 0.01f)
            {
                transform.position = new Vector3(transform.position.x, targetY, transform.position.z);
                isMoving = false;
                UpdateLabel();
            }
        }
    }

    public void AddRequest(int floor)
    {
        if (!queue.Contains(floor) && floor != currentFloor)
            queue.Enqueue(floor);
    }

    void UpdateLabel()
    {
        string[] names = { "G", "1", "2", "3" };
        if (floorLabel != null)
            floorLabel.text = names[currentFloor];
    }
}