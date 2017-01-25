using UnityEngine;
using System.Collections;

public class Cannon : MonoBehaviour {

    public float speed = 6000.0f;

    public GameObject expEffect;

    private CapsuleCollider _collider;

    private Rigidbody _rigidbody;

    private bool isTrigger = false;

	// Use this for initialization
	void Start () {

        _collider = GetComponent<CapsuleCollider>();
        _rigidbody = GetComponent<Rigidbody>();

        GetComponent<Rigidbody>().AddForce(transform.forward * speed);

        //3秒后执行自动爆炸的协程函数
        StartCoroutine("ExplositionCannon",3.0f);

	}

    void OnTriggerEnter()
    {

        StartCoroutine("ExplositionCannon",0.0f);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator ExplositionCannon(float tm)
    {
        yield return new WaitForSeconds(tm);

        if (isTrigger)
        {
            yield return null;
        }
        else {

            isTrigger = true;

            //禁用Collider组件
            _collider.enabled = false;

            //无需再受物理引擎影响
            _rigidbody.isKinematic = true;

            //动态生成爆炸预设
            GameObject obj = (GameObject)Instantiate(expEffect, transform.position, Quaternion.identity);

            Destroy(obj, 1.0f);

            //Trail Renderer消失并等待一段时间后删除炮弹
            Destroy(this.gameObject, 1.0f);     
   

        }




    }

}
