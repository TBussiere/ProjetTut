using UnityEngine;
using System.Collections;

public class camControl : MonoBehaviour {


    public playerControler player;
    public Camera cam;

    public Vector3 mousePos;
    public float dragSpeed;
    // Use this for initialization
    void Start () {
        player = GetComponentInParent<playerControler>();
        gameObject.transform.SetParent(null);
	}
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
        }
        if (Input.GetMouseButtonDown(2))
        {
            mousePos = Input.mousePosition;
        }

        if (Input.GetMouseButton(2))
        {
            Vector3 delta = Input.mousePosition - mousePos;
            transform.Translate(-delta.x * dragSpeed, -delta.y * dragSpeed, 0);
            mousePos = Input.mousePosition;
        }



        /*if (pop_player.playerCount > 0)
        {
            player = FindObjectOfType<playerControler>();
            cam = FindObjectOfType<Camera>();
        }
        

        if (Input.GetMouseButtonDown(0))
        {
            mousePos = Input.mousePosition;
            return;
        }

        if (!Input.GetMouseButton(0)) return;

        Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mousePos);
        Vector3 move = new Vector3(-pos.x * dragSpeed,-pos.y * dragSpeed,0);

        //transform.position = move;
        transform.Translate(move, Space.World);*/
    }

}
