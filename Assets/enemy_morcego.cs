using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_morcego : MonoBehaviour
{
    [SerializeField] int hp;
    [SerializeField] float step;
    int atual_hp;
    bool on_range;
    bool atked_player;
    [SerializeField] float atked_total, atked_elapsed;

    private bool damaged;
    private float total_time;
    private float time_elapsed;

    GameObject player;
	// Use this for initialization
	void Start ()
    {
        this.damaged = false;
        this.total_time = 0.1f;
        this.time_elapsed = 0;
        player = GameObject.FindGameObjectWithTag("Player");
        atual_hp = hp;
        on_range = false;
        atked_elapsed = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
        move();
        update_times();
        damaged_color();
        check_death();
	}

    void damaged_color()
    {
         if (this.damaged)
        {
            GetComponentInChildren<SpriteRenderer>().color = Color.red;
        }
        else
        {
            GetComponentInChildren<SpriteRenderer>().color = Color.white;
        }
        this.time_elapsed += Time.deltaTime;
        if((this.time_elapsed >= this.total_time) && this.damaged)
        {
            damaged = false;
            this.time_elapsed = 0;
}
    }

    void update_times()
    {
        if (atked_player)
        {
            atked_elapsed += Time.deltaTime;
            if(atked_elapsed >= atked_total)
            {
                atked_player = false;
                atked_elapsed = 0;
            }
        }
    }

    void move()
    {
        if (on_range && !atked_player)
        {
            transform.LookAt(player.transform);
            GetComponent<Transform>().Translate(new Vector3(0,0,1) * step * Time.deltaTime);

        }else if (atked_player)
        {
            transform.LookAt(player.transform);
            GetComponent<Transform>().Translate(new Vector3(0, 0, -1) * (step*5) * Time.deltaTime);
        }
    }
    void check_death()
    {
        if(hp <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void take_dam(int damagee)
    {
        hp -= damagee;
        this.damaged = true;
        this.time_elapsed = 0;
        atked_player = true;

    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            on_range = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            on_range = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<player>().take_dam(1);
            atked_player = true;
        }
    }
}
