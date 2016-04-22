using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Paper_Maker
{
    class TilesetSelector : WinFormsGraphicsDevice.MapEditorControl
    {
        public Texture2D Tileset;

        protected override void Initialize()
        {
            base.Initialize();
            FileStream fs = new FileStream(WANOK.CurrentDir + "\\Content\\Pictures\\Textures2D\\Tilesets\\plains.png", FileMode.Open);
            Tileset = Texture2D.FromStream(GraphicsDevice,fs);
        }

        // -------------------------------------------------------------------
        // Update
        // -------------------------------------------------------------------

        protected override void Update(GameTime gameTime)
        {

        }

        // -------------------------------------------------------------------
        // Draw
        // -------------------------------------------------------------------

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(224,224,224));

            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);
            spriteBatch.Draw(Tileset, new Rectangle(0, 0, 256, 256), Color.White);
            spriteBatch.End();
        }
    }
}
