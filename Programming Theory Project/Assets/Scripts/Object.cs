using UnityEngine;

public class Object : MonoBehaviour, UIMainScene.IUIInfoContent
{
    [SerializeField] private string objectName; // Name of the object
    [TextArea]
    [SerializeField] private string description; // Description or properties of the object

    // Encapsulated properties
    public string ObjectName
    {
        get => objectName;
        set
        {
            if (value.Length > 50)
                throw new System.ArgumentException("Object name is too long!");
            objectName = value;
        }
    }

    public string Description
    {
        get => description;
        set
        {
            if (value.Length > 200)
                throw new System.ArgumentException("Description is too long!");
            description = value;
        }
    }

    // Implement IUIInfoContent methods
    public string GetName()
    {
        return ObjectName;
    }

    public string GetData()
    {
        return Description;
    }

    private void OnValidate()
    {
        if (string.IsNullOrWhiteSpace(objectName))
        {
            objectName = "New Object";
        }
    }
}
