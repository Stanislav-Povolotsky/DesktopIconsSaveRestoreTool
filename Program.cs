// 
//  Author:     Stanislav Povolotsky <stas.dev[at]povolotsky.info>
//  Created:       
// 
using System;
using System.Collections.Generic;
using System.Text;
using IconsSaveRestore.Code;

namespace IconsSaveRestore
{
    class Program
    {
        public static void SavePositions(string sFileName, bool bSaveReg)
        {
            DesktopRegistry _registry = new DesktopRegistry();
            Desktop _desktop = new Desktop();
            Storage _storage = new Storage(sFileName);

            var registryValues = bSaveReg ? _registry.GetRegistryValues() : 
                new Dictionary<string, string>();

            var iconPositions = _desktop.GetIconsPositions();

            _storage.SaveIconPositions(iconPositions, registryValues);
        }

        public static void RestorePositions(string sFileName, bool bLoadReg)
        {
            DesktopRegistry _registry = new DesktopRegistry();
            Desktop _desktop = new Desktop();
            Storage _storage = new Storage(sFileName);

            var registryValues = bLoadReg ? _storage.GetRegistryValues() : 
                new Dictionary<string, string>();

            _registry.SetRegistryValues(registryValues);

            var iconPositions = _storage.GetIconPositions();
            //Console.WriteLine("Loaded {0} icons", System.Linq.Enumerable.Count(iconPositions));

            _desktop.SetIconPositions(iconPositions);

            _desktop.Refresh();
        }

        static void Usage()
        {
            Console.WriteLine(
                "{0} is command line tool, used to save and restore position of desktop icons\n" +
                "Source code is available at https://github.com/Stanislav-Povolotsky/DesktopIconsSaveRestoreTool/\n" +
                "\n" +
                "Usage:\n" + 
                "\t{0} [save|load] <file_path> [/with-reg]\n" +
                "Example 1:\n" +
                "\trem Saving position of desktop icons to the XML file\n" +
                "\t{0} save \"C:\\Users\\s.povolotsky\\Documents\\desktop.xml\"\n" +
                "Example 2:\n" +
                "\trem Loading position of desktop icons from the XML file\n" +
                "\t{0} load \"C:\\Users\\s.povolotsky\\Documents\\desktop.xml\"\n"
                , System.AppDomain.CurrentDomain.FriendlyName);
            Environment.Exit(1);
        }

        static void Main(string[] args)
        {
            try
            {
                if (args.Length < 1 || args[0] == "/?" || args[0] == "/help" || args[0] == "--help")
                {
                    Usage();
                }
                
                string sAction;
                string sFile;
                int nPos = 0;
                
                if(args[0] != "load" && args[0] != "save") {
                    sAction = "load";
                }
                else {
                    sAction = args[nPos++];
                }

                if (nPos >= args.Length) Usage();
                sFile = args[nPos++];

                bool bOptForceUseReg = false;

                while(nPos < args.Length)
                {
                    string sArg = args[nPos++];
                    if (string.Compare(sArg, "/with-reg", true) == 0)
                    {
                        bOptForceUseReg = true;
                    }
                    else
                    {
                        Console.WriteLine("Invalid option: {0}", sArg);
                        Usage();
                    }
                }


                if (string.Compare(sAction, "save", true) == 0)
                {
                    Console.WriteLine("Saving icon positions to the file {0}", sFile);
                    SavePositions(sFile, bOptForceUseReg);
                    Console.WriteLine("Done");
                }
                else if (string.Compare(sAction, "load", true) == 0 || string.Compare(sAction, "restore", true) == 0)
                {
                    Console.WriteLine("Loading icon positions from the file {0}", sFile);
                    RestorePositions(sFile, bOptForceUseReg);
                    Console.WriteLine("Done");
                }
                else
                {
                    Usage();
                }
            }
            catch(Exception e)
            {
                Console.Write("Error: {0}", e.Message);
                Environment.Exit(e.HResult != 0 ? e.HResult : 2);
            }
        }
    }
}
