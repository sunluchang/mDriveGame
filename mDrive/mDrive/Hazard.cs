using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace mDrive
{
    class Hazard : Item
    {
        public Hazard(Vector2 p, float s, Vector2 si)
            : base(p, s, si)
        {
            Bomb = true;
        }

        public new Boolean Move(float sx, float sy)
        {
            base.Move(sx, sy);
            if (Position.Y >= 800 || Position.Y < -Size.Y)
                return true;
            return false;
        }
    }
}
