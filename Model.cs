
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace ObjToMtrxMesh
{
    public class Model
    {
        private List<Vector3> Vertices;
        private List<Face> Faces;
        private Dictionary<string, Material> Materials;
        private Material CurrentMaterial;

        public Model()
        {
            Vertices = new List<Vector3>();
            Faces = new List<Face>();
            Materials = new Dictionary<string, Material>();
        }

        public void LoadObj(string objPath)
        {
            var lines = File.ReadAllLines(objPath);
            foreach (var line in lines)
            {
                var arr = line.Trim().Split(' ');
                if (arr[0] == "v")
                {
                    var x = float.Parse(arr[1].Replace('.', ','));
                    var y = float.Parse(arr[2].Replace('.', ','));
                    var z = float.Parse(arr[3].Replace('.', ','));
                    Vertices.Add(new Vector3(x, y, z));
                }
                
                if (arr[0] == "f")
                {
                    var list = new List<int>();
                    for (int i = 1; i < arr.Length; i++)
                    {
                        list.Add(int.Parse(arr[i].Split('/')[0]) - 1);
                    }
                    Faces.Add(new Face(list, CurrentMaterial));
                }

                if (arr[0] == "mtllib")
                {
                    var directoryPath = new FileInfo(objPath).Directory.FullName;
                    var mtlPath = Path.Combine(directoryPath, arr[1]);
                    LoadMaterialLibrary(mtlPath);
                }

                if (arr[0] == "usemtl")
                {
                    CurrentMaterial = Materials[arr[1]];
                }
            }
        }

        public void LoadMaterialLibrary(string mtlPath)
        {
            Material current = null;   
            foreach (var line in File.ReadAllLines(mtlPath))
            {
                var arr = line.Trim().Split(' ');
                if (arr[0] == "newmtl")
                {
                    var name = arr[1];
                    current = new Material(name, Materials.Count);
                    Materials.Add(name, current);
                }
                else if (current != null)
                {
                    current.ProcessLine(arr);
                }
            }
        }

        public void SaveMtrxMesh(string dstPath)
        {
            var className = Path.GetFileNameWithoutExtension(dstPath);
            className = Char.ToUpper(className[0]) + className.Substring(1);

            var builder = new StringBuilder();
            var offset = 0;

            var AddLine = (string s) => {
                for (var i = 0; i < offset; i++)
                {
                    builder.Append(new String(' ', 4));
                }
                builder.AppendLine(s);
            };

            AddLine("class " + className + " extends Mesh");
            AddLine("{");
            offset++;
            AddLine("constructor()");
            AddLine("{");
            offset++;
            AddLine("super();");

            AddLine("this.vertices = [");
            offset++;
            foreach (var vertex in Vertices)
            {
                AddLine(vertex.ToString() + ",");
            }
            offset--;
            AddLine("];");

            AddLine("this.materials = [");
            offset++;
            foreach (var material in Materials.Values)
            {
                AddLine(material.ToString() + ",");
            }
            offset--;
            AddLine("];");

            AddLine("this.faces = [");
            offset++;
            foreach (var face in Faces)
            {
                AddLine(face.ToString() + ",");
            }
            offset--;
            AddLine("];");

            offset--;
            AddLine("}");
            offset--;
            AddLine("};");

            File.WriteAllText(dstPath, builder.ToString());
        }
    }
}
