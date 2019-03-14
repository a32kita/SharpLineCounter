using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpLineCounter
{
    /// <summary>
    /// アプリケーションのメイン クラスです。
    /// </summary>
    public static class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            // カレント ディレクトリ以下のソース ファイルをすべて取得
            Console.WriteLine("C# ソースファイルを検索しております ...");
            var files = Directory.EnumerateFiles(Environment.CurrentDirectory, "*.cs", SearchOption.AllDirectories);

            if (args == null || args.Length == 0 || args[0] != "/all")
            {
                var filter = "\\HoloToolkit";
                files = files.Where(p => !p.Contains(filter));
                Console.WriteLine("'{0}' をパス名に含むものを除外します ...", filter);
            }

            var filesCount = files.Count();
            Console.WriteLine(" => {0} 件を検出", filesCount);
            Console.WriteLine("行数をカウントしております ...");
            
            var lines = 0;
            if (filesCount == 0)
            {
                Console.WriteLine("カウント対象のファイルがありません。");
            }
            else
            {
                foreach (var path in files)
                {
                    using (var sr = new StreamReader(File.OpenRead(path)))
                        lines += sr.ReadToEnd().Split('\n').Length;
                    Console.Write(" => {0} 行", lines);
                    Console.CursorLeft = 0;
                }
            }
            
            Console.WriteLine();
            Console.WriteLine("完了しました。");
            Console.Write("何かキーを押して続行してください");
            Console.ReadKey();
        }
    }
}
