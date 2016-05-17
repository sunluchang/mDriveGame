using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace mDrive
{
    class Car : Item
    {
        private float Mins, Acc;
        public float Maxs;
        public Car(Vector2 p, float s, float maxs, int Acc, Vector2 si)
            : base(p, s, si)
        {
            this.Maxs = maxs;
            this.Speed = s;
            this.Acc = (float)Acc / 100;
            this.Mins = (int)this.Speed;
            this.Bomb = true;
        }

        public void Higher()
        {
            if (Speed + Acc < Maxs)
                Speed += Acc;
            else Speed = Maxs;
        }

        public void Lower()
        {
            if (Speed - Acc > Mins)
                Speed -= Acc;
            else Speed = Mins;
        }

        public new void Move(float sx, float sy)
        {
            base.Move(sx * Acc * 100, Speed);
            if (Position.X < 10)
                Position.X = 10;
            if (Position.X > Item.WindowsSize.X - Size.X - 10)
                Position.X = Item.WindowsSize.X - Size.X - 10;

        }
        public float Move()
        {
            return Speed;
        }
    }
}
