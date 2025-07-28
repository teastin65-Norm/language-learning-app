// GameManager.cs
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] private TextMeshProUGUI promptText;
    [SerializeField] private GameObject[] itemButtons; // assign in Inspector
    [SerializeField] private TextMeshProUGUI feedbackText;
    [SerializeField] private CanvasGroup feedbackCanvasGroup;
    private HashSet<string> correctItems = new HashSet<string>();

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

    private List<string> allItems = new List<string> { "apple", "ball", "shoe", "cell phone" };
    private List<string> items; // will be a copy of allItems per game
    
    private void Start()
    {
        StartRound(); // Initialize the game properly
    }

    //public string GetCurrentItem()
    //{
    //    return currentItem;
    //}

    public void StartRound()
    {
        items = new List<string>(allItems);  // fresh copy of all items
        correctItems.Clear();                // reset correct answer tracking
        ShowNextItem();                      // start first round
    }

    public bool CheckItem(string itemName)
    {
        return itemName == currentRequest;
    }

    public void CheckSelection(DraggableItem item)
    {
        string itemName = item.itemName;

        if (itemName == currentItem)
        {
            if (!correctItems.Contains(itemName))
            {
                correctItems.Add(itemName);
                items.Remove(itemName); // remove from future prompt pool
            }

            Debug.Log($"Correct! You gave me the {itemName}.");
            ShowFeedback($"Correct! You gave me the {itemName}.", true);
        }
        else
        {
            Debug.Log($"Incorrect. The prompt was {currentItem}, but you selected {itemName}.");
            ShowFeedback($"Try again! That was the {itemName}.", false);
            item.SnapBackToStart();
            return; // Don't proceed to next item
        }

        if (correctItems.Count == allItems.Count)
        {
            promptText.text = "You Win! Great job!";
            Debug.Log("Game over: All items were selected correctly.");
            return; // Game ends
        }

        // Continue to next prompt
        ShowNextItem();
    }

    private void ShowFeedback(string message, bool isCorrect)
    {
        feedbackText.color = isCorrect ? Color.green : Color.red;
        feedbackText.text = message;

        StopAllCoroutines(); // In case a fade is in progress
        StartCoroutine(FadeFeedback(isCorrect));
    }

    public void ShowNextItem()
    {
        if (items.Count == 0)
        {
            promptText.text = "You Win! Great job!";
            Debug.Log("Game over: All items were selected correctly.");
            return;
        }

        // Get a random item from remaining items
        int index = Random.Range(0, items.Count);
        currentItem = items[index];

        // Update the prompt text
        promptText.text = $"Please give me the {currentItem}.";
    }

    private IEnumerator FadeFeedback(bool isCorrect)
    {
        feedbackCanvasGroup.alpha = 1f;
        yield return new WaitForSeconds(1.5f);

        float duration = 0.5f;
        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;
            feedbackCanvasGroup.alpha = Mathf.Lerp(1f, 0f, t / duration);
            yield return null;
        }

        feedbackCanvasGroup.alpha = 0f;
    }
}
