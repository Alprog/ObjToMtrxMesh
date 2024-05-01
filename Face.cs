
namespace ObjToMtrxMesh
{
    public struct Face
    {
        List<int> Indices;
        Material Material;

        public Face(List<int> Indices, Material material)
        {
            this.Indices = Indices;
            this.Material = material;
        }

        public new string ToString()
        {
            var colorIndex = Material != null ? Material.Index : 0;
            return string.Format("[{0}, {1}]", colorIndex, String.Join(", ", Indices));
        }
    }
}
