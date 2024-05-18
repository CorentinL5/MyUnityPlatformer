using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public int id;
    public string label; //name
    public string description;
    public Sprite image;
    public int hpGiven;
    public int speedGiven;
    public float speedDuration;
}
