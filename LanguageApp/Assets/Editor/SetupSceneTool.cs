using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class SetupSceneTool : MonoBehaviour
{
    [MenuItem("Tools/Setup Receptive Language Scene")]
    public static void SetupScene()
    {
        // Create Canvas if it doesn't exist
        Canvas canvas = FindObjectOfType<Canvas>();
        if (canvas == null)
        {
            GameObject canvasGO = new GameObject("Canvas");
            canvas = canvasGO.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasGO.AddComponent<CanvasScaler>();
            canvasGO.AddComponent<GraphicRaycaster>();
        }

        // Create PromptText
        GameObject textGO = new GameObject("PromptText");
        textGO.transform.SetParent(canvas.transform);
        Text promptText = textGO.AddComponent<Text>();
        promptText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        promptText.text = "Prompt will appear here";
        promptText.alignment = TextAnchor.MiddleCenter;
        promptText.fontSize = 36;
        RectTransform textRT = textGO.GetComponent<RectTransform>();
        textRT.sizeDelta = new Vector2(600, 100);
        textRT.anchoredPosition = new Vector2(0, 200);

        // Create GameManager
        GameObject gmGO = new GameObject("GameManager");
        GameManager gm = gmGO.AddComponent<GameManager>();

        // Link PromptText to GameManager
        gm.promptText = promptText;

        // Select GameManager in Hierarchy
        Selection.activeGameObject = gmGO;

        Debug.Log("✅ Scene setup complete: Canvas, PromptText, GameManager created and linked.");
    }
}

