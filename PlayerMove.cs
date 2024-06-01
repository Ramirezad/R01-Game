using UnityEngine;
using UnityEngine.UI;


public class PlayerMove : MonoBehaviour
{
    public float Speed;
    public float JumpForce;
    private bool Grounded;

    public GameObject pauseMenu;
    public GameObject barraSaludPlayer;
    public GameObject barraSaludEnemy;
    public GameObject barraEnergiaPlayer;
    
    private Rigidbody2D Rigidbody2D;
    private Animator Animator;
    private float Horizontal;
    
    private bool isPaused = false;
    public bool sePuedeMover = true;
    [SerializeField] private Vector2 velocidadRebote;

    [SerializeField] Slider sliderMana;

    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
    }

    void Update()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");
        if(sePuedeMover) { Mover(Horizontal); }

        bool pause = Input.GetKeyDown(KeyCode.Escape);
        if (pause)
        {
            isPaused = !isPaused;
            TogglePause(isPaused);
        }
    }

    private void Mover(float horizontal)
    {
        if (horizontal < 0.0f) transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if (horizontal > 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        Animator.SetBool("running", horizontal != 0.0f);

        Debug.DrawRay(transform.position, Vector3.down * 0.4f, Color.red);
        Grounded = Physics2D.Raycast(transform.position, Vector3.down, 0.4f);

        Animator.SetBool("jump", !Grounded);

        if (Input.GetKeyDown(KeyCode.W) && Grounded)
        {
            Jump();
        }
    }

    private void TogglePause(bool isPaused)
    {
        Time.timeScale = isPaused ? 0 : 1;
        pauseMenu.SetActive(isPaused);
        barraSaludEnemy.SetActive(!isPaused);
        barraSaludPlayer.SetActive(!isPaused);
        barraEnergiaPlayer.SetActive(!isPaused);
    }

    private void Jump()
    {
        Rigidbody2D.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
    }

    private void FixedUpdate()
    {
        if (sePuedeMover)
        {
            Rigidbody2D.velocity = new Vector2(Horizontal * Speed, Rigidbody2D.velocity.y);
        }
    }

    public void Rebote(Vector2 puntoGolpe)
    {
        Rigidbody2D.velocity = new Vector2(-velocidadRebote.x, velocidadRebote.y);
        Debug.Log("Velocidad después del rebote: " + Rigidbody2D.velocity);
    }
}


