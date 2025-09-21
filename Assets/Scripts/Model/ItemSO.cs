using UnityEngine;

namespace Inventory.Model
{
    [CreateAssetMenu(fileName = "New Item", menuName = "mainproject/ItemSO")]
    public class ItemSO : ScriptableObject
    {
        [field: SerializeField] public bool isStackable { get; set; }
        public int ID => GetInstanceID();
        [field: SerializeField] public int maxStackSize { get; set; } = 1;
        [field: SerializeField] public string Name { get; set; } = string.Empty;
        [field: SerializeField][TextArea] public string Description { get; set; } = string.Empty;
        [field: SerializeField] public Sprite ItemImage { get; set; } = null;
    }
}
