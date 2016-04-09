using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace WinFormsGraphicsDevice
{
    class MapEditorControl : GraphicsDeviceControl
    {
        protected ContentManager content;
        protected SpriteBatch spriteBatch;

        protected GameTime gameTime;

        protected override void Initialize()
        {
            content = new ContentManager(Services, "Content");
            gameTime = new GameTime();
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // Hook the idle event to constantly redraw our animation.
            Application.Idle += delegate { Invalidate(); };
        }


        protected override void Draw()
        {

        }
    }
}
