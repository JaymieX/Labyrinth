using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterCreateButtonHandler : MonoBehaviour
{
    public Text ErrorText;
    public InputField Name;
    public Dropdown StartingGear;

    public TraitsManager SceneTraitsManager;

    [System.Serializable]
    public struct TraitsToggles
    {
        public string ID;
        public Toggle TraitsToggle;
    }

    public TraitsToggles[] TraitsToggleList;

    private HashSet<string> _selectedTraits;

    private Queue<string> _errorQueue;

    void Awake()
    {
        _selectedTraits = new HashSet<string>();
        _errorQueue = new Queue<string>();
    }

    public void OnNextButtonClick()
    {
        // Clear error
        ErrorText.text = "";

        // Adds selected traits
        _selectedTraits.Clear();
        foreach (var trait in TraitsToggleList)
        {
            if (trait.TraitsToggle.isOn)
            {
                _selectedTraits.Add(trait.ID);
            }
        }

        // Too many traits
        if (_selectedTraits.Count > SceneTraitsManager.MaxCharTraitCount)
        {
            _errorQueue.Enqueue("Cannot have more than " + SceneTraitsManager.MaxCharTraitCount + " traits for one character\n");
        }
        else if (_selectedTraits.Count == 0)
        {
            _errorQueue.Enqueue("No traits picked\n");
        }
        else
        {
            // Adding traits
            if (AddTraits())
            {
                float walkSpeed = 3.0f;
                float health = 100.0f;
                float luckyP = 1.0f;
                float moneyRate = 1.0f;
                ICommand meleeCommand = new MeleeNTrait();
                ICommand rangeCommand = new RangeNTrait();

                foreach (var trait in _selectedTraits)
                {
                    switch (trait)
                    {
                        case "fast_runner":
                            walkSpeed = 6.0f;
                            break;
                        case "thick_skin":
                            health = 130.0f;
                            break;
                        case "lucky":
                            luckyP = 2.5f;
                            break;
                        case "melee_s":
                            meleeCommand = new MeleeSTrait();
                            break;
                        case "range_s":
                            rangeCommand = new RangeSTrait();
                            break;
                        case "greedy":
                            moneyRate = 3.0f;
                            break;
                    }
                }

                // Setup player
                PlayerManager.Instance.PlayerBehaviours[0] =
                    new PlayerBehaviour(Name.text, walkSpeed, health, luckyP, moneyRate, meleeCommand, rangeCommand);
                PlayerManager.Instance.Gears += Convert.ToUInt16(StartingGear.itemText);

                // Switch game
                SceneManager.LoadScene("GameScene");
            }
        }

        // Output errors if any
        while (_errorQueue.Count != 0)
        {
            ErrorText.text += _errorQueue.Dequeue();
        }

    }

    private bool AddTraits()
    {
        HashSet<string> localTraitPool = new HashSet<string> (SceneTraitsManager.GlobalTraitPool);

        // Check if there are any conflicts
        foreach (var trait in _selectedTraits)
        {
            var traitItem = SceneTraitsManager.TraitList[trait]; // Fetch the trait property

            // This trait has been locally forbid
            if (!localTraitPool.Contains(trait))
            {
                _errorQueue.Enqueue("Character trait " + SceneTraitsManager.TraitTranslator[trait] +
                                    " conflict with trait " + SceneTraitsManager.TraitTranslator[traitItem.LocalForbid] +
                                    " on this character!\n");
                return false;
            }

            // This trait has been globally forbid
            if (!SceneTraitsManager.GlobalTraitPool.Contains(trait))
            {
                _errorQueue.Enqueue("Character trait " + SceneTraitsManager.TraitTranslator[trait] +
                                    " conflict with trait " + SceneTraitsManager.TraitTranslator[traitItem.GlobalForbid] +
                                    " on another character!\n");

                return false;
            }

            localTraitPool.Remove(traitItem.LocalForbid);
        }

        // Actually adding traits
        foreach (var trait in _selectedTraits)
        {
        }

        return true;
    }
}
