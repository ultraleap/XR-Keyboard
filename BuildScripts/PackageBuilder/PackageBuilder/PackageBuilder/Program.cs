using System;
using System.Collections.Generic;
using System.IO;
using Ultrahaptics.UnityPackageTool;

namespace PackageBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Tuple<string, string>> paths = GetArrayOfPaths(args[0]);


            //string projectPath = @".\InteractionEngineKeyboards2.0\Assets\XR_Keyboard\Scripts";
            //Tuple<string, string>[] files = new Tuple<string, string>[]
            //{
            //    new Tuple<string, string>("Assets/XR_Keyboard/Input/InputFieldTextReceiver.cs",projectPath + @"\Input\InputFieldTextReceiver.cs")
            //};


            UnityPackage pkg = new UnityPackage("CapableKeyboard");
            foreach (Tuple<string, string> path in paths)
            {
                pkg.Add(path.Item1, path.Item2);
            }
            pkg.Save("Test.unitypackage");
        }

        private static List<Tuple<string, string>> GetArrayOfPaths(string path)
        {
            using (var reader = new StreamReader(path))
            {
                List<Tuple<string, string>> paths = new List<Tuple<string, string>>();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    paths.Add(new Tuple<string, string>(values[0], values[1]));
                }
                return paths;
            }
        }
    }
}
