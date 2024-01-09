using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magiaMetalFogo : MonoBehaviour
{
    public float range, dano, duracao = 5f, castDuration = 0.3f, metalDuracao = 0.35f, duracaoDaParede = 5f, custoMana = 10, custo2 = 50, custo3 = 80;
    public int rangefogo = 3, longrange = 8;
    bool castando = false;

    float TimerCastDuration = 0;

    LayerMask mask;

    GameObject prefabMetal, prefabFogo, prefabFoguinho;

    int time = 0;

    bool active = false;

    public void activate(bool tof){
        active = tof;
    }

    // Start is called before the first frame update
    void Start()
    {
        prefabFogo = Resources.Load<GameObject>("fogo");
        prefabFoguinho = Resources.Load<GameObject>("FogoUnidade");
        prefabMetal = Resources.Load<GameObject>("metalArea");
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
                GetComponent<playerMovement>().slow(0.5f);
            }
        }
        if(Input.GetKeyUp(KeyCode.Space) && active && castando){
            //if(TimerCastDuration >= castDuration){
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if( Physics.Raycast(ray, out hit, mask) )
                {
                    //Debug.DrawLine( transform.position, hit.point );
                    Vector3 target = hit.point;
                    //metal
                    GameObject metal = Instantiate(prefabMetal);
                    metal.transform.position = target;
                    metal.transform.right = target - transform.position;
                    metal.transform.GetChild(0).GetComponent<AnimControlDuracao>().duracao = metalDuracao;
                    //fogo
                    GameObject fogo = Instantiate(prefabFogo);
                    fogo.transform.position = target;
                    fogo.transform.forward = new Vector3(transform.position.x, 0, transform.position.z).normalized;
                    
                    float ncarregado = GetComponent<battleManager>().castMana(custoMana, custo2, custo3);

                    float auxdano = dano, auxduracao = duracao;
                    int auxrange = rangefogo;
                    if (ncarregado >= custo2)
                    {
                        auxrange = longrange;
                    }
                    fogo.GetComponent<magia>().spawnar(dmg: auxdano, team: time, dt: auxduracao);
                    fogo.GetComponent<BoxCollider>().size = new Vector3(1, 1, auxrange);
                    fogo.GetComponent<BoxCollider>().center = new Vector3(0, 0, (auxrange - 1) / 2);

                    for (int i = 0; i < auxrange; i++)
                    {
                        GameObject foguinho = Instantiate(prefabFoguinho);
                        foguinho.transform.position = fogo.transform.position + new Vector3(transform.position.x, 0, transform.position.z).normalized * i;
                        foguinho.transform.SetParent(fogo.transform);
                        //foguinho.GetComponent<AnimControlDuracao>().duracao = duracao;
                    }
                    //fogo
                    if (ncarregado > custo3)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            GameObject fgo = Instantiate(prefabFogo);
                            fgo.transform.position = target;
                            if (j == 0)
                            {
                                fgo.transform.forward = -1 * fogo.transform.forward;
                                fgo.name = "aaa";
                            }
                            if (j == 1)
                            {
                                fgo.transform.forward = fogo.transform.right;
                            }
                            if (j == 2)
                            {
                                fgo.transform.forward = -1 * fogo.transform.right;
                            }
                            fgo.GetComponent<magia>().spawnar(dmg: auxdano, team: time, dt: auxduracao);
                            fgo.GetComponent<BoxCollider>().size = new Vector3(1, 1, auxrange);
                            fgo.GetComponent<BoxCollider>().center = new Vector3(0, 0, (auxrange - 1) / 2);
                            for (int i = 0; i < auxrange; i++)
                            {
                                GameObject foguinho = Instantiate(prefabFoguinho);
                                foguinho.transform.position = fgo.transform.position + fgo.transform.forward * i;
                                foguinho.transform.SetParent(fgo.transform);
                            }
                        }
                    }
                    //metal
                    if (ncarregado < custo2)
                    {
                        Destroy(metal, metalDuracao);
                        metal.GetComponent<magia>().spawnar(dmg: dano, team: time, dt: metalDuracao);
                        /*metalfogo
                        Transform OsFogo= metal.transform.Find("OsFogo");
                        if(OsFogo!=null) for (int i = 0; i < OsFogo.childCount; i++)
                        {
                            OsFogo.GetChild(i).GetComponent<magia>().spawnar(dmg: dano, team: time, dt: metalDuracao);
                        }*/
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
