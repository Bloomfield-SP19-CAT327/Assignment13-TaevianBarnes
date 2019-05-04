using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour
{
    [SyncVar]
    public Color color;

    float moveSpeed = 3.0f;
    float yRotate = 50f;

    private void Start()
    {
        if (isLocalPlayer && hasAuthority)
        {
            transform.Translate(0, 1, 0);
        }
    }

    public override void OnStartClient()
    {
        gameObject.GetComponent<Renderer>().material.color = color;
    }

    void Update()
    {
        if (isLocalPlayer && hasAuthority)
        {
            Movement();
        }
    }

    void GetAuthority()
    {
        if (isServer)
        {
            RpcMovement();
        }
        else
        {
            CmdMovement();
        }
    }

    void Movement()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * Time.deltaTime * moveSpeed;
        }

        if (Input.GetKey(KeyCode.A))
        {
           
            transform.Translate((Time.deltaTime * (moveSpeed * -1)), 0, 0);
        }

        if (Input.GetKey(KeyCode.D))
        {
            //transform.Rotate(0, (Time.deltaTime * yRotate), 0);
            transform.Translate((Time.deltaTime * moveSpeed), 0, 0);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(0, 0, (Time.deltaTime * (moveSpeed * -1)));
        }

        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(0, ((Time.deltaTime * yRotate) * -1), 0);
        }

        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(0, (Time.deltaTime * yRotate), 0);
        }

    }

    [ClientRpc]
    void RpcMovement()
    {
        Movement();
    }

    [Command]
    public void CmdMovement()
    {
        RpcMovement();
    }
}
