using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System;

public class AnimationOverrides : MonoBehaviour
{
    [SerializeField] private GameObject character = null;    //player so can get its animators.
    [SerializeField] private SO_AnimationType[] soAnimationTypeArray;

    Dictionary<AnimationClip, SO_AnimationType> animationTypeDictionaryByAnimationClip;   //give animation clip, get animation type SO. THESE DICTIONARIES MAKE IT EASIER TO REF OUR SO'S WE CREATED.
    Dictionary<string, SO_AnimationType> animationTypeDictionaryByCompositeAttributeKey;   //give string, get animation type SO.



    private void Start()
    {
        //Initialise animation type dictionary keyed by animation clip.
        animationTypeDictionaryByAnimationClip = new Dictionary<AnimationClip, SO_AnimationType>();

        foreach (SO_AnimationType item in soAnimationTypeArray)
        {
            animationTypeDictionaryByAnimationClip.Add(item.animationClip, item);
        }

        //Initialise animation type dictionary keyed by string.
        animationTypeDictionaryByCompositeAttributeKey = new Dictionary<string, SO_AnimationType>();

        foreach (SO_AnimationType item in soAnimationTypeArray)
        {
            string key = item.characterPart.ToString() + item.partVariantColour.ToString() + item.partVariantType.ToString() + item.animationName.ToString();   //this is the composite key.
            animationTypeDictionaryByCompositeAttributeKey.Add(key, item);
        }




    }

    /// <summary>
    /// Pass in list of attributes which define animations we want to switch, use scriptable object asset to look to see if we have matching swaps to swap to, then swap animator with override controller.
    /// </summary>
    /// <param name="characterAttributesList"></param>
    public void ApplyCharacterCustomisationParameters(List<CharacterAttribute> characterAttributesList)   //pass in list of character attributes, defining what we want to switch, like arms to carry.
    {                                                                                                 //for changing arms to carry, is just one char attribute in this list. but we could do multiple.
        //Stopwatch s1 = Stopwatch.StartNew();

        //Loop thru all character attributes and set the animation override controller for each.  For arms to carry, is just one char attribute in this list. but we could do multiple.
        foreach (CharacterAttribute characterAttribute in characterAttributesList)
        {
            Animator currentAnimator = null;
            List<KeyValuePair<AnimationClip, AnimationClip>> animsKeyValuePairList = new List<KeyValuePair<AnimationClip, AnimationClip>>();  //List that we will populate with current anim and toSwap anim.

            string animatorSOAssetName = characterAttribute.characterPart.ToString();    //Look at character part we passed in as character attribute. This is the animator we want to locate, like arms.


            //Find animators in scene that match scriptable object animator type.
            Animator[] animatorsArray = character.GetComponentsInChildren<Animator>();     //Build up array of all animators that are children of player.

            foreach (Animator animator in animatorsArray)     //Loop thru them looking for the animator that matches the part we passed in as character attribute.
            {
                if (animator.name == animatorSOAssetName)
                {
                    currentAnimator = animator;    //Found the animator we want to apply override to. Like arms animator.
                    break;
                }
            }

            //Get base current animations for animator.
            AnimatorOverrideController animationOverrideController = new AnimatorOverrideController(currentAnimator.runtimeAnimatorController);   //get animator controller on arms, create new animator override controller.
            List<AnimationClip> animationsList = new List<AnimationClip>(animationOverrideController.animationClips); //with animator override controller, can get all clips on them in an array. We making list of the array of clips.

            foreach (AnimationClip animationClip in animationsList)     //go thru the clips on the arms animator.
            {
                //See if the clip in question is in our list of scriptable objects as one we want to swap.
                SO_AnimationType soAnimationType;
                bool foundAnimation = animationTypeDictionaryByAnimationClip.TryGetValue(animationClip, out soAnimationType);  //if found, we might want to swap the clip.

                if (foundAnimation) //check what swap animation is for this clip.
                {    //get composite key from the attribute we passed in, like arms. ALl the switchable things basically like part and colour.
                    string key = characterAttribute.characterPart.ToString() + characterAttribute.partVariantColour.ToString() + characterAttribute.partVariantType.ToString() + soAnimationType.animationName.ToString();

                    SO_AnimationType swapSOAnimationType;    //Look if we have Scriptable object for this, if we do, we want to swap the clip.
                    bool foundSwapAnimation = animationTypeDictionaryByCompositeAttributeKey.TryGetValue(key, out swapSOAnimationType);

                    if (foundSwapAnimation)
                    {
                        AnimationClip swapAnimationClip = swapSOAnimationType.animationClip;

                        animsKeyValuePairList.Add(new KeyValuePair<AnimationClip, AnimationClip>(animationClip, swapAnimationClip));    //Add the anim and swap anim to list, and process the rest of the clips.
                    }
                }
            }

            //Apply animation updates to animation override controller and then update animator with the new controller.
            animationOverrideController.ApplyOverrides(animsKeyValuePairList);     //takes list of anims to swap anims we built up, 
            currentAnimator.runtimeAnimatorController = animationOverrideController;    //and thhen applies the controller with the swapped anims to the current runtime animator.


        }
    }

    
    
}
