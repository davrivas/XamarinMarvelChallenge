using XamarinMarvelChallenge.Globals;

namespace XamarinMarvelChallenge.Model
{
    public class Comic
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool HasDescription => !string.IsNullOrWhiteSpace(Description);
        public bool DoesNotHaveDescription => string.IsNullOrWhiteSpace(Description);
        public string Thumbnail { get; set; }
        public bool IsFavorite => GlobalVariables.FavoriteComics.Contains(this);
        public string FavoriteIcon => IsFavorite ? "btn_favourites_primary.png" : "btn_favourites_default.png";
    }
}
