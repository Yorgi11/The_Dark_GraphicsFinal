using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Vector3 startPos;
    [SerializeField] private Vector3 endPos;
    [SerializeField] private float speed;

    private float t = 0f;

    private bool activate;
    void Update()
    {
        if (activate)
        {
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, endPos, speed * t);
            if (t*speed>=1)
            {
                activate = false;
                Vector3 temp = startPos;
                startPos = endPos;
                endPos = temp;
                t = 0;
            }
        }
    }
    public void ActivateDoor()
    {
        activate = true;
    }
}
