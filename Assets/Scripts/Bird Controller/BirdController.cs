using UnityEngine;

public class BirdController : MonoBehaviour
{
    public static BirdController instance;
    enum PlayerAction { Attack, Defend, Flee };
    public float bounceForce;
    private Rigidbody2D myBody;
    private Animator anim;
    private bool isAlive;
    private bool didFlap;
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip flyClip, pingClip, diedClip;
    private GameObject spawner;
    public int score;
    public int flag = 0;
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
            var angle = Mathf.Lerp(0, 90, myBody.velocity.y / 7);
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        else if (myBody.velocity.y == 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            var angle = Mathf.Lerp(0, -90, -myBody.velocity.y / 7);
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
    public void FlapButton()
    {
        didFlap = true;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PipeHolder"))
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
        if (other.gameObject.CompareTag("Pipe") || other.gameObject.CompareTag("Ground"))
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

    private void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

}
