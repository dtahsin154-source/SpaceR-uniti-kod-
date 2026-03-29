using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class XrSnap : MonoBehaviour
{
    private XRGrabInteractable grab;
    private Snapping snapObject;

    void Awake()
    {
        grab = GetComponent<XRGrabInteractable>();
        snapObject = GetComponent<Snapping>();

        grab.selectExited.AddListener(OnRelease);
    }

    void OnRelease(SelectExitEventArgs args)
    {
        snapObject.TrySnap();
    }
}
