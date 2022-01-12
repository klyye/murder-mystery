using UnityEngine;

/// <summary>
///     Generic singleton. Inherit from this to make a singleton object.
///     https://gamedev.stackexchange.com/questions/184826/efficient-way-to-implement-a-lot-of-singletons
/// </summary>
/// <typeparam name="T">The type that we're making a singleton of.</typeparam>
public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    public static T inst { get; private set; }

    protected virtual void Awake()
    {
        if (inst != null)
        {
            Debug.LogErrorFormat(
                "Two copies of singleton {0} in the scene: ({1}, {2}). Please ensure only one is present.",
                typeof(T).FullName, inst.name, name);
            Destroy(gameObject);
            return;
        }

        inst = (T) this;
    }
}