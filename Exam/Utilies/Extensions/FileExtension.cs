namespace Exam.Utilies.Extensions
{
    public static class FileExtension
    {
        public static bool CheckType(this IFormFile file, string type)
            =>file.ContentType.Contains(type);

        public static bool CheckSize(this IFormFile file, double kb)
            => kb * 1024 > file.Length;


        public static string SaveFile(this IFormFile file,string path)
        {
            string filename = ChageName(file.FileName);

            using(FileStream fs = new FileStream(Path.Combine(path, filename), FileMode.Create))
            {
                file.CopyTo(fs);
            }
            return filename;
        }


        public static void SaveFileWithName(this IFormFile file, string path,string filename)
        {

            using (FileStream fs = new FileStream(Path.Combine(path, filename), FileMode.Create))
            {
                file.CopyTo(fs);
            }
        }

        static string ChageName(string name)
        {
            name = Guid.NewGuid().ToString() + name.Substring(name.LastIndexOf("."));
            return name;
        }


        public static void DeleteFile(this string filename,string root,string folder)
        {
            folder = Path.Combine(root,folder,filename);
            if(File.Exists(folder))
            {
                File.Delete(folder);
            }
        }


        public static string CheckValidate(this IFormFile file,string type,double kb)
        {
            string result = "";

            if (!file.CheckType(type)) result += $"File type is not {type}";
            if (!file.CheckSize(kb)) result += $"File size is not bigger than {kb} kb";
            return result;
        }
    }
}
