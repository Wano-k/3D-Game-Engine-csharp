using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Paper_Maker
{
    class MapEditor : WinFormsGraphicsDevice.MapEditorControl
    {
        Camera Camera;
        Map Map = null;
        CursorEditor CursorEditor;
        BasicEffect effect;
        bool isMapReloading = false;
        public string SelectedDrawType = "ItemFloor";
        public static int GridHeight = 0;

        // Content
        public static Texture2D TexCursor;


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
                // Update camera
                CursorEditor.Update(gameTime, Camera, Map.MapInfos);
                Camera.Update(gameTime, CursorEditor);

                // Update keyboard
                WANOK.KeyboardManager.Update();
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
                // Effect settings
                effect.View = Camera.View;
                effect.Projection = Camera.Projection;
                effect.World = Matrix.Identity;

                // Drawings components
                Map.Draw(gameTime, effect);
                CursorEditor.Draw(gameTime, effect);
            }
        }
    }
}
