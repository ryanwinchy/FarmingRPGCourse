using UnityEngine;
using Unity.Cinemachine;

public class SwitchConfineBoundingShape : MonoBehaviour
{
    void Start()
    {
        SwitchBoundingShape();
    }

    void Update()
    {

    }


    /// <summary>
    /// Switches the collider that cinemachine uses to confine the camera to the scene.
    /// </summary>
    void SwitchBoundingShape()     //IN MY RPG, probably more efficient to have the camera in each scene and already linked to the collider?
    {
        //Get the polygon collider on the 'boundsconfiner' gameobject tagged in the scene. Cinemachine will use this to confine the camera to the scene.
        PolygonCollider2D polygonCollider2D = GameObject.FindGameObjectWithTag(Tags.BOUNDS_CONFINER).GetComponent<PolygonCollider2D>();

        CinemachineConfiner2D cinemachineConfiner = GetComponent<CinemachineConfiner2D>();

        cinemachineConfiner.BoundingShape2D = polygonCollider2D;

        cinemachineConfiner.InvalidateBoundingShapeCache();    //Clears and reapplies the new collider.
    }
}
