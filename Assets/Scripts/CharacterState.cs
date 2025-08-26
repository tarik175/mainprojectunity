using UnityEngine;

[CreateAssetMenu(fileName = "CharacterState", menuName = "mainproject/CharacterState")]
public class CharacterState : ScriptableObject
{
    [field: SerializeField] public bool canMove { get; set; } = true;
    [field: SerializeField] public bool CanExitWhilePlaying { get; set; } = true;
}
