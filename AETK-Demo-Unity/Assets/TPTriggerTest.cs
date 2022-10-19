using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPTriggerTest : MonoBehaviour
{  
    
	public Transform TPDest;
    
	private void OnTriggerEnter( Collider col )
	{
		var go = col.gameObject;
		
		go.active = false;
		go.transform.position = TPDest.position;
		go.active = true;
	}
}
