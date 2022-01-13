using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
///     Creates and destroys a layout of buttons.
///     This is a UI class and should not contain any dialogue logic.
/// </summary>
[RequireComponent(typeof(LayoutGroup))]
public class ButtonLayout : MonoBehaviour
{
    [SerializeField] private Button buttonPrefab;

    /// <summary>
    ///     Creates a button with text that calls a function on press.
    /// </summary>
    /// <param name="onPressed">Calls this function when the button is pressed.</param>
    /// <param name="text">The text on the button.</param>
    public void CreateButton(UnityAction onPressed, string text)
    {
        var button = Instantiate(buttonPrefab, transform);
        button.onClick.AddListener(onPressed);
        button.GetComponentInChildren<TextMeshProUGUI>().text = text;
    }

    public void ClearButtons()
    {
        foreach (Transform child in transform) Destroy(child.gameObject);
    }
}