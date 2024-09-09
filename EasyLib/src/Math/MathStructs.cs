namespace EasyLib
{
    public struct Size
    {
        float Width;
        float Height;

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
