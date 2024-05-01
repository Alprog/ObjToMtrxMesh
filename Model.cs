
using System.Text;

namespace ObjToMtrxMesh
{
    public class Model
    {

        public void Process(string srcPath, string dstPath)
        {
            var lines = File.ReadAllLines(srcPath);
            var vertices = new List<Vector3>();
            var triangles = new List<Triangle>();
            foreach (var line in lines)
            {
                var arr = line.Split(' ');
                if (arr[0] == "v")
                {
                    var x = float.Parse(arr[1].Replace('.', ','));
                    var y = float.Parse(arr[2].Replace('.', ','));
                    var z = float.Parse(arr[3].Replace('.', ','));
                    vertices.Add(new Vector3(x, y, z));
                }

                if (arr[0] == "f")
                {
                    var list = new List<int>();
                    for (int i = 1; i < arr.Length; i++)
                    {
                        list.Add(int.Parse(arr[i].Split('/')[0]) - 1);
                    }
                    triangles.Add(new Triangle(list));
                }
            }

            var text = ToText(vertices, triangles);
            File.WriteAllText(dstPath, text);
        }

        public string ToText(List<Vector3> vertices, List<Triangle> triangles)
        {
            var builder = new StringBuilder();
            var offset = 0;

            var AddLine = (string s) => {
                for (var i = 0; i < offset; i++)
                {
                    builder.Append(new String(' ', 4));
                }
                builder.AppendLine(s);
            };

            AddLine("this.vertices = [");
            offset++;
            foreach (var v in vertices)
            {
                AddLine(v.ToString() + ",");
            }
            offset--;
            AddLine("];");

            AddLine("this.faces = [");
            offset++;
            foreach (var t in triangles)
            {
                AddLine(t.ToString() + ",");
            }
            offset--;
            AddLine("];");

            return builder.ToString();
        }
    }
}
