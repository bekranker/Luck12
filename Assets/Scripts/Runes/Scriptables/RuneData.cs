using UnityEngine;
[CreateAssetMenu(fileName = "Rune Data", menuName = "Create Rune Data", order = 3)]
public class RuneData : ScriptableObject
{
    public string Name;
    public string Description;
    public Sprite SpriteValue;
}