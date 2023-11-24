using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class escmenu : MonoBehaviour
{
    GameObject emenu;
    bool ativo = false;
    // Start is called before the first frame update
    void Start()
    {
        emenu = transform.Find("menuzin").gameObject;
        emenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape)){
            ativo = !ativo;
            emenu.SetActive(ativo);
        }
    }

    public void reiniciar(){
        SceneManager.LoadScene(1);
    }

    public void fechar(){
        Application.Quit();
    }
}
