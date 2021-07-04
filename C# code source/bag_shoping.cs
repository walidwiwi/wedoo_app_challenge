using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bag_shoping : MonoBehaviour
{
	public static bag_shoping bagg; 
	private void Start()
	{
	bagg = this;	
	}
    public List<GameObject> bag = new List<GameObject>();
    public GameObject bagElement; 
    public Transform bagPlace; 
/*
    public void addToListe(){
	
	GameObject ex = Instanitate(bagElement, bagPlace );
	ex.GetComponet()

    }

    // Update is called once per frame
    void Update()
    {
        
    }
*/
}
