using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace mDrive
{
    class Road : Item
    {
        public Road(Vector2 p, int s, Vector2 si)
            : base(p, s,si)
        {
            Bomb = false;
        }

        public new void Move(float sx, float sy)
        {
            base.Move(sx, sy);
            if (Position.Y >= WindowsSize.Y)
                Position.Y = 0 ;
        }

    }
}
