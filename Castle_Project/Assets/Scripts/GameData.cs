using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
    public const float stint = 0.7f;        //力道超過這個數值，就一定會抵達對面

    public class ThrowData
    {
        public float gravity { get; set; }
        public float h { get; set; }
        public ThrowData()
        {
            this.gravity = -18f;
            this.h = 10f;
        }
    }
}
