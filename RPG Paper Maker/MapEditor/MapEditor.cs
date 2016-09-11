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
        AlphaTestEffect effect;
        SpriteFont font;

        public static MapEditorControl Control = new MapEditorControl();
        public string SelectedDrawType { get { return Control.SelectedDrawType; } set { Control.SelectedDrawType = value; } }
        public DrawType SelectedDrawTypeParticular { get { return Control.SelectedDrawTypeParticular; } set { Control.SelectedDrawTypeParticular = value; } }
        public DrawMode DrawMode { get { return Control.DrawMode; } set { Control.DrawMode = value; } }
        public Point MouseBeforeUpdate { get { return Control.MouseBeforeUpdate; } set { Control.MouseBeforeUpdate = value; } }
        public int PreviousWidth, PreviousHeight;
        private FrameCounter FrameCounter = new FrameCounter();
        public static double ClickTimer;
        double TimerDelay = 500;
        public static bool IsViewMode = false;

        // Textures
        public static Texture2D TexCursor, TexStartCursor, TexEventCursor, TexEventSelectCursor, TexTileset, TexNone, TexGrid;
        public static Dictionary<int,Texture2D> TexAutotiles = new Dictionary<int, Texture2D>();
        public static Dictionary<int, Texture2D> TexReliefs = new Dictionary<int, Texture2D>();
        public static Dictionary<SystemGraphic, Texture2D> TexCharacters = new Dictionary<SystemGraphic, Texture2D>();

        public static int Debug = 0;
        public static int[] Debug2 = null;

        // -------------------------------------------------------------------
        // Initialize
        // -------------------------------------------------------------------

        protected override void Initialize()
        {
            base.Initialize();
            PreviousWidth = Width;
            PreviousHeight = Height;

            // Load textures
            FileStream fs;
            fs = new FileStream("Config/bmp/editor_cursor.png", FileMode.Open);
            TexCursor = Texture2D.FromStream(GraphicsDevice, fs);
            fs = new FileStream("Config/bmp/start_cursor.png", FileMode.Open);
            TexStartCursor = Texture2D.FromStream(GraphicsDevice, fs);
            fs = new FileStream("Config/bmp/event_square.png", FileMode.Open);
            TexEventCursor = Texture2D.FromStream(GraphicsDevice, fs);
            fs = new FileStream("Config/bmp/event_square_select.png", FileMode.Open);
            TexEventSelectCursor = Texture2D.FromStream(GraphicsDevice, fs);
            TexNone = new Texture2D(GraphicsDevice, 1, 1);
            TexGrid = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            TexGrid.SetData(new Color[] { new Color(Color.White, 0.5f) });
            fs.Close();
            

            // Create game components
            Control.Camera = new Camera(GraphicsDevice);
            Control.CursorEditor = new CursorEditor(GraphicsDevice);

            // Load Settings
            LoadSettings();

            // Effect
            effect = new AlphaTestEffect(GraphicsDevice);
            effect.AlphaFunction = CompareFunction.Greater;
            effect.ReferenceAlpha = 1;

            // Load content
            
            font = Content.Load<SpriteFont>("Fonts/corbel");
        }

        // -------------------------------------------------------------------
        // GetMapTileset
        // -------------------------------------------------------------------

        public static SystemTileset GetMapTileset()
        {
            return WANOK.Game.Tilesets.GetTilesetById(Control.Map.MapInfos.Tileset);
        }

        // -------------------------------------------------------------------
        // SetCurrentTexture
        // -------------------------------------------------------------------

        public void SetCurrentTexture(int[] tex)
        {
            Control.CurrentTexture = tex;
        }

        public void SetCurrentTextureBasic()
        {
            SetCurrentTexture(new int[] { 0, 0, 1, 1 });
        }

        // -------------------------------------------------------------------
        // SetSpecialItemId
        // -------------------------------------------------------------------

        public void SetCurrentSpecialItemId(int id)
        {
            Control.CurrentSpecialItemId = id;
        }

        // -------------------------------------------------------------------
        // GetAutotileId
        // -------------------------------------------------------------------

        public int GetCurrentSpecialItemId()
        {
            return Control.CurrentSpecialItemId;
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
        // SetMountainHeight
        // -------------------------------------------------------------------

        public void SetMountainHeight(int[] height)
        {
            Control.CurrentMountainHeight = height;
        }

        // -------------------------------------------------------------------
        // SaveMap
        // -------------------------------------------------------------------

        public void SaveMap(bool b = true)
        {
            Control.Map.Saved = b;
        }

        // -------------------------------------------------------------------
        // ReCalculateCameraProjection
        // -------------------------------------------------------------------

        public void ReCalculateCameraProjection()
        {
            if (Control.Camera != null) Control.Camera.CalculateCameraProjection(GraphicsDevice);
        }

        // -------------------------------------------------------------------
        // LoadSystemGraphic
        // -------------------------------------------------------------------

        public static void LoadSystemGraphic(SystemGraphic graphic, GraphicsDevice device)
        {
            if (!TexCharacters.ContainsKey(graphic)) TexCharacters[graphic] = graphic.LoadTexture(device);
        }

        // -------------------------------------------------------------------
        // GetCharacterTexture
        // -------------------------------------------------------------------

        public static Texture2D GetCharacterTexture(GraphicsDevice device, Texture2D originalTexture)
        {
            Texture2D newTexture = new Texture2D(device, 125 * WANOK.SQUARE_SIZE, 5 * WANOK.SQUARE_SIZE);
            Color[] imageData = new Color[originalTexture.Width * originalTexture.Height];
            originalTexture.GetData(imageData);

            int count;
            for (int a = 0; a < Autotiles.listA.Length; a++)
            {
                count = 0;
                for (int b = 0; b < Autotiles.listB.Length; b++)
                {
                    for (int c = 0; c < Autotiles.listC.Length; c++)
                    {
                        for (int d = 0; d < Autotiles.listD.Length; d++)
                        {
                            WANOK.FillImage(device, newTexture, imageData, originalTexture.Width, new Rectangle(count * WANOK.SQUARE_SIZE, a * WANOK.SQUARE_SIZE, WANOK.SQUARE_SIZE / 2, WANOK.SQUARE_SIZE / 2), new Rectangle((Autotiles.AutotileBorder[Autotiles.listA[a]] % 4) * (WANOK.SQUARE_SIZE / 2), (Autotiles.AutotileBorder[Autotiles.listA[a]] / 4) * (WANOK.SQUARE_SIZE / 2), WANOK.SQUARE_SIZE / 2, WANOK.SQUARE_SIZE / 2));
                            WANOK.FillImage(device, newTexture, imageData, originalTexture.Width, new Rectangle(count * WANOK.SQUARE_SIZE + (WANOK.SQUARE_SIZE / 2), a * WANOK.SQUARE_SIZE, WANOK.SQUARE_SIZE / 2, WANOK.SQUARE_SIZE / 2), new Rectangle((Autotiles.AutotileBorder[Autotiles.listB[b]] % 4) * (WANOK.SQUARE_SIZE / 2), (Autotiles.AutotileBorder[Autotiles.listB[b]] / 4) * (WANOK.SQUARE_SIZE / 2), WANOK.SQUARE_SIZE / 2, WANOK.SQUARE_SIZE / 2));
                            WANOK.FillImage(device, newTexture, imageData, originalTexture.Width, new Rectangle(count * WANOK.SQUARE_SIZE, a * WANOK.SQUARE_SIZE + (WANOK.SQUARE_SIZE / 2), WANOK.SQUARE_SIZE / 2, WANOK.SQUARE_SIZE / 2), new Rectangle((Autotiles.AutotileBorder[Autotiles.listC[c]] % 4) * (WANOK.SQUARE_SIZE / 2), (Autotiles.AutotileBorder[Autotiles.listC[c]] / 4) * (WANOK.SQUARE_SIZE / 2), WANOK.SQUARE_SIZE / 2, WANOK.SQUARE_SIZE / 2));
                            WANOK.FillImage(device, newTexture, imageData, originalTexture.Width, new Rectangle(count * WANOK.SQUARE_SIZE + (WANOK.SQUARE_SIZE / 2), a * WANOK.SQUARE_SIZE + (WANOK.SQUARE_SIZE / 2), WANOK.SQUARE_SIZE / 2, WANOK.SQUARE_SIZE / 2), new Rectangle((Autotiles.AutotileBorder[Autotiles.listD[d]] % 4) * (WANOK.SQUARE_SIZE / 2), (Autotiles.AutotileBorder[Autotiles.listD[d]] / 4) * (WANOK.SQUARE_SIZE / 2), WANOK.SQUARE_SIZE / 2, WANOK.SQUARE_SIZE / 2));
                            count++;
                        }
                    }
                }
            }
            originalTexture.Dispose();

            return newTexture;
        }

        // -------------------------------------------------------------------
        // LoadSettings
        // -------------------------------------------------------------------

        protected void LoadSettings()
        {
            GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;
            GraphicsDevice.RasterizerState = new RasterizerState()
            {
                CullMode = CullMode.None,
                MultiSampleAntiAlias = true
            };
            GraphicsDevice.BlendState = BlendState.NonPremultiplied;
            GraphicsDevice.DepthStencilState = DepthStencilState.Default;
        }

        // -------------------------------------------------------------------
        // ReLoadMap
        // -------------------------------------------------------------------

        public void ReLoadMap(string mapName)
        {
            Control.IsMapReloading = true;
            Control.CursorEditor.Reset();
            Control.CurrentPortion = new int[] { 0, 0 };
            Control.Camera.ReLoadMap(GraphicsDevice);
            if (Control.Map != null) DisposeVertexBuffer(); // Dispose the previous vertexBuffer to create a new one for the object
            Control.Map = new Map(GraphicsDevice, mapName);
            Control.IsMapReloading = false;
        }

        // -------------------------------------------------------------------
        // InitCursorEventPosition
        // -------------------------------------------------------------------

        public void InitCursorEventPosition()
        {
            Control.Map.EventPosition = null;
        }

        // -------------------------------------------------------------------
        // Update
        // -------------------------------------------------------------------

        protected override void Update(GameTime gameTime)
        {
            if (!Control.IsMapReloading && Control.Map != null)
            {
                // Calculate FPS
                var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
                FrameCounter.Update(deltaTime);

                // If resizing window, recalculate camera position
                if (PreviousWidth != Width || PreviousHeight != Height)
                {
                    ReCalculateCameraProjection();
                    PreviousWidth = Width;
                    PreviousHeight = Height;
                }

                // Update camera
                bool moving = Control.CursorEditor.Update(gameTime, Control.Camera, Control.Map.MapInfos);
                Control.Camera.Update(gameTime, Control.CursorEditor, MouseBeforeUpdate, WANOK.GetPixelHeight(Control.GridHeight));

                // Map editor update
                Control.Update(GraphicsDevice, Control.Camera);
                moving |= MouseBeforeUpdate != WANOK.MapMouseManager.GetPosition();
                ClickTimer += gameTime.ElapsedGameTime.TotalMilliseconds;

                // Classic button down
                if (WANOK.MapMouseManager.IsButtonDown(MouseButtons.Left) || (WANOK.MapMouseManager.IsButtonDownRepeat(MouseButtons.Left) && moving))
                {
                    if (WANOK.MapMouseManager.IsButtonDown(MouseButtons.Left))
                    {
                        Control.Add(true, ClickTimer < TimerDelay);
                        if (ClickTimer >= TimerDelay) ClickTimer = 0;
                    }
                    else Control.Add(true);
                }
                if (WANOK.MapMouseManager.IsButtonDown(MouseButtons.Right) || (WANOK.MapMouseManager.IsButtonDownRepeat(MouseButtons.Right) && moving)) Control.Remove(true);
                if (WANOK.KeyboardManager.IsButtonDownRepeat(WANOK.Settings.KeyboardAssign.EditorDrawCursor)) Control.Add(false);
                if (WANOK.KeyboardManager.IsButtonDownRepeat(WANOK.Settings.KeyboardAssign.EditorRemoveCursor)) Control.Remove(false);

                // Event button down / up
                if (SelectedDrawType == "ItemEvent")
                {
                    if (WANOK.MapMouseManager.IsButtonDown(MouseButtons.Left))
                    {
                        Control.SelectedMoveEvent = Control.SelectedEvent();
                        Control.SelectedMoveEventPosition = Control.Map.EventPosition;
                    }

                    if (WANOK.KeyboardManager.IsButtonCtrlXDown()) {
                        Control.CopiedEvent = Control.SelectedEvent().CreateCopy();
                        if (Control.CopiedEvent != null) Control.RemoveEvent(Control.Map.EventPosition, true);
                    }
                    else if (WANOK.KeyboardManager.IsButtonCtrlCDown())
                    {
                        Control.CopiedEvent = Control.SelectedEvent();
                        if (Control.CopiedEvent != null) Control.CopiedEvent = Control.CopiedEvent.CreateCopy();
                    }
                    else if (WANOK.KeyboardManager.IsButtonCtrlVDown())
                    {
                        if (Control.CopiedEvent != null) Control.AddEvent(Control.CopiedEvent.CreateCopy(), true);
                    }

                    if (WANOK.MapMouseManager.IsButtonUp(MouseButtons.Left))
                    {
                        if (Control.SelectedMoveEvent != null)
                        {
                            Control.CreateCancel(true);
                            Control.RemoveEvent(Control.SelectedMoveEventPosition, false, false);
                            Control.AddEvent(Control.SelectedMoveEvent, false, false);
                            Control.SelectedMoveEvent = null;
                            Control.SelectedMoveEventPosition = null;
                        }
                    }
                }

                // Button up
                Control.ButtonUp();

                // Options
                Control.Options();

                // Is view mode
                IsViewMode = SelectedDrawType == "ItemView";

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
            if (!Control.IsMapReloading && Control.Map != null)
            {
                LoadSettings();

                GraphicsDevice.Clear(WANOK.GetColor(Control.Map.MapInfos.SkyColor));

                // Effect settings
                effect.View = Control.Camera.View;
                effect.Projection = Control.Camera.Projection;

                // Drawings components
                Control.Map.Draw(gameTime, effect, Control.Camera, SelectedDrawType);
                effect.Alpha = 1.0f;
                if (!IsViewMode) Control.CursorEditor.Draw(GraphicsDevice, gameTime, effect);

                // Draw position
                string pos = "[" + Control.CursorEditor.GetX() + "," + Control.CursorEditor.GetZ() + "]";
                string fps = string.Format("FPS: {0}", (int)FrameCounter.AverageFramesPerSecond);
                //string fps = Debug2 == null ? "null" : string.Format("FPS: {0}, {1}", Debug2[0], Debug2[1]);
                //string fps = string.Format("FPS: {0}", Debug);
                SpriteBatch.Begin();
                SpriteBatch.DrawString(font, pos, new Vector2(GraphicsDevice.Viewport.Width - 10, GraphicsDevice.Viewport.Height - 10), Color.White, 0, font.MeasureString(pos), 1.0f, SpriteEffects.None, 0.5f);
                SpriteBatch.DrawString(font, fps, new Vector2(GraphicsDevice.Viewport.Width - 10, 40), Color.White, 0, font.MeasureString(fps), 1.0f, SpriteEffects.None, 0.5f);
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
