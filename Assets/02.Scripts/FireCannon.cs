using UnityEngine;
using System.Collections;

public class FireCannon : MonoBehaviour {

    public GameObject cannon = null;

    public Transform firePos;

    private AudioClip fireSfx = null;

    private AudioSource sfx;

    void Awake()
    { 
        //加载Resource文件夹下的Cannon预设
        cannon = (GameObject)Resources.Load("Cannon");

        fireSfx = Resources.Load<AudioClip>("CannonFire");

        sfx = GetComponent<AudioSource>();
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }

	}

    void Fire()
    {
        sfx.PlayOneShot(fireSfx, 1.0f);

        Instantiate(cannon, firePos.position, firePos.rotation);
    }
}
