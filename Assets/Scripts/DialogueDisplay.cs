using Ink.Runtime;

/// <summary>
///     Sends lines of data to be displayed by DialogueText and DialogueButtons when prompted by DialogueTrigger.
///     It is the job of this class to interface with the UI elements.
/// </summary>
public class DialogueDisplay
{
    private readonly DialogueButtons _buttons;
    private readonly Story _story;
    private readonly DialoguePanel panel;

    public DialogueDisplay(DialogueButtons buttons, DialoguePanel panel, Story story)
    {
        this.panel = panel;
        _buttons = buttons;
        _story = story;
        DisplayNextLine();
    }

    public void OnChoiceSelected(Choice choice)
    {
        _story.ChooseChoiceIndex(choice.index);
        DisplayNextLine();
    }


    public void DisplayNextLine()
    {
        if (_story == null || !_story.canContinue && _story.currentChoices.Count == 0)
        {
            panel.Hide();
            return;
        }

        _buttons.ClearButtons();
        var output = _story.canContinue ? _story.Continue() : _story.currentText;
        foreach (var choice in _story.currentChoices) _buttons.CreateButton(this, choice);
        panel.DisplayLine(output);
    }
}