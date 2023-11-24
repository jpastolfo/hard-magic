using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magiaDeFogo : MonoBehaviour
{
    public float dano = 10, duracao = 5f, custoMana = 10f, custo2 = 35, custo3 = 70;
    public int range = 3, longrange = 8;
    int time = 0;
    GameObject prefabFogo, prefabFoguinho;

    float TimerCastDuration = 0;

    bool castando = false;

    bool active = false;

    LayerMask mask;

    public void activate(bool tof){
        active = tof;
    }

     // Start is called before the first frame update
    void Start()
    {
        prefabFogo = Resources.Load<GameObject>("fogo");
        prefabFoguinho = Resources.Load<GameObject>("FogoUnidade");
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
                    Vector3 target = hit.point - transform.position;
                    GameObject fogo = Instantiate(prefabFogo);
                    fogo.transform.position = transform.position;
                    fogo.transform.forward = new Vector3(target.x, 0, target.z).normalized;                    
                    //Destroy(fogo, duracao);
                    float ncarregado = GetComponent<battleManager>().castMana(custoMana, custo2, custo3);
                    float auxdano = dano, auxduracao = duracao;
                    int auxrange = range;
                    if(ncarregado >= custo2){
                        auxrange = longrange;
                    }
                    fogo.GetComponent<magia>().spawnar(dmg: auxdano, team: time, dt: auxduracao);
                    fogo.GetComponent<BoxCollider>().size = new Vector3(1,1,auxrange);
                    fogo.GetComponent<BoxCollider>().center = new Vector3(0,0,(auxrange- 1)/2);
                    for(int i = 0; i < auxrange; i++){
                        GameObject foguinho = Instantiate(prefabFoguinho);
                        foguinho.transform.position = fogo.transform.position + new Vector3(target.x, 0, target.z).normalized * i;
                        foguinho.transform.SetParent(fogo.transform);
                        //foguinho.GetComponent<AnimControlDuracao>().duracao = duracao;
                    }
                    if(ncarregado > custo3){
                        for(int j = 0; j < 3; j++){
                            GameObject fgo = Instantiate(prefabFogo);
                            fgo.transform.position = transform.position;
                            if(j == 0){
                                fgo.transform.forward = -1 * fogo.transform.forward;
                                fgo.name = "aaa";
                            }
                            if(j == 1){
                                fgo.transform.forward = fogo.transform.right;
                            }
                            if(j == 2){
                                fgo.transform.forward = -1* fogo.transform.right;
                            }
                            fgo.GetComponent<magia>().spawnar(dmg: auxdano, team: time, dt: auxduracao);
                            fgo.GetComponent<BoxCollider>().size = new Vector3(1,1,auxrange);
                            fgo.GetComponent<BoxCollider>().center = new Vector3(0,0,(auxrange- 1)/2);
                            for(int i = 0; i < auxrange; i++){
                                GameObject foguinho = Instantiate(prefabFoguinho);
                                foguinho.transform.position = fgo.transform.position + fgo.transform.forward * i;
                                foguinho.transform.SetParent(fgo.transform);
                            }
                        }
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
