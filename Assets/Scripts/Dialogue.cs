using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textDelay;

    private int _index;
    
    // Start is called before the first frame update
    private void Start()
    {
        textComponent.text = "";
        StartDialogue();
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void StartDialogue()
    {
        _index = 0;
        StartCoroutine(TypeLine());
    }

    private IEnumerator TypeLine()
    {
        foreach (var c in lines[_index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textDelay);
        }
    }
}
