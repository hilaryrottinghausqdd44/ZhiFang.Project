using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.IBLL.Common
{
    public interface IFilesHelper
    {
        bool CheckAndCreatDir(string Path);
        bool CreatDirFile(string FilePath, string FileName, Byte[] Filesteam);
        bool CreatDirFile(string FilePath, string FileName, string context);
        bool CheckDirFile(string FilePath, string FileName);
        bool CheckDirectory(string FilePath);
        bool DelDir(string Path);
        bool DelDirFile(string FilePath, string FileName);
        void WriteContext(string Context, string path);
        string ReadContext(string path);

    }
}
