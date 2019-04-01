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
    public partial class Cow
    {
        public void Draw(GraphicsDevice graphicsDevice)
        {
            throw new NotImplementedException();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawCircle(new CircleF(new Point(xloc * (int)SimulationStateEnums.MapValues.TileSize - ((int)SimulationStateEnums.MapValues.TileSize / 2),
                yloc * (int)SimulationStateEnums.MapValues.TileSize - ((int)SimulationStateEnums.MapValues.TileSize / 2)), (int)SimulationStateEnums.MapValues.TileSize * 0.5f),
                8, cowColor, 4.0f);
        }
    }
}
