using System;
using Ink.Runtime;

/// <summary>
///     Sends lines of data to be displayed by DialogueText and DialogueButtons when prompted by DialogueTrigger.
///     It is the job of this class to interface with the UI elements.
///     TODO: detect which character is talking. should probably make a new wrapper class for Story
///     TODO: https://github.com/inkle/ink/blob/master/Documentation/RunningYourInk.md#engine-usage-and-philosophy
/// </summary>
public class DialogueDisplay
{
    private readonly ButtonLayout _buttons;
    private readonly Dialogue _dialogue;
    private readonly TextPanel _panel;

    public DialogueDisplay(ButtonLayout buttons, TextPanel panel, Dialogue dialogue)
    {
        _panel = panel;
        _buttons = buttons;
        _dialogue = dialogue;
        DisplayNextLine();
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
            _panel.gameObject.SetActive(false);
            return;
        }

        _buttons.ClearButtons();
        var output = _dialogue.canContinue && !_panel.currentlyTyping ? _dialogue.Continue() : _dialogue.currentText;
        foreach (var choice in _dialogue.currentChoices)
            _buttons.CreateButton(delegate { OnChoiceSelected(choice); }, choice.text);
        if (_panel.currentlyTyping)
            _panel.textDelay = 0;
        else
            _panel.DisplayLine(output);
        Console.WriteLine(_dialogue.speaker);
    }
}