using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    [SerializeField] int hp;
    [SerializeField] float speed;
    [SerializeField] Sprite[] sprites_;
    [SerializeField] GameObject[] atkk;
    [SerializeField] Transform[] atkk_points;
    enum states { frente, tras, esquerda, direita}
    states atual_state;

    private bool damaged;
    private float total_time;
    private float time_elapsed;

    // Use this for initialization4
    void Start ()
    {
        this.damaged = false;
        this.total_time = 0.1f;
        this.time_elapsed = 0;

        atual_state = states.frente;
	}
	
	// Update is called once per frame
	void Update ()
    {
        move();
        sprite_controller();
        damaged_color();
        check_death();
        atkkk();

    }

    void atkkk()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (atual_state == states.frente)
            {
                Instantiate(atkk[0], atkk_points[0].position, atkk_points[0].rotation);
                
            }
            else if (atual_state == states.tras)
            {
                Instantiate(atkk[1], atkk_points[1].position, atkk_points[1].rotation);
            }
            else if (atual_state == states.esquerda)
            {
                Instantiate(atkk[2], atkk_points[2].position, atkk_points[2].rotation);
            }
            else
            {
                Instantiate(atkk[3], atkk_points[3].position, atkk_points[3].rotation);
            }
        }
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
        if ((this.time_elapsed >= this.total_time) && this.damaged)
        {
            damaged = false;
            this.time_elapsed = 0;
        }
    }


    void move()
    {
        if(Input.GetAxisRaw("Horizontal") != 0)
        {
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                GetComponent<Transform>().Translate(new Vector3(1,0,0) * speed * Time.deltaTime);
                atual_state = states.direita;
            }
            else
            {
                GetComponent<Transform>().Translate(new Vector3(-1, 0, 0) * speed * Time.deltaTime);
                atual_state = states.esquerda;
            }
        }
        if (Input.GetAxisRaw("Vertical") != 0)
        {
            if (Input.GetAxisRaw("Vertical") > 0)
            {
                GetComponent<Transform>().Translate(new Vector3(0, 0, 1) * speed * Time.deltaTime);
                atual_state = states.tras;
            }
            else
            {
                GetComponent<Transform>().Translate(new Vector3(0, 0, -1) * speed * Time.deltaTime);
                atual_state = states.frente;
            }
        }
    }

    void check_death()
    {
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }
    void sprite_controller()
    {
        if (atual_state == states.frente)
        {
            GetComponentInChildren<SpriteRenderer>().sprite = sprites_[0];
            GetComponentInChildren<SpriteRenderer>().flipX = false;
        }
        else if (atual_state == states.tras)
        {
            GetComponentInChildren<SpriteRenderer>().sprite = sprites_[1];
            GetComponentInChildren<SpriteRenderer>().flipX = false;
        }
        else if (atual_state == states.esquerda)
        {
            GetComponentInChildren<SpriteRenderer>().sprite = sprites_[2];
            GetComponentInChildren<SpriteRenderer>().flipX = true;
        }
        else
        {
            GetComponentInChildren<SpriteRenderer>().sprite = sprites_[2];
            GetComponentInChildren<SpriteRenderer>().flipX = false;
        }
    }

    public void take_dam(int dm)
    {
        hp -= dm;
        this.damaged = true;
        this.time_elapsed = 0;
    }
}
