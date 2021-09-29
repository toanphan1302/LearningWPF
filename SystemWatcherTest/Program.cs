using System;
using System.IO;

namespace SystemWatcherTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = @"C:\Demo";
            NotifyFilters notificationFilters = new NotifyFilters();
            notificationFilters = notificationFilters | NotifyFilters.Attributes;
            notificationFilters = notificationFilters | NotifyFilters.CreationTime;
            notificationFilters = notificationFilters | NotifyFilters.DirectoryName;
            notificationFilters = notificationFilters | NotifyFilters.FileName;
            notificationFilters = notificationFilters | NotifyFilters.LastAccess;
            notificationFilters = notificationFilters | NotifyFilters.LastWrite;
            notificationFilters = notificationFilters | NotifyFilters.Security;
            notificationFilters = notificationFilters | NotifyFilters.Size;
            watcher.NotifyFilter = notificationFilters;

            watcher.IncludeSubdirectories = true;
            watcher.Changed += new FileSystemEventHandler(LogFileSystemChanges);
            watcher.Created += new FileSystemEventHandler(LogFileSystemChanges);
            watcher.Deleted += new FileSystemEventHandler(LogFileSystemChanges);
            watcher.Renamed += new RenamedEventHandler(LogFileSystemRenaming);
            watcher.Error += new ErrorEventHandler(LogBufferError);

            while (true){ 
            watcher.EnableRaisingEvents = true;
            };

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
                string log = string.Format("{0:G} | {1} | {2}", DateTime.Now, e.FullPath, e.ChangeType);
                Console.WriteLine(log);
            }
        }


      

    }
}
