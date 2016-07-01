using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace WinFormsGraphicsDevice
{
    abstract class MapEditorControl : GraphicsDeviceControl
    {
        protected ContentManager Content;
        protected SpriteBatch SpriteBatch;
        protected Stopwatch Timer;
        protected TimeSpan Elapsed;
        protected GameTime GameTime;

        protected override void Initialize()
        {
            Timer = Stopwatch.StartNew();
            Content = new ContentManager(Services, "Content");
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            Application.Idle += delegate { GameLoop(); };
        }

        private void GameLoop()
        {
            GameTime = new GameTime(Timer.Elapsed, Timer.Elapsed - Elapsed);
            Elapsed = Timer.Elapsed;

            Update(GameTime);
            Invalidate();
        }

        protected override void Draw()
        {
            Draw(GameTime);
        }

        protected abstract void Update(GameTime gameTime);
        protected abstract void Draw(GameTime gameTime);
    }
}
