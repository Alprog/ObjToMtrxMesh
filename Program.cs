
namespace ObjToMtrxMesh
{
    public static class Program
    {
        public static void Main()
        {
            var srcPath = "C:/Alprog/mtrx/char.obj";
            var dstPath = "C:/Alprog/mtrx/char.js";
            var model = new Model();
            model.Process(srcPath, dstPath);
        }
    }
}
