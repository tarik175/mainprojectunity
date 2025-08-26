using System;
using UnityEngine;

[Serializable]
public class StateAnimationSetDictionary : SerializableDictionary<CharacterState, DirectionalAnimationSet>
{
    public AnimationClip GetFacingClipFromState(CharacterState state, Vector2 facingDirection)
    {
        if (TryGetValue(state, out DirectionalAnimationSet animationSet))
            {
                return animationSet.GetFacingClip(facingDirection);
            }
            else
            {
                Debug.LogError($"No animation set found for state: {state}");
            }
            return null;
        }
    }

