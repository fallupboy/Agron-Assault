using System.Collections;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [Header("General Setup Settings")]
    [SerializeField] float controlSpeed = 20f;
    [SerializeField] float xRange = 5f;
    [SerializeField] float yRange = 5f;

    [Header("Laser Guns")]
    [SerializeField] GameObject[] lasers;
    [SerializeField] AudioClip laserSFX;
    [SerializeField] int laserDamage = 25;
    [SerializeField] float rateOfLaserShot = 0.3f;

    [Header("Screen Position Based Tuning")]
    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float positionYawFactor = 2f;

    [Header("Player Input Based Tuning")]
    [SerializeField] float controllPitchFactor = -10f;
    [SerializeField] float controllThrowFactor = -20f;

    AudioSource myAudioSource;

    float xThrow, yThrow;
    bool isShooting = false;

    private void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        ProcessFiring();
        Move();
        Rotate();
    }

    void Move()
    {
        xThrow = Input.GetAxis("Horizontal");
        yThrow = Input.GetAxis("Vertical");

        float xOffset = xThrow * Time.deltaTime * controlSpeed;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        float yOffset = yThrow * Time.deltaTime * controlSpeed;
        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    void Rotate()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControllThrow = yThrow * controllPitchFactor;
        float pitch = pitchDueToPosition + pitchDueToControllThrow;

        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = xThrow * controllThrowFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    void ProcessFiring()
    {
        if (Input.GetButton("Fire1"))
        {
            SetLasersActive(true);
            if (!isShooting)
            {
                StartCoroutine(PlayShotSFX());
            }
        }
        else
        {
            SetLasersActive(false);
        }
    }

    IEnumerator PlayShotSFX()
    {
        isShooting = true;
        yield return new WaitForSeconds(rateOfLaserShot);
        myAudioSource.PlayOneShot(laserSFX);
        isShooting = false;
    }

    void SetLasersActive(bool isActive)
    {
        foreach (GameObject laser in lasers)
        {
            var emmisionModule = laser.GetComponent<ParticleSystem>().emission;
            emmisionModule.enabled = isActive;
        }
    }

    public int GetPlayerLaserDamage()
    {
        return laserDamage;
    }
}
