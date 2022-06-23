using UnityEngine;

public class CamSwitchManager : MonoBehaviour
{
    [SerializeField] private CameraFollow camFollow;
    [SerializeField] private CameraRoam camRoam;
    [SerializeField] private KeyCode keyCode;

    private int _camViewState;

    // Start is called before the first frame update
    void Start()
    {
        camRoam.enabled = false;
        _camViewState = 0;
        // default: camFollow.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Input.GetKeyDown(keyCode)) return;

        camFollow.enabled = _camViewState > 0;
        camRoam.enabled = !camFollow.enabled;

        _camViewState = (_camViewState + 1) % 2;
    }
}