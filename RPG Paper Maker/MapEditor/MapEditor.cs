using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
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
        public string SelectedDrawType = "ItemFloor";

        // Content
        public static Texture2D currentFloorTex;


        protected override void Initialize()
        {
            base.Initialize();

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
        // Update
        // -------------------------------------------------------------------

        protected override void Update(GameTime gameTime)
        {
            // Update camera
            Camera.Update(gameTime, CursorEditor);
        }

        // -------------------------------------------------------------------
        // Draw
        // -------------------------------------------------------------------

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(32,32,32));

            // Effect settings
            effect.View = Camera.View;
            effect.Projection = Camera.Projection;
            effect.World = Matrix.Identity;

            Map.Draw(gameTime, effect);
        }
    }
}
