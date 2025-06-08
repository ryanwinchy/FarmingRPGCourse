using UnityEngine;

[CreateAssetMenu(fileName = "so_animationType", menuName = "Scriptable Objects/Animation/Animation Type")]   //menu option to crerate these.
public class SO_AnimationType : ScriptableObject
{
    public AnimationClip animationClip;
    public AnimationName animationName;   //like idleUp.
    public CharacterPartAnimator characterPart;      //arms, tools.
    public PartVariantColour partVariantColour;       //if vary colour, we are not using this.
    public PartVariantType partVariantType;          //carry variant for arms for eg.
}
