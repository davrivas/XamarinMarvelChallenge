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

            Assert.AreNotEqual(null, characters, "Characters must not be null", null);
            Assert.Pass();
        }

        [Test]
        public async Task GetComicsByCharacter_WhenCalled_ReturnNotNull()
        {
            string resourceURI = "http://gateway.marvel.com/v1/public/characters/1011334/comics";
            var comic = await _marvelApi.GetComicsByCharacter(resourceURI);

            Assert.AreNotEqual(null, comic, "Comic must not be null", null);
            Assert.Pass();
        }
    }
}