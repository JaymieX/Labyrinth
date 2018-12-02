using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronGearController : MonoBehaviour
{
    public ushort amount;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.RotateAround(transform.position, transform.up, Time.deltaTime * 40f);
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.tag == "Player")
        //{
        //    PlayerManager.Instance.Gears += amount;
        //    Destroy(this.gameObject);
        //}
    }
}
