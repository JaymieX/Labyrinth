using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterUiController : MonoBehaviour
{
    public Text Name;
    public Image HealthBar;
    private MonsterStateController _msc;

    // Use this for initialization
    void Start()
    {
        // Camera
        GetComponent<Canvas>().worldCamera = Camera.main;

        // Monster info
        _msc = transform.parent.GetComponent<MonsterStateController>();

        // Set name
        Name.text = _msc.MInfo.MonsterName;
    }

    // Update is called once per frame
    void Update()
    {
        // Billboard
        transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward,
            Camera.main.transform.rotation * Vector3.up);

        // Update health
        HealthBar.fillAmount = _msc.Health / _msc.MInfo.Health;
    }
}
