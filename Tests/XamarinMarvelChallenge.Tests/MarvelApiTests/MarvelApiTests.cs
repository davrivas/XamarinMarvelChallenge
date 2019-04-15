using NUnit.Framework;
using System;
using System.Threading.Tasks;
using XamarinMarvelChallenge.MarvelApi;

namespace Tests.MarvelApiTests
{
    public class Tests
    {
        private MarvelApi _marvelApi;

        [SetUp]
        public void Setup()
        {
            _marvelApi = new MarvelApi();
        }

        [Test]
        public async Task GetCharacters_WhenCalled_ReturnAnything()
        {
            dynamic characters = await _marvelApi.GetCharacters();
            Console.WriteLine(characters);

            Assert.Pass();
            Assert.Fail();
        }
    }
}