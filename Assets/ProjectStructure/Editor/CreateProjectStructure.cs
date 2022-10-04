using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
using System.IO;

namespace ProjectStructureGenerator
{
    class Folder
    {
        public string DirPath { get; private set; }
        public string ParentPath { get; private set; }
        public string Name;
        public List<Folder> Subfolders;

        public Folder Add(string name)
        {
            Folder subfolder = null;
            if (ParentPath.Length > 0)
                subfolder = new Folder(name, ParentPath + Path.DirectorySeparatorChar + Name);
            else
                subfolder = new Folder(name, Name);

            Subfolders.Add(subfolder);
            return subfolder;
        }

        public Folder(string name, string dirPath)
        {
            Name = name;
            ParentPath = dirPath;
            DirPath = ParentPath + Path.DirectorySeparatorChar + Name;
            Subfolders = new List<Folder>();
        }
    }

    public class CreateProjectStructure : EditorWindow
    {
        private string RootName = "";

        public void Execute()
        {
            if (string.IsNullOrEmpty(RootName))
            {
                Debug.LogError("Can't Create Folder Structure, Need Input Root Name");
                return;
            }
            var assets = GenerateFolderStructure();
            CreateFolders(assets);
        }

        [MenuItem("Tools/Generate Project Structure")]
        static void Init()
        {
            CreateProjectStructure window = (CreateProjectStructure)GetWindow(typeof(CreateProjectStructure));
            window.Show();
        }
        void OnGUI()
        {
            GUIStyle textFieldGUI = new GUIStyle(GUI.skin.textField) { alignment = TextAnchor.LowerLeft };
            GUIStyle titleStyle = new GUIStyle(GUI.skin.label) { fixedWidth = 70, alignment = TextAnchor.MiddleLeft };

            GUIStyle style = new GUIStyle(GUI.skin.button) { fixedWidth = 280f, fixedHeight = 35, fontSize = 14 };
            GUILayout.BeginVertical();
            GUILayout.BeginHorizontal();

            GUILayout.Label("Root Name", titleStyle);
            RootName = GUILayout.TextField(RootName, textFieldGUI);
            GUILayout.EndHorizontal();
            GUILayout.Space(5);
            if (GUILayout.Button("Create Structure", style))
                Execute();
            GUILayout.EndVertical();
        }

        private void CreateFolders(Folder rootFolder)
        {
            if (!AssetDatabase.IsValidFolder(rootFolder.DirPath))
            {
                Debug.Log("Creating: <b>" + rootFolder.DirPath + "</b>");
                AssetDatabase.CreateFolder(rootFolder.ParentPath, rootFolder.Name);
                File.Create(Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + rootFolder.DirPath + Path.DirectorySeparatorChar + ".keep");
            }
            else
            {
                if (Directory.GetFiles(Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + rootFolder.DirPath).Length < 1)
                {
                    File.Create(Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + rootFolder.DirPath + Path.DirectorySeparatorChar + ".keep");
                    Debug.Log("Creating '.keep' file in: <b>" + rootFolder.DirPath + "</b>");
                }
                else
                {
                    Debug.Log("Directory <b>" + rootFolder.DirPath + "</b> already exists");
                }
            }

            foreach (var folder in rootFolder.Subfolders)
            {
                CreateFolders(folder);
            }
        }

        private Folder GenerateFolderStructure()
        {
            Folder rootFolder = new Folder("Assets", "");
            var Structure = rootFolder.Add(RootName);
            var art_s = Structure.Add("00_Art");
            var Program_s = Structure.Add("01_Program");
            ArtStructure(ref art_s);
            ProgramStructure(ref Program_s);
            return rootFolder;
        }

        private void ProgramStructure(ref Folder structure)
        {
            structure.Add("00_Scenes");
            structure.Add("01_Scripts");
            structure.Add("02_Test");
            structure.Add("03_Prefabs");
            structure.Add("04_UI");
        }

        private void ArtStructure(ref Folder structure)
        {
            var Prefabs_s1 = structure.Add("00_Prefabs");
            var Shaders_s1 = structure.Add("01_Shaders");
            structure.Add("02_Timelines");
            var Models_s1 = structure.Add("03_Models");
            structure.Add("04_Scenes");
            var UI_s1 = structure.Add("05_UI");
            structure.Add("06_Audios");
            structure.Add("07_Videos");

            /// Prefabs
            Prefabs_s1.Add("Models");
            Prefabs_s1.Add("UI");

            /// Shaders
            Shaders_s1.Add("UI_Shaders");

            /// Models
            var ModelsExampleModel_s2 = Models_s1.Add("Example_Model");
            ModelsExampleModel_s2.Add("3D");
            ModelsExampleModel_s2.Add("Animations");
            ModelsExampleModel_s2.Add("Textures");

            var ModelsExampleEffect_s2 = Models_s1.Add("Example_Effect");
            ModelsExampleEffect_s2.Add("Textures");

            /// UI
            UI_s1.Add("00_Textures");
            var UIExampleEffect_s2 = UI_s1.Add("Example_Effect");
            UIExampleEffect_s2.Add("Textures");
            UIExampleEffect_s2.Add("Animations");
            UIExampleEffect_s2.Add("Material");
        }
    }
}
