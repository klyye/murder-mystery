using Ink.Runtime;
using UnityEngine;
using manager = DialogueManager;

/// <summary>
///     Starts a dialogue upon being triggered.
///     Currently, can only be triggered upon mouse click.
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
        manager.inst.story = _story;
    }

}