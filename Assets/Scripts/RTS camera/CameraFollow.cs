using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    [Range(0.01f, 1.0f)] public float smoothness = 0.5f;

    private Vector3 _cameraOffset;

    // Start is called before the first frame update
    void Start()
    {
        _cameraOffset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        var newPos = player.transform.position + _cameraOffset;
        transform.position = Vector3.Slerp(transform.position, newPos, smoothness);
    }
}