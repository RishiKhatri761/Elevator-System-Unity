using UnityEngine;

public class ElevatorManager : MonoBehaviour
{
    public ElevatorController[] elevators;

    public void RequestFloor(int floor)
    {
        ElevatorController best = null;
        int bestDist = int.MaxValue;

        // First pass — prefer idle elevators
        foreach (var e in elevators)
        {
            if (!e.IsIdle) continue;
            int dist = Mathf.Abs(e.CurrentFloor - floor);
            if (dist < bestDist)
            {
                bestDist = dist;
                best = e;
            }
        }

        // Second pass — if none idle, pick closest regardless
        if (best == null)
        {
            bestDist = int.MaxValue;
            foreach (var e in elevators)
            {
                int dist = Mathf.Abs(e.CurrentFloor - floor);
                if (dist < bestDist)
                {
                    bestDist = dist;
                    best = e;
                }
            }
        }

        best?.AddRequest(floor);
    }
}