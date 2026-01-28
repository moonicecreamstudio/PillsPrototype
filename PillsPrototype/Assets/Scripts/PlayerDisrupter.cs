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
    public Slider wakeUpSlider;
    public GameObject wakeUpSliderObject;

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
    public float currentWakeUpPoint;
    public float requiredWakeUpPoints;
    public float accelerationThreshold;
    public Vector2 lastVelocity;

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
        wakeUpSlider.maxValue = requiredWakeUpPoints;
        wakeUpSliderObject.SetActive(false);
    }
    void Update()
    {
        // Hunger Bar Effect

        // Energy Bar Effects
        // When the player is tired, move the camera and shut eyelids
        if (playerStatusManager._isTired == true)
        {
            cameraController.isCameraDisabled = false;
            cameraController.cameraHolderObject.transform.position = cameraController.cameraOriginPosition.position; // This is setting it every update, NG, find a way to set up once
            wakeUpSliderObject.SetActive(true);

            // Move the camera down
            current = Mathf.MoveTowards(current, 1, _dozingSpeed * Time.deltaTime);
            cameraController.xRotation = Mathf.Lerp(cameraController.xRotation, 90, curve.Evaluate(current));

            // Close eyelids
            current2 = Mathf.MoveTowards(current2, 1, _eyelidSpeed * Time.deltaTime);
            Vector2 newPos1 = Vector2.Lerp(eyelid1StartPosition, eyelid1GoalPosition, curve.Evaluate(current2));
            eyelid1.anchoredPosition = newPos1;
            Vector2 newPos2 = Vector2.Lerp(eyelid2StartPosition, eyelid2GoalPosition, curve.Evaluate(current2));
            eyelid2.anchoredPosition = newPos2;

            // Shake the mouse to wake up
            Vector2 currentVelocity = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            Vector2 acceleration = currentVelocity - lastVelocity;
            if (acceleration.magnitude > accelerationThreshold)
            {
                Debug.Log("Shaking Mouse");
                currentWakeUpPoint += 1;
            }
            lastVelocity = currentVelocity;
            wakeUpSlider.value = currentWakeUpPoint;

            // Old method
            //Vector3 currentMousePosition = Input.mousePosition; // There's a bug that wakes the player up
            //if (currentMousePosition != lastMousePosition)
            //{
            //    currentWakeUpPoint += 1;
            //}
            //lastMousePosition = currentMousePosition;

            if (currentWakeUpPoint >= requiredWakeUpPoints)
            {
                eyelid1.anchoredPosition = eyelid1StartPosition;
                eyelid2.anchoredPosition = eyelid2StartPosition;
                playerStatusManager._isTired = false;
                current = 0f;
                current2 = 0f;
                Debug.Log("Wake up");
                currentWakeUpPoint = 0;
                wakeUpSlider.value = 0;
                wakeUpSliderObject.SetActive(false);
            }



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
