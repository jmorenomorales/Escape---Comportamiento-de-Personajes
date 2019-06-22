using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed = 8, shiftSpeed = 12, rotSpeed = 250f, life = 1000f;
    public Vector3 playerPos;

    private Renderer playerRenderer;
    
    // Start is called before the first frame update
    void Start()
    {
        playerRenderer = gameObject.GetComponent<Renderer>();
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
            playerSpeed = 12;
        }
        else
        {
            playerSpeed = 8;
        }

        if (Input.GetKey(KeyCode.Q))
        {
            gameObject.transform.Rotate(-Vector3.up * rotSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.E))
        {
            gameObject.transform.Rotate(Vector3.up * rotSpeed * Time.deltaTime);
        }

        if(life <= 0f)
        {
            SceneManager.LoadScene("Final Simulation");
        }
    }

    public Vector3 GetPlayerPos()
    {
        return transform.position;
    }

    public void ProjectileImpact()
    {
        life = life - 100f;
        UpdateColor();
    }

    public void AttackImpact()
    {
        life = life - 5f;
        UpdateColor();
    }

    private void UpdateColor()
    {
        Color lerpedColor;
        lerpedColor = Color.Lerp(Color.red, Color.green, life/1000);
        playerRenderer.material.SetColor("_Color", lerpedColor);
    }
}