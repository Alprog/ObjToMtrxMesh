
namespace ObjToMtrxMesh
{
    public class Material
    {
        public string Name;
        public int Index;
        public Color Diffuse;

        public Material(string name, int index)
        {
            this.Name = name;
            this.Index = index;
        }

        public void ProcessLine(string[] arr)
        {
            if (arr[0] == "Kd")
            {
                var r = float.Parse(arr[1].Replace('.', ','));
                var g = float.Parse(arr[2].Replace('.', ','));
                var b = float.Parse(arr[3].Replace('.', ','));
                Diffuse = new Color(r, g, b);
            }
        }

        public new string ToString()
        {
            return string.Format("new Material({0})", Diffuse.ToString());
        }
    }
}
