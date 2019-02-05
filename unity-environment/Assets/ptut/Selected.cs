using UnityEngine;
using System.Collections;

public class Selected : MonoBehaviour {

        public enum CursorState {MOVE,SHOOT,CAST,INSPECT};

        public static CursorState _curentCursorState = CursorState.MOVE;


    public void onButtonClicked(int id)
    {
        Debug.Log("switch");
        if (id == 0)
        {
            _curentCursorState = CursorState.MOVE;
        }
        else if (id == 1)
        {
            _curentCursorState = CursorState.SHOOT;
        }
        else if (id == 2)
        {
            _curentCursorState = CursorState.CAST;
        }
        else
        {
            _curentCursorState = CursorState.INSPECT;
        }
    }

    public void newbuttonClicked(int id)
    {
        Debug.Log("switch");
        if (id == 0)
        {
            _curentCursorState = CursorState.MOVE;
        }
        else if (id == 1)
        {
            _curentCursorState = CursorState.SHOOT;
        }
        else if (id == 2)
        {
            _curentCursorState = CursorState.CAST;
        }
        else
        {
            _curentCursorState = CursorState.INSPECT;
        }
    }
}
