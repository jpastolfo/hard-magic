using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magia_metal_gelo : MonoBehaviour
{
    public float range, dano, speed = 20f, castDuration = 0.3f, metalDuracao = 0.35f, duracaoDaParede = 5f, custoMana = 10, custo2 = 50, custo3 = 80;
    bool castando = false;

    float TimerCastDuration = 0;

    LayerMask mask;

    GameObject prefabMetal;
    GameObject prefabGelo, prefabCastGelo, castSprite;

    int time = 0;

    bool active = false;

    public void activate(bool tof){
        active = tof;
    }


    // Start is called before the first frame update
    void Start()
    {
        prefabMetal = Resources.Load<GameObject>("metalArea");//"metalArea");
        prefabGelo = Resources.Load<GameObject>("gelinho");
        prefabCastGelo = Resources.Load<GameObject>("Gelo Cast");
        mask = GetComponent<battleManager>().mask;
    }

    // Update is called once per frame
    void Update()
    {
        if(castando){
            TimerCastDuration += Time.deltaTime;
        }
        if(Input.GetKeyDown(KeyCode.Space) && !castando && active){
            float ncarregado = GetComponent<battleManager>().castMana(custoMana, custo2, custo3);
            if(ncarregado != -2){
                castando = true;

                castSprite = Instantiate(prefabCastGelo);
                castSprite.transform.SetParent(gameObject.transform);
                castSprite.GetComponent<AnimControlDuracao>().duracao = castDuration; 
                castSprite.transform.position = transform.position;
                Destroy(castSprite, castDuration);

                GetComponent<playerMovement>().slow(0.5f);
            }
        }
        if(Input.GetKeyUp(KeyCode.Space) && active && castando){
            //if(TimerCastDuration >= castDuration){
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                dano = 10;
                if( Physics.Raycast(ray, out hit, mask) )
                {
                    //Debug.DrawLine( transform.position, hit.point );
                    Vector3 target = hit.point;                  
                    GameObject metal = Instantiate(prefabMetal);
                    GameObject gelo = Instantiate(prefabGelo);

                    metal.transform.position = target;
                    metal.transform.right = target - transform.position;
                    metal.transform.GetChild(0).GetComponent<AnimControlDuracao>().duracao = metalDuracao;
                    float ncarregado = GetComponent<battleManager>().castMana(custoMana, custo2, custo3);
                    
                    gelo.transform.position = metal.transform.position;
                    Vector3 direction = target - transform.position;

                    if(ncarregado < custo2){
                        Destroy(metal, metalDuracao);
                        metal.GetComponent<magia>().spawnar(dmg: dano, team: time, dt: metalDuracao);
                        gelo.GetComponent<magia>().spawnar(spd:speed, dmg: dano, team: time, dt: metalDuracao, doc: true);
                        gelo.transform.forward = direction.normalized;
                        Destroy(castSprite);
                    }
                    if(ncarregado >= custo2 && ncarregado < custo3){
                        metal.GetComponent<magia>().spawnar(dmg: dano*2, team: time, dt: metalDuracao + duracaoDaParede, pdur: duracaoDaParede);

                        gelo.GetComponent<magia>().spawnar(spd:speed, dmg: dano, team: time, dt: metalDuracao, doc: true);
                        gelo.transform.forward = direction.normalized;
                        gelo.transform.localScale = new Vector3(1,1,1); 
                        Destroy(castSprite);
                        
                    }
                    if(ncarregado >= custo3){
                        metal.GetComponent<magia>().spawnar(dmg: dano*2, team: time, dt: metalDuracao + duracaoDaParede, pdur:duracaoDaParede);
                        metal.transform.localScale *=2;
        
                        gelo.GetComponent<magia>().spawnar(spd:speed, dmg: dano, team: time, dt: metalDuracao, doc: true);
                        gelo.transform.forward = direction.normalized;
                        gelo.transform.localScale = new Vector3(1,1,1); 
                        gelo.AddComponent<miniGelinhos>();
                        Destroy(castSprite);
                        
                    }
                    //AreaSkill(target);
                }
            //}
            castando = false;
            TimerCastDuration = 0;
            GetComponent<playerMovement>().slow(1);
        }
    }
}