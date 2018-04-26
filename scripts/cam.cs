using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cam : MonoBehaviour {
    [SerializeField] GameObject player;
    [SerializeField] float speed;
    [SerializeField] float timeScale;
    Transform _pTran;
    // Use this for initialization
    void Start () {
         _pTran = player.GetComponent<Transform>();
        Time.timeScale = timeScale;
    }

    // Update is called once per frame
    void FixedUpdate() {
        var targetPos = new Vector3(_pTran.position.x, _pTran.position.y, this.transform.position.z);
        var nextPos = Vector3.Lerp(this.transform.position, targetPos, speed*Time.deltaTime);
        transform.position = nextPos;
     }

}
