using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Net;

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
                player.Update(players, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
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

    public class Player
    {
        public Vector2 Position;
        public Color Color;
        public int Size = 50;
        public float Speed = 3f;

        private Keys up, down, left, right;

        public Player(Vector2 position, Color color, Keys up, Keys Down, Keys Left, Keys Right)
        {
            Position = position;
            Color = color;
            this.up = up;
            this.down = down;
            this.left = left;
            this.right = right;
        }

        public Rectangle BoundingBox => new Rectangle((int)Position.X, (int)Position.Y, Size, Size);

        public void Update(List<Player> players, int screenWidth, int screenHeight)
        {
            Vector2 oldPos = Position;
            KeyboardState ks = Keyboard.GetState();

            Vector2 move = Vector2.Zero;

            if (ks.IsKeyDown(up)) move.Y -= Speed;
            if (ks.IsKeyDown(down)) move.Y += Speed;
            if (ks.IsKeyDown(left)) move.X -= Speed;
            if (ks.IsKeyDown(right)) move.X += Speed;

            Position.X += move.X;
            foreach (var other in players)
            {
                if (other == this) continue;
                if (BoundingBox.Intersects(other.BoundingBox))
                    Position.X = oldPos.X;
            }

            if (Position.X < 0) Position.X = 0;
            if (Position.Y < 0) Position.Y = 0;
            if (Position.X > screenWidth - Size) Position.X = screenWidth - Size;
            if (Position.Y > screenHeight - Size) Position.Y = screenHeight - Size;
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D texture)
        {
            spriteBatch.Draw(texture, BoundingBox, Color);






        }
    }
}
