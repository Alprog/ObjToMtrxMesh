
namespace ObjToMtrxMesh
{
    public struct Triangle
    {
        List<int> Indices;

        public Triangle(List<int> Indices)
        {
            this.Indices = Indices;
        }

        public new string ToString()
        {
            return string.Format("[{0}]", String.Join(", ", Indices));
        }
    }
}
