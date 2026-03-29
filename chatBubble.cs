using TMPro;
using Unity.Tutorials.Core.Editor;
using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class chatBubble : MonoBehaviour
{
    public TextMeshProUGUI textUI;

    public string[] sentences;
    public float typingSpeed = 0.03f;

    public Transform target;
    public Vector3 offset = new Vector3(0, 2f, 0);

    public InputActionProperty nextButton;

    private int index = 0;
    private Coroutine typingCoroutine;
    private bool isTyping = false;

    void Start()
    {
        if (nextButton.action == null)
        {
            Debug.LogError("next button null");
        }
        else
        {
            Debug.Log("action: " + nextButton.action.name);
        }
        showSenctence();
    }

    void Update()
    {
        if (UnityEngine.XR.InputDevices.GetDeviceAtXRNode(UnityEngine.XR.XRNode.RightHand).TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out bool pressed) && pressed)
        {
            Debug.Log("Pressed");
            OnNextPressed();
        }

        if (nextButton.action == null) return;

        float value = nextButton.action.ReadValue<float>();

        if (value > 0.1f)
        {
            Debug.Log("trigger value : " + value);
            OnNextPressed();
        }

        /*if (target != null)
        {
            transform.position = target.position + offset;

            transform.LookAt(Camera.main.transform);
            transform.Rotate(0, 180, 0);
        }
        if (nextButton.action != null && nextButton.action.WasPressedThisFrame())
        {
            Debug.Log("pressed");
            OnNextPressed();
        }*/
    }

    void showSenctence()
    {
        if (sentences.Length == 0) return;
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        typingCoroutine = StartCoroutine(TypeSentence(sentences[index]));
    }

    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        textUI.text = "";

        foreach (char letter in sentence)
        {
            textUI.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        isTyping = false;
    }

    public void OnNextPressed()
    {
        if (isTyping)
        {
            StopCoroutine(typingCoroutine);
            textUI.text = sentences[index];
            isTyping = false;
        }
        else
        {
            index++;
            if (index >= sentences.Length)
            {
                index = 0;
            }
            showSenctence();
        }
    }
}
