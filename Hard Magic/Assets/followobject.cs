using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followobject : MonoBehaviour
{
    public GameObject fObject;
    private Transform Position;
    private Vector3 corection;
    // Start is called before the first frame update
    void Start()
    {
        Position = GetComponent<Transform>();
        corection = fObject.transform.position-Position.position;
    }

    // Update is called once per frame
    void Update()
    {
        Position.position = fObject.transform.position-corection;
    }
}
