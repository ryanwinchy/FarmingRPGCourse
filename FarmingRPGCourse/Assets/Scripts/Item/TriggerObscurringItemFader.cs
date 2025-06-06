using UnityEngine;

public class TriggerObscurringItemFader : MonoBehaviour     //On player to tell objects when to fade as player moves behind.
{

    private void OnTriggerEnter2D(Collider2D collision)     //Get game object we triggered with, get all obscurring item fader on it and its children, trigger fade out of those items.
    {
        ObscuringItemFader[] obscuringItemFaders = collision.GetComponentsInChildren<ObscuringItemFader>();   //Parent and children.

        if (obscuringItemFaders.Length > 0)
        {
            foreach (ObscuringItemFader item in obscuringItemFaders)
            {
                item.FadeOut();
            }
        }

    }

    private void OnTriggerExit2D(Collider2D collision)        //Get game object we stopped triggering with, get all obscurring item fader on it and its children, trigger fade back in of those items.
    {
        ObscuringItemFader[] obscuringItemFaders = collision.GetComponentsInChildren<ObscuringItemFader>();

        if (obscuringItemFaders.Length > 0)
        {
            foreach (ObscuringItemFader item in obscuringItemFaders)
            {
                item.FadeIn();
            }
        }

    }     






}
