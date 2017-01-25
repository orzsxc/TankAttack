using UnityEngine;
using System.Collections;

using UnityStandardAssets.Utility;


public class TankMove : MonoBehaviour {

    //表示坦克移动和旋转速度的变量
    public float moveSpeed = 20.0f;
    public float rotSpeed = 50.0f;

    //要分配各组件的变量
    private Rigidbody rbody;
    private Transform tr;

    private float h, v;

    private PhotonView pv = null;

    public Transform camPivot;

    //用来接收网络玩家的tank数据
    private Vector3 currPos = Vector3.zero;
    private Quaternion currRot = Quaternion.identity;


	// Use this for initialization
	void Awake () {
        rbody = GetComponent<Rigidbody>();
        tr = GetComponent<Transform>();
       

        pv = GetComponent<PhotonView>();

        //设置传输类型
        pv.synchronization = ViewSynchronization.UnreliableOnChange;

        //将PhotonView组件的Observed属性设置为TankMove脚本
        pv.ObservedComponents[0] = this;


        if (pv.isMine)
        {
            Camera.main.GetComponent<SmoothFollow>().target = camPivot;

            rbody.centerOfMass = new Vector3(0.0f, -0.5f, 0.0f);
        }
        else
        { 
            //如果是远程玩家的坦克，则设置使其不受物理力的影响
            rbody.isKinematic = true;
        }

        //设置网络玩家坦克位置和旋转值得初始值
        currPos = tr.position;
        currRot = tr.rotation;


	}

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    { 
        //传送本地坦克的位置和炮塔旋转信息
        if (stream.isWriting)
        {
            stream.SendNext(tr.position);
            stream.SendNext(tr.rotation);
        }
        else {
            currPos = (Vector3)stream.ReceiveNext();
            currRot = (Quaternion)stream.ReceiveNext();
        }
    }



	// Update is called once per frame
	void Update () {

        if (pv.isMine)
        {
            h = Input.GetAxis("Horizontal");
            v = Input.GetAxis("Vertical");

            //旋转和移动
            tr.Rotate(Vector3.up * rotSpeed * h * Time.deltaTime);
            tr.Translate(Vector3.forward * v * moveSpeed * Time.deltaTime);
        }
        else
        { 
            //将远程玩家的坦克平滑移动到目标位置
            tr.position = Vector3.Lerp(tr.position,currPos,Time.deltaTime*3.0f);
            tr.rotation = Quaternion.Slerp(tr.rotation, currRot, Time.deltaTime*3.0f);
        }




	}
}
