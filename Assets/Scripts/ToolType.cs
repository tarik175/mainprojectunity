using UnityEngine;

[CreateAssetMenu(fileName = "ToolType", menuName = "mainproject/ToolType")]
public class ToolType : ScriptableObject
{
    [field: SerializeField] public string DisplayName { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }
    [field: SerializeField] public string Description { get; private set; }
}
