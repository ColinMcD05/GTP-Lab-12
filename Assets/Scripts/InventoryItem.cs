using UnityEngine;

public class InventoryItem
{
    [SerializeField] int id;
    [SerializeField] string name;
    [SerializeField] int value;

    public InventoryItem(int id, string name, int value)
    {
        this.id = id;
        this.name = name;
        this.value = value;
    }

    public string GetName()
    {
        return name;
    }

    public int GetID()
    {
        return id;
    }

    public int GetValue()
    {
        return value;
    }
}
