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
        public async Task GetCharacters_WhenCalled_ReturnAnything()
        {
            await _marvelApi.GetCharacters();

            Assert.Pass();
            Assert.Fail();
        }
    }
}