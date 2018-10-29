using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUiController : MonoBehaviour
{
    public Text GearCount;
    public Image HpBar;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        GearCount.text = PlayerManager.Instance.Gears + "";

        HpBar.transform.localScale =
            new Vector3(PlayerManager.Instance.CurrentHealth / PlayerManager.Instance.GetCurrentBehaviour().BaseHealth, 1f, 1f);
            //SetSizeWithCurrentAnchors
            //    (
            //        RectTransform.Axis.Horizontal,
            //        PlayerManager.Instance.CurrentHealth * 3f
            //    );
    }
}
