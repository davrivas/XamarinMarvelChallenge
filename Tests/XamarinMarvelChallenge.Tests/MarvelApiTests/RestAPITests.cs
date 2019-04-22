using NUnit.Framework;
using System.Threading.Tasks;
using XamarinMarvelChallenge.MarvelApi;

namespace Tests.MarvelApiTests
{
    public class RestApiTests
    {
        private RestApi _marvelApi;

        [SetUp]
        public void Setup()
        {
            _marvelApi = new RestApi();
        }

        [Test]
        public async Task GetCharacters_WhenCalled_ReturnNotNull()
        {
            var characters = await _marvelApi.GetCharacters();

            Assert.AreNotEqual(null, characters);
        }

        [Test]
        public async Task GetComic_WhenCalled_ReturnNotNull()
        {
            string resourceURI = "http://gateway.marvel.com/v1/public/comics/21366";
            var comic = await _marvelApi.GetComic(resourceURI);

            Assert.AreNotEqual(null, comic);
        }
    }
}