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
        BasicEffect effect;
        SpriteFont font;

        public MapEditorControl Control = new MapEditorControl();
        public string SelectedDrawType { get { return Control.SelectedDrawType; } set { Control.SelectedDrawType = value; } }
        public DrawMode DrawMode { get { return Control.DrawMode; } set { Control.DrawMode = value; } }
        public Point MouseBeforeUpdate { get { return Control.MouseBeforeUpdate; } set { Control.MouseBeforeUpdate = value; } }

        // Content
        public static Texture2D TexCursor;
        public static Texture2D TexStartCursor;


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
            fs = new FileStream("Config/bmp/start_cursor.png", FileMode.Open);
            TexStartCursor = Texture2D.FromStream(GraphicsDevice, fs);
            fs.Close();

            // Create game components
            Control.Camera = new Camera(GraphicsDevice);
            Control.CursorEditor = new CursorEditor(GraphicsDevice);

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

        public void SetCurrentTexture(int[] tex)
        {
            Control.CurrentTexture = tex;
        }

        // -------------------------------------------------------------------
        // SetGridHeight
        // -------------------------------------------------------------------

        public void SetGridHeight(int[] height)
        {
            Control.GridHeight = height;
            Control.CursorEditor.Position.Y = WANOK.GetPixelHeight(height);
            Update(GameTime);
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
            Control.IsMapReloading = true;
            Control.Camera.ReLoadMap();
            if (Control.Map != null) DisposeVertexBuffer(); // Dispose the previous vertexBuffer to create a new one for the object
            Control.Map = new Map(GraphicsDevice, mapName);
            Control.CursorEditor.Reset();
            Control.IsMapReloading = false;
        }

        // -------------------------------------------------------------------
        // Update
        // -------------------------------------------------------------------

        protected override void Update(GameTime gameTime)
        {
            if (Control.Map != null)
            {
                // Update camera
                Control.CursorEditor.Update(gameTime, Control.Camera, Control.Map.MapInfos);
                Control.Camera.Update(gameTime, Control.CursorEditor, MouseBeforeUpdate);

                // Map editor update
                Control.Update(GraphicsDevice, Control.Camera);
                if (WANOK.MapMouseManager.IsButtonDownRepeat(MouseButtons.Left)) Control.Add(true);
                if (WANOK.MapMouseManager.IsButtonDownRepeat(MouseButtons.Right)) Control.Remove(true);
                if (WANOK.KeyboardManager.IsButtonDownRepeat(WANOK.Settings.KeyboardAssign.EditorDrawCursor)) Control.Add(false);
                if (WANOK.KeyboardManager.IsButtonDownRepeat(WANOK.Settings.KeyboardAssign.EditorRemoveCursor)) Control.Remove(false);
                Control.ButtonUp();

                // Options
                Control.Options();

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
            GraphicsDevice.Clear(new Color(32, 32, 32));

            if (!Control.IsMapReloading && Control.Map != null)
            {
                LoadSettings();

                // Effect settings
                effect.View = Control.Camera.View;
                effect.Projection = Control.Camera.Projection;
                effect.World = Matrix.Identity;

                // Drawings components
                Control.Map.Draw(gameTime, effect);
                Control.CursorEditor.Draw(GraphicsDevice, gameTime, effect);

                // Draw position
                string pos = "[" + Control.CursorEditor.GetX() + "," + Control.CursorEditor.GetZ() + "]";
                SpriteBatch.Begin();
                SpriteBatch.DrawString(font, pos, new Vector2(GraphicsDevice.Viewport.Width-10, GraphicsDevice.Viewport.Height-10), Color.White, 0, font.MeasureString(pos), 1.0f, SpriteEffects.None, 0.5f);
                SpriteBatch.End();
            }
        }

        // -------------------------------------------------------------------
        // DisposeVertexBuffer
        // -------------------------------------------------------------------

        public void DisposeVertexBuffer()
        {
            Control.Map.DisposeVertexBuffer();
        }
    }
}
