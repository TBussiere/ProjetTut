using UnityEngine;
using System.Collections;
using System;
using Assets;
using System.Collections.Generic;

public class OtherPlayerControler : Agent {
	public static int idmax=1;
	public int ida;
	//public Boolean validerCoups;
	public Boolean validerTour;
	public int compteurTour;
	public Boolean mort;
	public float[] mvts;
	public float[] coups;
	public override System.Collections.Generic.List<float> CollectState ()
	{
		List<float> state = cellManager.allCellsPos(ida);
		//print (state.Count);
		return state;
	}

    public float preReward = 0;

	public override void InitializeAgent ()
	{
		ida = idmax;
		idmax += 1;
		Rbody = gameObject.GetComponent<Rigidbody2D>();
		//anim = gameObject.GetComponents<Animator>()[0];
		//anim_gun = GetComponentInChildren<Animator>();
		//print ("other");
		pop_player.addPlayerCount();
		//print ("a");
		curPM = maxPM;
		vie = 10;
		if (ida == 1) {
			curWeapon = new Pistoulaid ();
		} else {
			curWeapon = new Marteau ();
		}
		curSpell = new Flamiche();
		transform.position = new Vector3(14.3f, 0.65f);
		//Debug.Log(this.transform.position);
		AgentReset();
	}
	public void deplacer(){
		bool retourCell=false;
		GameObject nextcell = Cells;
		int i = 0;
		while(mvts[i]!=0){
			//Debug.Log ("i " + i);
			if ((int)mvts [i] == 4 && Cells.GetComponent<ClicCell> ().getY ()<14) {
				nextcell = cellManager.findCell (Cells.GetComponent<ClicCell> ().getX (), Cells.GetComponent<ClicCell> ().getY () + 1);
				//Debug.Log("nextcell : " + nextcell);
				retourCell = cellManager.othergotoNewCell(nextcell,ida);
			} else if ((int)mvts [i] == 1 && Cells.GetComponent<ClicCell> ().getX ()>0) {
				nextcell = cellManager.findCell (Cells.GetComponent<ClicCell> ().getX () - 1, Cells.GetComponent<ClicCell> ().getY ());
				retourCell = cellManager.othergotoNewCell (nextcell,ida);
			} else if ((int)mvts [i] == 2 && Cells.GetComponent<ClicCell> ().getX ()<23) {
				nextcell = cellManager.findCell (Cells.GetComponent<ClicCell> ().getX () + 1, Cells.GetComponent<ClicCell> ().getY ());
				retourCell = cellManager.othergotoNewCell (nextcell,ida);
			} else if ((int)mvts [i] == 3 && Cells.GetComponent<ClicCell> ().getY ()>0) {
				nextcell = cellManager.findCell (Cells.GetComponent<ClicCell> ().getX (), Cells.GetComponent<ClicCell> ().getY () - 1);
				retourCell = cellManager.othergotoNewCell (nextcell,ida);
			}
			i = i + 1;
		}
	}
	public void frapper(){
		int i = 0;
		var players = FindObjectsOfType<OtherPlayerControler> ();
		OtherPlayerControler enemy;
		if (ida == 1) {
			enemy = players [1];
		} else {
			enemy = players [0];
		}
		var c2 = enemy.Cells.GetComponent<ClicCell>();
		int distance =Mathf.Abs (c2.getX () - Cells.GetComponent<ClicCell> ().getX ()) + Mathf.Abs(c2.getY() - Cells.GetComponent<ClicCell> ().getY ());
		if (distance <= curWeapon.range ) {
			while (coups [i] != 0 && curWeapon.cost<=curPM) {
				curPM -= curWeapon.cost;
				enemy.damageTaking (curWeapon.Damage);
				i = i + 1;
			}
		}
	}
	public override void AgentStep (float[] action)
	{
		if (!validerTour) {
			reward = -0.005f;
			if (cellManager.estARangeDeMechant (ida)) {
				reward = -0.01f;
			}
			if (cellManager.mechantEstARange (ida)) {
				reward = 0.01f;
			}
			if(nbMvt+nbCoup*curWeapon.cost<maxPM){
				if ((int)action [0] == 0 && nbMvt+nbCoup*curWeapon.cost+curWeapon.cost<maxPM) {
					coups [nbCoup] = 1;
					nbCoup += 1;
				}
				if ((int)action [0] >= 1 && (int)action [0] <= 4) {
					mvts [nbMvt] = action [0];
					nbMvt += 1;
				}
			}
			if ((int)action [0] == 5 || nbMvt+nbCoup*curWeapon.cost>=maxPM) {
				validerTour = true;
				done = true;
				TurnManager.otherValiderTour ();
			}
		}/*
		if (curPM > 0) {
			
			var players = FindObjectsOfType<OtherPlayerControler>();
			OtherPlayerControler player;
			if (ida == 1) {
				player = players [1];
			} else {
				player = players [0];
			}
			Debug.Log("player.ida "+player.ida);
			Debug.Log("ida "+ida);

			var c2 = player.Cells.GetComponent<ClicCell>();
			int distAv= Mathf.Abs (c2.getX () - Cells.GetComponent<ClicCell> ().getX ()) + Mathf.Abs(c2.getY() - Cells.GetComponent<ClicCell> ().getY ());
            bool retourCell=false;
			Debug.Log("distAv "+distAv);
			Debug.Log("AAAAAAAAAAAAAAAAAAA "+ida);
            GameObject nextcell = Cells;
			if ((int)action [0] == 0 && Cells.GetComponent<ClicCell> ().getY ()<14) {
				nextcell = cellManager.findCell (Cells.GetComponent<ClicCell> ().getX (), Cells.GetComponent<ClicCell> ().getY () + 1);
				Debug.Log("nextcell : " + nextcell);
				retourCell = cellManager.othergotoNewCell(nextcell,ida);
            } else if ((int)action [0] == 1 && Cells.GetComponent<ClicCell> ().getX ()>0) {
				nextcell = cellManager.findCell (Cells.GetComponent<ClicCell> ().getX () - 1, Cells.GetComponent<ClicCell> ().getY ());
                retourCell = cellManager.othergotoNewCell (nextcell,ida);
            } else if ((int)action [0] == 2 && Cells.GetComponent<ClicCell> ().getX ()<23) {
				nextcell = cellManager.findCell (Cells.GetComponent<ClicCell> ().getX () + 1, Cells.GetComponent<ClicCell> ().getY ());
                retourCell = cellManager.othergotoNewCell (nextcell,ida);
            } else if ((int)action [0] == 3 && Cells.GetComponent<ClicCell> ().getY ()>0) {
				nextcell = cellManager.findCell (Cells.GetComponent<ClicCell> ().getX (), Cells.GetComponent<ClicCell> ().getY () - 1);
                retourCell = cellManager.othergotoNewCell (nextcell,ida);
            }
			if (retourCell) {
				int distAp=Mathf.Abs (c2.getX () - nextcell.GetComponent<ClicCell> ().getX ()) + Mathf.Abs(c2.getY() - nextcell.GetComponent<ClicCell> ().getY ());
				if (distAv - distAp == 0) {
					reward = 0f;
				}
				if (distAv > distAp) {
					reward = 0.1f;
				}
				if (distAv < distAp) {
					reward = -0.05f;
				}
				if (distAp <= 1) {
					reward = 0.5f;
					preReward += reward;
					return;
				}
			} else {
				reward = -0.5f;
				curPM -= 1;
			}
			preReward += reward;
		} else {
			done = true;
			return;
		}*/
	}
	public int nbMvt;
	public int nbCoup;
	public override void AgentReset ()
	{
        //print ("fin");
		compteurTour=0;
       // Debug.Log("NEXT");
		//Debug.Log("preReward " + preReward);
	 	preReward = 0;
		curPM = maxPM;
		vie = 10;
		//mort = false;
		nbCoup = 0;
		nbMvt = 0;
		//validerCoups = false;
		validerTour=false;
		done = false;
		mvts = new float[maxPM+1];
		coups = new float[maxPM+1];
        //float x=UnityEngine.Random.RandomRange (1f, 24f);
        //float y=UnityEngine.Random.RandomRange (1f, 15f);
        int x=1,y=1;
        do{
			x=UnityEngine.Random.Range (1, 24);
		 	y=UnityEngine.Random.Range (1, 15);
		} while (!cellManager.findCell((int)x, (int)y).GetComponent<ClicCell>().crosable) ;


        Cells = cellManager.findCell((int)x,y).GetComponent<ClicCell>().gameObject;
		transform.position = new Vector3(x, y);
	}
	public void setReward(float reward){
		this.reward = reward;
	}
	public void finTour(){
		mvts = new float[maxPM+1];
		coups = new float[maxPM+1];
		nbCoup = 0;
		nbMvt = 0;
		curPM = maxPM;
		validerTour=false;
		done = false;
		compteurTour += 1;
	}

