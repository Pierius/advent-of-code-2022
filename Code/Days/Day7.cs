using AdventOfCode2022.Code.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Code.Days
{
    internal class Day7 : AbstractDay
    {
        public override Day Day => Day.Seven;
        public override string Title => "No Space Left On Device";
        public override bool EnableSampleInput => false;
        public override string[] SampleInput => new string[]
        {
            "$ cd /",
            "$ ls",
            "dir a",
            "14848514 b.txt",
            "8504156 c.dat",
            "dir d",
            "$ cd a",
            "$ ls",
            "dir e",
            "29116 f",
            "2557 g",
            "62596 h.lst",
            "$ cd e",
            "$ ls",
            "584 i",
            "$ cd ..",
            "$ cd ..",
            "$ cd d",
            "$ ls",
            "4060174 j",
            "8033020 d.log",
            "5626152 d.ext",
            "7214296 k",
        };

        public override void PartOne(string[] puzzleInput)
        {
            FileSystem fileSystem = new FileSystem();

            foreach (var output in puzzleInput)
            {
                fileSystem.ReadOutput(output);
            }

            var dirs = fileSystem.GetDirectoriesOfSize(100000, FileSystem.SizeRestriction.Maximum);

            Answers.Add(dirs.Sum(o => o.totalSize));
        }

        public override void PartTwo(string[] puzzleInput)
        {
            FileSystem fileSystem = new FileSystem();

            foreach (var output in puzzleInput)
            {
                fileSystem.ReadOutput(output);
            }

            int necessarySpace = 30000000 - fileSystem.UnusedSpace;
            var dirs = fileSystem.GetDirectoriesOfSize(necessarySpace, FileSystem.SizeRestriction.Minimum);

            Answers.Add(dirs.Min(o => o.totalSize));
        }

        private class FileSystem
        {
            private string[] _commands = new string[]
            {
                "$ cd /",
                "$ cd ..",
                "$ cd ",
                "$ ls",
                "dir "
            };
            private Directory _activeDirectory;

            public int UnusedSpace
            {
                get {  return 70000000 - RootDirectory.Size; }
            }
            public Directory RootDirectory { get; }

            public FileSystem()
            {
                RootDirectory = new Directory("/");
                _activeDirectory = RootDirectory;
            }

            public void ReadOutput(string output)
            {
                string command = _commands.FirstOrDefault(o => output.StartsWith(o)) ?? "";
                var name = string.Empty;

                switch(command)
                {
                    case "$ cd /":
                        _activeDirectory = RootDirectory;
                        break;
                    case "$ cd ..":
                        if (_activeDirectory.Parent != null)
                        {
                            _activeDirectory = _activeDirectory.Parent;
                        }
                        break;
                    case "$ cd ":
                        name = output.Substring(command.Length - 1);
                        if (_activeDirectory.Directories.Exists(o => o.Name == name))
                        {
                            _activeDirectory = _activeDirectory.Directories.First(o => o.Name == name);
                        }
                        else
                        {
                            _activeDirectory.Directories.Add(new Directory(name) { Parent = _activeDirectory });
                            _activeDirectory = _activeDirectory.Directories.First(o => o.Name == name);
                        }
                        break;
                    case "$ ls":
                        // do nothing
                        break;
                    case "dir ":
                        name = output.Substring(command.Length - 1);
                        break;
                    default:
                        string[] file = output.Split(' ');
                        name = file[1];
                        int size = int.Parse(file[0]);

                        _activeDirectory.Files.Add(new File(name, size));
                        IncreaseDirectorySize(_activeDirectory, size);
                        break;
                }
            }

            public enum SizeRestriction
            {
                Maximum,
                Minimum
            }

            public List<(string directoryName, int totalSize)> GetDirectoriesOfSize(int size, SizeRestriction restriction)
            {
                return GetDirectorySize(RootDirectory, size, restriction);
            }

            private void IncreaseDirectorySize(Directory directory, int size)
            {
                directory.Size += size;

                if (directory.Parent != null)
                {
                    IncreaseDirectorySize(directory.Parent, size);
                }
            }

            private List<(string directoryName, int totalSize)> GetDirectorySize(Directory directory, int size, SizeRestriction restriction)
            {
                var result = new List<(string directoryName, int totalSize)>();
                
                if ((restriction == SizeRestriction.Maximum && directory.Size <= size) || 
                    (restriction == SizeRestriction.Minimum && directory.Size >= size))
                {
                    result.Add((directory.Name, directory.Size));
                }

                foreach (var childDirectory in directory.Directories)
                {
                    result.AddRange(GetDirectorySize(childDirectory, size, restriction));
                }

                return result;
            }
        }

        private class Directory
        {
            public string Name { get; set; }
            public int Size { get; set; }
            public Directory? Parent { get; set; }
            public List<File> Files { get; set; }
            public List<Directory> Directories { get; set; }

            public Directory(string name)
            {
                Name = name;
                Size = 0;
                Parent = null;
                Files = new List<File>();
                Directories = new List<Directory>();
            }
        }

        private class File
        {
            public string Name { get; set; }
            public int Size { get; set; }

            public File(string name, int size)
            {
                Name = name;
                Size = size;
            }
        }
    }    
}
