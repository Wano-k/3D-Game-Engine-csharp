using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

// -------------------------------------------------------------------
// STATIC Class for global variables
// -------------------------------------------------------------------

namespace RPG_Paper_Maker
{
    static class WANOK
    {
        public static Vector3[] VERTICESFLOOR = new Vector3[]
        {
            new Vector3(0.0f, 0.0f, 0.0f),
            new Vector3(1.0f, 0.0f, 0.0f),
            new Vector3(1.0f, 0.0f, 1.0f),
            new Vector3(0.0f, 0.0f, 1.0f)
        };
        public static Vector3[] VERTICESSPRITE = new Vector3[]
        {
            new Vector3(0.0f, 1.0f, 0.0f),
            new Vector3(1.0f, 1.0f, 0.0f),
            new Vector3(1.0f, 0.0f, 0.0f),
            new Vector3(0.0f, 0.0f, 0.0f)
        };
        public static int BASIC_SQUARE_SIZE = 32;
        public static int SQUARE_SIZE { get { return Game.System.SquareSize; } }
        public static float RELATION_SIZE { get { return (float)(BASIC_SQUARE_SIZE) / SQUARE_SIZE; } }
        public static int PORTION_SIZE = 16;
        public static int PORTION_RADIUS = 5;
        public static int COEF_BORDER_TEX = 10000;
        public static int MAX_CANCEL = 20;
        public static EngineSettings Settings = null;
        public static DemoSteps DemoStep = DemoSteps.None;
        public static Form CurrentDemoDialog = null;
        public static string CurrentLang = "eng";
        public static KeyboardManager KeyboardManager = new KeyboardManager();
        public static MouseManager TilesetMouseManager = new MouseManager();
        public static MouseManager MapMouseManager = new MouseManager();
        public static string ListBeginning = "<> ";
        public static GameDatas Game = new GameDatas();
        public static HashSet<string> ListMapToSave = new HashSet<string>();
        public static TreeNode SelectedNode = null;
        public static string NONE_IMAGE_STRING = "<None>";
        public static string TILESET_IMAGE_STRING = "<Tileset>";
        public static DialogProgressBar DialogProgressBar = null;
        public static string CurrentMainLang { get { return Game.System.Langs[0]; } }
        public static MapEditor MapEditor;

        // COLORS
        public static System.Drawing.Color COLOR_BACKGROUND_PREVIEW_IMAGE = System.Drawing.Color.FromArgb(220, 220, 220);

        // CANCEL
        public static Dictionary<string, List<Dictionary<int[], object>>> CancelRedo = new Dictionary<string, List<Dictionary<int[], object>>>();
        public static List<int[]> PortionsToAddCancel = new List<int[]>();
        public static Dictionary<string, int> CancelRedoIndex = new Dictionary<string, int>();
        public static bool CanStartCancel = true;

        // PATHS
        public static string ABSOLUTEENGINEPATH;
        public static string PATHSETTINGS = "Config/EngineSettings.JSON";
        public static string CurrentDir = ".";
        public static string ExcecutablePath { get { return Application.ExecutablePath; } }
        public static string ExcecutablePathDir { get { return Path.GetDirectoryName(ExcecutablePath); } }
        public static string HeroesPath { get { return Path.Combine(CurrentDir, "Content", "Datas", "Heroes.rpmd"); } }
        public static string SystemPath { get { return Path.Combine(CurrentDir, "Content", "Datas", "System.rpmd"); } }
        public static string BattleSystemPath { get { return Path.Combine(CurrentDir, "Content", "Datas", "BattleSystem.rpmd"); } }
        public static string TilesetsPath { get { return Path.Combine(CurrentDir, "Content", "Datas", "Tilesets.rpmd"); } }
        public static string MapsDirectoryPath { get { return Path.Combine(CurrentDir, "Content", "Datas", "Maps"); } }


        // -------------------------------------------------------------------
        // GetDefaultNames
        // -------------------------------------------------------------------

        public static Dictionary<string, string> GetDefaultNames(string name = "")
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic["eng"] = name;

            return dic;
        }

        // -------------------------------------------------------------------
        // CopyAll
        // -------------------------------------------------------------------

        public static void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            if (source.FullName.ToLower() == target.FullName.ToLower())
            {
                return;
            }

