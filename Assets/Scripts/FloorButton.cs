using UnityEngine;
using UnityEngine.UI;

public class FloorButton : MonoBehaviour
{
    public ElevatorManager manager;
    public int floorIndex;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
            manager.RequestFloor(floorIndex));
    }
}