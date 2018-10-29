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
}
