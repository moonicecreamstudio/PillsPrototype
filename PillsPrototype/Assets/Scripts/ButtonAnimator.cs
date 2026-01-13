using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAnimator : MonoBehaviour
{
    [Header("References")]
    public GameObject button;

    [Header("Parameters")]
    public float offset;
    public float pressDuration;

    private Vector3 startPosition;
    private Vector3 endPosition;



    void Start()
    {
        startPosition = new Vector3(button.transform.position.x,
                                    button.transform.position.y,
                                    button.transform.position.z);
        endPosition = new Vector3(button.transform.position.x,
                                  button.transform.position.y - offset,
                                  button.transform.position.z);
    }

    public IEnumerator ButtonPressed()
    {


        button.transform.position = endPosition;
        yield return new WaitForSeconds(pressDuration);
        button.transform.position = startPosition;
    }
}
