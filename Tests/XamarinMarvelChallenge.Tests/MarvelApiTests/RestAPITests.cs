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
            var characters = await _marvelApi.GetCharactersAsync();

            Assert.AreNotEqual(null, characters, "Characters must not be null", null);
            Assert.Pass();
        }

        [Test]
        public async Task GetComicByCharacter_WhenCalled_ReturnNotNull()
        {
            string resourceURI = "http://gateway.marvel.com/v1/public/comics/21366";
            var comic = await _marvelApi.GetComicByCharacterAsync(resourceURI);

            Assert.AreNotEqual(null, comic, "Comic must not be null", null);
            Assert.Pass();
        }
    }
}