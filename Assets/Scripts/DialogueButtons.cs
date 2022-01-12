using Ink.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///     Creates and destroys buttons used for dialogue choices.
///     This is a UI class and should not contain any dialogue logic.
/// </summary>
[RequireComponent(typeof(LayoutGroup))]
public class DialogueButtons : MonoBehaviour
{
    [SerializeField] private Button buttonPrefab;

    public void CreateButton(DialogueDisplay display, Choice choice)
    {
        var button = Instantiate(buttonPrefab, transform);
        button.onClick.AddListener(delegate
        {
            display.story.ChooseChoiceIndex(choice.index);
            display.DisplayNextLine();
        });
        button.GetComponentInChildren<TextMeshProUGUI>().text = choice.text;
    }

    public void ClearButtons()
    {
        foreach (Transform child in transform) Destroy(child.gameObject);
    }
}