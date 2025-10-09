using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace MonoGame1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D _squareTexture;
        private List<Player> players;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 600;
            _graphics.ApplyChanges();

            players = new List<Player>
            {
                new Player(new Vector2(100, 100), Color.Red, Keys.W, Keys.S, Keys.A, Keys.D),
                new Player(new Vector2(400, 300), Color.Blue, Keys.Up, Keys.Down, Keys.Left, Keys.Right)
            };

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _squareTexture = new Texture2D(GraphicsDevice, 1, 1);
            _squareTexture.SetData(new[] { Color.White });


            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            foreach (var player in players)
                player.Update(players);
            base.Update(gameTime);

        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            _spriteBatch.Begin();
            foreach (var player in players)
                player.Draw(_spriteBatch, _squareTexture);
            _spriteBatch.End();
            
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
