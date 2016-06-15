using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RPG_Paper_Maker
{
    class TilesetSelector : WinFormsGraphicsDevice.MapEditorControl
    {
        public static Texture2D TexTileset;
        protected Texture2D TexSelector;
        protected SelectionRectangle SelectionRectangle;


        // -------------------------------------------------------------------
        // Initialize
        // -------------------------------------------------------------------

        protected override void Initialize()
        {
            base.Initialize();
            FileStream fs;
            
            // Loading images
            fs = new FileStream(Path.Combine(new string[] { WANOK.CurrentDir, "Content", "Pictures", "Textures2D", "Tilesets", "plains.png" }), FileMode.Open);
            TexTileset = Texture2D.FromStream(GraphicsDevice,fs);
            fs = new FileStream(Path.Combine(new string[] { "Config", "bmp", "tileset_cursor.png" }), FileMode.Open);
            TexSelector = Texture2D.FromStream(GraphicsDevice, fs);
            SelectionRectangle = new SelectionRectangle(GraphicsDevice, TexSelector, 0, 0, WANOK.BASIC_SQUARE_SIZE, WANOK.BASIC_SQUARE_SIZE);
        }

        // -------------------------------------------------------------------
        // GetCurrentTexture
        // -------------------------------------------------------------------

        public int[] GetCurrentTexture()
        {
            return SelectionRectangle.GetRectangleArray();
        }

        // -------------------------------------------------------------------
        // MakeRectangleSelection
        // -------------------------------------------------------------------

        protected void MakeRectangleSelection()
        {
            // If first pressure
            if (WANOK.TilesetMouseManager.IsButtonDown(MouseButtons.Left))
            {
                int x = WANOK.TilesetMouseManager.GetPosition().X;
                int y = WANOK.TilesetMouseManager.GetPosition().Y;

                if (x >= 0 && x < Width && y >= 0 && y < Height) SelectionRectangle.SetRectangle(x, y, 1, 1);
            }
            else
            {
                int x = WANOK.TilesetMouseManager.GetPosition().X;
                if (x < 0) x = 0;
                if (x >= Width) x = Width - 1;
                int init_pos_x = SelectionRectangle.X / WANOK.BASIC_SQUARE_SIZE;
                int pos_x = x / WANOK.BASIC_SQUARE_SIZE;
                int i_x = init_pos_x <= pos_x ? 1 : -1;
                int width = (pos_x - init_pos_x) + i_x;
                SelectionRectangle.Width = width * WANOK.BASIC_SQUARE_SIZE;

                int y = WANOK.TilesetMouseManager.GetPosition().Y;
                if (y < 0) y = 0;
                if (y >= Height) y = Height - 1;
                int init_pos_y = SelectionRectangle.Y / WANOK.BASIC_SQUARE_SIZE;
                int pos_y = y / WANOK.BASIC_SQUARE_SIZE;
                int i_y = init_pos_y <= pos_y ? 1 : -1;
                int height = (pos_y - init_pos_y) + i_y;
                SelectionRectangle.Height = height * WANOK.BASIC_SQUARE_SIZE;
            }
        }

        // -------------------------------------------------------------------
        // Update
        // -------------------------------------------------------------------

        protected override void Update(GameTime gameTime)
        {
            // Button Down
            if (WANOK.TilesetMouseManager.IsButtonDownRepeat(MouseButtons.Left))
            {
                MakeRectangleSelection();
            }

            // Button Up
            if (WANOK.TilesetMouseManager.IsButtonUp(MouseButtons.Left))
            {
                SelectionRectangle.SetRealPosition();
            }

            // Update mouse
            WANOK.TilesetMouseManager.Update();
        }

        // -------------------------------------------------------------------
        // Draw
        // -------------------------------------------------------------------

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(224, 224, 224));
            
            SpriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);
            SpriteBatch.Draw(TexTileset, new Rectangle(0, 0, Width, Height), Color.White);
            SelectionRectangle.Draw(SpriteBatch);
            SpriteBatch.End();
        }
    }
}
