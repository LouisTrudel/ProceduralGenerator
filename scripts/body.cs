using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class body : MonoBehaviour {

    Rigidbody _rigidBody;
    [SerializeField] float speed;
    [SerializeField] float dashForce;
    [SerializeField] float turnSpeed;
    Animator _anim;
    public bool _canMove;//
	void Start () {
        _rigidBody = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
        _canMove = true;
	}
	
    public void move(Vector2 input)
    {
        input.Normalize();
        if (_canMove)
        {
            if (_rigidBody.velocity.magnitude != (input * speed).magnitude)
                _rigidBody.velocity = input * speed;
            if (input != Vector2.zero)
                _anim.SetBool("walk", true);
            else
                _anim.SetBool("walk", false);
        }
        else
            _anim.SetBool("walk", false);
       
    }
    public void setRotation(Vector2 dir)
    {
        dir.Normalize();
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg+180;
        this.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
    public void lerpRotation(float targetAngle)
    {
       this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation,
                                                          Quaternion.Euler(0, 0, targetAngle),                                                         turnSpeed);
    }
    public void dashAttack(Vector2 dir)
    {
        StartCoroutine(attack());
        StartCoroutine(dash(dir));
        
    }
    public IEnumerator dash(Vector2 dir)
    {
        if (_canMove)
        {
            _canMove = false;
            _rigidBody.velocity = dir.normalized*dashForce;
            yield return new WaitUntil(() => _rigidBody.velocity.magnitude <= speed);
            _canMove = true;
        }           
    }

    public IEnumerator attack()
    {       
        if (!_anim.GetBool("attack"))
        {
            _anim.SetBool("attack", true);
            yield return new WaitForSeconds(0.1f);
            _anim.SetBool("attack", false);
        }           
    }
}
