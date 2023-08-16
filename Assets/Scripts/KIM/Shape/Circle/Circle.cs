using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Shape
{
    public class Circle
    {
        public Vector2 center;
        public float radius;
        public Circle(Vector2 center, float radius) 
        { 
            this.center = center;
            this.radius = radius;
        }

        public bool IsIn(Vector2 point)
        {
            if((this.center - point).sqrMagnitude > radius)
                return false;

            return true;
        }
    }
}
