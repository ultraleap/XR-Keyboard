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
            if (args.Length == 0)
            {

            }
            else if (args.Length != 2)
            {
                throw new Exception("Need two args - <csv_filepath> <package_name>");
            }

            List<Tuple<string, string>> paths = GetArrayOfPaths(args[0]);

            UnityPackage pkg = new UnityPackage("CapableKeyboard");
            foreach (Tuple<string, string> path in paths)
            {
                pkg.Add(path.Item1, path.Item2);
            }
            pkg.Save(args[1].Trim() + ".unitypackage");
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
                    string packagePath = values[0].Substring(values[0].IndexOf("Assets/", 0));
                    paths.Add(new Tuple<string, string>(packagePath, values[0]));
                    paths.Add(new Tuple<string, string>(packagePath + ".meta", values[0] + ".meta"));

                }
                return paths;
            }
        }
    }
}
