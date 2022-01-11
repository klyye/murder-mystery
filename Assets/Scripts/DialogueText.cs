using System.Collections;
using Ink.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
///     Displays the dialogue of a given ink story line by line.
/// </summary>
[RequireComponent(typeof(TextMeshProUGUI))]
public class DialogueText : MonoBehaviour
{
    /// <summary>
    ///     The number of seconds between each character being displayed.
    /// </summary>
    [SerializeField] private float textDelay;

    /// <summary>
    ///     Pressing this button skips to the next line of dialogue.
    /// </summary>
    [SerializeField]
    private KeyCode advanceKey;

    private Story _currentStory;

    private TextMeshProUGUI _text;

    /// <summary>
    ///     The coroutine that is currently typing text onto the screen.
    /// </summary>
    private IEnumerator _typeCoroutine;

    public Story CurrentStory
    {
        get => _currentStory;
        set
        {
            _currentStory = value;
            if (_currentStory.canContinue)
                DisplayLine(CurrentStory.Continue());
        }
    }

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
        _text.text = "";
    }

    private void Update()
    {
        if (Input.GetKeyDown(advanceKey) && CurrentStory.canContinue) DisplayLine(CurrentStory.Continue());
    }

    private void DisplayLine(string line)
    {
        if (_typeCoroutine != null)
            StopCoroutine(_typeCoroutine);
        _typeCoroutine = TypeText(line);
        StartCoroutine(_typeCoroutine);
    }

    private IEnumerator TypeText(string line)
    {
        _text.text = "";
        foreach (var c in line)
        {
            _text.text += c;
            yield return new WaitForSeconds(textDelay);
        }
    }
}