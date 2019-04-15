using NUnit.Framework;
using System.Threading.Tasks;
using XamarinMarvelChallenge.MarvelApi;

namespace Tests.MarvelApiTests
{
    public class RestAPITests
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
            var characters = await _marvelApi.GetCharacters();

            if (characters != null)
                Assert.Pass();
            else
                Assert.Fail();
        }
    }
}