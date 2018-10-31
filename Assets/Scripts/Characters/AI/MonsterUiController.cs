using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterUiController : MonoBehaviour
{
    private GameObject _uiCanvas;
    public GameObject UI;

    private GameObject _spawnedUI;
    // Use this for initialization
    void Start()
    {
        _uiCanvas = GameObject.FindWithTag("EnemyUICanvas");

        _spawnedUI = Instantiate(UI, Camera.main.WorldToScreenPoint(transform.position), Quaternion.identity);
        _spawnedUI.transform.SetParent(_uiCanvas.transform);
    }

    // Update is called once per frame
    void Update()
    {
        _spawnedUI.transform.position = Camera.main.WorldToScreenPoint(transform.position);
    }
}
