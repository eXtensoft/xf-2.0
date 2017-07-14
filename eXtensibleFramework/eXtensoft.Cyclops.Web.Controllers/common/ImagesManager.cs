// <copyright company="eXtensoft, LLC" file="ImagesManager.cs">
// Copyright © 2016 All Right Reserved
// </copyright>

namespace Cyclops
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Web;

    public static class ImagesManager
    {
        private static List<string> imageExtensions = new List<string>()
        {
            ".png",".jpg",".bmp",".tif"
        };
        public static IEnumerable<FileInfoViewModel> GetAll(string folderPath, string relativeFolderpath)
        {
            List<FileInfoViewModel> list = new List<FileInfoViewModel>();
            if (Directory.Exists(folderPath))
            {
                FileInfo[] infos = new DirectoryInfo(folderPath).GetFiles();
                foreach (FileInfo info in infos)
                {
                    if (IsImage(info))
                    {
                        list.Add(new FileInfoViewModel(info) { Folderpath = folderPath, RelativeFolderpath = relativeFolderpath });
                    }
                }
            }
            return list;
        }

        private static bool IsImage(FileInfo info)
        {
            bool b = false;
            if (imageExtensions.Contains(info.Extension.ToLower()))
            {
                b = true;
            }
            return b;
        }
    }

}
