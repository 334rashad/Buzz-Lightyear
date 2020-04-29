using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //configuration parameters
    [Header("Player settings")]
    [SerializeField] float playerMoveSpeed = 15f;
    [SerializeField] float padding = .5f;
    [SerializeField] float health = 300f;
    [SerializeField] AudioClip destroySFX;
    [SerializeField] AudioClip laserSFX;
    [SerializeField] [Range(0, 1)] float destroyVolume = 0.7f;
    [SerializeField] [Range(0, 1)] float laserSoundVolume = 0.2f;

    [Header("Projectile")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 15f;
    [SerializeField] float projectileFirePeriod = 0.1f;

    Coroutine fireCoroutine;

    float xMin;
    float xMax;
    float yMin;
    float yMax;
    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries();
    }



    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1")) 
        {
            fireCoroutine = StartCoroutine(FireContinuously());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(fireCoroutine);
        }
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * playerMoveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * playerMoveSpeed;

        var newXPosition = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPosition = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        transform.position = new Vector2(newXPosition, newYPosition);
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }

    IEnumerator FireContinuously() 
    {
        while (true)
        {
            GameObject playerLaser = Instantiate(
                laserPrefab,
                transform.position,
                Quaternion.identity) as GameObject;
            playerLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            AudioSource.PlayClipAtPoint(laserSFX, Camera.main.transform.position, laserSoundVolume);
            yield return new WaitForSeconds(projectileFirePeriod);
            Debug.Log("player shoots");
        }
    }

    private void OnTriggerEnter2D(Collider2D others)
    {
        DamageDealer damageDealer = others.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) return;
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            DestroyPlayer();
        }
    }

    private void DestroyPlayer()
    {
        FindObjectOfType<LevelLoader>().LoadGameOver();
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(destroySFX, Camera.main.transform.position, destroyVolume);
    }
}
