using System.IO;

namespace PROTO.Utils
{
    internal static class FileDB
    {
        private const string DB_ACCOUNT_PATH = "db_login_accounts.db";

        public static string GetFilePath()
        {
            string currentParentPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            return $"{currentParentPath}\\{DB_ACCOUNT_PATH}";
        }
    }
}
