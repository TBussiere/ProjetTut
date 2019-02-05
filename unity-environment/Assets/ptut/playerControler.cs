using UnityEngine;
using System.Collections;
using System;
using Assets;

public class playerControler : MonoBehaviour {
    public bool Isgrounged;
    public int vie;
    private Rigidbody2D Rbody;
    private Animator anim;
    public Animator anim_gun;
    private float timereset;
    public int curPM;
    public int maxPM = 5;
    public GameObject Cells;
    private Weapon curWeapon;
    private Spell curSpell;

    internal Weapon CurWeapon
    {
        get
        {
            return curWeapon;
        }

        set
        {
            curWeapon = value;
        }
    }

    internal Spell CurSpell
    {
        get
        {
            return curSpell;
        }

        set
        {
            curSpell = value;
        }
    }

    // Use this for initialization
    void Start () {
		//print ("cc");
		curWeapon = new Pistoulaid();
		curSpell = new Flamiche();
	    Rbody = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponents<Animator>()[0];
        //anim_gun = GetComponentInChildren<Animator>();
		curPM = maxPM;

        pop_player.addPlayerCount();
		vie = 10;
		//transform.position= new Vector3(0.64f, 0.64f);
		//updateCell(cellManager.findCell (1, 1));
        
    }

    public void updateCell(GameObject cell)
    {

        this.Cells = cell;
    }

    // Update is called once per frame
    void FixedUpdate () {

        if (vie <= 0)
        {
            die();
        }


        if (Rbody.IsTouchingLayers())
        {
            Isgrounged = true;
        }
        else
        {
            Isgrounged = false;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            var nextcell = cellManager.findCell(Cells.GetComponent<ClicCell>().getX(), Cells.GetComponent<ClicCell>().getY()+1);
            cellManager.gotoNewCell(nextcell);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            var nextcell = cellManager.findCell(Cells.GetComponent<ClicCell>().getX()-1, Cells.GetComponent<ClicCell>().getY());
            cellManager.gotoNewCell(nextcell);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            var nextcell = cellManager.findCell(Cells.GetComponent<ClicCell>().getX()+1, Cells.GetComponent<ClicCell>().getY());
            cellManager.gotoNewCell(nextcell);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            var nextcell = cellManager.findCell(Cells.GetComponent<ClicCell>().getX(), Cells.GetComponent<ClicCell>().getY() - 1);
            cellManager.gotoNewCell(nextcell);
        }
        /*if (Input.GetKeyDown(KeyCode.Return))
        {
            FindObjectOfType<TurnManager>().validerTour();
        }*/
        /*
        if (Input.GetAxis("Horizontal") > 0.2f || Input.GetAxis("Horizontal") < -0.2f)
        {
            Rbody.velocity = new Vector2(vitesse * Input.GetAxis("Horizontal"), Rbody.velocity.y);
            anim.SetFloat("move", Mathf.Abs(Input.GetAxis("Horizontal")));
        }
        else
        {
            Rbody.velocity = new Vector2(0f, Rbody.velocity.y);
            anim.SetFloat("move", 0f);
        }


        if (Input.GetAxis("Horizontal") < -0.2)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (Input.GetAxis("Horizontal") > 0.2)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        
        if (Input.GetKeyDown(KeyCode.A))
        {
            Selected._curentCursorState = Selected.CursorState.MOVE;
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            Selected._curentCursorState = Selected.CursorState.SHOOT;
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            Selected._curentCursorState = Selected.CursorState.CAST;
        }
        */
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            //GameObject.Destroy(gameObject);
            die();
        }
        /*
        else if(Input.GetKeyDown(KeyCode.Space) && Isgrounged)
        {
            Rbody.velocity = new Vector2(0f, jumpH);
        }*/

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            anim_gun.SetBool("p", true);
            timereset = 0.05f;
        }

        if (timereset > 0)
        {
            timereset -= Time.deltaTime;
        }
        else if (timereset <= 0)
        {
            anim_gun.SetBool("p", false);
        }
       
	}

    public override string ToString()
    {
        return "player";
    }


    public void die()
    {
        pop_player.removePlayer(gameObject);// gameObject comme this
        
    }

    public void damageTaking(int dmg)
    {
        vie -= dmg;
    }

    public static void setTransform(playerControler pc,GameObject cell,int pm)
    {
		if (pc != null) {
			pc.transform.position = new Vector3 (cell.transform.position.x, cell.transform.position.y, 0);//newTrans.position
			pc.Rbody.velocity = Vector2.zero;
			pc.curPM -= pm; 
		}
    }
}
