using System.Collections;
using TMPro;
using UnityEngine;

/// <summary>
///     Displays dialogue line by line.
///     This is a UI class and should not contain any dialogue logic.
/// </summary>
[RequireComponent(typeof(TextMeshProUGUI))]
public class DialogueText : MonoBehaviour
{
    /// <summary>
    ///     The number of seconds between each character being displayed.
    /// </summary>
    [SerializeField] private float textDelay;

    private TextMeshProUGUI _text;

    /// <summary>
    ///     The coroutine that is currently typing text onto the screen.
    /// </summary>
    private IEnumerator _typeCoroutine;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
        _text.text = "";
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