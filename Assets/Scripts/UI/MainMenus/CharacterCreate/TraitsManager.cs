using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraitsManager : MonoBehaviour
{
    internal ushort MaxTraitCount = 6; // Total number of traits
    internal ushort MaxCharTraitCount = 3; // Total number of traits a character can have

    internal Dictionary<string, TraitEnforce> TraitList;
    internal HashSet<string> GlobalTraitPool; // Traits that are currently available

    internal Dictionary<string, string> TraitTranslator = new Dictionary<string, string>();
    void Awake()
    {
        // Adding all traits
        TraitList = new Dictionary<string, TraitEnforce>();
        TraitList["fast_runner"] = new TraitEnforce() { ID = "fast_runner", LocalForbid = null, GlobalForbid = "fast_runner" };
        TraitList["thick_skin"] = new TraitEnforce() { ID = "thick_skin", LocalForbid = null, GlobalForbid = "thick_skin" };
        TraitList["lucky"] = new TraitEnforce() { ID = "lucky", LocalForbid = "greedy", GlobalForbid = "lucky" };
        TraitList["melee_s"] = new TraitEnforce() { ID = "melee_s", LocalForbid = "range_s", GlobalForbid = null };
        TraitList["range_s"] = new TraitEnforce() { ID = "range_s", LocalForbid = "melee_s", GlobalForbid = null };
        TraitList["greedy"] = new TraitEnforce() { ID = "greedy", LocalForbid = "lucky", GlobalForbid = "greedy" };

        // Global pool
        GlobalTraitPool = new HashSet<string>();
        RenewGlobalPool();

        // Setting up translator
        TraitTranslator = new Dictionary<string, string>();
        TraitTranslator["fast_runner"] = "Fast Runner";
        TraitTranslator["thick_skin"] = "Thick Skin";
        TraitTranslator["lucky"] = "Lucky";
        TraitTranslator["melee_s"] = "Melee Specialist";
        TraitTranslator["range_s"] = "Range Specialist";
        TraitTranslator["greedy"] = "Greedy";
    }

    public void RenewGlobalPool()
    {
        foreach (var key in TraitList.Keys)
        {
            GlobalTraitPool.Add(key);
        }
    }
}
