using System.IO;

namespace Company.DEMO.PL.Helpers
{
    public class DocumentSettings
    {//1) Upload
        public static string Upload(IFormFile file,string FolderName)
        {
            /*string folderpath = "D:\\Web .net\\Company.Demo\\Company.DEMO.PL\\wwwroot\\File\\Images\\";*/

            //var folderpath=Directory.GetCurrentDirectory()+"\\wwwroot\\File\\"+FolderName;

            var folderpath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot","File" , FolderName);
            var FileName = $"{Guid.NewGuid()}{file.FileName}";
            var filepath = Path.Combine(folderpath, FileName);
           using   var fileStream = new FileStream(filepath, FileMode.Create);
            file.CopyTo(fileStream);

            return FileName;
        }
        //2) Delete
        public static void Delete(string FolderName, string FileName)
        {
            var folderpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "File", FolderName);
            var filepath = Path.Combine(folderpath, FileName);

            if (File.Exists(filepath))
            {
                File.Delete(filepath);
            }
        }

    }
}
