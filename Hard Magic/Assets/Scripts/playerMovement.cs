using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    private Vector3 direction;
    public float speed;
    Rigidbody rb;
    GameObject camra;
    Vector3 movedir = Vector3.zero;
    public bool grounded = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        camra = GameObject.Find("Main Camera"); //trocar para nome da camera da cena
    }

    private void FixedUpdate() {
        //rb.velocity = new Vector3(direction.x * speed * Time.deltaTime,0.0f,direction.z * speed * Time.deltaTime);
        if(movedir != Vector3.zero && grounded){
            rb.velocity = movedir.normalized * speed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal"),0.0f,Input.GetAxisRaw("Vertical")).normalized;
        movedir = Vector3.zero;
        if(direction.magnitude > 0.1f){
            float angulo = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + camra.transform.eulerAngles.y;
            movedir = Quaternion.Euler(0, angulo, 0) * Vector3.forward;
        }
        RaycastHit hit;
        if(Physics.Raycast(transform.position, -transform.up, out hit, 0.1f)) {
            grounded = true;
            rb.velocity = Vector3.zero;
        }else{
            grounded = false;
        }
    }
}