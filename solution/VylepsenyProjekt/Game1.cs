﻿using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace VylepsenyProjekt
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D _squareTexture;
        private List<Player> players;
        private List<Tlacitko> tlacitka;
        private List<Dvere> dvere;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 600;
            _graphics.ApplyChanges();

            players = new List<Player>
            {
                new Player(new Vector2(100, 100), Color.Red, Keys.W, Keys.S, Keys.A, Keys.D),
                new Player(new Vector2(400, 300), Color.Blue, Keys.Up, Keys.Down, Keys.Left, Keys.Right)
            };

            tlacitka = new List<Tlacitko>();
            dvere = new List<Dvere>();

            var dvere1 = new Dvere(new Vector2(50, 250), 20, 100, Color.DarkGreen);
            var button1 = new Tlacitko(new Vector2(650, 500), 40, Color.Yellow, dvere1);
            dvere.Add(dvere1);
            tlacitka.Add(button1);

            var dvere2 = new Dvere(new Vector2(730, 150), 20, 100, Color.DarkRed);
            var button2 = new Tlacitko(new Vector2(150, 50), 40, Color.Orange, dvere2);
            dvere.Add(dvere2);
            tlacitka.Add(button2);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _squareTexture = new Texture2D(GraphicsDevice, 1, 1);
            _squareTexture.SetData(new[] { Color.White });
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            foreach (var tlacitko in tlacitka)
                tlacitko.Update(players);

            foreach (var player in players)
                player.Update(players, dvere, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightGray);

            _spriteBatch.Begin();

            foreach (var dv in dvere)
                dv.Draw(_spriteBatch, _squareTexture);

            foreach (var tlacitko in tlacitka)
                tlacitko.Draw(_spriteBatch, _squareTexture);

            foreach (var player in players)
                player.Draw(_spriteBatch, _squareTexture);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }

    public class Tlacitko
    {
        public Vector2 Position;
        public int Size;
        public Color Color;
        public bool IsPressed;
        private Dvere PripojeneDvere;

        public Tlacitko(Vector2 position, int size, Color color, Dvere dvere)
        {
            Position = position;
            Size = size;
            Color = color;
            PripojeneDvere = dvere;
            IsPressed = false;
        }

        public Rectangle BoundingBox => new Rectangle((int)Position.X, (int)Position.Y, Size, Size);

        public void Update(List<Player> players)
        {
            IsPressed = false;
            foreach (var player in players)
            {
                if (player.BoundingBox.Intersects(BoundingBox))
                {
                    IsPressed = true;
                    break;
                }
            }

            if (PripojeneDvere != null)
                PripojeneDvere.IsOpen = IsPressed;
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D texture)
        {
            Color drawColor = IsPressed ? Color.Lerp(Color, Color.White, 0.5f) : Color;
            spriteBatch.Draw(texture, BoundingBox, drawColor);
        }
    }

    public class Dvere
    {
        public Vector2 Position;
        public int Width;
        public int Height;
        public Color Color;
        public bool IsOpen;

        public Dvere(Vector2 position, int width, int height, Color color)
        {
            Position = position;
            Width = width;
            Height = height;
            Color = color;
            IsOpen = false;
        }

        public Rectangle BoundingBox => new Rectangle((int)Position.X, (int)Position.Y, Width, Height);

        public void Draw(SpriteBatch spriteBatch, Texture2D texture)
        {
            if (!IsOpen)
            {
                spriteBatch.Draw(texture, BoundingBox, Color);
            }
            else
            {
                spriteBatch.Draw(texture, BoundingBox, Color * 0.3f);
            }
        }
    }

    public class Player
    {
        public Vector2 Position;
        public Color Color;
        public int Size = 50;
        public float Speed = 3f;

        private Keys up, down, left, right;

        public Player(Vector2 position, Color color, Keys up, Keys down, Keys left, Keys right)
        {
            Position = position;
            Color = color;
            this.up = up;
            this.down = down;
            this.left = left;
            this.right = right;
        }

        public Rectangle BoundingBox => new Rectangle((int)Position.X, (int)Position.Y, Size, Size);

        public void Update(List<Player> players, List<Dvere> dvere, int screenWidth, int screenHeight)
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

            foreach (var dv in dvere)
            {
                if (!dv.IsOpen && BoundingBox.Intersects(dv.BoundingBox))
                    Position.X = oldPos.X;
            }

            Position.Y += move.Y;

            foreach (var other in players)
            {
                if (other == this) continue;
                if (BoundingBox.Intersects(other.BoundingBox))
                    Position.Y = oldPos.Y;
            }

            foreach (var dv in dvere)
            {
                if (!dv.IsOpen && BoundingBox.Intersects(dv.BoundingBox))
                    Position.Y = oldPos.Y;
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