using Favourites.Data.Entities;

namespace Favourites.Data.Tests
{
    internal class DataSeed
    {
        internal static object[] GetBookmarkData()
        {
            var rpgTag = new Tag("rpg");
            var data = new object[]
            {
                 new Bookmark("donjon", new Uri("https://donjon.bin.sh/"))
                 {
                    Description = "RPG Randomizer",
                    Tags = new[] { rpgTag },
                 },
                 new Bookmark("roll20APIScripts", new Uri("https://github.com/RobinKuiper/Roll20APIScripts"))
                 {
                    Tags = new[] { rpgTag, new Tag("roll20"), new Tag("github") }
                 }
            };
            return data;
        }
    }
}
