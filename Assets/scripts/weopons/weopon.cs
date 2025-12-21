using System.Collections.Generic;

using UnityEngine;

public class weopen : MonoBehaviour
{
    public int weoponlevel;
    public List <Weoponstats> stats;

    [System.Serializable]
    public class Weoponstats
    {
        public float speed;
        public int damage;
        public float size;
        public float amount;
        public float range;
    }

}