            // Check if the target directory exists, if not, create it.
            if (Directory.Exists(target.FullName) == false)
            {
                Directory.CreateDirectory(target.FullName);
            }

            // Copy each file into its new directory.
            foreach (FileInfo fi in source.GetFiles())
            {
                fi.CopyTo(Path.Combine(target.ToString(), fi.Name), true);
            }

            // Copy each subdirectory using recursion.
            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir =
                    target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir);
            }
        }

        // -------------------------------------------------------------------
        // SaveDatas
        // -------------------------------------------------------------------

        public static void SaveDatas(object obj, string path)
        {
            try
            {
                string json = JsonConvert.SerializeObject(obj, Formatting.Indented);
                FileStream fs = new FileStream(path, FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine(json);
                sw.Close();
                fs.Close();
            } catch(Exception e)
            {
                PathErrorMessage(e);
            }
        }

        // -------------------------------------------------------------------
        // SaveBinaryDatas
        // -------------------------------------------------------------------

        public static void SaveBinaryDatas(object obj, string path)
        {
            try
            {
                FileStream fs = new FileStream(path, FileMode.Create);
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fs, obj);
                fs.Close();
            }
            catch (Exception e)
            {
                PathErrorMessage(e);
            }
        }

        // -------------------------------------------------------------------
        // LoadDatas
        // -------------------------------------------------------------------

        public static T LoadDatas<T>(string path)
        {
            T obj;
            try
            {
                FileStream fs = new FileStream(path, FileMode.Open);
                StreamReader sr = new StreamReader(fs);
                string json = sr.ReadToEnd();
                obj = JsonConvert.DeserializeObject<T>(json);
                sr.Close();
                fs.Close();
            }
            catch (Exception e)
            {
                obj = default(T);
                PathErrorMessage(e);
            }

            return obj;
        }

        // -------------------------------------------------------------------
        // LoadBinaryDatas
        // -------------------------------------------------------------------

        public static T LoadBinaryDatas<T>(string path)
        {
            T obj;
            try
            {
                FileStream fs = new FileStream(path, FileMode.Open);
                BinaryFormatter formatter = new BinaryFormatter();
                obj = (T)formatter.Deserialize(fs);
                fs.Close();
            }
            catch (Exception e)
            {
                obj = default(T);
                PathErrorMessage(e);
            }

            return obj;
        }

        // -------------------------------------------------------------------
        // GetImageData
        // -------------------------------------------------------------------

        public static Color[] GetImageData(Color[] colorData, int width, Rectangle rectangle)
        {
            Color[] color = new Color[rectangle.Width * rectangle.Height];
            for (int x = 0; x < rectangle.Width; x++)
                for (int y = 0; y < rectangle.Height; y++)
                    color[x + y * rectangle.Width] = colorData[x + rectangle.X + (y + rectangle.Y) * width];
            return color;
        }

        // -------------------------------------------------------------------
        // GetSubImage
        // -------------------------------------------------------------------

        public static Texture2D GetSubImage(GraphicsDevice GraphicsDevice, Texture2D image, Rectangle rectangle)
        {
            Color[] imageData = new Color[image.Width * image.Height];
            image.GetData<Color>(imageData);
            Color[] imagePiece = GetImageData(imageData, image.Width, rectangle);
            Texture2D subtexture = new Texture2D(GraphicsDevice, rectangle.Width, rectangle.Height);
            subtexture.SetData<Color>(imagePiece);

            return subtexture;
        }

        // -------------------------------------------------------------------
        // SaveTree
        // -------------------------------------------------------------------

        public static void SaveTree(TreeView tree, string filename)
        {
            using (Stream file = File.Open(filename, FileMode.Create))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(file, tree.Nodes.Cast<TreeNode>().ToList());
            }
        }

        // -------------------------------------------------------------------
        // LoadTree
        // -------------------------------------------------------------------

        public static void LoadTree(TreeView tree, string filename)
        {
            using (Stream file = File.Open(filename, FileMode.Open))
            {
                BinaryFormatter bf = new BinaryFormatter();
                object obj = bf.Deserialize(file);
                TreeNode[] nodeList = (obj as IEnumerable<TreeNode>).ToArray();
                SetIconsTreeNodes(nodeList);
                tree.Nodes.AddRange(nodeList);
            }
        }

        // -------------------------------------------------------------------
        // SetIconsTreeNodes
        // -------------------------------------------------------------------

        public static void SetIconsTreeNodes(IEnumerable<TreeNode> treeNodeCollection)
        {
            foreach (TreeNode node in treeNodeCollection)
            {
                TreeTag tag = (TreeTag)node.Tag;
                if (tag.IsMap)
                {
                    MapInfos mapInfos = LoadBinaryDatas<MapInfos>(Path.Combine(MapsDirectoryPath, tag.RealMapName, "infos.map"));
                    node.Tag = TreeTag.CreateMap(mapInfos.MapName, tag.RealMapName);
                    node.Text = mapInfos.MapName;
                    node.ImageIndex = 1;
                    node.SelectedImageIndex = 1;
                }

                TreeNode[] nodes = new TreeNode[node.Nodes.Count];
                node.Nodes.CopyTo(nodes, 0);
                SetIconsTreeNodes(nodes);
            }
        }

        // -------------------------------------------------------------------
        // PathErrorMessage
        // -------------------------------------------------------------------

        public static void PathErrorMessage(Exception e)
        {
            MessageBox.Show("You get a path error, enable to open a binary file.\n" + e.Message + "\n" + e.StackTrace, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void ShowWarningMessage(string m)
        {
            MessageBox.Show(m, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        // -------------------------------------------------------------------
        // Print
        // -------------------------------------------------------------------

        public static void Print(string m)
        {
            MessageBox.Show(m, "Print", MessageBoxButtons.OK);
        }

        // -------------------------------------------------------------------
        // CalculateRay : thanks to http://rbwhitaker.wikidot.com/picking
        // -------------------------------------------------------------------

        public static Ray CalculateRay(Vector2 mouseLocation, Matrix view, Matrix projection, Viewport viewport)
        {
            Vector3 nearPoint = viewport.Unproject(
                    new Vector3(mouseLocation.X,mouseLocation.Y, 0.0f),
                    projection,
                    view,
                    Matrix.Identity);

            Vector3 farPoint = viewport.Unproject(
                    new Vector3(mouseLocation.X,mouseLocation.Y, 1.0f),
                    projection,
                    view,
                    Matrix.Identity);

            Vector3 direction = farPoint - nearPoint;
            direction.Normalize();

            return new Ray(nearPoint, direction);
        }

        public static Vector3 GetPointOnRay(Ray ray, Camera camera, float distance)
        {
            return new Vector3((ray.Direction.X * distance) + camera.Position.X, (ray.Direction.Y * distance) + camera.Position.Y, (ray.Direction.Z * distance) + camera.Position.Z);
        }

        public static int[] GetCorrectPointOnRay(Ray ray, Camera camera, float distance, int height)
        {
            Vector3 point = GetPointOnRay(ray, camera, distance);
            int[] correctedPoint = new int[] { (int)(point.X / SQUARE_SIZE), 0, 0, (int)(point.Z / SQUARE_SIZE) };
            if (point.X < 0) correctedPoint[0] -= 1;
            if (point.Z < 0) correctedPoint[3] -= 1;

            return correctedPoint;
        }

        public static float? IntersectDistance(BoundingSphere sphere, Vector2 mouseLocation, Matrix view, Matrix projection, Viewport viewport)
        {
            Ray mouseRay = CalculateRay(mouseLocation, view, projection, viewport);
            return mouseRay.Intersects(sphere);
        }

        public static bool IntersectsModel(Vector2 mouseLocation, Model model, Matrix world, Matrix view, Matrix projection, Viewport viewport)
        {
            for (int index = 0; index < model.Meshes.Count; index++)
            {
                BoundingSphere sphere = model.Meshes[index].BoundingSphere;
                sphere = sphere.Transform(world);
                float? distance = IntersectDistance(sphere, mouseLocation, view, projection, viewport);

                if (distance != null)
                {
                    return true;
                }
            }

            return false;
        }

        public static Vector3 IntersectPlane(Ray ray, Plane plane, Camera camera)
        {
            float? distance = ray.Intersects(plane);
            return GetPointOnRay(ray, camera, distance.Value);
        }

        // -------------------------------------------------------------------
        // GetPixelHeight
        // -------------------------------------------------------------------

        public static int GetPixelHeight(int[] height)
        {
            return GetPixelHeight(height[0], height[1]);
        }

        public static int GetCoordsPixelHeight(int[] coords)
        {
            return GetPixelHeight(coords[1], coords[2]);
        }

        public static int GetPixelHeight(int y, int yPlus)
        {
            return (y * SQUARE_SIZE) + yPlus;
        }

        // -------------------------------------------------------------------
        // GetStrings
        // -------------------------------------------------------------------

        public static string GetStringList(int id, string name)
        {
            return string.Format("{0} {1}", GetStringBegining(id), name);
        }

        public static string GetStringComboBox(int id, string name)
        {
            return string.Format("ID{0:D4}: {1}", id, name);
        }

        public static string GetStringBegining(int id)
        {
            return string.Format("{0}ID{1:D4}:", ListBeginning, id);
        }

        public static string GetStringTileset(object[] args)
        {
            return args[0] + ", " + args[1] + ", " + args[2] + ", " + args[3];
        }

        public static string GetStringEvent(int num)
        {
            return string.Format("EV{0:D4}", num);
        }

        public static string GetStringSwitch(int id)
        {
            return GetStringComboBox(id, Game.System.GetSwitchById(id).Name);
        }

        public static string GetStringSwitch(object[] args)
        {
            return GetStringSwitch((int)args[0]);
        }

        // -------------------------------------------------------------------
        // Mod
        // -------------------------------------------------------------------

        public static int Mod(int x, int m)
        {
            int r = x % m;
            return r < 0 ? r + m : r;
        }

        // -------------------------------------------------------------------
        // GetColor
        // -------------------------------------------------------------------

        public static Color GetColor(int id)
        {
            return SystemColor.GetMonogameColor(Game.System.GetColorById(id));
        }

        // -------------------------------------------------------------------
        // MapNameExists
        // -------------------------------------------------------------------

        public static bool MapNameExists(string mapName)
        {
            string[] dirPaths = Directory.GetDirectories(MapsDirectoryPath);
            for (int i = 0; i < dirPaths.Length; i++)
            {
                if (Path.GetFileName(dirPaths[i]) == mapName) return true;
            }
            return false;
        }

        // -------------------------------------------------------------------
        // GenerateMapName
        // -------------------------------------------------------------------

        public static string GenerateMapName()
        {
            string mapName = "";
            int nbMaps = Directory.GetDirectories(MapsDirectoryPath).Length;
            for (int i = 0; i <= nbMaps; i++)
            {
                mapName = string.Format("MAP{0:D4}", (i + 1));
                if (!MapNameExists(mapName)) break;
            }
            return mapName;
        }

        // -------------------------------------------------------------------
        // DisposeProgressBar
        // -------------------------------------------------------------------

        public static void DisposeProgressBar()
        {
            DialogProgressBar.Stop();
            DialogProgressBar.Hide();
            DialogProgressBar.Dispose();
            DialogProgressBar = null;
        }

        // -------------------------------------------------------------------
        // DisposeProgressBar
        // -------------------------------------------------------------------

        public static void StartProgressBar(string text, int value, bool top = true)
        {
            DialogProgressBar = new DialogProgressBar(text, top);
            DialogProgressBar.Show();
            DialogProgressBar.SetValue(value);
        }

        // -------------------------------------------------------------------
        // IsInPortions
        // -------------------------------------------------------------------

        public static bool IsInPortions(int[] portion, int offset = 0)
        {
            return (portion[0] <= (PORTION_RADIUS + offset) && portion[0] >= -(PORTION_RADIUS + offset) && portion[1] <= (PORTION_RADIUS + offset) && portion[1] >= -(PORTION_RADIUS + offset));
        }

        // -------------------------------------------------------------------
        // Load / Save
        // -------------------------------------------------------------------

        public static GameMapPortion LoadPortionMap(string mapName, int i, int j)
        {
            string path = Path.Combine(MapsDirectoryPath, mapName, "temp", i + "-" + j + ".pmap");
            if (File.Exists(path)) return LoadBinaryDatas<GameMapPortion>(path);
            else return null;
        }

        public static MapInfos LoadMapInfos(string mapName)
        {
            return LoadBinaryDatas<MapInfos>(Path.Combine(MapsDirectoryPath, mapName, "temp", "infos.map"));
        }

        public static Events LoadEvents(string mapName)
        {
            return LoadBinaryDatas<Events>(Path.Combine(MapsDirectoryPath, mapName, "temp", "events.map"));
        }

        public static void SavePortionMap(GameMapPortion map, string mapName, int i, int j)
        {
            string path = Path.Combine(MapsDirectoryPath, mapName, "temp", i + "-" + j + ".pmap");
            
            if (map == null)
            {
                if (File.Exists(path)) File.Delete(path);
            }
            else
            {
                SaveBinaryDatas(map, path);
            }
        }

        public static void SaveMapInfos(MapInfos mapInfos, string mapName)
        {
            SaveBinaryDatas(mapInfos, Path.Combine(MapsDirectoryPath, mapName, "temp", "infos.map"));
        }

        public static void SaveEventsMap(Events events, string mapName)
        {
            SaveBinaryDatas(events, Path.Combine(MapsDirectoryPath, mapName, "temp", "events.map"));
        }

        // -------------------------------------------------------------------
        // ShowActionMessage
        // -------------------------------------------------------------------

        public static void ShowActionMessage()
        {
            MessageBox.Show("Action unavailable now.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        // -------------------------------------------------------------------
        // CANCEL
        // -------------------------------------------------------------------

        public static void ResetCancel()
        {
            CancelRedo = new Dictionary<string, List<Dictionary<int[], object>>>();
            PortionsToAddCancel = new List<int[]>();
            CancelRedoIndex = new Dictionary<string, int>();
            CanStartCancel = true;
        }

        public static void CreateCancelMap(string mapName)
        {
            CancelRedo[mapName] = new List<Dictionary<int[], object>>();
            CancelRedoIndex[mapName] = 0;
            CancelRedo[mapName].Add(new Dictionary<int[], object>(new IntArrayComparer()));
        }

        public static void CreateCancel(string mapName, bool flagOK = false)
        {
            if (flagOK || (CanStartCancel && (MapMouseManager.IsButtonDown(MouseButtons.Left) || KeyboardManager.IsButtonDown(Settings.KeyboardAssign.EditorDrawCursor) || MapMouseManager.IsButtonDown(MouseButtons.Right))))
            {
                int size = CancelRedo[mapName].Count;

                int lim = -1;
                for (int i = 0; i < size; i++)
                {
                    if (lim == -1 && i > CancelRedoIndex[mapName]) lim = i;
                    if (lim != -1)
                    {
                        CancelRedo[mapName].RemoveAt(lim);
                    }
                }
                if (CancelRedo[mapName].Count == MAX_CANCEL)
                {
                    CancelRedo[mapName].RemoveAt(0);
                    CancelRedoIndex[mapName]--;
                }

                CancelRedo[mapName].Add(new Dictionary<int[], object>(new IntArrayComparer()));
                CancelRedoIndex[mapName]++;
                CanStartCancel = false;
            }
        }

        public static void AddPortionsToAddCancel(string mapName, int[] portion, int kind = 0)
        {
            // Create a special portion
            int[] newPortion = new int[portion.Length + 1];
            newPortion[0] = kind;
            for (int i = 1; i < newPortion.Length; i++)
            {
                newPortion[i] = portion[i - 1];
            }

            // Adding to list
            for (int i = PortionsToAddCancel.Count - 1; i >= 0; i--)
            {
                if (PortionsToAddCancel[i].SequenceEqual(newPortion)) return;
            }
            PortionsToAddCancel.Add(newPortion);

            // Checking the previous cancel portion
            if (!CancelRedo[mapName][CancelRedoIndex[mapName] - 1].ContainsKey(newPortion))
            {
                switch (kind)
                {
                    case 0:
                        CancelRedo[mapName][CancelRedoIndex[mapName] - 1][newPortion] = LoadPortionMap(mapName, portion[0], portion[1]);
                        break;
                    case 1:
                        Events events = LoadEvents(mapName);
                        CancelRedo[mapName][CancelRedoIndex[mapName] - 1][newPortion] = events.CompleteList.ContainsKey(portion) ? events.CompleteList[portion] : null;
                        break;
                    case 2:
                        CancelRedo[mapName][CancelRedoIndex[mapName] - 1][newPortion] = LoadMapInfos(mapName);
                        break;
                }
            }
        }

        public static void LoadCancel(string mapName)
        {
            for (int i = 0; i < PortionsToAddCancel.Count; i++)
            {
                switch (PortionsToAddCancel[i][0])
                {
                    case 0:
                        CancelRedo[mapName][CancelRedoIndex[mapName]][PortionsToAddCancel[i]] = LoadPortionMap(mapName, PortionsToAddCancel[i][1], PortionsToAddCancel[i][2]);
                        break;
                    case 1:
                        Events events = LoadEvents(mapName);
                        int[] portion = new int[] { PortionsToAddCancel[i][1], PortionsToAddCancel[i][2] };
                        CancelRedo[mapName][CancelRedoIndex[mapName]][PortionsToAddCancel[i]] = events.CompleteList.ContainsKey(portion) ? events.CompleteList[portion] : null;
                        break;
                    case 2:
                        CancelRedo[mapName][CancelRedoIndex[mapName]][PortionsToAddCancel[i]] = LoadMapInfos(mapName);
                        break;
                }
            }
            PortionsToAddCancel.Clear();
        }

        // -------------------------------------------------------------------
        // GetSuperListItem
        // -------------------------------------------------------------------

        public static List<SuperListItem> GetSuperListItem(List<SuperListItem> list)
        {
            List<SuperListItem> newList = new List<SuperListItem>();
            for (int i = 0; i < list.Count; i++)
            {
                newList.Add(list[i].CreateCopy());
            }

            return newList;
        }

        // -------------------------------------------------------------------
        // CheckControls
        // -------------------------------------------------------------------

        public static void CheckControls(List<Control[]> list, int index)
        {
            UnCheckAllControls(list);

            for (int i = 0; i < list[index].Length; i++)
            {
                list[index][i].Enabled = true;
            }
        }

        public static void UnCheckAllControls(List<Control[]> list, bool enable = false)
        {
            for (int i = 0; i < list.Count; i++)
            {
                for (int j = 0; j < list[i].Length; j++)
                {
                    list[i][j].Enabled = enable;
                }
            }
        }

        public static void UnCheckRadios(List<RadioButton> radioButtons, bool enable = false)
        {
            for (int i = 0; i < radioButtons.Count; i++)
            {
                radioButtons[i].Checked = enable;
            }
        }

        public static void UnableRadios(List<RadioButton> radioButtons, bool enable = false)
        {
            for (int i = 0; i < radioButtons.Count; i++)
            {
                radioButtons[i].Enabled = enable;
            }
        }

        // -------------------------------------------------------------------
        // DrawTypeToString
        // -------------------------------------------------------------------

        public static string DrawTypeToString(DrawType type)
        {
            switch (type)
            {
                case DrawType.None:
                    return "None";
                case DrawType.Floors:
                    return "Floor";
                case DrawType.Autotiles:
                    return "Autotile";
                case DrawType.FaceSprite:
                    return "Face Sprite";
                case DrawType.FixSprite:
                    return "Fix Sprite";
                case DrawType.DoubleSprite:
                    return "Double Sprite";
                case DrawType.QuadraSprite:
                    return "Quadra Sprite";
                case DrawType.OnFloorSprite:
                    return "On floor Sprite";
                case DrawType.Montains:
                    return "Mountain";
                default:
                    return "";
            }
        }

        // -------------------------------------------------------------------
        // GetGeneralDrawType
        // -------------------------------------------------------------------

        public static GeneralDrawtype GetGeneralDrawType(DrawType type)
        {
            switch (type)
            {
                case DrawType.None:
                    return GeneralDrawtype.None;
                case DrawType.Floors:
                    return GeneralDrawtype.Floor;
                case DrawType.Autotiles:
                    return GeneralDrawtype.Autotile;
                case DrawType.FaceSprite:
                    return GeneralDrawtype.Sprite;
                case DrawType.FixSprite:
                    return GeneralDrawtype.Sprite;
                case DrawType.DoubleSprite:
                    return GeneralDrawtype.Sprite;
                case DrawType.QuadraSprite:
                    return GeneralDrawtype.Sprite;
                case DrawType.OnFloorSprite:
                    return GeneralDrawtype.Sprite;
                case DrawType.Montains:
                    return GeneralDrawtype.Relief;
                default:
                    return GeneralDrawtype.None;
            }
        }
    }
}
