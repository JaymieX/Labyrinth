using UnityEngine;

public class Pickupable : MonoBehaviour
{
    public PickupAction Action;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Action.Execute();
            Destroy(this.gameObject);
        }
    }

    void FixedUpdate()
    {
        transform.RotateAround(transform.position, transform.up, Time.deltaTime * 40f);
    }
}
