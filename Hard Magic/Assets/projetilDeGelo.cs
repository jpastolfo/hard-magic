using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projetilDeGelo : MonoBehaviour
{
    GameObject prefabGelo, prefabCastGelo;
        float dano = 10f, duracao = 3f, speed = 20f, castDuration = 1f, TimerCastDuration = 0f;
        int time = 0;
        bool castando = false;
    // Start is called before the first frame update
    void Start()
    {
        prefabGelo = Resources.Load<GameObject>("gelinho");
        prefabCastGelo = Resources.Load<GameObject>("Gelo Cast");
    }

    // Update is called once per frame
    void Update()
    {
        if(castando){
            TimerCastDuration += Time.deltaTime;
        }
        if(Input.GetKeyDown(KeyCode.Q) && !castando){
            castando = true;
            GameObject castSprite = Instantiate(prefabCastGelo);
            castSprite.transform.SetParent(gameObject.transform);
            castSprite.GetComponent<AnimControlDuracao>().duracao = castDuration; 
            castSprite.transform.position = transform.position;
        }
        if(Input.GetKeyUp(KeyCode.Q)){
            if(TimerCastDuration >= castDuration){
                Ray ray= Camera.main.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
                Plane plane=new Plane(Vector3.up, Vector3.zero);
                float distance;
                if(plane.Raycast(ray, out distance)) {
                    Vector3 target=ray.GetPoint(distance);
                    Vector3 direction=target-transform.position;
                    float rotation=Mathf.Atan2(direction.x, direction.z)*Mathf.Rad2Deg;
                    GameObject gelo = Instantiate(prefabGelo);
                    gelo.GetComponent<magia>().spawnar(spd:speed, dmg: dano, team: time, dt: duracao, doc: true);
                    gelo.transform.rotation = Quaternion.Euler(0, rotation, 0);
                    gelo.transform.position = transform.position + new Vector3(0,1,0);
                }
            }
            castando = false;
            TimerCastDuration = 0;
        }
    }
}
