using System.Collections;
using UnityEngine;

public class ItemNudge : MonoBehaviour
{

    float pause = 0.04f;
    bool isAnimating = false;


    void Awake()
    {
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isAnimating)
        {
            if (gameObject.transform.position.x < collision.transform.position.x)    //Nudge dir depends where player triggers it.
            {
                StartCoroutine(RotateAntiClock());
            }
            else
            {
                StartCoroutine(RotateClock());
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (!isAnimating)
        {
            if (gameObject.transform.position.x > collision.transform.position.x)
            {
                StartCoroutine(RotateAntiClock());
            }
            else
            {
                StartCoroutine(RotateClock());
            }
        }
    }

    IEnumerator RotateAntiClock()
    {
        isAnimating = true;

        for (int i = 0; i < 4; i++)
        {
            gameObject.transform.GetChild(0).Rotate(0, 0, 2f);    // 2 degrees, pauses, then 2 more.

            yield return new WaitForSeconds(pause);
        }

        for (int i = 0; i < 5; i++)
        {
            gameObject.transform.GetChild(0).Rotate(0, 0, -2f);    //Other way.

            yield return new WaitForSeconds(pause);
        }

        gameObject.transform.GetChild(0).Rotate(0, 0, 2f);      //Back to start.

        yield return new WaitForSeconds(pause);

        isAnimating = false;
    }

    IEnumerator RotateClock()
    {
        isAnimating = true;

        for (int i = 0; i < 4; i++)
        {
            gameObject.transform.GetChild(0).Rotate(0, 0, -2f);

            yield return new WaitForSeconds(pause);
        }

        for (int i = 0; i < 5; i++)
        {
            gameObject.transform.GetChild(0).Rotate(0, 0, 2f);

            yield return new WaitForSeconds(pause);
        }

        gameObject.transform.GetChild(0).Rotate(0, 0, -2f);

        yield return new WaitForSeconds(pause);

        isAnimating = false;
    }

}
