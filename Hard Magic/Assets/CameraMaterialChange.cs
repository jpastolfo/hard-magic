using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMaterialChange : MonoBehaviour
{
    public GameObject Camera;
    public GameObject player;
    public Material transparent;
    private Material materiales;
    public BoxCollider Collider;

    [System.Serializable]
    public struct objetonafrente 
    {
        public GameObject gameobjectn;
        public Material materialn;
    }

    public int i = 0;
    public objetonafrente[] objetosn;
    void Start()
    {
        Collider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<MeshRenderer>() != null && objetosn[i].gameobjectn == null)
        {
            if (i >= objetosn.Length-1) i = 0;
            //materiales = other.gameObject.GetComponent<MeshRenderer>().material;
            objetosn[i].materialn = other.gameObject.GetComponent<MeshRenderer>().material;
            objetosn[i].gameobjectn = other.gameObject; 
            i++;
            other.gameObject.GetComponent<MeshRenderer>().material = transparent;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<MeshRenderer>() != null)
        {
            for (int n = 0; n < objetosn.Length;n++) 
            {
                if (other.gameObject == objetosn[n].gameobjectn)
                {
                    other.gameObject.GetComponent<MeshRenderer>().material = objetosn[n].materialn;
                    objetosn[n].gameobjectn=null;
                }
            }
        }
    }
}
