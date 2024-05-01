
namespace ObjToMtrxMesh
{
    public static class Program
    {
        public static void ProcessFolder(string path)
        {
            var objFiles = new DirectoryInfo(path).GetFiles("*.obj", SearchOption.TopDirectoryOnly);
            foreach ( var objFile in objFiles )
            {
                var model = new Model();
                model.LoadObj(objFile.FullName);

                var directory = objFile.Directory.FullName;
                var fileName = Path.GetFileNameWithoutExtension(objFile.FullName);     
                var dstPath = Path.Combine(directory, fileName + ".js");
                model.SaveMtrxMesh(dstPath);
            }
        }

        public static void Main()
        {
            ProcessFolder("C:\\alprog.github.io\\mtrx\\models");
        }
    }
}
