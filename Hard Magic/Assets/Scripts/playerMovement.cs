using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    private Vector3 direction;
    public float speed;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        rb.velocity = new Vector3(direction.x * speed * Time.deltaTime,0.0f,direction.z * speed * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        direction = new Vector3(Input.GetAxisRaw("Horizontal"),0.0f,Input.GetAxisRaw("Vertical")).normalized;
    }
}
