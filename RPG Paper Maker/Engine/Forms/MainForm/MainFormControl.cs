using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RPG_Paper_Maker
{
    public class MainFormControl
    {
        public const int MAX_MAP = 9999;
        public int HeightSquare;
        public int HeightPixel;
        public int HeightSquareMountain;
        public int HeightPixelMountain;
        public int MountainAngle;

        public void InitializeMain()
        {
            // Creating RPG Paper Maker Games folder
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            path = Path.Combine(path, "RPG Paper Maker Games");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            // This code created the basic engine settings
            /*
            WANOK.SaveDatas(new EngineSettings(), "EngineSettings.JSON");
            */
            

            // Getting engine settings
            WANOK.Settings = WANOK.LoadDatas<EngineSettings>(WANOK.PATHSETTINGS);

            // Updating special infos
            WANOK.ABSOLUTEENGINEPATH = Path.GetDirectoryName(WANOK.ExcecutablePath);
            ClearHeight();
        }

        // -------------------------------------------------------------------
        // SetTitle
        // -------------------------------------------------------------------

        public string SetTitle(string dir)
        {
            WANOK.CurrentDir = dir;
            WANOK.Game.LoadDatas();
            return SetTitle();
        }

        public string SetTitle()
        {
            return WANOK.LoadBinaryDatas<SystemDatas>(WANOK.SystemPath).GameName[WANOK.CurrentLang];
        }

        // -------------------------------------------------------------------
        // OpenNewDialog
        // -------------------------------------------------------------------

        public void OpenNewDialog()
        {
            WANOK.KeyboardManager.InitializeKeyboard();
        }

        // -------------------------------------------------------------------
        // CloseProject
        // -------------------------------------------------------------------

        public void CloseProject()
        {
            WANOK.CurrentDir = ".";
            WANOK.SelectedNode = null;
        }


        // -------------------------------------------------------------------
        // SaveTreeMap
        // -------------------------------------------------------------------

        public void SaveTreeMap(TreeView treeMap)
        {
            WANOK.SaveTree(treeMap, Path.Combine(new string[] { WANOK.CurrentDir, "Content", "Datas", "Maps", "TreeMapDatas.rpmdatas" }));
        }

        // -------------------------------------------------------------------
        // FindRootNode
        // -------------------------------------------------------------------

        public TreeNode FindRootNode(TreeNode treeNode)
        {
            while (treeNode.Parent != null)
            {
                treeNode = treeNode.Parent;
            }
            return treeNode;
        }

        // -------------------------------------------------------------------
        // IsAChild
        // -------------------------------------------------------------------

        public bool IsATreeChild(TreeNode node, TreeNode parent)
        {
            while (node.Parent != null)
            {
                if (node == parent) return true;
                node = node.Parent;
            }
            return false;
        }

        // -------------------------------------------------------------------
        // DeleteMapDirectory
        // -------------------------------------------------------------------

        public void DeleteMapsDirectory(string mapName)
        {
            Directory.Delete(Path.Combine(WANOK.MapsDirectoryPath, mapName, "temp"), true);
            Directory.Delete(Path.Combine(WANOK.MapsDirectoryPath, mapName), true);

            WANOK.ListMapToSave.Remove(mapName);
        }

        // -------------------------------------------------------------------
        // LoadMapInfos
        // -------------------------------------------------------------------

        public MapInfos LoadMapInfos(string mapName)
        {
            return WANOK.LoadBinaryDatas<MapInfos>(Path.Combine(WANOK.MapsDirectoryPath, mapName, "temp", "infos.map"));
        }

        // -------------------------------------------------------------------
        // SetHeight
        // -------------------------------------------------------------------

        public void SetHeight(bool square, bool up)
        {
            int add = up ? 1 : -1;
            if (square) HeightSquare += add;
            else {
                HeightPixel += add;
                if (HeightPixel > WANOK.SQUARE_SIZE - 1) HeightPixel = 0;
                else if (HeightPixel < 0) HeightPixel = WANOK.SQUARE_SIZE - 1;
            }
        }

        // -------------------------------------------------------------------
        // GetHeight
        // -------------------------------------------------------------------

        public int[] GetHeight()
        {
            return new int[] { HeightSquare, HeightPixel };
        }

        // -------------------------------------------------------------------
        // SetMountainHeight
        // -------------------------------------------------------------------

        public void SetMountainHeight(bool square, bool up)
        {
            int add = up ? 1 : -1;
            if (square)
            {
                HeightSquareMountain += add;
                if (HeightSquareMountain < 0) HeightSquareMountain = 0;
            }
            else {
                HeightPixelMountain += add;
                if (HeightPixelMountain > WANOK.SQUARE_SIZE - 1) HeightPixelMountain = 0;
                else if (HeightPixelMountain < 0) HeightPixelMountain = WANOK.SQUARE_SIZE - 1;
            }
        }

        // -------------------------------------------------------------------
        // GetHeight
        // -------------------------------------------------------------------

        public int[] GetMountainHeight()
        {
            return new int[] { HeightSquareMountain, HeightPixelMountain };
        }

        // -------------------------------------------------------------------
        // ClearHeight
        // -------------------------------------------------------------------

        public void ClearHeight()
        {
            HeightPixel = 0;
            HeightSquare = 0;
            HeightSquareMountain = 1;
            HeightPixelMountain = 0;
            MountainAngle = 90;
        }

        // -------------------------------------------------------------------
        // DeleteAllTemp
        // -------------------------------------------------------------------

        public void DeleteAllTemp(string exept = null)
        {
            foreach (string mapName in WANOK.ListMapToSave)
            {
                if (mapName != exept) DeleteTemp(mapName, false);
            }
            WANOK.ListMapToSave.Clear();
        }

        // -------------------------------------------------------------------
        // DeleteTemp
        // -------------------------------------------------------------------

        public void DeleteTemp(string mapName, bool deleteInList = true)
        {
            if (Directory.Exists(Path.Combine(WANOK.MapsDirectoryPath, mapName))){
                string[] filePaths = Directory.GetFiles(Path.Combine(WANOK.MapsDirectoryPath, mapName, "temp"));
                foreach (string filePath in filePaths) File.Delete(filePath);
            }
            if (deleteInList) WANOK.ListMapToSave.Remove(mapName);
        }

        // -------------------------------------------------------------------
        // SaveMap
        // -------------------------------------------------------------------

        public void SaveMap(string mapName = null, bool deleteInList = true)
        {
            if (mapName == null) mapName = ((TreeTag)WANOK.SelectedNode.Tag).RealMapName;
            if (WANOK.ListMapToSave.Contains(mapName))
            {
                // Delete all the files
                string[] filePaths = Directory.GetFiles(Path.Combine(WANOK.MapsDirectoryPath, mapName));
                foreach (string filePath in filePaths) File.Delete(filePath);

                // Remplace it by temp files
                filePaths = Directory.GetFiles(Path.Combine(WANOK.MapsDirectoryPath, mapName, "temp"));
                foreach (string filePath in filePaths) File.Copy(filePath, Path.Combine(WANOK.MapsDirectoryPath, mapName, Path.GetFileName(filePath)));
                if (deleteInList) WANOK.ListMapToSave.Remove(mapName);
            }
        }

        // -------------------------------------------------------------------
        // SaveAllMaps
        // -------------------------------------------------------------------

        public void SaveAllMaps(bool deleteAllTemps)
        {
            foreach (string mapName in WANOK.ListMapToSave)
            {
                SaveMap(mapName, false);
            }
            if (deleteAllTemps) DeleteAllTemp();
            else DeleteAllTemp(((TreeTag)WANOK.SelectedNode.Tag).RealMapName);
        }

        // -------------------------------------------------------------------
        // PasteMap
        // -------------------------------------------------------------------

        public string PasteMap(string mapName)
        {
            string newMapName = WANOK.GenerateMapName();

            Directory.CreateDirectory(Path.Combine(WANOK.MapsDirectoryPath, newMapName));
            string[] filePaths = Directory.GetFiles(Path.Combine(WANOK.MapsDirectoryPath, mapName, "temp"));
            if (filePaths.Length == 0) filePaths = Directory.GetFiles(Path.Combine(WANOK.MapsDirectoryPath, mapName));
            foreach (string filePath in filePaths) File.Copy(filePath, Path.Combine(WANOK.MapsDirectoryPath, newMapName, Path.GetFileName(filePath)));
            Directory.CreateDirectory(Path.Combine(WANOK.MapsDirectoryPath, newMapName, "temp"));
            string infosPath = Path.Combine(WANOK.MapsDirectoryPath, newMapName, "infos.map");
            MapInfos mapInfos = WANOK.LoadBinaryDatas<MapInfos>(infosPath);
            mapInfos.RealMapName = newMapName;
            WANOK.SaveBinaryDatas(mapInfos, infosPath);

            return newMapName;
        }
    }
}
