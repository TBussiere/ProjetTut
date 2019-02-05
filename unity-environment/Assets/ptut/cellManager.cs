using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class cellManager : MonoBehaviour {

	public int X, Y;

    public GameObject prefab;

    public float div;

    public static Vector3 newPos;

    private static playerControler player;
	private static OtherPlayerControler otherplayer1;
	private static OtherPlayerControler otherplayer2;
    internal static bool cliked=false;
    public int id;

    public static GameObject[][] CellsTab = null; //= new GameObject[X][];

    //private bool start = true;


    // Use this for initialization
    void Start ()
    {
        initquibug();
    }

    private void initquibug()
    {
        CellsTab = new GameObject[X][];
        for (int i = 0; i < CellsTab.Length; i++)
        {
            CellsTab[i] = new GameObject[Y];
        }
        for (int i = 0; i < X; i++)
        {
            for (int j = 0; j < Y; j++)
            {
                CellsTab[i][j] = Instantiate(prefab);

                //GameObject[][] tempo = null;
                //tempo[i][j] = temp;


                CellsTab[i][j].transform.position = new Vector3(i * div, j * div);
                CellsTab[i][j].transform.SetParent(gameObject.transform);
                CellsTab[i][j].name = "" + i + ',' + j;
                //temp.GetComponent<ClicCell>().
                //var tempcc = temp.GetComponent<ClicCell>();//.askedUpdate = true;
                //tempcc.askedUpdate = true;
                //if ( ClicCell.distanceCells(tempcc, player.Cells.GetComponent<ClicCell>()) <= player.curPM)
                //{
                //    tempcc.reachable = true;
                //}
            }
        }
    }

    public static GameObject findCell(int x, int y)
    {
        return CellsTab[x][y];
    }

	public static List<float> allCellsPos(int id)
	{
		List<float> state = new List<float>();
		ClicCell c;
		ClicCell c2;
		if (id == 1) {
			 c = otherplayer1.Cells.GetComponent<ClicCell>();
			 c2 = otherplayer2.Cells.GetComponent<ClicCell>();
			state.Add (1);
			state.Add (otherplayer1.curPM/10f);
		} else {
			 c = otherplayer2.Cells.GetComponent<ClicCell>();
			 c2 = otherplayer1.Cells.GetComponent<ClicCell>();
			state.Add (-1);
			state.Add (otherplayer2.curPM/10f);
		}

		int x = c.getX ();
		int y = c.getY ();
		state.Add ((x - c2.getX ()) / 15f);
		state.Add ((y - c2.getY ()) / 24f);

		int xi=-1;
		int yi;
		while (xi <= 1) {
			yi = -1;
			while (yi <= 1) {
				yi = yi + 1;
				if (x+xi<0 ||x+xi>23 ||y+yi<1 ||y+yi>14 || !findCell (x +xi, y+yi).GetComponent<ClicCell>().crosable) {
					state.Add (1f);
				} else {
					state.Add (0f);
				}
			}
			xi=xi+1;
		}

		return state;
	}
	private static bool estSurTexture(int i,int j) // repond un peu n'importe quoi
	{
		RaycastHit2D[] hit = Physics2D.RaycastAll(new Vector2(i,j), Vector2.zero);

		if (hit.Length>0)
		{
			for ( i = 0; i < hit.Length; i++)
			{
				if (hit[i].collider.tag == "Mur")
				{
					print (i);
					Debug.Log(hit[i]);
					Debug.Log(hit[i].collider);
					Debug.Log(hit[i].collider.tag);
					return true;
				}
			}
		}
		return false;
	}


    // Update is called once per frame
    void Update()
    {
        if (pop_player.playerCount > 0)
        {
			
            player = FindObjectOfType<playerControler>();
			//print (player);

        }
		if (FindObjectsOfType<OtherPlayerControler> ().Length >= 2) {
			otherplayer1 = FindObjectsOfType<OtherPlayerControler> ()[0];
			otherplayer2 = FindObjectsOfType<OtherPlayerControler> ()[1];

		}
		reachGlobalUpdate (CellsTab);
        
    }

    public static void gotoNewCell(GameObject newCell)
    {
        var c = player.Cells.GetComponent<ClicCell>();
        var c2 = newCell.GetComponent<ClicCell>();

        int dist = ClicCell.distanceCells(c, c2);

        if (player.curPM >= dist)
        {
            if (c2.reachable && !c2.playerOnIt)
            {
                playerControler.setTransform(player, newCell, dist);
                reachGlobalUpdate(CellsTab);
            }
        }
    }
	public static bool estARangeDeMechant(int ida){
		ClicCell c;
		ClicCell c2;
		if (ida == 1) {
			c = otherplayer1.Cells.GetComponent<ClicCell>();
			c2=otherplayer2.Cells.GetComponent<ClicCell>();
		} else {
			c2=otherplayer1.Cells.GetComponent<ClicCell>();
			c = otherplayer2.Cells.GetComponent<ClicCell>();
		}
		int dist = ClicCell.distanceCells(c, c2);
		return dist < otherplayer2.CurWeapon.range;
	}
	public static bool mechantEstARange(int ida){
		ClicCell c;
		ClicCell c2;
		if (ida == 2) {
			c = otherplayer1.Cells.GetComponent<ClicCell>();
			c2=otherplayer2.Cells.GetComponent<ClicCell>();
		} else {
			c2=otherplayer1.Cells.GetComponent<ClicCell>();
			c = otherplayer2.Cells.GetComponent<ClicCell>();
		}
		int dist = ClicCell.distanceCells(c, c2);
		return dist < otherplayer2.CurWeapon.range;
	}
	public static bool othergotoNewCell(GameObject newCell, int ida)
	{
		
		ClicCell c;
		if (ida == 1) {
			 c = otherplayer1.Cells.GetComponent<ClicCell>();
		} else {
			 c = otherplayer2.Cells.GetComponent<ClicCell>();
		}
		var c2 = newCell.GetComponent<ClicCell>();
		//Debug.Log("c : " + c);
		//Debug.Log("c2.crosable : " + c2.crosable);
		//Debug.Log("ida : " + ida);
		int dist = ClicCell.distanceCells(c, c2);
	//	Debug.Log("dist : " + dist);
		if (otherplayer1.curPM >= dist && ida==1 && c2.crosable)
		{
			OtherPlayerControler.setTransform(otherplayer1, newCell, dist);
			reachGlobalUpdate(CellsTab);
			return true;
		}
		if (otherplayer2.curPM >= dist && ida==2  && c2.crosable)
		{
			OtherPlayerControler.setTransform(otherplayer2, newCell, dist);
			reachGlobalUpdate(CellsTab);
			return true;
		}
		return false;
	}
    public static bool canGotoNewCell(GameObject newCell)
    {
        if (Selected._curentCursorState == Selected.CursorState.MOVE)
        {
            var c = player.Cells.GetComponent<ClicCell>();
            var c2 = newCell.GetComponent<ClicCell>();

            int dist = ClicCell.distanceCells(c, c2);

            if (player.curPM >= dist)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else if (Selected._curentCursorState == Selected.CursorState.SHOOT)
        {
            var c = player.Cells.GetComponent<ClicCell>();
            var c2 = newCell.GetComponent<ClicCell>();

            int dist = ClicCell.distanceCells(c, c2);

            if (player.CurWeapon.range >= dist)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            var c = player.Cells.GetComponent<ClicCell>();
            var c2 = newCell.GetComponent<ClicCell>();

            int dist = ClicCell.distanceCells(c, c2);

            if (player.CurSpell.range >= dist)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public static void reachGlobalUpdate(GameObject[][] tab)
    {
        for (int i = 0; i < tab.Length; i++)
        {
            for (int j = 0; j < tab[i].Length; j++)
            {
                var tempcc = tab[i][j].GetComponent<ClicCell>();
                if (Selected._curentCursorState == Selected.CursorState.MOVE)
                {
					//print (tempcc);
					//print (player.curPM);
					//print (player.Cells.GetComponent<ClicCell>());


					if (player!=null&& player.Cells!=null&&ClicCell.distanceCells(tempcc, player.Cells.GetComponent<ClicCell>()) <= player.curPM)
                    {
                        tempcc.reachable = true;
                    }
                    else
                    {
                        tempcc.reachable = false;
                    }
                }
                else if (Selected._curentCursorState == Selected.CursorState.SHOOT)
                {
                    if (ClicCell.distanceCells(tempcc, player.Cells.GetComponent<ClicCell>()) <= player.CurWeapon.range)
                    {
                        tempcc.reachable = true;
                    }
                    else
                    {
                        tempcc.reachable = false;
                    }
                }
                else if (Selected._curentCursorState == Selected.CursorState.CAST)
                {
                    if (ClicCell.distanceCells(tempcc, player.Cells.GetComponent<ClicCell>()) <= player.CurSpell.range)
                    {
                        tempcc.reachable = true;
                    }
                    else
                    {
                        tempcc.reachable = false;
                    }
                }
                tempcc.askedUpdate = true;
            }
        }
    }
}
