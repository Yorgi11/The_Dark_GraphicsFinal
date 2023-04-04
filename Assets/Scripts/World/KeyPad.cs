using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyPad : MonoBehaviour
{
    [SerializeField] private GameObject Prompt;
    [SerializeField] private Door[] doors;

    private bool canInteract = false;
    [SerializeField] private TMP_InputField inputField;

    [SerializeField] private string correctInput;

    private string input = "";

    private bool correct = false;
    // MainHub
    private MainHub hub;
    private void Start()
    {
        hub = FindObjectOfType<MainHub>();
    }
    void Update()
    {
        if (canInteract)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                hub.DisableMouse = !hub.DisableMouse;
            }


            HideObject(false, Prompt, 4f);
            if (input.Equals(correctInput) && !correct)
            {
                correct = true;
                for (int i = 0; i < doors.Length; i++)
                {
                    doors[i].ActivateDoor();
                }
            }
        }
        else HideObject(true, Prompt, 4f);
    }
    private void HideObject(bool state, GameObject obj, float rate)
    {
        Vector3 temp;
        if (state) temp = new Vector3(0f, 0f, 0f);
        else temp = new Vector3(0.35f, 0.35f, 0.35f);
        obj.transform.localScale = Vector3.Lerp(obj.transform.localScale, temp, Time.deltaTime * rate);
    }
    public void SetInputs()
    {
        input = inputField.text;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 7) canInteract = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 7) canInteract = false;
    }
}
