using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///     Displays strings with a character by character "typing" effect.
///     This is a UI class and should not contain any dialogue logic.
/// </summary>
[RequireComponent(typeof(Image))]
public class TypingTextBox : MonoBehaviour
{
    /// <summary>
    ///     The number of seconds between each character being displayed.
    /// </summary>
    [HideInInspector] public float textDelay;

    /// <summary>
    ///     The default number of seconds between each character being displayed.
    /// </summary>
    [SerializeField] private float defaultTextDelay;

    private TextMeshProUGUI _text;

    private IEnumerator _typingCoroutine;

    /// <summary>
    ///     Whether or text is still being typed character by character onto the screen.
    /// </summary>
    public bool currentlyTyping => _typingCoroutine != null;

    private void Awake()
    {
        _text = GetComponentInChildren<TextMeshProUGUI>();
        textDelay = defaultTextDelay;
        _text.text = "";
        gameObject.SetActive(false);
    }

    /// <summary>
    ///     Displays the given string in the panel.
    /// </summary>
    /// <param name="line">The string to display.</param>
    public void DisplayLine(string line)
    {
        if (currentlyTyping)
        {
            StopCoroutine(_typingCoroutine);
            _typingCoroutine = null;
            _text.text = line;
        }
        else
        {
            _typingCoroutine = TypeText(line);
            gameObject.SetActive(true);
            StartCoroutine(_typingCoroutine);
        }
    }

    private IEnumerator TypeText(string line)
    {
        _text.text = "";
        textDelay = defaultTextDelay;
        foreach (var c in line)
        {
            _text.text += c;
            yield return new WaitForSeconds(textDelay);
        }

        _typingCoroutine = null;
    }
}