using Ink.Runtime;
using TMPro;
using UnityEngine;

/// <summary>
///     Sends lines of data to be displayed by DialogueText and DialogueButtons when prompted by DialogueTrigger.
///     It is the job of this class to interface with the UI elements. 
/// </summary>
public class DialogueDisplay : MonoBehaviour
{
    [SerializeField] private ButtonLayout buttons;
    [SerializeField] private TypingTextBox dialogueBox;
    [SerializeField] private TextMeshProUGUI speakerText;
    public Dialogue dialogue;

    public bool dialogueActive => dialogueBox.isActiveAndEnabled;

    /// <summary>
    ///     Advances the story to the choice that is selected.
    ///     Should be triggered upon a choice button being pressed.
    /// </summary>
    /// <param name="choice">The choice that is selected.</param>
    private void OnChoiceSelected(Choice choice)
    {
        dialogue.ChooseChoice(choice);
        DisplayNextLine();
    }

    /// <summary>
    ///     Signals the UI to display the next line of the story.
    /// </summary>
    public void DisplayNextLine()
    {
        if (dialogue == null || !dialogue.canContinue && !dialogueBox.currentlyTyping &&
            dialogue.currentChoices.Count == 0)
        {
            dialogueBox.gameObject.SetActive(false);
            return;
        }

        buttons.ClearButtons();
        var output = dialogue.canContinue && !dialogueBox.currentlyTyping
            ? dialogue.Continue()
            : dialogue.currentText;
        foreach (var choice in dialogue.currentChoices)
            buttons.CreateButton(delegate { OnChoiceSelected(choice); }, choice.text);

        dialogueBox.DisplayLine(output);
        speakerText.text = dialogue.speaker;
    }
}