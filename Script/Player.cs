using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    public new Rigidbody2D rigidbody { get; private set; }
    public Bullet bulletPrefab;

    public float thrustSpeed = 1f;
    public bool thrusting { get; private set; }

    public float turnDirection { get; private set; } = 0f;
    public float rotationSpeed = 0.1f;

    public float respawnDelay = 3f;
    public float respawnInvulnerability = 3f;

    public bool screenWrapping = true;
    private Bounds screenBounds;

    public Button UpButton;
    public Button LeftButton;
    public Button RightButton;
    public Button ShootButton;

    private bool isUpButtonClicked = false;
    private bool isLeftButtonClicked = false;
    private bool isRightButtonClicked = false;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        GameObject[] boundaries = GameObject.FindGameObjectsWithTag("Boundary");

        for (int i = 0; i < boundaries.Length; i++)
        {
            boundaries[i].SetActive(!screenWrapping);
        }

        screenBounds = new Bounds();
        screenBounds.Encapsulate(Camera.main.ScreenToWorldPoint(Vector3.zero));
        screenBounds.Encapsulate(Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f)));

        // Add listeners to UI buttons for button clicks and releases
        UpButton.onClick.AddListener(OnUpButtonClick);
        UpButton.onClick.AddListener(OnUpButtonRelease);

        LeftButton.onClick.AddListener(OnLeftButtonClick);
        LeftButton.onClick.AddListener(OnLeftButtonRelease);

        RightButton.onClick.AddListener(OnRightButtonClick);
        RightButton.onClick.AddListener(OnRightButtonRelease);


        ShootButton.onClick.AddListener(OnShootButtonClick);
        ShootButton.onClick.AddListener(OnShootButtonRelease);
    }

    private void Update()
    {
        // Keyboard Inputs
        thrusting = Input.GetKey(KeyCode.W) || isUpButtonClicked;

        if (Input.GetKey(KeyCode.A) || isLeftButtonClicked)
        {
            turnDirection = 1f;
        }
        else if (Input.GetKey(KeyCode.D) || isRightButtonClicked)
        {
            turnDirection = -1f;
        }
        else
        {
            turnDirection = 0f;
        }

        if (Input.GetKeyDown(KeyCode.Space) || ShootButtonClicked())
        {
            Shoot();
        }
    }

    private void FixedUpdate()
    {
        if (thrusting)
        {
            rigidbody.AddForce(transform.up * thrustSpeed);
        }

        if (turnDirection != 0f)
        {
            rigidbody.AddTorque(rotationSpeed * turnDirection);
        }
    }

    private bool ShootButtonClicked()
    {
        return Input.GetMouseButtonDown(0);
    }

    private void Shoot()
    {
        Bullet bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        bullet.Project(transform.up);
    }

    // Functions to handle button clicks and releases
    public void OnUpButtonClick()
    {
        isUpButtonClicked = true;
    }

    public void OnUpButtonRelease()
    {
        isUpButtonClicked = false;
    }

    public void OnLeftButtonClick()
    {
        isLeftButtonClicked = true;
    }

    public void OnLeftButtonRelease()
    {
        isLeftButtonClicked = false;
    }

    public void OnRightButtonClick()
    {
        isRightButtonClicked = true;
    }

    public void OnRightButtonRelease()
    {
        isRightButtonClicked = false;
    }

    public void OnShootButtonClick()
    {
        Shoot();
    }

    public void OnShootButtonRelease()
    {

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularDrag = 0f;

            gameObject.SetActive(false);

            FindObjectOfType<GameManager>().playedDied();
        }
    }
}
