using Ink.Runtime;

/// <summary>
///     Sends lines of data to be displayed by DialogueText and DialogueButtons when prompted by DialogueTrigger.
///     It is the job of this class to interface with the UI elements.
/// </summary>
public class DialogueDisplay
{
    private readonly ButtonLayout _buttons;
    private readonly TextPanel _panel;
    private readonly Story _story;

    public DialogueDisplay(ButtonLayout buttons, TextPanel panel, Story story)
    {
        _panel = panel;
        _buttons = buttons;
        _story = story;
        DisplayNextLine();
    }

    /// <summary>
    ///     Advances the story to the choice that is selected.
    ///     Should be triggered upon a choice button being pressed.
    /// </summary>
    /// <param name="choice">The choice that is selected.</param>
    public void OnChoiceSelected(Choice choice)
    {
        _story.ChooseChoiceIndex(choice.index);
        DisplayNextLine();
    }


    /// <summary>
    ///     Signals the UI to display the next line of the story.
    /// </summary>
    public void DisplayNextLine()
    {
        if (_story == null || !_story.canContinue && _story.currentChoices.Count == 0)
        {
            _panel.Hide();
            return;
        }

        _buttons.ClearButtons();
        var output = _story.canContinue ? _story.Continue() : _story.currentText;

        foreach (var choice in _story.currentChoices)
            _buttons.CreateButton(delegate { OnChoiceSelected(choice); }, choice.text);
        _panel.DisplayLine(output);
    }
}