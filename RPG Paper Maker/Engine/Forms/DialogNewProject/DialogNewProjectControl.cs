using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Paper_Maker.Controls
{
    public class DialogNewProjectControl : INotifyPropertyChanged
    {
        public Project Model;
        public string ProjectName
        {
            get { return Model.ProjectName; }
            set
            {
                Model.ProjectName = value;
                NotifyPropertyChanged("ProjectName");
            }
        }
        public string DirPath
        {
            get { return Model.DirPath; }
            set
            {
                Model.DirPath = value;
                NotifyPropertyChanged("DirPath");
            }
        }

        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------

        public DialogNewProjectControl()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            path = Path.Combine(path, "RPG Paper Maker Games");
            Model = new Project("Project without name 1", path);
        }

        // -------------------------------------------------------------------
        // INotifyPropertyChanged
        // -------------------------------------------------------------------

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        // -------------------------------------------------------------------
        // LoadDirectoryLocation
        // -------------------------------------------------------------------

        public void LoadDirectoryLocation(string path)
        {
            DirPath = path;
        }

        // -------------------------------------------------------------------
        // CreateProject
        // -------------------------------------------------------------------

        public string CreateProject()
        {
            if (Directory.Exists(DirPath))
            {
                if (ProjectName.Contains("/") || ProjectName.Contains("\\") || ProjectName.Contains(":") || ProjectName.Contains("*") || ProjectName.Contains("?") || ProjectName.Contains("<") || ProjectName.Contains(">") || ProjectName.Contains("\"") || ProjectName.Contains("|") || ProjectName.Trim().Equals("") || ProjectName.Replace('.', ' ').Trim().Equals("") || ProjectName.Contains("..") || ProjectName.Trim()[ProjectName.Trim().Length - 1] == '.')
                {
                    return "Could not create a directory with that name.Do not use / \\ : ? * | < > \". You can't name with an empty field, or \".\" or \"..\" field.";
                }
                else
                {
                    string fullPath = Path.Combine(DirPath, ProjectName);
                    if (!Directory.Exists(fullPath))
                    {
                        try
                        {
                            Directory.CreateDirectory(fullPath);
                            try
                            {
                                string executablePath = Path.GetDirectoryName(WANOK.ExcecutablePath);
                                string basicPath = Path.Combine(executablePath, "Basic");
                                WANOK.CopyAll(new DirectoryInfo(basicPath), new DirectoryInfo(fullPath));
                                WANOK.SaveBinaryDatas(new SystemDatas(ProjectName), Path.Combine(fullPath, "Content", "Datas", "System.rpmd"));
                                WANOK.SaveBinaryDatas(new TilesetsDatas(), Path.Combine(fullPath, "Content", "Datas", "Tilesets.rpmd"));
                                WANOK.SaveBinaryDatas(new HeroesDatas(), Path.Combine(fullPath, "Content", "Datas", "Heroes.rpmd"));
                                Directory.CreateDirectory(Path.Combine(fullPath, "Content", "Pictures"));
                                Directory.CreateDirectory(Path.Combine(fullPath, "Content", "Pictures","Textures2D"));
                                Directory.CreateDirectory(Path.Combine(fullPath, "Content", "Pictures", "Textures2D", "Characters"));
                                Directory.CreateDirectory(Path.Combine(fullPath, "Content", "Pictures", "Textures2D", "Tilesets"));
                                Directory.CreateDirectory(Path.Combine(fullPath, "Content", "Pictures", "Textures2D", "Autotiles"));
                                Directory.CreateDirectory(Path.Combine(fullPath, "Content", "Pictures", "Textures2D", "Reliefs"));
                                Directory.CreateDirectory(Path.Combine(fullPath, "Content", "Pictures", "UI", "Others"));
                                Directory.CreateDirectory(Path.Combine(fullPath, "Content", "Pictures", "UI", "Icons"));
                                Directory.CreateDirectory(Path.Combine(fullPath, "Content", "Pictures", "UI", "Bars"));
                                Directory.CreateDirectory(Path.Combine(fullPath, "Content", "Datas", "Maps", "MAP0001"));
                                Directory.CreateDirectory(Path.Combine(fullPath, "Content", "Datas", "Maps", "MAP0001", "temp"));
                                WANOK.SaveBinaryDatas(new MapInfos(), Path.Combine(fullPath, "Content", "Datas", "Maps", "MAP0001","infos.map"));
                                ProjectName = ProjectName.Trim();
                                DirPath = Path.Combine(DirPath, ProjectName);

                                return null;
                            }
                            catch
                            {
                                return "Could not generate the project. See if you have \"Basic\" folder in the main folder.";
                            }
                        }
                        catch
                        {
                            return "Could not create the directory. Report it to the dev.";
                        }
                    }
                    else
                    {
                        return "This project already exist in this folder. Please change your project name or your folder.";
                    }
                }
            }
            else
            {
                return "The directory path is incorrect.";
            }
        }
    }
}
