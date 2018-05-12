using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {

    public float speed = 6f;

    private Vector3 movement;


    private Rigidbody rb;


    private float vertical;
    private float horizontal;


	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
    void Update(){
     
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        vertical = Input.GetAxisRaw("Vertical");
        horizontal = Input.GetAxisRaw("Horizontal");
        Move(vertical, horizontal);
        Turn(vertical, horizontal);
	}


    void Move(float v, float h){
        //asignar la direccion, reutilizando el vector que tenemos.
        movement.Set(h, 0f,v);
        //Añadimos la velocidad con la que nos queremos mover y normalizamos el vector.
        movement = movement.normalized * speed * Time.deltaTime;
        //Movemos el character Controller
        if(GameManager.instance.playerMovement){
            rb.MovePosition(transform.position + movement);
        }

       

    }

    void Turn(float v, float h){

        //Rotamos el objeto de manera que siempre mire hacia la direccion en la que nos dirigimos
        Vector3 direction = new Vector3(h, 0f, v);

        //Preguntamos si el personaje cambia de direccion
        if(direction != Vector3.zero){
            //Definimos para donde debe rotar el personaje
            Quaternion newRotation = Quaternion.LookRotation(direction);
            //Asignamos la nueva rotacion
            if (GameManager.instance.playerMovement)
            {
                rb.rotation = newRotation;
            }

           
        }
    }

}
