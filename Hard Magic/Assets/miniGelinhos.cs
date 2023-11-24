using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miniGelinhos : MonoBehaviour
{
    int ngelos= 3, time = 0;
    public float dano = 10f, duracao = 0.7f, speed = 7f;
    GameObject prefabGelo;
    // Start is called before the first frame update
    void Start()
    {
        prefabGelo = Resources.Load<GameObject>("gelinho");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawnarGelo(){
        for(int i = 0; i < ngelos; i++){
            GameObject gelo = Instantiate(prefabGelo);
            gelo.transform.position = transform.position;
            gelo.GetComponent<magia>().spawnar(spd:speed, dmg: 10, team: time, dt: duracao, doc: false);
            gelo.transform.forward = transform.forward;
            if(i == 1){
                gelo.transform.forward = (transform.forward + transform.right * -1).normalized;
            }
            if(i == 2){
                gelo.transform.forward = (transform.forward + transform.right).normalized;
            }
        }
    }
}
