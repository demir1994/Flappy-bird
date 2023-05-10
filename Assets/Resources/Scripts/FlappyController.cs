using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FlappyController : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    public Transform mainCamera;
    public Transform map_Tile;
    public Rigidbody rb;
    public AudioSource jumpFX;
    public Animator playerAnimator;

    public float mov_cycle;
    public float jumpHeightVel;
    public float twoDimensionalViewOffsetValue;
    public float rewindTime = 4f;

    public Vector3 camPosOffset;
    public GameObject startPanel;
    public GameObject gameOverPanel;

    bool startPhase = false;
    public bool rewind = false;
    public List<Vector3> _MemPositions;
    public Vector3 currentDirection;

    private void Start()
    {
        _MemPositions = new List<Vector3>();
    }

    void Update()
    {
        mainCamera.position = player.transform.position + camPosOffset;

        if (startPhase && !rewind)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + mov_cycle * Time.deltaTime);
            map_Tile.position = new Vector3(0, 0, map_Tile.position.z + mov_cycle * Time.deltaTime);
            ObjectsManager.instance.transform.position = new Vector3(transform.position.x, transform.position.y, map_Tile.position.z + mov_cycle * Time.deltaTime);
        } else if (rewind)
        {
            map_Tile.position = new Vector3(0, 0, map_Tile.position.z - mov_cycle * Time.deltaTime);
            ObjectsManager.instance.transform.position = new Vector3(transform.position.x, transform.position.y, map_Tile.position.z - mov_cycle * Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        if (rewind)
        {
            // rewind process start
            OnRewindStart();
        }
        else
        {
            // rewind process saving positions
            RewindSaving();
        }
    }

    /// <summary>
    /// Rewind function started
    /// </summary>
    public void OnRewindStart()
    {
        if (_MemPositions.Count > 0)
        {
            print("rewind");
            rewind = true;
            transform.position = _MemPositions[0];
            _MemPositions.RemoveAt(0);
            rb.isKinematic = true;
        } else
        {
            OnRewindStop();
        }
    }

    /// <summary>
    /// Rewind stop
    /// </summary>
    public void OnRewindStop()
    {
        rewind = false;
        rb.isKinematic = false;
    }

    /// <summary>
    /// Rewind recording function
    /// </summary>
    public void RewindSaving()
    {
        if (_MemPositions.Count > Mathf.Round(rewindTime / Time.fixedDeltaTime))
        {
            _MemPositions.RemoveAt(_MemPositions.Count - 1);
        }
        _MemPositions.Insert(0, player.position);
    }

    /// <summary>
    /// Jump interaction btn
    /// </summary>
    public void Jump()
    {
        Vector3 force = new Vector3(0, jumpHeightVel, 0);
        rb.AddForce(force);
        jumpFX.Play();
    }

    /// <summary>
    /// Tap To Start
    /// </summary>
    public void TapStart()
    {
        rb.isKinematic = false;
        startPhase = true;
        startPanel.SetActive(false);
    }

    /// <summary>
    /// Pause game
    /// </summary>
    public void Pause()
    {
        rb.isKinematic = true;
        startPhase = false;
    }

    /// <summary>
    /// Continue game
    /// </summary>
    public void Continue()
    {
        rb.isKinematic = false;
        startPhase = true;
    }

    /// <summary>
    /// Restart level
    /// </summary>
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// Toggle view Mode
    /// </summary>
    bool toggleView = true;

    /// <summary>
    /// Toggle change view perspective
    /// 2D / 3D
    /// </summary>
    public void ToggleView()
    {

        if (toggleView)
        {
            mainCamera.eulerAngles = new Vector3(0, -90, 0);
            camPosOffset = new Vector3(twoDimensionalViewOffsetValue, 1, 0);
        }
        else
        {
            mainCamera.eulerAngles = new Vector3(5, 0, 0);
            camPosOffset = new Vector3(0, 1, -3);
        }

        toggleView = !toggleView;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PipeSense" && !rewind)
        {
            GameManager.instance.EarnPoint(1);
        } else if (rewind)
        {
            GameManager.instance.LosePoint(1);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Obstacles" || collision.collider.tag == "MapTile")
        {
            // bird died - obstacle collided.
            playerAnimator.SetTrigger("death");
            startPhase = false;
            gameOverPanel.SetActive(true);
        }

    }
}
