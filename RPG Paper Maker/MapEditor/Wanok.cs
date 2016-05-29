using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
        public static int SQUARE_SIZE = 16;
        public static float RELATION_SIZE { get { return (float)(BASIC_SQUARE_SIZE) / SQUARE_SIZE; } }
        public static int PORTIONSIZE = 16;
        public static int PORTION_RADIUS = 10;
        public static string ProjectName = null;
        public static EngineSettings Settings = null;
        public static DemoSteps DemoStep = DemoSteps.None;
        public static Form CurrentDemoDialog = null;
        public static Dictionary<Keys, bool> KeyBoardStates = new Dictionary<Keys, bool>();
        public static MouseManager TilesetMouseManager = new MouseManager();
        public static MouseManager MapMouseManager = new MouseManager();

        // PATHS
        public static string ABSOLUTEENGINEPATH;
        public static string PATHSETTINGS = "Config/EngineSettings.JSON";
        public static string CurrentDir = ".";
        public static string ExcecutablePath { get { return Application.ExecutablePath; } }



        // -------------------------------------------------------------------
        // InitializeKeyBoard
        // -------------------------------------------------------------------

        public static void InitializeKeyBoard()
        {
            foreach (Keys k in Enum.GetValues(typeof(Keys)))
            {
                KeyBoardStates[k] = false;
            }
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

        public static void SaveDatas(Object obj, string path)
        {
            try
            {
                string json = JsonConvert.SerializeObject(obj);
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
            Color[] imagePiece = WANOK.GetImageData(imageData, image.Width, rectangle);
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
                if (((TreeTag)node.Tag).IsMap)
                {
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
            MessageBox.Show("You get a path error. You can send a report to Wanok.rpm@gmail.com.\n" + e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
