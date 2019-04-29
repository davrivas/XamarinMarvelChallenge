namespace XamarinMarvelChallenge.Model
{
    public class Comic
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool HasDescription => !string.IsNullOrWhiteSpace(Description);
        public bool DoesNotHaveDescription => string.IsNullOrWhiteSpace(Description);
        public string Thumbnail { get; set; }
    }
}
