using System;
using UnityEngine;

[Serializable]
public struct EffectObject
{
    public string Name;
    public GameObject Effect;
}

public class EffectBank : MonoBehaviour
{
    public EffectObject[] Effects;

    public static EffectBank Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public GameObject GetEffect(string id)
    {
        foreach (var effectObject in Effects)
        {
            if (effectObject.Name == id) return effectObject.Effect;
        }

        return null;
    }
}
