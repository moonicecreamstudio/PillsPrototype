using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class PlayerDisrupter : MonoBehaviour
{
    [Header("References")]
    public AnimationCurve curve;
    public CameraController cameraController;
    public PlayerStatusManager playerStatusManager;
    public Slider energySlider;
    public Slider focusSlider;
    public RectTransform eyelid1;
    public RectTransform eyelid2;
    public PostProcessVolume volume;

    [Header("Dozing Parameters")]
    public float _dozingSpeed;
    private Vector3 lastMousePosition;
    private float current;
    private float current2;
    public float _eyelidSpeed;
    public Vector2 eyelid1StartPosition;
    public Vector2 eyelid1GoalPosition;
    public Vector2 eyelid2StartPosition;
    public Vector2 eyelid2GoalPosition;

    [Header("Unfocused Parameters")]
    public float value;
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
        // Hunger Bar Effect


        // Energy Bar Effects
        // When the player is tired, move the camera and shut eyelids
        if (playerStatusManager._isTired == true)
        {
            Vector3 currentMousePosition = Input.mousePosition; // There's a bug that wakes the player up

            current = Mathf.MoveTowards(current, 1, _dozingSpeed * Time.deltaTime);
            cameraController.xRotation = Mathf.Lerp(cameraController.xRotation, 90, curve.Evaluate(current));

            current2 = Mathf.MoveTowards(current2, 1, _eyelidSpeed * Time.deltaTime);
            Vector2 newPos1 = Vector2.Lerp(eyelid1StartPosition, eyelid1GoalPosition, curve.Evaluate(current2));
            eyelid1.anchoredPosition = newPos1;

            Vector2 newPos2 = Vector2.Lerp(eyelid2StartPosition, eyelid2GoalPosition, curve.Evaluate(current2));
            eyelid2.anchoredPosition = newPos2;


            if (currentMousePosition != lastMousePosition)
            {
                eyelid1.anchoredPosition = eyelid1StartPosition;
                eyelid2.anchoredPosition = eyelid2StartPosition;
                playerStatusManager._isTired = false;
                current = 0f;
                current2 = 0f;
                Debug.Log("Wake up");
            }

            lastMousePosition = currentMousePosition;
        }

        // Calmness Bar Effect


        // Focus Bar Effect
        // Currently, instantly blurs the camera.
        if (playerStatusManager._isUnfocused == true)
        {
            SetAperture(3f);
        }
        else
        {
            SetAperture(15f);
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
