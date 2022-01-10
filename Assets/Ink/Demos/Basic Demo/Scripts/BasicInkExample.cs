using System;
using Ink.Runtime;
using UnityEngine;
using UnityEngine.UI;

// This is a super bare bones example of how to play and display a ink story in Unity.
public class BasicInkExample : MonoBehaviour
{
    [SerializeField] private TextAsset inkJSONAsset;

    [SerializeField] private Canvas canvas;

    // UI Prefabs
    [SerializeField] private Text textPrefab;
    [SerializeField] private Button buttonPrefab;
    public Story story;

    private void Awake()
    {
        // Remove the default message
        RemoveChildren();
        StartStory();
    }

    public static event Action<Story> OnCreateStory;

    // Creates a new Story object with the compiled story which we can then play!
    private void StartStory()
    {
        story = new Story(inkJSONAsset.text);
        OnCreateStory?.Invoke(story);
        RefreshView();
    }

    // This is the main function called every time the story changes. It does a few things:
    // Destroys all the old content and choices.
    // Continues over all the lines of text, then displays all the choices. If there are no choices, the story is finished!
    private void RefreshView()
    {
        // Remove all the UI on screen
        RemoveChildren();

        // Read all the content until we can't continue any more
        while (story.canContinue)
        {
            // Continue gets the next line of the story
            var text = story.Continue();
            // This removes any white space from the text.
            text = text.Trim();
            // Display the text on screen!
            CreateContentView(text);
        }

        // Display all the choices, if there are any!
        if (story.currentChoices.Count > 0)
        {
            for (var i = 0; i < story.currentChoices.Count; i++)
            {
                var choice = story.currentChoices[i];
                var button = CreateChoiceView(choice.text.Trim());
                // Tell the button what to do when we press it
                button.onClick.AddListener(delegate { OnClickChoiceButton(choice); });
            }
        }
        // If we've read all the content and there's no choices, the story is finished!
        else
        {
            var choice = CreateChoiceView("End of story.\nRestart?");
            choice.onClick.AddListener(delegate { StartStory(); });
        }
    }

    // When we click the choice button, tell the story to choose that choice!
    private void OnClickChoiceButton(Choice choice)
    {
        story.ChooseChoiceIndex(choice.index);
        RefreshView();
    }

    // Creates a textbox showing the the line of text
    private void CreateContentView(string text)
    {
        var storyText = Instantiate(textPrefab, canvas.transform, false);
        storyText.text = text;
    }

    // Creates a button showing the choice text
    private Button CreateChoiceView(string text)
    {
        // Creates the button from a prefab
        var choice = Instantiate(buttonPrefab, canvas.transform, false);

        // Gets the text from the button prefab
        var choiceText = choice.GetComponentInChildren<Text>();
        choiceText.text = text;

        // Make the button expand to fit the text
        var layoutGroup = choice.GetComponent<HorizontalLayoutGroup>();
        layoutGroup.childForceExpandHeight = false;

        return choice;
    }

    // Destroys all the children of this gameobject (all the UI)
    private void RemoveChildren()
    {
        var childCount = canvas.transform.childCount;
        for (var i = childCount - 1; i >= 0; --i) Destroy(canvas.transform.GetChild(i).gameObject);
    }
}