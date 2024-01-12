using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class boss : MonoBehaviour
{
    public float arenaminx, arenaminy, arenamaxx, arenamaxy, raioatkDuracao = 3f, raioCastTime = 0.7f, targetSpeed = 5f, danoRaio = 5, paredeAtkDur = 2f, paredeDur = 60f, healCastTime = 1f, HealDur = 30f;
    public float raiocd = 7f, cdEntreAcao = 2f, paredeCastTime = 1f, levantaParedeCd = 60f, danoParede = 15f, spawnaFantasmaCd = 10f, spawnaBixoCastTime = 0.5f, fantasmaAtkDur = 3f, healcdTime = 45f;

    float castTimer = 0, durtimer = 0, raioCdTimer = 0, cdEntreAcaoTimer = 0, levantaParedeCdtimer = 0, fantasmaTimer = 0, spawnaFantasmacdTimer = 0, healcdTimer = 0, altura = 0;
    float colunaCaiTempo = 2f, colunaCaiTimer = 0;
    public int nparedes = 6, nghost = 3, npedras = 3;

    GameObject[] paredes;
    UnityEngine.AI.NavMeshAgent nma;
    GameObject veSeEncaixa, raio, raioArea, alvoprefab, alvo, player, raioprefab, maozinha, puddlePrefab, pilarPrefab, areaDMG, ghostprefab, walkTarget, healTotem, coluna;
    Animator animator;
    bool andando = false, atacando = false, atiraRaio = false, subindoParede= false, spawnandoBixo = false, healando = false, comeca = false, caiColuna = false;
    // Start is called before the first frame update
    void Start()
    {
        nma = GetComponent<NavMeshAgent>();
        veSeEncaixa = Resources.Load<GameObject>("veSeEncaixa");
        animator = GetComponent<Animator>();
        alvoprefab = Resources.Load<GameObject>("alvo");
        player = GameObject.Find("Player");
        raioprefab = Resources.Load<GameObject>("raio");
        maozinha = transform.Find("maozinha").gameObject;
        paredes = new GameObject[nparedes];
        puddlePrefab = Resources.Load<GameObject>("puddle");
        pilarPrefab = Resources.Load<GameObject>("pilar");
        levantaParedeCdtimer = levantaParedeCd;
        raioCdTimer = raiocd;
        areaDMG = Resources.Load<GameObject>("areaDMG");
        ghostprefab = Resources.Load<GameObject>("fire_ghost");
        spawnaFantasmacdTimer = spawnaFantasmaCd;
        healTotem = Resources.Load<GameObject>("Pedraheal");
        healcdTimer = healcdTime;
        arenamaxx += transform.position.x;
        arenaminx += transform.position.x;
        arenamaxy += transform.position.z;
        arenaminy += transform.position.z;
        altura = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(player.transform.position, transform.position) < 15f){
            if(!comeca){
                comeca = true;
                coluna = GameObject.Find("colunaquecai");
                if(coluna){
                    caiColuna = true;
                }
            }
            if(caiColuna){
                if(colunaCaiTimer < colunaCaiTempo){
                    colunaCaiTimer += Time.deltaTime;
                }
                coluna.transform.localEulerAngles = new Vector3(colunaCaiTimer/colunaCaiTempo * 80, 0, 0);
            }
            if(andando){
                //Debug.Log("aaaaaaaa");
                if(Vector3.Distance(walkTarget.transform.position, new Vector3(transform.position.x, walkTarget.transform.position.y, transform.position.z)) < 0.1f){
                    //if(nma.isStopped){
                    //Debug.Log("ccccc");
                    andando = false;
                    animator.SetBool("andar", false);
                }
            }

            if(cdEntreAcaoTimer < cdEntreAcao){
                cdEntreAcaoTimer += Time.deltaTime;
            }

            if(!andando && !atacando && cdEntreAcaoTimer > cdEntreAcao){
                int acao = Random.Range(0, 4);
                Debug.Log(acao);
                if(acao == 0 && raioCdTimer >= raiocd){
                    atacando = true;
                    atiraRaio = true;
                    animator.SetBool("raio", true);
                    alvo = GameObject.Instantiate(alvoprefab);
                    alvo.transform.position = transform.position;
                    alvo.GetComponent<magia>().spawnar(dmg:danoRaio, team:1, dt:100, continuo:true);
                }
                if(acao == 1 && levantaParedeCdtimer >= levantaParedeCd){
                    atacando = true;
                    subindoParede = true;
                    animator.SetBool("levantar", true);
                    paredes[0] = Instantiate(puddlePrefab);
                    paredes[0].transform.position = player.transform.position + new Vector3(0, 0.5f, 0);
                    //Destroy(paredes[0], paredeCastTime);
                    for(int i = 1; i < nparedes; i++){
                        float x = Random.Range(arenaminx, arenamaxx);
                        float y = Random.Range(arenaminy, arenamaxy);
                        paredes[i] = Instantiate(puddlePrefab);
                        paredes[i].transform.position = new Vector3(x, altura + 0.5f, y);
                        //Destroy(paredes[i], paredeCastTime + 1f);
                    }
                }
                if(acao == 2 && spawnaFantasmacdTimer >= spawnaFantasmaCd){
                    atacando = true;
                    spawnandoBixo = true;
                    animator.SetBool("paitaon", true);
                }
                if(acao == 3 && healcdTimer >= healcdTime){
                    atacando = true;
                    healando = true;
                    animator.SetBool("suba", true);
                }
            }

            if(healando){
                if(castTimer < healCastTime){
                    castTimer += Time.deltaTime;
                }else{
                    for(int i = 0; i < npedras; i++){
                        GameObject pedra = Instantiate(healTotem);
                        achaLugarPraPedra(pedra);
                        Destroy(pedra, HealDur);
                        animator.SetBool("suba", false);
                        durtimer = 0;
                        healando = false;
                        atacando = false;
                        castTimer = 0;
                        cdEntreAcaoTimer = 0;
                        healcdTimer = 0;
                    }
                }
            }

            if(spawnandoBixo){
                if(castTimer < spawnaBixoCastTime){
                    castTimer += Time.deltaTime;
                }else{
                    if(durtimer == 0){
                        GameObject go = Instantiate(ghostprefab, transform.position + new Vector3(0, 1, 0), transform.rotation);
                        //go.transform.position = transform.position + new Vector3(0, 1, 0);
                    }
                    fantasmaTimer += Time.deltaTime;
                    if(fantasmaTimer >= fantasmaAtkDur / nghost){
                        GameObject go = Instantiate(ghostprefab, transform.position + new Vector3(0, 1, 0), transform.rotation);
                        //go.transform.position = transform.position + new Vector3(0, 1, 0);
                        fantasmaTimer = 0;
                    }
                    durtimer += Time.deltaTime;
                    if(durtimer > fantasmaAtkDur){
                        animator.SetBool("paitaon", false);
                        durtimer = 0;
                        atacando = false;
                        castTimer = 0;
                        cdEntreAcaoTimer = 0;
                        spawnaFantasmacdTimer = 0;
                        fantasmaTimer = 0;
                        spawnandoBixo = false;
                    }
                }
            }

            if(subindoParede){
                if(castTimer < paredeCastTime){
                    castTimer += Time.deltaTime;
                }else{
                    if(durtimer == 0){
                        animator.SetBool("parede2", true);
                        animator.SetBool("levantar", false);
                        for(int i = 0; i < nparedes; i++){
                            Vector3 pos = paredes[i].transform.position;
                            Destroy(paredes[i]);
                            GameObject go = Instantiate(areaDMG);
                            go.transform.position = pos;
                            go.GetComponent<magia>().spawnar(dmg:danoParede, team:1, dt:0.25f);
                            paredes[i] = Instantiate(pilarPrefab);
                            paredes[i].transform.position = pos - Vector3.up * 10f;
                            Destroy(paredes[i], paredeDur);
                        }
                    }
                    for(int i = 0; i < nparedes; i++){
                        paredes[i].transform.position += new Vector3(0, 10/paredeAtkDur * Time.deltaTime, 0);
                    }
                    durtimer += Time.deltaTime;
                    if(durtimer > paredeAtkDur){
                        animator.SetBool("parede2", false);
                        durtimer = 0;
                        subindoParede = false;
                        atacando = false;
                        castTimer = 0;
                        cdEntreAcaoTimer = 0;
                        levantaParedeCdtimer = 0;
                    }
                }
            }

            if(atiraRaio){
                //alvo.transform.position += (alvo.transform.position - player.transform.position) * targetSpeed;
                alvo.transform.position = Vector3.MoveTowards(alvo.transform.position, player.transform.position, targetSpeed * Time.deltaTime);
                transform.forward = alvo.transform.position - transform.position;
                if(castTimer < raioCastTime){
                    castTimer += Time.deltaTime;
                    
                }else{
                    if(durtimer == 0){
                        raio = Instantiate(raioprefab);
                        raio.transform.position = maozinha.transform.position;
                        raio.transform.forward = alvo.transform.position - raio.transform.position;

                    }
                    durtimer += Time.deltaTime;
                    raio.transform.localScale = Vector3.Distance(maozinha.transform.position, alvo.transform.position) / 3 * new Vector3(1,1,1);
                    raio.transform.position = maozinha.transform.position;
                    raio.transform.forward = alvo.transform.position - raio.transform.position;
                    if(durtimer >= raioatkDuracao){
                        Destroy(raio);
                        Destroy(alvo);
                        animator.SetBool("raio", false);
                        atacando = false;
                        atiraRaio = false;
                        durtimer = 0;
                        castTimer = 0;
                        raioCdTimer = 0;
                        cdEntreAcaoTimer = 0;
                        //levantaParedeCdtimer = 0;
                    }
                }
            }
        }

        if(raioCdTimer < raiocd){
            raioCdTimer += Time.deltaTime;
        }
        if(levantaParedeCdtimer < levantaParedeCd){
            levantaParedeCdtimer += Time.deltaTime;
        }
        if(spawnaFantasmacdTimer < spawnaFantasmaCd){
            spawnaFantasmacdTimer += Time.deltaTime;
        }
        if(healcdTimer < healcdTime){
            healcdTimer += Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.GetComponent<foguinho>() && !atacando){
            /*float x = Random.Range(arenaminx, arenamaxx);
            float y = Random.Range(arenaminy, arenamaxy);
            nma.SetDestination(new Vector3(x,0,y));
            GameObject go = Instantiate(veSeEncaixa);
            go.transform.position = new Vector3(x,0,y);
            go.transform.localScale = transform.localScale;*/
            naoEncaixa();
            animator.SetBool("andar", true);
            andando = true;
        }
    }

    public void naoEncaixa(){
        float x = Random.Range(arenaminx, arenamaxx);
        float y = Random.Range(arenaminy, arenamaxy);
        RaycastHit hit;
        if(Physics.Raycast(new Vector3(x, 100, y), Vector3.up * -1, out hit, Mathf.Infinity)){
            nma.SetDestination(new Vector3(x,hit.point.y,y));
            GameObject go = Instantiate(veSeEncaixa);
            go.transform.position = new Vector3(x,hit.point.y,y);
            go.transform.localScale = transform.localScale;
            walkTarget = go;
        }
    }

    public void achaLugarPraPedra(GameObject pedra){
        float x = Random.Range(arenaminx, arenamaxx);
        float y = Random.Range(arenaminy, arenamaxy);
        pedra.transform.position = new Vector3(x, altura ,y);
    }
}
