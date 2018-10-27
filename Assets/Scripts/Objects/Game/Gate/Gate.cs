using System.Collections;
using UnityEngine;

public class Gate : MonoBehaviour
{
    /****************************************************
     *
     * Inspector related objects
     *
     ****************************************************/

    // GameObject Components
    private BoxCollider _collider;

    // Child GameObjects
    private GameObject _gateDoor;

    // Use this for initialization
    private void Start()
    {
        /****************************************************
         *
         * Inspector related objects init
         *
         ****************************************************/

        // GameObject Components
        _collider = GetComponent<BoxCollider>();

        // Child GameObjects
        _gateDoor = gameObject.transform.GetChild(1).gameObject;

    }

    private void OnTriggerEnter(Collider collision)
    {
        // Subscribe to event if player is in gate range
        if (collision.gameObject.tag == "Player")
        {
            PlayerManager.Instance.OnPlayerOpenInteract = OnGateOpens;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        // Unsubscribe to event if player is out of gate range
        if (collision.gameObject.tag == "Player")
        {
            PlayerManager.Instance.OnPlayerOpenInteract = null;
        }
    }

    /****************************************************
     *
     * Delegated functions
     *
     ****************************************************/

    private void OnGateOpens()
    {
        StartCoroutine("HandleGateOpen");
    }

    /****************************************************
     *
     * Gate Co-routines
     *
     ****************************************************/

    private IEnumerator HandleGateOpen()
    {
        // Begin lowering the gate
        yield return StartCoroutine("LowerGate");

        // Destroy gate
        Destroy(_gateDoor);

        // Initiate next round

        // Destroy script
        PlayerManager.Instance.OnPlayerOpenInteract = null;
        Destroy(this);
    }

    private IEnumerator LowerGate()
    {
        // While gate is still above ground
        while (_gateDoor.transform.localPosition.y > -4.8f)
        {
            // Lowering the gate
            _gateDoor.transform.position += Vector3.down * 0.3f;

            // Wait
            yield return new WaitForSeconds(0.2f);
        }
    }
}
