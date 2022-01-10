using System;
using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DialogueStarter : MonoBehaviour
{
    private Story _story;
    [SerializeField] private TextAsset inkJSONAsset;

    private void Awake()
    {
        _story = new Story(inkJSONAsset.text);
    }

    private void OnMouseDown()
    {
        var REMOVE_THIS = FindObjectOfType<DialogueText>();
        if (_story.canContinue)
        {
            REMOVE_THIS.DisplayText(_story.Continue());
        }
    }
}