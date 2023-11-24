using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projetilDeGelo : MonoBehaviour
{
    GameObject prefabGelo, prefabCastGelo, castSprite;
    public float dano = 10f, duracao = 3f, speed = 20f, castDuration = 1f, custoMana = 20, custo2 = 40, custo3 = 60;
    int time = 0;
    bool castando = false;
    bool active = true;

    public void activate(bool tof){
        active = tof;
    }
    // Start is called before the first frame update
    void Start()
    {
        prefabGelo = Resources.Load<GameObject>("gelinho");
        prefabCastGelo = Resources.Load<GameObject>("Gelo Cast");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !castando && active){
            float ncarregado = GetComponent<battleManager>().castMana(custoMana, custo2, custo3);
            //Debug.Log(ncarregado);
            castando = false;
            if(ncarregado != -2){
                castando = true;
                castSprite = Instantiate(prefabCastGelo);
                castSprite.transform.SetParent(gameObject.transform);
                castSprite.GetComponent<AnimControlDuracao>().duracao = castDuration; 
                castSprite.transform.position = transform.position;
                GetComponent<playerMovement>().slow(0.5f);
                Destroy(castSprite, castDuration);
            }
        }
        if(Input.GetKeyUp(KeyCode.Space) && active && castando){
            //if(TimerCastDuration >= castDuration){
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if( Physics.Raycast(ray, out hit) )
                {
                    //Debug.DrawLine( transform.position, hit.point );
                    Vector3 target = hit.point;
                    Vector3 direction=target-transform.position;
                    float rotation=Mathf.Atan2(direction.x, direction.z)*Mathf.Rad2Deg;
                    GameObject gelo = Instantiate(prefabGelo);
                    float ncarregado = GetComponent<battleManager>().castMana(custoMana, custo2, custo3);
                    if(ncarregado < custo2){
                        gelo.GetComponent<magia>().spawnar(spd:speed, dmg: dano, team: time, dt: duracao, doc: true);
                    }
                    if(ncarregado >= custo2 && ncarregado < custo3){
                        gelo.GetComponent<magia>().spawnar(spd:speed, dmg: dano * 2, team: time, dt: duracao, doc: true);
                        gelo.transform.localScale = new Vector3(1,1,1); 
                    }
                    if(ncarregado >= custo3) {
                        gelo.GetComponent<magia>().spawnar(spd:speed, dmg: dano * 2, team: time, dt: duracao, doc: true);
                        gelo.transform.localScale = new Vector3(1,1,1);
                        gelo.AddComponent<miniGelinhos>();
                    
                    }
                    //gelo.GetComponent<magia>().spawnar(spd:speed, dmg: dano, team: time, dt: duracao, doc: true);
                    gelo.transform.forward = direction.normalized;
                    gelo.transform.position = transform.position + new Vector3(0,1,0);
                    Destroy(castSprite);

                }
            //}
            castando = false;
            GetComponent<playerMovement>().slow(1);
        }
    }
}
