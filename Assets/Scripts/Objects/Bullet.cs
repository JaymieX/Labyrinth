using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * 1500f);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().RemoveHealth(0.8f);
        }

        Destroy(this.gameObject);
    }
}