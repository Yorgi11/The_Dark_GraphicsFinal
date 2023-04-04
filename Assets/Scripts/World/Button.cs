using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] private GameObject Prompt;
    [SerializeField] private Door[] doors;

    private bool canInteract = false;
    void Update()
    {
        if (canInteract)
        {
            HideObject(false, Prompt, 4f);
            if (Input.GetKeyDown(KeyCode.E))
            {
                for (int i=0; i<doors.Length; i++)
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
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 7) canInteract = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 7) canInteract = false;
    }
}
