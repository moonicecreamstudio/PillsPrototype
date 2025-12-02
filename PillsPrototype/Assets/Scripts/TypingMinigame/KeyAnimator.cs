using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeyAnimator : MonoBehaviour
{
    public string letter;
    public TextMeshPro textMeshProText;
    public GameObject keyPivot;
    public GameObject keyModel;
    public Renderer keyRenderer;
    public float pressDistance;

    void Start()
    {
        textMeshProText.text = letter.ToUpper();
        keyRenderer = keyModel.GetComponent<Renderer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(letter))
        {
            keyRenderer.material.color = Color.green;
            keyPivot.transform.position = new Vector3(keyPivot.transform.position.x,
                                                      keyPivot.transform.position.y - pressDistance,
                                                      keyPivot.transform.position.z);

        }
        if (Input.GetKeyUp(letter))
        {
            keyRenderer.material.color = Color.white;
            keyPivot.transform.position = new Vector3(keyPivot.transform.position.x,
                                                      keyPivot.transform.position.y + pressDistance,
                                                      keyPivot.transform.position.z);
        }

    }
}
