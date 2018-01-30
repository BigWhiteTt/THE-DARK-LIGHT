using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour {
    //private static CursorManager _instance;
    //private static readonly object locker = new object();
    //private CursorManager() { }
    //public static CursorManager Instance()
    //{
    //    if (_instance == null)
    //    {
    //        lock(locker){

    //            if (_instance == null)
    //            {
    //                _instance = new CursorManager();
    //            }
    //        }
    //    }
    //    return _instance;
    //}
    public static CursorManager Instance;

    public Texture2D cursor_Normal;
    public Texture2D cursor_Talk;
    public Texture2D cursor_Attack;
    public Texture2D cursor_Pick;
    public Texture2D cursor_Lock;

    private CursorMode mode = CursorMode.Auto;
    private Vector2 hotpot = Vector2.zero;

    private void Start()
    {
        Instance = this;
    }
    public void SetNormal()
    {
        Cursor.SetCursor(cursor_Normal, hotpot, mode);
    }
    public void SetTalk()
    {
        Cursor.SetCursor(cursor_Talk, hotpot, mode);
    }
    public void SetAttack()
    {
        Cursor.SetCursor(cursor_Attack, hotpot, mode);
    }
    public void SetPick()
    {
        Cursor.SetCursor(cursor_Pick, hotpot, mode);
    }
    public void SetLock()
    {
        Cursor.SetCursor(cursor_Lock, hotpot, mode);
    }

}