    //public float vitesse = 5.0f;
    //public float jumpH = 100f;
    public bool Isgrounged;

    public int vie;

    private Rigidbody2D Rbody;

    //private Animator anim;

    //public Animator anim_gun;

    private float timereset;

    public int curPM;
    public int maxPM;

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
  /*  void Start () {
	    Rbody = gameObject.GetComponent<Rigidbody2D>();
        //anim = gameObject.GetComponents<Animator>()[0];
        //anim_gun = GetComponentInChildren<Animator>();
		//print ("other");
        pop_player.addPlayerCount();
		//print ("a");
        curPM = maxPM;
		vie = 10;
        curWeapon = new Pistoulaid();
        curSpell = new Flamiche();
		transform.position = new Vector3(14.3f, 0.65f);
        Debug.Log(this.transform.position);
    }*/
    //void Start()
    //{
    //    //brain = FindObjectOfType<Brain>();
    //}

    public void updateCell(GameObject cell)
    {
		//Debug.Log ("this.Cells : " + this.Cells);
		//Debug.Log ("cell : " + cell);
        this.Cells = cell;
    }

    // Update is called once per frame
    
	void Update () {

        if (vie <= 0)
        {
			//print ("pv");
            die();
        }
        //Debug.Log(transform.position);
    }

    public override string ToString()
    {
        return "otherPlayer "+ida;
    }


    public void die()
    {
		//print ("creve");
		done=true;
		//pop_player.removePlayer(gameObject);// gameObject comme this
    }

    public void damageTaking(int dmg)
    {
        vie -= dmg;
    }

    public static void setTransform(OtherPlayerControler pc,GameObject cell,int pm)
    {
        pc.transform.position = new Vector3(cell.transform.position.x, cell.transform.position.y, 0);//newTrans.position
        pc.Rbody.velocity = Vector2.zero;
        pc.curPM -= pm;
		pc.updateCell(cell);
    }
}
