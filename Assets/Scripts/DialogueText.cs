using System.Collections;
using TMPro;
using UnityEngine;
using manager = DialogueManager;

/// <summary>
///     Displays dialogue line by line.
/// </summary>
[RequireComponent(typeof(TextMeshProUGUI))]
public class DialogueText : Singleton<DialogueText>
{
    /// <summary>
    ///     The number of seconds between each character being displayed.
    /// </summary>
    [SerializeField] private float textDelay;

    /// <summary>
    ///     Pressing this button skips to the next line of dialogue.
    /// </summary>
    [SerializeField] private KeyCode advanceKey;

    private TextMeshProUGUI _text;

    /// <summary>
    ///     The coroutine that is currently typing text onto the screen.
    /// </summary>
    private IEnumerator _typeCoroutine;

    protected override void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
        _text.text = "";
    }

    private void Update()
    {
        if (Input.GetKeyDown(advanceKey)) DisplayLine(manager.inst.NextLine());
    }

    public void DisplayLine(string line)
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