using UnityEngine;

public class TrapController : MonoBehaviour
{
    public TrapAction Action;

    internal float Interval;

    void Start()
    {
        Interval = 0f;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            Action.OnTrapTriggered(other, this);
        }
    }
}
