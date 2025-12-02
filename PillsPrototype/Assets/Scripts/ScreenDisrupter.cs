using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScreenDisrupter : MonoBehaviour
{
    public AnimationCurve curve;
    public GameObject _lettersOnScreen;
    public float _movementSpeed;
    private float current;

    public Slider focusSlider;

    public bool _isOn;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_isOn) 
        {
            if (focusSlider.value <= 5)
            {
                current = Mathf.MoveTowards(current, 1f, _movementSpeed * Time.deltaTime);

                float t = curve.Evaluate(current);

                float xRotation = Mathf.Lerp(0, 180, t);

                Vector3 euler = _lettersOnScreen.transform.eulerAngles;
                euler.z = xRotation;
                _lettersOnScreen.transform.rotation = Quaternion.Euler(euler);
            }
            else
            {
                _lettersOnScreen.transform.rotation = Quaternion.Euler(Vector3.zero);
                current = 0f;
            }
        }
    }
}
