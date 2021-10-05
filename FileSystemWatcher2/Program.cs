using System;
using System.IO;
using System.Threading;


/// <summary>
/// Trigger on: enter word file but not text file, create file, change file information on word but not text
/// Change ifo trigger twice
/// Not trigger on: delete file, file renamed, not in sub folder
/// </summary>
namespace FileSystemWatcher2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            string folderPath = @"C:\Demo";
            string secondFolderPath = @"C:\Demo\YO wtf";
            FileCreationWatcher watcher1 = new FileCreationWatcher(folderPath,null);
            watcher1.Start();
            Console.WriteLine("Enter to continue program");
            Console.ReadLine();
            watcher1.Dispose();
            // Has been disposed but debugger still show info
            
            //watcher1.Start();


            FileCreationWatcher watcher2 = new FileCreationWatcher(secondFolderPath, null);
            watcher2.Start();
            Console.WriteLine("watcher2 created");
            Console.WriteLine("Enter to end");
            Console.ReadLine();
            
        }

    }
    public sealed class FileCreationWatcher : IDisposable
    {
        private FileSystemWatcher _watcher;

        public FileCreationWatcher(string path, string filter)
        {
            Initialize(path, filter);
        }

        private void Initialize(string path, string filter)
        {
            if (filter != null)
                  { _watcher = new FileSystemWatcher(path, filter); }
                  else _watcher = new FileSystemWatcher(path);
            _watcher.Created += OnFileCreated;
            _watcher.Changed += OnFileChanged;
            _watcher.Deleted += OnFileDeleted;
            _watcher.Renamed += OnFileRenamed;
            _watcher.Error += OnError;
        }
        private void OnFileCreated(object sender, FileSystemEventArgs e)
        {
            FileInfo file = new FileInfo(e.FullPath);
            if (file.Exists)
            {
                // schedule the processing on a different thread
                ThreadPool.QueueUserWorkItem(delegate
                {
                    while (true)
                    {
                        // wait 100 milliseconds between attempts to read
                        Thread.Sleep(TimeSpan.FromMilliseconds(100));
                        try
                        {
                            // try to open the file
                            file.OpenRead().Close();
                            string log = string.Format("{0:G} | {1} | {2}", DateTime.Now, e.FullPath, e.ChangeType, e.Name);
                            Console.WriteLine(log);
                            break;
                        }
                        catch (IOException)
                        {
                            // if the file is still locked, keep trying
                            continue;
                        }
                    }

                    // the file can be opened successfully: raise the event
                    FileSystemEventHandler handler = Created;
                    if (handler != null)
                    {
                        handler(this, e);                        
                    }
                });
            }
        }

        private void OnFileChanged(object sender, FileSystemEventArgs e)
        {
            FileInfo file = new FileInfo(e.FullPath);
            if (file.Exists)
            {
                // schedule the processing on a different thread
                ThreadPool.QueueUserWorkItem(delegate
                {
                    while (true)
                    {
                        // wait 100 milliseconds between attempts to read
                        Thread.Sleep(TimeSpan.FromMilliseconds(100));
                        try
                        {
                            // try to open the file
                            file.OpenRead().Close();
                            string log = string.Format("{0:G} | {1} | {2}", DateTime.Now, e.FullPath, e.ChangeType, e.Name);
                            Console.WriteLine(log);
                            break;
                        }
                        catch (IOException)
                        {
                            // if the file is still locked, keep trying
                            continue;
                        }
                    }

                    // the file can be opened successfully: raise the event
                    FileSystemEventHandler handler = Created;
                    if (handler != null)
                    {
                        handler(this, e);
                    }
                });
            }
        }

        private void OnFileDeleted(object sender, FileSystemEventArgs e)
        {
            FileInfo file = new FileInfo(e.FullPath);
            
                // schedule the processing on a different thread
                ThreadPool.QueueUserWorkItem(delegate
                {
                    
                    // wait 100 milliseconds between attempts to read
                    Thread.Sleep(TimeSpan.FromMilliseconds(100));
                    string log = string.Format("{0:G} | {1} | {2}", DateTime.Now, e.FullPath, e.ChangeType, e.Name);
                    Console.WriteLine(log);
                    

                    // the file can be opened successfully: raise the event
                    FileSystemEventHandler handler = Created;
                    if (handler != null)
                    {
                        handler(this, e);
                    }
                });
        }

        private void OnFileRenamed(object sender, RenamedEventArgs e)
        {
            FileInfo file = new FileInfo(e.FullPath);
            if (file.Exists)
            {
                // schedule the processing on a different thread
                ThreadPool.QueueUserWorkItem(delegate
                {
                    while (true)
                    {
                        // wait 100 milliseconds between attempts to read
                        Thread.Sleep(TimeSpan.FromMilliseconds(100));
                        try
                        {
                            // try to open the file
                            file.OpenRead().Close();
                            string log = string.Format("{0:G} | {1} | Renamed from {2}", DateTime.Now, e.FullPath, e.OldName);
                            Console.WriteLine(log);
                            break;
                        }
                        catch (IOException)
                        {
                            // if the file is still locked, keep trying
                            continue;
                        }
                    }

                    // the file can be opened successfully: raise the event
                    FileSystemEventHandler handler = Created;
                    if (handler != null)
                    {
                        handler(this, e);
                    }
                });
            }
        }



        private void OnError(object sender, ErrorEventArgs e)
        {
            // when an error occurs, the current FileSystemWatcher is disposed,
            // and a new one is created

            string path = _watcher.Path;
            string filter = _watcher.Filter;
            _watcher.Dispose();
            Initialize(path, filter);
        }

        public void Start()
        {
            _watcher.EnableRaisingEvents = true;
        }

        public void Stop()
        {
            _watcher.EnableRaisingEvents = false;
        }

        public event FileSystemEventHandler Created;

        public void Dispose()
        {
            _watcher.Dispose();
        }
    }
}
