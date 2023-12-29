using System.Text;

namespace FileSystem {
    public static class StringSystem {
        // Static readonly string builder for save memeory and prevent allocated garbages.
        // If you call Clear() method, earlier contents are going to all removed!
        // We recommand to use your own StringBuilder() class in yout project.
        private static readonly StringBuilder sb = new StringBuilder();
        public static void Clear() {
            sb.Clear();
        }

        public static void Append(string str) {
            sb.Append(str);
        }

        public static string Values() {
            return sb.ToString();
        }
    }
}
