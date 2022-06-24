using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace profi.Helpers
{
    public static class Helper
    {
        public static string GetPath(string root, params string[] folders)
        {
            var resultPath = root;
            foreach (var folder in folders)
            {
                resultPath = Path.Combine(resultPath, folder);
            }
            return resultPath;
        }
    }
}
