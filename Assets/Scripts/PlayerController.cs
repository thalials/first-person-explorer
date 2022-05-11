using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   float _baseSpeed = 10.0f;
    // float _gravidade = 9.8f;

    //Referência usada para a câmera filha do jogador
   GameObject playerCamera;  
   //Utilizada para poder travar a rotação no angulo que quisermos.
   float cameraRotation;  

   CharacterController characterController;
   
   void Start()
   {
        characterController = GetComponent<CharacterController>();
        playerCamera = GameObject.Find("Main Camera");
        cameraRotation = 0.0f;
   }

   void Update()
   {
       float x = Input.GetAxis("Horizontal");
       float z = Input.GetAxis("Vertical");

       //Verificando se é preciso aplicar a gravidade
    //    float y = 0;
    //    if(!characterController.isGrounded){
    //        y = _gravidade;
    //    }
    //Tratando movimentação do mouse
       float mouse_dX = Input.GetAxis("Mouse X");
       float mouse_dY = Input.GetAxis("Mouse Y");

       //Tratando a rotação da câmera
    //    cameraRotation += mouse_dY;
    //    cameraRotation = Mathf.Clamp(cameraRotation, 0.0f, 0.0f);

       Vector3 direction = new Vector3(-x, 0, -z);
        // Vector3 direction = transform.right * (x) + transform.up * 0 + transform.forward * (z);
       characterController.Move(direction * _baseSpeed * Time.deltaTime);
       transform.Rotate(Vector3.up, mouse_dX);

    //    playerCamera.transform.localRotation = Quaternion.Euler(cameraRotation, 0.0f, 0.0f);
   }
}