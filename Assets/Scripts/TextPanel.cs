using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///     Displays dialogue line by line.
///     This is a UI class and should not contain any dialogue logic.
/// </summary>
[RequireComponent(typeof(Image))]
public class TextPanel : MonoBehaviour
{
    /// <summary>
    ///     The number of seconds between each character being displayed.
    /// </summary>
    [HideInInspector] public float textDelay;

    /// <summary>
    ///     The default number of seconds between each character being displayed.
    /// </summary>
    [SerializeField] private float defaultTextDelay;

    /// <summary>
    ///     Whether or text is still being typed character by character onto the screen.
    /// </summary>
    [HideInInspector] public bool currentlyTyping;

    private TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponentInChildren<TextMeshProUGUI>();
        textDelay = defaultTextDelay;
        _text.text = "";
        currentlyTyping = false;
    }

    /// <summary>
    ///     Displays the given string in the panel.
    /// </summary>
    /// <param name="line">The string to display.</param>
    public void DisplayLine(string line)
    {
        StartCoroutine(TypeText(line));
    }

    private IEnumerator TypeText(string line)
    {
        if (_text.text.Equals(line)) yield break; // prevents flickering

        currentlyTyping = true;
        _text.text = "";
        textDelay = defaultTextDelay;
        gameObject.SetActive(true);
        foreach (var c in line)
        {
            _text.text += c;
            yield return new WaitForSeconds(textDelay);
        }

        currentlyTyping = false;
    }
}