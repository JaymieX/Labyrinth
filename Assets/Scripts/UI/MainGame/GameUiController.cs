using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUiController : MonoBehaviour
{
    public Text GearCount;
    public Text OverheatNote;
    public Image HpBar;

    public PlayerController player;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //GearCount.text = PlayerManager.Instance.Gears + "";
        //AmmoCount.text = PlayerManager.Instance.CurrentAmmo + "/" + PlayerManager.Instance
        //                     .CurRangeWeaponInfo[PlayerManager.Instance.CurRangeWeaponId].MaxAmmo;

        HpBar.transform.localScale =
            new Vector3(player.health / 100.0f, 1f, 1f);
    }
}
