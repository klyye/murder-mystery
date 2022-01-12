using Ink.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using manager = DialogueManager;

[RequireComponent(typeof(LayoutGroup))]
public class DialogueButtons : Singleton<DialogueButtons>
{
    [SerializeField] private Button buttonPrefab;

    public void CreateButton(Choice choice)
    {
        var button = Instantiate(buttonPrefab, transform);
        button.onClick.AddListener(delegate { manager.inst.Choose(choice); });
        button.GetComponentInChildren<TextMeshProUGUI>().text = choice.text;
    }

    public void ClearButtons()
    {
        foreach (Transform child in transform) Destroy(child.gameObject);
    }
}