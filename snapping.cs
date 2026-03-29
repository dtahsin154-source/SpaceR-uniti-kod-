using UnityEngine;

public class Snapping : MonoBehaviour
{
    public Transform SnapPoint;
    public float snapDis = 0.2f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void TrySnap()
    {
        Debug.Log("trying to snap start");
        float distance = Vector3.Distance(transform.position, SnapPoint.position);

        if (distance <= snapDis)
        {
            Debug.Log("snapping");
            Snap();
        }
        else
        {
            Debug.Log("distance not enough");
        }
    }

    void Snap()
    {
        transform.position = SnapPoint.position;
        transform.rotation = SnapPoint.rotation;

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        rb.isKinematic = true;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("collided trying to snap");
        TrySnap();
    }
}
