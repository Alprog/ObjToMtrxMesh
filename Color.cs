using System.Globalization;

namespace ObjToMtrxMesh
{
    public struct Color
    {
        public float r;
        public float g;
        public float b;

        public Color(float r, float g, float b)
        {
            this.r = r;
            this.g = g;
            this.b = b;
        }

        public new string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "new Color({0}, {1}, {2})", r, g, b);
        }
    };
}
