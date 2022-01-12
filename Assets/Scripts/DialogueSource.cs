using Ink.Runtime;

/// <summary>
///     Sends lines of data to be displayed by DialogueText and DialogueButtons when prompted by DialogueTrigger.
/// </summary>
public class DialogueSource
{
    private readonly DialogueButtons _buttons;
    private readonly DialogueText _text;

    public DialogueSource(DialogueButtons buttons, DialogueText text, Story story)
    {
        _text = text;
        _buttons = buttons;
        this.story = story;
        DisplayNextLine();
    }

    public Story story { get; }

    public void DisplayNextLine()
    {
        if (story == null) return;
        _buttons.ClearButtons();
        var output = story.canContinue ? story.Continue() : story.currentText;
        foreach (var choice in story.currentChoices) _buttons.CreateButton(this, choice);
        _text.DisplayLine(output);
    }
}