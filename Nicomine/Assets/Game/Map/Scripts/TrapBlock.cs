using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapBlock : Block
{
    public int Damage = 0;

    public override void OnBlockBreak()
    {
        base.OnBlockBreak();
        Debug.Log("ITS A TRAP");
    }
}
