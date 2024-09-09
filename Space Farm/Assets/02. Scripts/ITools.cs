using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITools 
{
    public void Use();
    public void UnUse();

    public ToolState CurToolState();
}
