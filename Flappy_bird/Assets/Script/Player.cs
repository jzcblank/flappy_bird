using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public GameObject up;
    public GameObject down;
    public Rigidbody2D rigidbodybird;
    public float addforce = 300f;
    public Animator ani;
    public delegate void DeathNotify();
    public event DeathNotify OnDeath;
    public bool death = false;
    public UnityAction<int> OnScore;
    public CircleCollider2D circlecollider2D;
    public Vector3 prePosition;
    public Vector3 nowPosition;
    void Start()
    {
        this.circlecollider2D = this.GetComponent<CircleCollider2D>();
        this.ani = this.GetComponent<Animator>();
        idle();
    }
    void GetnowPotion()
    {
        nowPosition = circlecollider2D.transform.position;
    }
    void Update()
    {
        if (this.death) return;
        if (Input.GetMouseButtonDown(0) && this.rigidbodybird.gravityScale == 1)
        {
            rigidbodybird.velocity = Vector2.zero;
            rigidbodybird.AddForce(new Vector2(0, addforce));
        }
        prePosition = circlecollider2D.transform.position;
        Invoke("GetnowPotion", 0.01f);
        if (prePosition.y < nowPosition.y)
        {
            up.SetActive(true);
            down.SetActive(false);
        }
        else
        {
            up.SetActive(false);
            down.SetActive(true);
        }
    }
    public void idle()
    {
        this.ani.SetTrigger("idle");
        this.rigidbodybird.gravityScale = 0;
    }
    public void fly()
    {
        this.ani.SetTrigger("fly");
        this.rigidbodybird.gravityScale = 1;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.death == false && collision.name == "obstacle") dead();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.name == "scorearea")
        {
            if(this.OnScore != null)
            {
                this.OnScore(1);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (this.death == false)
        {
            dead();
        }
    }
    public void dead()
    {
        this.ani.SetTrigger("dead");
        this.death = true;
        if(this.OnDeath != null)
        {
            this.OnDeath();
        }
    }
}
