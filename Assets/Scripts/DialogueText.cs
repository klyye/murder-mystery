using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class DialogueText : MonoBehaviour
{
    private TextMeshProUGUI _text;

    /// <summary>
    ///     The number of seconds between each character being displayed.
    /// </summary>
    [SerializeField] private float textDelay;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
        _text.text = "";
    }

    public void DisplayText(string text)
    {
        _text.text = "";
        StartCoroutine(TypeText(text));
    }

    private IEnumerator TypeText(string text)
    {
        foreach (var c in text)
        {
            _text.text += c;
            yield return new WaitForSeconds(textDelay);
        }
    }
}
