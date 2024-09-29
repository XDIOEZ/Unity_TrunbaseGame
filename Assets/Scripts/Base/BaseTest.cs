using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BaseTest : Singleton<BaseTest>
{
   public int x; 
}

public class Test1
{
    void Main()
    {
        BaseTest.Instance.x = 10;
        
    }
}
