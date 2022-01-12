using Ink.Runtime;
using UnityEngine;

/// <summary>
///     Triggers dialogue to start or continue based on user input.
///     Currently only starts dialogue upon being clicked.
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

    private DialogueSource _source;
    private Story _story;

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
        _source = new DialogueSource(buttons, text, _story);
    }

    private void Update()
    {
        if (Input.GetKeyDown(advanceKey)) _source.DisplayNextLine();
    }

    private void OnMouseDown()
    {
        _source.DisplayNextLine();
    }
}