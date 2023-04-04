using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TubeAni : MonoBehaviour
{
    [SerializeField] private float offset = 0f;
    [SerializeField] private float speed = 1f;
    [SerializeField] private GameObject objToMove;
    private float t = 0f;
    private Vector3 startpos, endpos;
    void Start()
    {
        startpos = objToMove.transform.position - new Vector3(0f, offset, 0f);
        endpos = objToMove.transform.position + new Vector3(0f, offset, 0f);
        t = Random.Range(0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        if (t * speed > 1f)
        {
            Vector3 temp = endpos;
            endpos = startpos;
            startpos = temp;
            t = 0f;
        }
        objToMove.transform.position = Vector3.Lerp(startpos, endpos, t * speed);
    }
}
