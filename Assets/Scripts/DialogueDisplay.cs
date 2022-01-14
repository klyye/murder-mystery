using Ink.Runtime;

/// <summary>
///     Sends lines of data to be displayed by DialogueText and DialogueButtons when prompted by DialogueTrigger.
///     It is the job of this class to interface with the UI elements.
/// </summary>
public class DialogueDisplay
{
    private readonly ButtonLayout _buttons;
    private readonly Dialogue _dialogue;
    private readonly TextPanel _dialoguePanel;
    private readonly TextPanel _speakerPanel;

    public DialogueDisplay(ButtonLayout buttons, TextPanel dialoguePanel, TextPanel speakerPanel, Dialogue dialogue)
    {
        _dialoguePanel = dialoguePanel;
        _buttons = buttons;
        _dialogue = dialogue;
        _speakerPanel = speakerPanel;
    }

    /// <summary>
    ///     Advances the story to the choice that is selected.
    ///     Should be triggered upon a choice button being pressed.
    /// </summary>
    /// <param name="choice">The choice that is selected.</param>
    private void OnChoiceSelected(Choice choice)
    {
        _dialogue.ChooseChoice(choice);
        DisplayNextLine();
    }


    /// <summary>
    ///     Signals the UI to display the next line of the story.
    /// </summary>
    public void DisplayNextLine()
    {
        if (_dialogue == null || !_dialogue.canContinue && _dialogue.currentChoices.Count == 0)
        {
            _dialoguePanel.gameObject.SetActive(false);
            return;
        }

        _buttons.ClearButtons();
        var output = _dialogue.canContinue && !_dialoguePanel.currentlyTyping
            ? _dialogue.Continue()
            : _dialogue.currentText;
        foreach (var choice in _dialogue.currentChoices)
            _buttons.CreateButton(delegate { OnChoiceSelected(choice); }, choice.text);

        _dialoguePanel.DisplayLine(output);
        _speakerPanel.DisplayLine(_dialogue.speaker);
    }
}