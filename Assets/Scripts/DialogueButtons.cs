using Ink.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///     Creates and destroys buttons used for dialogue choices.
/// </summary>
[RequireComponent(typeof(LayoutGroup))]
public class DialogueButtons : MonoBehaviour
{
    [SerializeField] private Button buttonPrefab;

    public void CreateButton(DialogueSource source, Choice choice)
    {
        var button = Instantiate(buttonPrefab, transform);
        button.onClick.AddListener(delegate
        {
            source.story.ChooseChoiceIndex(choice.index);
            source.DisplayNextLine();
        });
        button.GetComponentInChildren<TextMeshProUGUI>().text = choice.text;
    }

    public void ClearButtons()
    {
        foreach (Transform child in transform) Destroy(child.gameObject);
    }
}