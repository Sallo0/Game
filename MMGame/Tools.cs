using System;
using System.IO;
using System.Linq;
using System.Security.Policy;

namespace MMGame
{
    public static class Tools
    {
        public static string GetFullPath(string fileName)
        {
            return String.Join('\\',Path.GetFullPath(fileName)
                .Split('\\')
                .TakeWhile(e => e!="bin"))+"\\image\\"+fileName;
        }
    }
}