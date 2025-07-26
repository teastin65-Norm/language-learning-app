// GameManager.cs
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] private TextMeshProUGUI promptText;
    [SerializeField] private GameObject[] itemButtons; // assign in Inspector
    private string currentItem;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private string currentRequest;

    private List<string> items = new List<string> { "apple", "ball", "shoe", "cell phone" };

    private void Start()
    {
        ShowNextItem();
    }


    public void StartRound()
    {
        int randomIndex = Random.Range(0, items.Count);
        currentRequest = items[randomIndex];
        promptText.text = $"Please give me the {currentRequest}.";
    }

    public bool CheckItem(string itemName)
    {
        return itemName == currentRequest;
    }

    public void CheckSelection(string itemName)
    {
        if (itemName == currentItem)
        {
            Debug.Log($"Correct! You gave the {itemName}.");
            // You could increment correct count here.
        }
        else
        {
            Debug.Log($"Incorrect. The prompt was {currentItem}, but you selected {itemName}.");
            // You could increment incorrect count here.
        }

        ShowNextItem(); // Automatically move to next prompt for demo simplicity
    }

    public void ShowNextItem()
    {
        if (items.Count == 0)
        {
            promptText.text = "All items have been selected!";
            return;
        }

        // Get a random item
        int index = Random.Range(0, items.Count);
        currentItem = items[index];

        // Update the prompt text
        promptText.text = $"Please give me the {currentItem}.";
    }


}
