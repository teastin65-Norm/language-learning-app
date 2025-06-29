using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the game flow for requesting and selecting items.
/// </summary>
public class GameManager : MonoBehaviour
{
    // List of items that the player can select. These can be set in the Inspector.
    [SerializeField]
    private List<string> items = new List<string> { "apple", "ball", "shirt" };

    // UI Text element that displays the current request.
    [SerializeField]
    private Text promptText;

    // Counts for correct and incorrect selections.
    private int correctCount = 0;
    private int incorrectCount = 0;

    // The item the player is currently being asked for.
    private string currentItem;

    private void Start()
    {
        // Start the game by showing the first item request.
        ShowNextItem();
    }

    /// <summary>
    /// Called by ItemButton when the player selects an item.
    /// </summary>
    /// <param name="itemName">The name of the item the player clicked.</param>
    public void CheckSelection(string itemName)
    {
        if (string.Equals(itemName, currentItem))
        {
            // Player chose correctly.
            correctCount++;
            // Remove the item so it won't be asked again.
            items.Remove(currentItem);
            ShowNextItem();
        }
        else
        {
            // Player chose incorrectly.
            incorrectCount++;
        }
    }

    /// <summary>
    /// Selects a new item at random and updates the UI. If no items remain, clears the prompt.
    /// </summary>
    private void ShowNextItem()
    {
        if (items.Count == 0)
        {
            // No more items to request.
            currentItem = string.Empty;
            if (promptText != null)
            {
                promptText.text = string.Empty;
            }
            return;
        }

        // Choose a random item from the remaining list.
        currentItem = items[Random.Range(0, items.Count)];

        // Update the UI text to ask for the item.
        if (promptText != null)
        {
            promptText.text = $"Can I have the {currentItem}?";
        }
    }
}
