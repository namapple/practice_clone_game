using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BirdController : MonoBehaviour
{
    public static BirdController instance;
    public float bounceForce;
    private Rigidbody2D myBody;
    private Animator anim;
    private bool isAlive;
    private bool didFlap;
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip flyClip, pingClip, diedClip;
    public float flag = 0;
    private GameObject spawner;
    public int score = 0;
    private void Awake()
    {
        MakeInstance();
        isAlive = true;
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spawner = GameObject.Find("Pipe Spawner");
    }

    private void FixedUpdate()
    {
        BirdMovement();
    }

    void BirdMovement()
    {
        if (isAlive)
        {
            if (didFlap)
            {
                didFlap = false;
                myBody.velocity = new Vector2(myBody.velocity.x, bounceForce);
                audioSource.PlayOneShot(flyClip);
            }
        }
        if (myBody.velocity.y > 0)
        {
            float angle = 0;
            angle = Mathf.Lerp(0, 90, myBody.velocity.y / 7);
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        else if (myBody.velocity.y == 0)
        {
            float angle = 0;
            angle = Mathf.Lerp(0, 90, myBody.velocity.y / 7);
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            float angle = 0;
            angle = Mathf.Lerp(0, -90, -myBody.velocity.y / 7);
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
    public void FlapButton()
    {
        didFlap = true;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PipeHolder")
        {
            score++;
            if (GamePlayController.instance != null)
            {
                GamePlayController.instance.SetScore(score);
            }
            audioSource.PlayOneShot(pingClip);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Pipe" || other.gameObject.tag == "Ground")
        {
            flag = 1;
            if (isAlive)
            {
                isAlive = false;
                audioSource.PlayOneShot(diedClip);
                anim.SetTrigger("Died");
                Destroy(spawner);
                if (GamePlayController.instance != null)
                {
                    GamePlayController.instance.BirdDiedShowPanel(score);
                }
            }
        }
    }
    public void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

}
