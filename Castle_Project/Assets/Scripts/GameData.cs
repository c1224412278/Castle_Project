using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
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
