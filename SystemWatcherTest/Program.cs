using System;
using System.IO;

namespace SystemWatcherTest
{
    public class Program
    {
        static void Main(string[] args)
        {
            string folderPath = @"C:\Demo";
            string secondFolderPath = @"C:\Demo\YO wtf";
            string fixString = "Hello";
            int counter = 0;


            var watcher = CreateFileWatcher(folderPath);
            Console.ReadLine();
            watcher.Path = secondFolderPath;

            Console.ReadLine();

            /// <summary>
            /// Log buffer overloading errors.
            /// </summary>
            void LogBufferError(object sender, ErrorEventArgs e)
            {
                
                string log = string.Format("{0:G} | Buffer limit exceeded", DateTime.Now);
                Console.WriteLine(log);
            }



            /// <summary>
            /// Process renaming actions.
            /// </summary>
            void LogFileSystemRenaming(object sender, RenamedEventArgs e)
            {
                
                string log = string.Format("{0:G} | {1} | Renamed from {2}", DateTime.Now, e.FullPath, e.OldName);
                Console.WriteLine(log);
            }



            /// <summary>
            /// Process creations, modifications and deletions.
            /// </summary>
            void LogFileSystemChanges(object sender, FileSystemEventArgs e)
            {
                
                string log = string.Format("{0:G} | {1} | {2}", DateTime.Now, e.FullPath, e.ChangeType, e.Name);
                Console.WriteLine(log);
            }

            FileSystemWatcher CreateFileWatcher(string folderPath)
            {
                fixString += "iteration";
                counter += 1;
                FileSystemWatcher watcher = new FileSystemWatcher();
                watcher.Path = folderPath;


                watcher.Changed += new FileSystemEventHandler(LogFileSystemChanges);
                watcher.Created += new FileSystemEventHandler(LogFileSystemChanges);
                watcher.Deleted += new FileSystemEventHandler(LogFileSystemChanges);
                watcher.Renamed += new RenamedEventHandler(LogFileSystemRenaming);
                watcher.Error += new ErrorEventHandler(LogBufferError);


                watcher.EnableRaisingEvents = true;
                Console.WriteLine("A watcher has been created");
                return (watcher);
            }
        }


      

    }
}
