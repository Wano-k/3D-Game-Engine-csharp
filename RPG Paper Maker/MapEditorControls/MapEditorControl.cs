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
        protected ContentManager content;
        protected SpriteBatch spriteBatch;
        protected Stopwatch timer;
        protected TimeSpan elapsed;
        protected GameTime gameTime;

        protected override void Initialize()
        {
            timer = Stopwatch.StartNew();
            content = new ContentManager(Services, "Content");
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Application.Idle += delegate { GameLoop(); };
        }

        private void GameLoop()
        {
            gameTime = new GameTime(timer.Elapsed, timer.Elapsed - elapsed);
            elapsed = timer.Elapsed;

            Update(gameTime);
            Invalidate();
        }

        protected override void Draw()
        {
            Draw(gameTime);
        }

        protected abstract void Update(GameTime gameTime);
        protected abstract void Draw(GameTime gameTime);
    }
}
