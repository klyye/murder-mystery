using Ink.Runtime;
using UnityEngine;

/// <summary>
///     Starts a dialogue upon being triggered.
///     Currently, can only be triggered upon mouse click.
///     TODO: Move story logic into here
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class DialogueSource : MonoBehaviour
{
    // ReSharper disable once InconsistentNaming
    [SerializeField] private TextAsset inkJSONAsset;
    private Story _story;

    private void Awake()
    {
        _story = new Story(inkJSONAsset.text);
    }

    private void OnMouseDown()
    {
        StartDialogue();
    }

    private void StartDialogue()
    {
        var REMOVE_THIS = FindObjectOfType<DialogueText>();
        REMOVE_THIS.CurrentStory = _story;
    }
}