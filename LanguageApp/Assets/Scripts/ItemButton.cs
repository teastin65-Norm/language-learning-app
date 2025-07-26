using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{
    [SerializeField] private string itemName;
    private Button button;
    private GameManager gameManager;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClicked);

        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("GameManager not found in the scene.");
        }
    }

    public void OnButtonClicked()
    {
        if (gameManager != null)
        {
            Debug.Log($"Button clicked: {itemName}");
            gameManager.CheckSelection(itemName);
        }
        else
        {
            Debug.LogError("GameManager is null in ItemButton");
        }
    }

}



