using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        public static float SQUARESIZE = 16.0f;
        public static int PORTIONSIZE = 16;
        public static int PORTION_RADIUS = 10;
        public static string CurrentDir = ".";
        public static string ProjectName = null;
        public static EngineSettings Settings = null;
        public static DemoSteps DemoStep = DemoSteps.None;
        public static Form CurrentDemoDialog = null;
        public static Dictionary<Keys, bool> KeyBoardStates = new Dictionary<Keys, bool>();

        // PATHS
        public static string PATHSETTINGS = "Config/EngineSettings.JSON";



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
            string json = JsonConvert.SerializeObject(obj);
            FileStream fs = new FileStream(path, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(json);
            sw.Close();
            fs.Close();
        }

        // -------------------------------------------------------------------
        // LoadDatas
        // -------------------------------------------------------------------

        public static T LoadDatas<T>(string path)
        {
            FileStream fs = new FileStream(path, FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            string json = sr.ReadToEnd();
            T obj = JsonConvert.DeserializeObject<T>(json);
            sr.Close();
            fs.Close();

            return obj;
        }
    }
}
