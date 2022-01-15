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

    [SerializeField] private DialogueDisplay display;

    private void Update()
    {
        if (Input.GetKeyDown(advanceKey) && display.dialogueActive) display.DisplayNextLine();
    }

    private void OnMouseDown()
    {
        if (display.dialogueActive) return;
        display.dialogue = new Dialogue(inkJSONAsset);
        display.DisplayNextLine();
    }
}