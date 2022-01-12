using Ink.Runtime;
using UnityEngine;

/// <summary>
///     The interface between DialogueSource and DialogueText. Dialogue sources send Stories to the manager,
///     which then passes them to Dialogue texts for them to be displayed.
/// </summary>
public class DialogueManager : Singleton<DialogueManager>
{
    private DialogueText _dialogueText;
    private Story _story;

    public Story story
    {
        get => _story;
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
    }

    public string NextLine()
    {
        return _story.canContinue ? _story.Continue() : _story.currentText;
    }
}