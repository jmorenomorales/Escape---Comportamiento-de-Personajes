using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed = 4, rotSpeed = 100f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            gameObject.transform.position = Vector3.Lerp(transform.position, transform.TransformPoint(Vector3.forward), playerSpeed*Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            gameObject.transform.position = Vector3.Lerp(transform.position, transform.TransformPoint(Vector3.left), playerSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            gameObject.transform.position = Vector3.Lerp(transform.position, transform.TransformPoint(Vector3.back), playerSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            gameObject.transform.position = Vector3.Lerp(transform.position, transform.TransformPoint(Vector3.right), playerSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            playerSpeed = 7;
        }
        else
        {
            playerSpeed = 4;
        }

        if (Input.GetKey(KeyCode.Q))
        {
            gameObject.transform.Rotate(-Vector3.up * rotSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.E))
        {
            gameObject.transform.Rotate(Vector3.up * rotSpeed * Time.deltaTime);
        }
    }
}
