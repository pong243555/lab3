using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPlayerBehaviour : MonoBehaviour
{
    public float speed = 1.0f;
    
    public WeaponBehaviour[] weapons ;
    public int arraySize;
    

    // Start is called before the first frame update
    void Start()
    {
        References.thePlayer = gameObject;
        weapons = new WeaponBehaviour[10];
        for (int index = 0; index<arraySize;index++){
            weapons[index] = new WeaponBehaviour();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //WASD to move
        Vector3 inputVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Rigidbody ourRigidBody = GetComponent<Rigidbody>();
        ourRigidBody.velocity = inputVector * speed;
        //
        Ray rayFromCameraToCursor = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        playerPlane.Raycast(rayFromCameraToCursor, out float distanceFromCamera);
        Vector3 cursorPosition = rayFromCameraToCursor.GetPoint(distanceFromCamera);
        //Face the new position
        Vector3 lookAtPosition = cursorPosition;
        transform.LookAt(lookAtPosition);
        if (Input.GetButton("Fire1"))
        {
            //Tell our weapon to fire
            weapons[0].Fire(cursorPosition);
        }
        //ทำการเปลี่ยนอาวุธกดปุ่มเมาขวา
    }
    private void OnTriggerEnter(Collider other)
    {
        WeaponBehaviour theirWeapon = other.GetComponentInParent<WeaponBehaviour>();
        if (theirWeapon != null)
        {
            for(int index = 0; index < weapons.Length; index++){
                if (weapons[index]==null)
                weapons[index]=theirWeapon;
            }

            //Add it to our internal list
            
            weapons[0] = theirWeapon;
            //Move it to our location
            theirWeapon.transform.position = transform.position;
            theirWeapon.transform.rotation = transform.rotation;
            //Parent it to us - attach it to us, so it moves with us
            theirWeapon.transform.SetParent(transform);
            //Select it!
           // ChangeWeaponIndex(weapons.Count - 1);
        }
    }
    

}
