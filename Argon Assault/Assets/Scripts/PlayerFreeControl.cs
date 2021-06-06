using UnityEngine;

public class PlayerFreeControl : MonoBehaviour
{
    [Header("General Setup Settings")]
    [SerializeField] float controlSpeed = 40f;
    [SerializeField] float controlRotation = 50f;

    [Header("Laser Guns")]
    [SerializeField] GameObject[] lasers;
    [SerializeField] int laserDamage = 25;

    float xThrow, yThrow, zThrow, rotateThrow;
    float xRotation, yRotation;

    void Update()
    {
        ProcessFiring();
        FreeMove();
        FreeRotate();
    }

    void FreeMove()
    {
        
        xThrow = Input.GetAxis("Horizontal");
        yThrow = Input.GetAxis("Up/Down");
        zThrow = Input.GetAxis("Vertical");

        if (Input.GetAxis("Horizontal") != 0)
        {
            transform.position = transform.position + Camera.main.transform.right * xThrow * controlSpeed * Time.deltaTime;
        }
        if (Input.GetAxis("Vertical") != 0)
        {
            transform.position = transform.position + Camera.main.transform.forward * zThrow * controlSpeed * Time.deltaTime;
        }
        if (Input.GetAxis("Up/Down") != 0)
        {
            transform.position = transform.position + Camera.main.transform.up * yThrow * controlSpeed * Time.deltaTime;
        }
    }

    void FreeRotate()
    {
        rotateThrow = Input.GetAxis("Rotate");

        xRotation += Input.GetAxis("Mouse Y") * controlRotation * Time.deltaTime;
        yRotation += Input.GetAxis("Mouse X") * controlRotation * Time.deltaTime;
        transform.rotation = Quaternion.Euler(xRotation, yRotation, transform.rotation.z);

        if (Input.GetAxis("Rotate") != 0)
        {
            float rightRotation = rotateThrow * controlSpeed;
            transform.Rotate(0, 0, rightRotation);
        }
    }

    void ProcessFiring()
    {
        if (Input.GetButton("Fire1"))
        {
            SetLasersActive(true);
        }
        else
        {
            SetLasersActive(false);
        }
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
