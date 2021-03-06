using System.Collections.Generic;
using Ink.Runtime;
using UnityEngine;

/// <summary>
///     TODO comments explaining wtf this is
/// </summary>
public class Dialogue
{
    private const string LINE_ENDER = ";;;\n";

    private const char SPEAKER_PROMPT = ':';
    private readonly Story _story;

    public Dialogue(TextAsset json)
    {
        _story = new Story(json.text);
    }

    public bool canContinue => _story.canContinue;

    public List<Choice> currentChoices => _story.currentChoices;

    public string speaker { get; private set; }

    public string currentText { get; private set; }

    public void ChooseChoice(Choice choice)
    {
        _story.ChooseChoiceIndex(choice.index);
    }

    public string Continue()
    {
        currentText = speaker = "";
        while (!currentText.EndsWith(LINE_ENDER)) currentText += _story.Continue().TrimStart();

        var speakerIdx = currentText.IndexOf(SPEAKER_PROMPT);
        speaker = currentText[..speakerIdx];
        currentText = currentText[(speakerIdx + 2)..^4];
        return currentText;
    }
}