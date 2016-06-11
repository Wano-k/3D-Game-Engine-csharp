using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RPG_Paper_Maker
{
    class MapEditor : WinFormsGraphicsDevice.MapEditorControl
    {
        Camera Camera;
        Map Map = null;
        CursorEditor CursorEditor;
        BasicEffect effect;
        SpriteFont font;
        bool isMapReloading = false;
        public string SelectedDrawType = "ItemFloor";
        public static int GridHeight = 0;
        public static Point MouseBeforeUpdate = WANOK.MapMouseManager.GetPosition();
        public int count = 0;

        // Content
        public static Texture2D TexCursor;
        public static Texture2D CurrentTexture = null;


        // -------------------------------------------------------------------
        // Initialize
        // -------------------------------------------------------------------

        protected override void Initialize()
        {
            base.Initialize();

            // Load textures
            FileStream fs;
            fs = new FileStream("Config/bmp/editor_cursor.png", FileMode.Open);
            TexCursor = Texture2D.FromStream(GraphicsDevice, fs);

            // Create game components
            Camera = new Camera(this.GraphicsDevice);
            CursorEditor = new CursorEditor(this.GraphicsDevice);

            // Load Settings
            LoadSettings();

            // Effect
            effect = new BasicEffect(GraphicsDevice);

            // Load content
            font = Content.Load<SpriteFont>("Fonts/corbel");
        }

        // -------------------------------------------------------------------
        // SetCurrentTexture
        // -------------------------------------------------------------------

        public void SetCurrentTexture(Texture2D tex)
        {
            CurrentTexture = tex;
        }

        // -------------------------------------------------------------------
        // LoadSettings
        // -------------------------------------------------------------------

        protected void LoadSettings()
        {
            GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;
            GraphicsDevice.RasterizerState = new RasterizerState()
            {
                CullMode = CullMode.None
            };
            GraphicsDevice.BlendState = BlendState.NonPremultiplied;
            GraphicsDevice.DepthStencilState = DepthStencilState.Default;
        }

        // -------------------------------------------------------------------
        // ReLoadMap
        // -------------------------------------------------------------------

        public void ReLoadMap(string mapName)
        {
            // Recreate game components
            isMapReloading = true;
            Camera.ReLoadMap();
            if (Map != null) Map.DisposeVertexBuffer(); // Dispose the previous vertexBuffer to create a new one for the object
            Map = new Map(GraphicsDevice, mapName);
            CursorEditor.Reset();
            isMapReloading = false;
        }

        // -------------------------------------------------------------------
        // Update
        // -------------------------------------------------------------------

        protected override void Update(GameTime gameTime)
        {
            if (Map != null)
            {
                count = (count + 1) % 80;
                // Update camera
                CursorEditor.Update(gameTime, Camera, Map.MapInfos);
                Camera.Update(gameTime, CursorEditor);

                // Raycasting
                Ray ray = WANOK.CalculateRay(new Vector2(MouseBeforeUpdate.X, MouseBeforeUpdate.Y), Camera.View, Camera.Projection, GraphicsDevice.Viewport);
                float distance = (GridHeight - Camera.Position.Y) / ray.Direction.Y;
                Vector3 pointOnPlane = WANOK.GetCorrectPointOnRay(ray, Camera, distance);

                // Update keyboard
                MouseBeforeUpdate = WANOK.MapMouseManager.GetPosition();
                WANOK.KeyboardManager.Update();
                WANOK.MapMouseManager.Update();
            }
        }

        // -------------------------------------------------------------------
        // Draw
        // -------------------------------------------------------------------

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(32,32,32));

            if (!isMapReloading && Map != null)
            {
                LoadSettings();

                // Effect settings
                effect.View = Camera.View;
                effect.Projection = Camera.Projection;
                effect.World = Matrix.Identity;

                // Drawings components
                Map.Draw(gameTime, effect);
                CursorEditor.Draw(gameTime, effect);

                // Draw position
                string pos = "[" + CursorEditor.GetX() + "," + CursorEditor.GetY() + "]";
                SpriteBatch.Begin();
                SpriteBatch.DrawString(font, pos, new Vector2(GraphicsDevice.Viewport.Width-10, GraphicsDevice.Viewport.Height-10), Color.White, 0, font.MeasureString(pos), 1.0f, SpriteEffects.None, 0.5f);
                SpriteBatch.End();
            }
        }
    }
}
