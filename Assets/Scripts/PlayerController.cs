using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 1;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public GameObject pickupParent;

    public GameObject followCamera;
    
    private Rigidbody rb;

    private Vector3 forwardVector;

    private int count;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        setCountText();
        winTextObject.SetActive(false);
    }

    void setCountText()
    {
        countText.text = "Score: " + count.ToString();
        
        if (count >= pickupParent.transform.childCount) winTextObject.SetActive(true);
    }

    void FixedUpdate()
    {
        float cameraDirection = followCamera.GetComponent<CameraController>().viewAngleX;

        if (Input.GetKey("w")) rb.AddForce(createVectorFromDirection(cameraDirection) * speed);
        if (Input.GetKey("s")) rb.AddForce(-createVectorFromDirection(cameraDirection) *  speed);
        if (Input.GetKey("a"))
        {
            if (cameraDirection - 90.0f <= -180.0f)
            {
                rb.AddForce(-createVectorFromDirection(cameraDirection - 270.0f) * speed);
            }
            else
            {
                rb.AddForce(createVectorFromDirection(cameraDirection - 90.0f) * speed);
            }
        }
        if (Input.GetKey("d"))
        {
            if (cameraDirection + 90.0f > 180.0f)
            {
                rb.AddForce(createVectorFromDirection(cameraDirection - 270.0f) * speed);
            }
            else
            {
                rb.AddForce(createVectorFromDirection(cameraDirection + 90.0f) * speed);
            }
        }
    }

    Vector3 createVectorFromDirection(float direction)
    {
        return new Vector3(Mathf.Sin(direction * Mathf.Deg2Rad), 0.0f, Mathf.Cos(direction * Mathf.Deg2Rad));
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            count++;
            setCountText();
        }
        if (other.gameObject.CompareTag("Respawn")) transform.position = new Vector3(0.0f, 0.5f, 0.0f);
        if (other.gameObject.CompareTag("BouncePad")) rb.AddForce(new Vector3(0.0f, other.GetComponent<BouncePad>().force, 0.0f), ForceMode.Impulse);
        if (other.gameObject.CompareTag("BoostPad")) rb.AddForce(other.GetComponentInParent<BoostPad>().getForwardVector(), ForceMode.Impulse);
    }
}
