namespace XamarinMarvelChallenge.Model
{
    public class Thumbnail
    {
        public string path { get; set; }
        public string extension { get; set; }
        public string PathAndExtension => path + "." + extension;
    }
}