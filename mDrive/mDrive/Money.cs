using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace mDrive
{
    class Money : Item
    {
        public int Value;

        public Money(Vector2 p, float s, Vector2 si, int val)
            : base(p, s, si)
        {
            Value = val;
            Bomb = false;
        }

        public new Boolean Move(float sx, float sy)
        {
            base.Move(sx, sy);
            if (Position.Y >= 800)
                return true;
            return false;
        }
    }
}
