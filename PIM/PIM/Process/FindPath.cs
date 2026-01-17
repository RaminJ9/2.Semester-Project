using System;
using System.IO;

namespace PIM.Data
{
    public class FindPath {
        //Used like this:
        //string filePath = new FindPath("filename").GetPath();

        string filename;
        public FindPath(string filename) {
            this.filename = "\\" + filename;
        }
        public string GetPath() => FindLocalPath() + filename;
        public string FindLocalPath() {
            string localDir = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            return localDir;
        }
    }
}
