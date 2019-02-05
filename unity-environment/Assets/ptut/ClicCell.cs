using UnityEngine;
using System.Collections;

public class ClicCell : MonoBehaviour {

    private SpriteRenderer spriteR;


    public bool crosable=true;
    public GameObject playerOnIt;
    public bool reachable;
    public bool askedUpdate;
    cellManager mng;
    public bool CellHovered = false; 

    //public float posX,posY
    //private cellManager cM;
	public override string ToString ()
	{
		return "" + getX() + " " + getY ();
	}
    void Start()
    {
        spriteR = gameObject.GetComponent<SpriteRenderer>();
        spriteR.color = new Color(0, 0, 0);

        crosable = !estSurTexture();
        transform.position = new Vector3(transform.position.x, transform.position.y, -1);

        askedUpdate = true;
        //cM = FindObjectOfType<cellManager>();
    }
	void  Update()
    {
        if (crosable == false)
        {
            reachable = false;
        }
        if (askedUpdate)
        {
            updateColor();
            askedUpdate = false;
        }
        playerOnIt = detectPlayer();
        if (playerOnIt != null)
        {
			
            playerControler temp;

            try
            {
                temp = playerOnIt.GetComponent<playerControler>();
                temp.updateCell(this.gameObject);
            }
            catch (System.Exception)
            {

				OtherPlayerControler temp2 = playerOnIt.GetComponent<OtherPlayerControler>();
                temp2.updateCell(this.gameObject);
            }
            
            

            
        }
    }
    void OnMouseEnter()
    {
        CellHovered = true;
        updateColor();
    }

    void OnMouseOver()
    {
        updateColor();
    }

    void OnMouseExit()
    {
        //resetColor();
        CellHovered = false;

        //askedUpdate = true;
        updateColor();
    }

    void OnMouseUp()
    {
        if (Selected._curentCursorState == Selected.CursorState.SHOOT)
        {
            if (playerOnIt != null)
            {
                var player = FindObjectOfType<playerControler>();
                if (player.curPM >= player.CurWeapon.cost)
                {
                    playerOnIt.GetComponent<OtherPlayerControler>().damageTaking(player.CurWeapon.Damage);
                    //play the anim
                    player.curPM -= player.CurWeapon.cost;
                }
            }
        }
        else if (Selected._curentCursorState == Selected.CursorState.MOVE)
        {
            if (crosable && cellManager.canGotoNewCell(gameObject))
            {
                cellManager.gotoNewCell(gameObject);

            }            
        }
        else if (Selected._curentCursorState == Selected.CursorState.CAST)
        {
            if (playerOnIt != null)
            {
                var player = FindObjectOfType<playerControler>();
                if (player.curPM >= player.CurSpell.cost)
                {
                    playerOnIt.GetComponent<OtherPlayerControler>().damageTaking(player.CurSpell.Damage);
                    //play the anim
                    player.curPM -= player.CurSpell.cost;
                }
            }
        }
    }


    void updateColor()
    {
        /*
        if (playerOnIt != null)
        {
            spriteR.color = new Color(255, 255, 255);
        }
        else if (crosable && cellManager.canGotoNewCell(gameObject))
        {
            switch (Selected._curentCursorState)
            {
                case Selected.CursorState.MOVE:
                    spriteR.color = new Color(0, 255, 0);
                    break;
                case Selected.CursorState.SHOOT:
                    spriteR.color = new Color(0, 0, 255);
                    break;
                case Selected.CursorState.CAST:
                    spriteR.color = new Color(0, 255, 255);
                    break;
                //case Selected.CursorState.INSPECT:
                //    spriteR.color = new Color(255, 255, 255);
                //    break;
                default:
                    break;
            }
        }
        else
        {
            spriteR.color = new Color(255, 0, 0);
        }*/
        if (CellHovered)//pointe la case
        {
            if (crosable && cellManager.canGotoNewCell(gameObject))
            {
                if (Selected._curentCursorState == Selected.CursorState.MOVE)//(0, 255, 0)
                {
                    spriteR.color = new Color(0, 255, 0);
                }
                else if (Selected._curentCursorState == Selected.CursorState.SHOOT)//(0, 0, 255)
                {
                    spriteR.color = new Color(0, 0, 255);
                }
                else if (Selected._curentCursorState == Selected.CursorState.CAST)//(0, 255, 255)
                {
                    spriteR.color = new Color(0, 255, 255);
                }

            }
            else//rouge (255,0,0)
            {
                spriteR.color = new Color(255, 0, 0);
            }
            if (playerOnIt != null)//over write ce qui est au dessus pour l'inspector (255,255,255)
            {
                if (Selected._curentCursorState == Selected.CursorState.MOVE)
                {
                    spriteR.color = new Color(255, 255, 255);
                }
                else
                {
                    spriteR.color = new Color(255, 255, 0);
                }
                
            }
        }
        else//on fait un beau reset
        {
            if (reachable)//(255,255,255)
            {
                spriteR.color = new Color(255, 255, 255);
            }
            else//(0,0,0)
            {
                spriteR.color = new Color(0, 0, 0);
            }
        }


    }

    private bool estSurTexture()
    {
        RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, Vector2.zero);
        if (hit.Length>0)
        {
            for (int i = 0; i < hit.Length; i++)
            {
                if (hit[i].collider.tag == "Mur")
                {
                    return true;
                }
            }
        }
        return false;
    }
    private GameObject detectPlayer()
    {
        RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, Vector2.zero);
        if (hit.Length > 0)
        {
            for (int i = 0; i < hit.Length; i++)
            {
                if (hit[i].collider.tag == "Player")
                {
                    return hit[i].transform.gameObject;//GetComponent<playerControler>();
                }
                else if (hit[i].collider.tag == "otherPlayer")
                {
                    return hit[i].transform.gameObject;//GetComponent<playerControler>();
                }
            }
        }
        return null;
    }

    public int getX()
    {
        string[] coord = gameObject.name.Split(',');
        int res;
        int.TryParse(coord[0],out res);
        return res;
    }
    public int getY()
    {
        string[] coord = gameObject.name.Split(',');
        int res;
		//print (coord);
        int.TryParse(coord[1], out res);
        return res;
    }
    public static int distanceCells(ClicCell cell1 , ClicCell cell2)
    {
        int dist = 0;

		dist = Mathf.Abs (cell2.getX () - cell1.getX ()) + Mathf.Abs(cell2.getY() - cell1.getY());

        return dist;
             
    }

    /*public void resetColor()
    {
        spriteR.color = new Color(0, 0, 0);
    }*/
}
