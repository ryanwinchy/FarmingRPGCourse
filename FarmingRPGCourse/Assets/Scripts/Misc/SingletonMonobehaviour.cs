using UnityEngine;

public abstract class SingletonMonobehaviour<T> : MonoBehaviour where T : MonoBehaviour   //Any class we pass in has to be a monobehaviour
{      //abstract means we can't create an instance of this class, but we can inherit from it.
    private static T instance;

    public static T Instance
    {
        get
        {
            return instance;
        }
    }


    protected virtual void Awake()  //Protected means can be accessed by children, and virtual means can be overridden by children
    {
        if (instance == null)
        {
            instance = this as T;
        }
        else
        {
            Destroy(gameObject);     //Only want one instance of the singleton, so destroy any extra ones.
        }
        
        
    }
   
}
