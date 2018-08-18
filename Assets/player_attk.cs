using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_attk : MonoBehaviour {

    
	// Use this for initialization
	void Start ()
    {
        Destroy(gameObject,     0.6f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            collision.gameObject.GetComponent<enemy_morcego>().take_dam(3);
        }
    }
}
