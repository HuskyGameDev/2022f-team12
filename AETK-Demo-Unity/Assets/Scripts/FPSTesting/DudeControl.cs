using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DudeControl : MonoBehaviour
{
    public float MoveSpeed = 2;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var h = Input.GetAxisRaw("Move_Horizontal");

        var pos = transform.position;

        pos.x += h * MoveSpeed * Time.deltaTime;

        transform.position = pos;
    }
}
