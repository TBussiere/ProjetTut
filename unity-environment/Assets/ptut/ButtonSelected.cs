using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonSelected : MonoBehaviour {

    public Image[] imgs;

	// Use this for initialization
	void Start () {
        for (int i = 0; i < imgs.Length; i++)
        {
            imgs[i].gameObject.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update () {

        switch (Selected._curentCursorState)
        {
            case Selected.CursorState.MOVE:
                imgs[0].gameObject.SetActive (true);
                imgs[1].gameObject.SetActive (false);
                imgs[2].gameObject.SetActive (false);
                break;
            case Selected.CursorState.SHOOT:
                imgs[1].gameObject.SetActive (true);
                imgs[0].gameObject.SetActive (false);
                imgs[2].gameObject.SetActive (false);
                break;
            case Selected.CursorState.CAST:
                imgs[2].gameObject.SetActive (true);
                imgs[1].gameObject.SetActive (false);
                imgs[0].gameObject.SetActive (false);
                break;
            case Selected.CursorState.INSPECT:
                imgs[0].gameObject.SetActive (false);
                imgs[1].gameObject.SetActive (false);
                imgs[2].gameObject.SetActive (false);
                break;
            default:
                break;
        }
    }

    void OnGUI()
    {
        if (Event.current.isKey)
        {
            if (Event.current.Equals(Event.KeyboardEvent("A")))
            {
                Selected._curentCursorState = Selected.CursorState.MOVE;
            }
            else if (Event.current.Equals(Event.KeyboardEvent("Z")))
            {
                Selected._curentCursorState = Selected.CursorState.SHOOT;
            }
            else if (Event.current.Equals(Event.KeyboardEvent("E")))
            {
                Selected._curentCursorState = Selected.CursorState.CAST;
            }
            else if (Event.current.Equals(Event.KeyboardEvent("return")))
            {
                TurnManager.Passer = true;
            }
        }
    }
}
