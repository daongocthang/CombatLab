using UnityEngine;


public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    [Range(0.01f, 1.0f)] public float smoothness = 0.5f;

    private Vector3 camOffset;

    // Start is called before the first frame update
    void Start()
    {
        camOffset= transform.position - player.transform.position;
        Follow();
    }

    // Update is called once per frame
    void Update()
    {
        Follow();
    }

    private void Follow()
    {
        if (player == null) return;

        var newPos = player.transform.position + camOffset;
        transform.position = Vector3.Slerp(transform.position, newPos, smoothness);
    }

    public Transform Player => player;
}