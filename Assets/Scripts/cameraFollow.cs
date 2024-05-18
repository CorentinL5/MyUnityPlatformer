using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public float timeOffset;
    public Vector3 posOffset;

    private GameObject player;
    private Vector3 velocity;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, player.transform.position + posOffset, ref velocity, timeOffset);
    }
}
