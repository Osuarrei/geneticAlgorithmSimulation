using Game1.Creatures;
using Game1.Entities;
using Game1.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Game1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        BasicEffect effect;
        Matrix world = Matrix.CreateTranslation(0, 0, 0);
        Matrix view = Matrix.CreateLookAt(new Vector3(0, 0, 3), new Vector3(0, 0, 0), new Vector3(0, 1, 0));
        Matrix projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), 800f / 480f, 0.01f, 100f);
        int mapWidth = (int)SimulationStateEnums.MapValues.MapWidthTiles;
        int mapHeight = (int)SimulationStateEnums.MapValues.MapHeightTiles;
        Map map;
        List<IBreedable> cowList;
        Random random = new Random();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            map = new Map(mapWidth, mapHeight);
            cowList = new List<IBreedable>();

            for (int i = 0; i < 20; i++)
            {
                cowList.Add(new Cow(random.Next(0, mapWidth), random.Next(0, mapHeight)));
            }

            graphics.PreferredBackBufferHeight = (int)SimulationStateEnums.WindowSize.Height;
            graphics.PreferredBackBufferWidth = (int)SimulationStateEnums.WindowSize.Width;
            graphics.ApplyChanges();

            effect = new BasicEffect(GraphicsDevice);

            effect.Projection = projection;
            effect.View = view;
            effect.World = world;
            effect.VertexColorEnabled = true;

            RasterizerState rasterizerState = new RasterizerState();
            rasterizerState.CullMode = CullMode.None;
            GraphicsDevice.RasterizerState = rasterizerState;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            for (int i = 0; i < cowList.Count; i++)
            {
                (cowList[i] as Interfaces.IUpdateable).Update(map);
                for (int j = 0; j < cowList.Count; j++)
                {
                    if (i != j)
                    {
                        if ((((cowList[i] as IMovable).xloc) == (cowList[j] as IMovable).xloc) && ((cowList[i] as IMovable).yloc == (cowList[j] as IMovable).yloc))
                        {
                            if (cowList[i].CanBreed && cowList[j].CanBreed)
                            {
                                cowList[i].BreedWith(cowList[j], (List<Interfaces.IBreedable>)cowList);
                            }
                        }
                    }
                }
            }

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            GraphicsDevice.Textures[0] = null;

            // TODO: Add your drawing code here
            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                using (SpriteBatch sb = new SpriteBatch(GraphicsDevice))
                {
                    sb.Begin();
                    map.Draw(sb);

                    foreach (var cow in cowList)
                    {
                        (cow as Interfaces.IDrawable).Draw(sb);
                    }

                    sb.End();
                }
            }

            base.Draw(gameTime);
        }
    }
}
