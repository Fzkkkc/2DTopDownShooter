using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GunType gunType;

    public float offset;

    public GameObject fireball;
    public Transform shotPoint;
    public Joystick joystick;

    private float timeBtwShots;
    public float startTimeBtwShots;

    public Animator anim;
    private Animator camAnim;

    

    public enum GunType{Default, Enemy}

    private Player player;
    private Vector3 difference;
    private float rotZ;

    private void Start()
    {
        camAnim = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (player.controlType == Player.ControlType.PC && gunType == GunType.Default)
        {
            joystick.gameObject.SetActive(false);
        }
    }


    void Update()
    {
        if (gunType == GunType.Default)
        {
            if (player.controlType == Player.ControlType.PC)
            {
                difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            }
            else if (player.controlType == Player.ControlType.Android && Mathf.Abs(joystick.Horizontal) > 0.3f || Mathf.Abs(joystick.Vertical) > 0.3f)
            {
                rotZ = Mathf.Atan2(joystick.Vertical, joystick.Horizontal) * Mathf.Rad2Deg;
            }
        }
        else if (gunType == GunType.Enemy)
        {
            difference = player.transform.position - transform.position;
            rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        }

        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

        if (timeBtwShots <= 0)
        {
            if (Input.GetMouseButton(0) && player.controlType == Player.ControlType.PC || gunType == GunType.Enemy )
            {
                Shoot();               
            }
            else if (player.controlType == Player.ControlType.Android)
            {
                if (joystick.Horizontal !=0 || joystick.Vertical!=0)
                {
                    Shoot();                    
                }
            }
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }

    public void Shoot()
    {
        Instantiate(fireball, shotPoint.position, shotPoint.rotation);
        timeBtwShots = startTimeBtwShots;
        camAnim.SetTrigger("shake");
        anim.SetTrigger("attack");
    }

}
