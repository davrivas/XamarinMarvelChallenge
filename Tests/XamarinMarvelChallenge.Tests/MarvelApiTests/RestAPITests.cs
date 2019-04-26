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
        public async Task GetCharactersAsync_WhenCalled_ReturnNotNull()
        {
            var characters = await _marvelApi.GetCharactersAsync();

            Assert.AreNotEqual(null, characters, "Characters must not be null", null);
            Assert.Pass();
        }

        [Test]
        public async Task GetCharactersAsync_WhenCalledWithParameters_ReturnNotNull()
        {
            var characters = await _marvelApi.GetCharactersAsync(nameStartsWith: "spider", offset: 1, orderBy: "-name%2C-modified");

            Assert.AreNotEqual(null, characters, "Characters must not be null", null);
            Assert.Pass();
        }

        [Test]
        public async Task GetComicByCharacterAsync_WhenCalled_ReturnNotNull()
        {
            string resourceURI = "http://gateway.marvel.com/v1/public/comics/21366";
            var comic = await _marvelApi.GetComicByCharacterAsync(resourceURI);

            Assert.AreNotEqual(null, comic, "Comic must not be null", null);
            Assert.Pass();
        }
    }
}