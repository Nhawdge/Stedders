namespace Stedders.Utilities
{
    internal class AsepriteData
    {
        public Frame[] frames { get; set; }
        public Meta meta { get; set; }
    }

    public class Meta
    {
        public string app { get; set; }
        public string version { get; set; }
        public string image { get; set; }
        public string format { get; set; }
        public Size size { get; set; }
        public string scale { get; set; }
        public FrameTag[] frameTags { get; set; }
        public Layer[] layers { get; set; }
        public object[] slices { get; set; }
    }

    public class Size
    {
        public int w { get; set; }
        public int h { get; set; }
    }

    public class Layer
    {
        public string name { get; set; }
        public int opacity { get; set; }
        public string blendMode { get; set; }
    }

    public class Frame
    {
        public string filename { get; set; }
        public Frame1 frame { get; set; }
        public bool rotated { get; set; }
        public bool trimmed { get; set; }
        public Spritesourcesize spriteSourceSize { get; set; }
        public Sourcesize sourceSize { get; set; }
        public int duration { get; set; }
    }

    public class Frame1
    {
        public int x { get; set; }
        public int y { get; set; }
        public int w { get; set; }
        public int h { get; set; }
    }

    public class Spritesourcesize
    {
        public int x { get; set; }
        public int y { get; set; }
        public int w { get; set; }
        public int h { get; set; }
    }

    public class Sourcesize
    {
        public int w { get; set; }
        public int h { get; set; }
    }

    public class FrameTag
    {
        public string name { get; set; }
        public int from { get; set; }
        public int to { get; set; }
        public string direction { get; set; }
    }


}