using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScroll : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private float zoomSpeed;

    private float _camFOV;
    private float _mouseScrollInput;

    // Start is called before the first frame update
    void Start()
    {
        _camFOV = cam.fieldOfView;
    }

    // Update is called once per frame
    void Update()
    {
        _mouseScrollInput = Input.GetAxis(("Mouse ScrollWheel"));
        _camFOV -= _mouseScrollInput * zoomSpeed;
        _camFOV = Mathf.Clamp(_camFOV, 30, 60);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, _camFOV, zoomSpeed);
    }
}