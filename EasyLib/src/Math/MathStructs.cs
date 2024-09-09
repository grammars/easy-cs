namespace EasyLib
{
    public struct Size
    {
        public float Width;
        public float Height;

        public Size(float w, float h)
        {
            this.Width = w; this.Height = h;
        }

        public override string ToString()
        {
            return $"(W={Width}, H={Height})";
        }

    }
}
