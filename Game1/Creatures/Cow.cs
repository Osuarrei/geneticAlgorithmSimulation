using Game1.Entities;
using Game1.Interfaces.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Creatures
{
    public class Cow : Interfaces.Interfaces.IDrawable
    {
        public Cow(int xloc, int yloc)
        {
            this.xloc = xloc;
            this.yloc = yloc;
        }

        public void Draw(GraphicsDevice graphicsDevice)
        {
            throw new NotImplementedException();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawCircle(new CircleF(new Point(xloc*Tile.pixelwidth - (Tile.pixelwidth/2), yloc*Tile.pixelwidth - (Tile.pixelwidth/2)), Tile.pixelwidth* 0.5f), 8, Color.White, 4.0f);
        }

        public int xloc { get; set; }
        public int yloc { get; set; }
    }
}
