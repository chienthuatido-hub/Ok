using System;
using System.IO;
using System.Linq;

namespace ConsoleApplication
{
    class Program
    {
        static void Print(string path)
        {
            try
            {
                if (!Directory.Exists(path))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error: Thu muc khong ton tai.");
                    return;
                }
                var listDirs = Directory.EnumerateDirectories(path);
                var listFiles = Directory.EnumerateFiles(path, "");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Danh sach cac thu muc (" + listDirs.Count() + "): ");
                Console.ForegroundColor = ConsoleColor.Green;
                foreach (var currentDir in listDirs)
                {
                    Console.WriteLine('\t' + currentDir.Substring(path.Length + 1).PadRight(30) 
                        + '\t' + Directory.GetCreationTime(currentDir) + '\t' + Directory.GetParent(currentDir));
                }
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Danh sach cac tep (" + listFiles.Count() + "): ");
                Console.ForegroundColor = ConsoleColor.Green;
                foreach (var currentFile in listFiles)
                {
                    Console.WriteLine("\t" + currentFile.Substring(path.Length + 1).PadRight(30)
                        + '\t' + File.GetCreationTime(currentFile) + '\t' + File.GetAttributes(currentFile));
                }
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Tong cong: " + (listFiles.Count() + listDirs.Count()) + " tep va thu muc.");
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error: " + e.Message);
            }
        }

        static void CreateDir(string path)
        {
            try
            {
                if (!(Directory.Exists(path) || File.Exists(path)))
                {
                    Directory.CreateDirectory(path);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Thu muc duoc tao thanh cong.");
                    return;
                }
                Console.ForegroundColor = ConsoleColor.Red;
                if (Directory.Exists(path))
                    Console.WriteLine("Canh bao: Thu muc da ton tai.");
                else
                    Console.WriteLine("Canh bao: Tep da ton tai.");
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error: " + e.Message);
            }
        }

        static void CreateFile(string path)
        {
            try
            {
                if (!(File.Exists(path) || Directory.Exists(path)))
                {
                    File.CreateText(path);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Tep duoc tao thanh cong.");
                    return;
                }
                Console.ForegroundColor = ConsoleColor.Red;
                if (Directory.Exists(path))
                    Console.WriteLine("Canh bao: Thu muc da ton tai.");
                else
                    Console.WriteLine("Canh bao: Tep da ton tai.");
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error: " + e.Message);
            }
        }

        static void Delete(string path)
        {
            try
            {
                if (Directory.Exists(path))
                {
                    Directory.Delete(path, true);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Thu muc da duoc xoa.");
                }
                else if (File.Exists(path))
                {
                    File.Delete(path);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Tep da duoc xoa.");
                }
                else 
                {
                    string[] tmps = path.Split(@"\");
            
                    string tmp = tmps[tmps.Length - 1];
                    //Console.WriteLine(tmp);
                    if (tmp.IndexOf("*.") != -1)
                    {
                        tmps = tmp.Split('*');
                        string tmp2 = tmps[1];
                        var files = Directory.GetFiles(path.Substring(0, path.Length - tmp.Length - 1),"*" + tmp2);
                        foreach (var file in files)
                        {
                            File.Delete(file);
                        }

                        Console.ForegroundColor = ConsoleColor.Green;
                        if (files.Length > 0)
                            Console.WriteLine("Tep da duoc xoa.");

                        var dirs = Directory.GetDirectories(path.Substring(0, path.Length - tmp.Length - 1),"*" + tmp2);
                        foreach (var dir in dirs)
                        {
                            Directory.Delete(dir);
                        }
                        if (dirs.Length > 0)
                            Console.WriteLine("Thu muc da duoc xoa.");
                        if (dirs.Length != 0 || files.Length != 0)
                            return;
                    }

                    else if (tmp.IndexOf(".*") != -1)
                    {
                        tmps = tmp.Split('*');
                        string tmp2 = tmps[0];
                        var files = Directory.GetFiles(path.Substring(0, path.Length - tmp.Length - 1), tmp2 + "*");
                        foreach (var file in files)
                        {
                            File.Delete(file);
                        }

                        Console.ForegroundColor = ConsoleColor.Green;
                        if (files.Length > 0)
                            Console.WriteLine("Tep da duoc xoa.");

                        var dirs = Directory.GetDirectories(path.Substring(0, path.Length - tmp.Length - 1), tmp2 + "*");
                        
                        foreach (var dir in dirs)
                        {
                            Directory.Delete(dir);
                        }
                        if (dirs.Length > 0)
                            Console.WriteLine("Thu muc da duoc xoa.");
                        if (dirs.Length != 0 || files.Length != 0)
                            return;
                    }
                    
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Muc tieu khong ton tai");
                }
                    
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error: " + e.Message);
            }
        }
        static void Main(string[] args)
        {
            //args = @"D:\phu".Split(' ');
            if (args.Length == 1)
                Print(args[0]);
            else if (args.Length == 2)
                if (args[0] == "createdir")
                    CreateDir(args[1]);
                else if (args[0] == "createfile")
                    CreateFile(args[1]);
                else if (args[0] == "delete")
                    Delete(args[1]);
            Console.ResetColor();
        }
    }
}

