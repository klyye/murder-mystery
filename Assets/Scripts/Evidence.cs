using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Evidence : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnMouseDown()
    {
        print(gameObject.name + " clicked!");
    }
}