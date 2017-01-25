using UnityEngine;
using System.Collections;

public class TrackAnim : MonoBehaviour {

    //纹理旋转速度
    private float scrollSpeed = 1.0f;

    private Renderer _renderer;


	// Use this for initialization
	void Start () {

        _renderer = GetComponent<Renderer>();

	}
	
	// Update is called once per frame
	void Update () {

        var offset = Time.time * scrollSpeed * Input.GetAxisRaw("Vertical");

        //更改默认的Y偏移值
        _renderer.material.SetTextureOffset("_MainTex",new Vector2(0,offset));

        //更改常规纹理的Y偏移量值
        _renderer.material.SetTextureOffset("_BumpMap",new Vector2(0,offset));

	}
}
