using Ink.Runtime;

/// <summary>
///     The interface between DialogueSource and DialogueText. Dialogue sources send Stories to the manager,
///     which then passes them to Dialogue texts for them to be displayed.
/// </summary>
public class DialogueManager : Singleton<DialogueManager>
{
    private DialogueButtons _dialogueButtons;
    private DialogueText _dialogueText;
    private Story _story;

    public Story story
    {
        set
        {
            _story = value;
            _dialogueText.DisplayLine(NextLine());
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        _dialogueText = FindObjectOfType<DialogueText>();
        _dialogueButtons = FindObjectOfType<DialogueButtons>();
    }

    public string NextLine()
    {
        if (_story == null) return "";
        _dialogueButtons.ClearButtons();
        foreach (var choice in _story.currentChoices) _dialogueButtons.CreateButton(choice);
        return _story.canContinue ? _story.Continue() : _story.currentText;
    }

    public void Choose(Choice choice)
    {
        _story.ChooseChoiceIndex(choice.index);
    }
}