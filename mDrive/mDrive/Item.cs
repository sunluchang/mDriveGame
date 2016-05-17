using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace mDrive
{
    class Item
    {
        public static readonly Vector2 WindowsSize = new Vector2(960, 720); 
        public Vector2 Position,Size;
        public float Speed;
        public Texture2D Source;
        public Boolean Bomb;
        public Item(Vector2 p, float s,Vector2 si)
        {
            Position = p;
            Speed = s;
            Size = si;
            Source = null;
        }

        public void Add(Texture2D so)
        {
            Source = so;
        }

        public void Move(float sx,float sy)
        {
            this.Position.X = Position.X + sx;
            this.Position.Y = Position.Y + sy - Speed;
        }

        protected float abs(float a)
        {
            if (a < 0)
                return -a;
            return a;
        }
        protected float min(float a, float b)
        {
            if (abs(a) > abs(b))
                return b;
            return a;
        }
    }
}
