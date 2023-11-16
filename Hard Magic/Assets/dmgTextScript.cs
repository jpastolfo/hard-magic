using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dmgTextScript : MonoBehaviour
{
    float txtMoveSpeed = 2f, duracao = 1f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, duracao);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.up * txtMoveSpeed * Time.deltaTime;
    }
}
