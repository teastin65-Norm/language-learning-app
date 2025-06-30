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

    private void OnButtonClicked()
    {
        if (gameManager != null)
        {
            gameManager.CheckSelection(itemName);
        }
    }
}

