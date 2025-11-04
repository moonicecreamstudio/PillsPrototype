using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;
using static UnityEngine.GraphicsBuffer;

public class CameraDisrupter : MonoBehaviour
{
    public AnimationCurve curve;
    public CameraController cameraController;
    public bool _isTired;
    public float _movementSpeed;
    private Vector3 lastMousePosition;
    private float current;

    public Slider energySlider;
    public float timer;
    public Slider focusSlider;

    private float current2;
    public float _movementSpeed2;
    public RectTransform eyelid1;
    public RectTransform eyelid2;
    public Vector2 eyelid1StartPosition;
    public Vector2 eyelid1GoalPosition;
    public Vector2 eyelid2StartPosition;
    public Vector2 eyelid2GoalPosition;

    public PostProcessVolume volume;
    public float value = 15;
    public float blur;
    DepthOfField dof;

    void Awake()
    {

        volume.profile = Instantiate(volume.profile);
        if (volume.profile.TryGetSettings(out dof) == false)
        {
            Debug.LogError("No volume profile");
        }
    }
    void Update()
    {
        if (energySlider.value <= 50)
        {
            timer += Time.deltaTime;
            if (timer >= 5)
            {
                timer = 0;
                _isTired = true;
            }
        }

        if (_isTired == true)
        {
            Vector3 currentMousePosition = Input.mousePosition; // There's a bug that wakes the player up

            current = Mathf.MoveTowards(current, 1, _movementSpeed * Time.deltaTime);
            cameraController.xRotation = Mathf.Lerp(cameraController.xRotation, 90, curve.Evaluate(current));

            current2 = Mathf.MoveTowards(current2, 1, _movementSpeed2 * Time.deltaTime);
            Vector2 newPos1 = Vector2.Lerp(eyelid1StartPosition, eyelid1GoalPosition, curve.Evaluate(current2));
            eyelid1.anchoredPosition = newPos1;

            Vector2 newPos2 = Vector2.Lerp(eyelid2StartPosition, eyelid2GoalPosition, curve.Evaluate(current2));
            eyelid2.anchoredPosition = newPos2;


            if (currentMousePosition != lastMousePosition)
            {
                eyelid1.anchoredPosition = eyelid1StartPosition;
                eyelid2.anchoredPosition = eyelid2StartPosition;
                _isTired = false;
                current = 0f;
                current2 = 0f;
                Debug.Log("Wake up");
            }

            lastMousePosition = currentMousePosition;
        }

        // Currently, instantly blurs the camera. Need to fix and remove hard coded elements.

        if (focusSlider.value <= 5)
        {
            value = 3f;
            SetAperture(value);
        }
        else
        {
            value = 15f;
            SetAperture(value);
        }
    }

    public void SetAperture(float newAperture)
    {
        if (dof != null)
        {
            dof.aperture.value = newAperture;
            dof.aperture.overrideState = true;
        }
    }

}
