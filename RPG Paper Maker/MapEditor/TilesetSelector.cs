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
        protected Texture2D TexTileset, TexSelector;
        protected SelectionRectangle SelectionRectangle;


        // -------------------------------------------------------------------
        // Initialize
        // -------------------------------------------------------------------

        protected override void Initialize()
        {
            base.Initialize();
            FileStream fs;
            
            // Loading images
            fs = new FileStream(WANOK.CurrentDir + "\\Content\\Pictures\\Textures2D\\Tilesets\\plains.png", FileMode.Open);
            TexTileset = Texture2D.FromStream(GraphicsDevice,fs);
            fs = new FileStream("Config/bmp/editor_cursor.png", FileMode.Open);
            TexSelector = Texture2D.FromStream(GraphicsDevice, fs);
            SelectionRectangle = new SelectionRectangle(GraphicsDevice, TexSelector, 0, 0, 32, 32);
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
            spriteBatch.Draw(TexTileset, new Rectangle(0, 0, 256, 256), Color.White);
            SelectionRectangle.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
