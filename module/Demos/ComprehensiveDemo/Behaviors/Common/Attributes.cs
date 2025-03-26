using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfiniteTree
{
    public abstract class Attributes : MonoBehaviour
    {
        public float Health = 100;
        public float Cash = 5;
        public int FoodItem = 0;
        public float MoveSpeed = 2.3f;

        public (int, int) GetPos => (Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z));
    }
}
