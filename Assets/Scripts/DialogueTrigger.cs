using Ink.Runtime;
using UnityEngine;

/// <summary>
///     Triggers dialogue to start or continue based on user input.
///     Currently only starts dialogue upon being clicked.
///     All dialogue related user input should be handled by this class only.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class DialogueTrigger : MonoBehaviour
{
    // ReSharper disable once InconsistentNaming
    [SerializeField] private TextAsset inkJSONAsset;

    /// <summary>
    ///     Pressing this button skips to the next line of dialogue.
    /// </summary>
    [SerializeField] private KeyCode advanceKey;

    private Story _story;

    private DialogueDisplay display;

    private void Awake()
    {
        _story = new Story(inkJSONAsset.text);
    }

    private void Start()
    {
        /* Replace FindObjectOfType if you want triggers to link to specific UI elements rather than just the first one
         they can find in the scene. */
        var text = FindObjectOfType<DialogueText>();
        var buttons = FindObjectOfType<DialogueButtons>();
        display = new DialogueDisplay(buttons, text, _story);
    }

    private void Update()
    {
        if (Input.GetKeyDown(advanceKey)) display.DisplayNextLine();
    }

    private void OnMouseDown()
    {
        display.DisplayNextLine();
    }
}