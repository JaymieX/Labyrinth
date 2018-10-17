using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickupable : MonoBehaviour
{
    public enum PickupType
    {
        Invincibility
    }

    public PickupType Type;
    public GameObject GraphicEffect;

    private ICommand _pickupAction;

    void Awake()
    {
        switch (Type)
        {
            case PickupType.Invincibility:
                _pickupAction = new InvincibilityPickup();
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _pickupAction.Execute();
            StartCoroutine("PlayEffect");
        }
    }

    IEnumerator PlayEffect()
    {
        GraphicEffect = Instantiate(GraphicEffect, new Vector3(transform.position.x, 0f, transform.position.z), Quaternion.identity);
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;

        yield return new WaitForSeconds(2f);
        Destroy(GraphicEffect);
        Destroy(this.gameObject);
    }
}
