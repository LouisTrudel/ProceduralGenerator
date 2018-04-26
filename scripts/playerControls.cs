using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControls : MonoBehaviour {

    body _body;
    Camera _cam;
    private void Start()
    {
        _cam = FindObjectOfType<Camera>();
        _body = GetComponent<body>();
    }
    private void FixedUpdate()
    {
        var input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 mouseDir = this.transform.position - _cam.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetButtonDown("Jump"))
            _body.StartCoroutine(_body.dash(input));
        if (Input.GetMouseButtonDown(0))
            _body.StartCoroutine(_body.attack());
        _body.move(input);
        _body.setRotation(mouseDir);
        
     
    }
}

