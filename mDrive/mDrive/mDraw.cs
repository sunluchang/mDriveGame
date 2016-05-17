using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace mDrive
{
    class mDraw : SpriteBatch
    {
        public mDraw(GraphicsDevice graphicsdevice):base(graphicsdevice)
        {
        }

        public void Draw(Road road)
        {
            int height = (int)Road.WindowsSize.Y;
            base.Draw(road.Source, new Rectangle((int)road.Position.X, (int)road.Position.Y, (int)road.Size.X, (int)road.Size.Y), Color.White);
            base.Draw(road.Source, new Rectangle((int)road.Position.X, (int)road.Position.Y - height, (int)road.Size.X, (int)road.Size.Y), Color.White);
        }

        public void Draw(Item item)
        {
            base.Draw(item.Source, new Rectangle((int)item.Position.X, (int)item.Position.Y, (int)item.Size.X, (int)item.Size.Y), Color.White);
        }

        internal void Draw(SpriteFont mFont, string disScore, Vector2 vector2, Color color)
        {
            throw new NotImplementedException();
        }
    }
}
