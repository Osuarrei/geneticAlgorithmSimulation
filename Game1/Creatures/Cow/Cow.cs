using Game1.Abstracts;
using Game1.Entities;
using Game1.InfoTransfer;
using Game1.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Game1.SimulationStateEnums;

namespace Game1.Creatures
{
    public class Cow : Creature
    {
        public Cow(int xloc, int yloc) : base(xloc, yloc)
        {
            cowColor = Color.Black;
        }

        private Color cowColor { get; set; }

        override public void Draw(GraphicsDevice graphicsDevice)
        {
            throw new NotImplementedException();
        }

        override public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawCircle(new CircleF(new Point(xloc * (int)SimulationStateEnums.MapValues.TileSize + ((int)SimulationStateEnums.MapValues.TileSize / 2),
                yloc * (int)SimulationStateEnums.MapValues.TileSize + ((int)SimulationStateEnums.MapValues.TileSize / 2)), (int)SimulationStateEnums.MapValues.TileSize * 0.5f),
                8, cowColor, 4.0f);
        }
    }
}
