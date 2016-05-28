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
        Map Map;
        CursorEditor CursorEditor;
        BasicEffect effect;
        Boolean isMapReloading = false;
        public string SelectedDrawType = "ItemFloor";
        public static int Height = 0;

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
            Map = new Map(this.GraphicsDevice, "testmap");
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

        public void ReLoadMap(String mapName)
        {
            // Recreate game components
            isMapReloading = true;
            Camera.ReLoadMap();
            Map.DisposeVertexBuffer(); // Dispose the previous vertexBuffer to create a new one for the object
            Map = new Map(GraphicsDevice, mapName);
            isMapReloading = false;
        }

        // -------------------------------------------------------------------
        // Update
        // -------------------------------------------------------------------

        protected override void Update(GameTime gameTime)
        {
            // Update camera
            Camera.Update(gameTime, CursorEditor);
            CursorEditor.Update(gameTime);
        }

        // -------------------------------------------------------------------
        // Draw
        // -------------------------------------------------------------------

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(32,32,32));

            if (!isMapReloading)
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
