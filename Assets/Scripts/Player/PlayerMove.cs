using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]
public class PlayerMove : MonoBehaviour
{
    CharacterController cc;

    [SerializeField]
    LabyrinthBuilder lb;

    [SerializeField]
    private float speed = 12f;

    private void Start()
    {
        cc = GetComponent<CharacterController>();
        transform.position = lb.PhysicPosition(Vector2Int.zero);
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        cc.Move(move * speed * Time.deltaTime);
    }
}
