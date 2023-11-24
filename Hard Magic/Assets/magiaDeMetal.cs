using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magiaDeMetal : MonoBehaviour
{
    public float range, dano, castDuration = 0.3f, metalDuracao = 0.35f, duracaoDaParede = 5f, custoMana = 10, custo2 = 50, custo3 = 80;
    bool castando = false;

    float TimerCastDuration = 0;

    public LayerMask mask;

    GameObject prefabMetal;

    int time = 0;

    bool active = false;

    public void activate(bool tof){
        active = tof;
    }

    // Start is called before the first frame update
    void Start()
    {
        prefabMetal = Resources.Load<GameObject>("metalArea");
    }

    // Update is called once per frame
    void Update()
    {
        if(castando){
            TimerCastDuration += Time.deltaTime;
        }
        if(Input.GetKeyDown(KeyCode.Space) && !castando && active){
            castando = true;
            GetComponent<playerMovement>().slow(0.5f);
            float ncarregado = GetComponent<battleManager>().castMana(custoMana, custo2, custo3);
        }
        if(Input.GetKeyUp(KeyCode.Space) && active){
            //if(TimerCastDuration >= castDuration){
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if( Physics.Raycast(ray, out hit, mask) )
                {
                    //Debug.DrawLine( transform.position, hit.point );
                    Vector3 target = hit.point;
                    GameObject metal = Instantiate(prefabMetal);
                    metal.transform.position = target;
                    metal.transform.right = target - transform.position;
                    metal.transform.GetChild(0).GetComponent<AnimControlDuracao>().duracao = metalDuracao;
                    float ncarregado = GetComponent<battleManager>().castMana(custoMana, custo2, custo3);
                    if(ncarregado < custo2){
                        Destroy(metal, metalDuracao);
                        metal.GetComponent<magia>().spawnar(dmg: dano, team: time, dt: metalDuracao);
                    }
                    if(ncarregado >= custo2 && ncarregado < custo3){
                        metal.GetComponent<magia>().spawnar(dmg: dano*2, team: time, dt: metalDuracao + duracaoDaParede, pdur: duracaoDaParede);
                    }
                    if(ncarregado >= custo3){
                        metal.GetComponent<magia>().spawnar(dmg: dano*2, team: time, dt: metalDuracao + duracaoDaParede, pdur:duracaoDaParede);
                        metal.transform.localScale *=2;
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
