using Proxy.Lib.Drawing.Implementation;

namespace Proxy.Lib.Drawing.Tests
{
    [TestFixture]
    public class ImageTests
    {
        private const int TILE_SIZE = ITile.SIZE;

        [TearDown]
        public void TearDown()
        {
            Tile.ClearInstancesCount();
        }

        [Test]
        public void Constructor_CreatesCorrectTileGrid()
        {
            // 20 на 10 пикселей - 3 на 2 тайла 
            var img = new Image(new Size(20, 10));

            Assert.That(img.Width, Is.EqualTo(20));
            Assert.That(img.Height, Is.EqualTo(10));
            Assert.That(img.TileSize.Width, Is.EqualTo(3));
            Assert.That(img.TileSize.Height, Is.EqualTo(2));
        }

        [Test]
        public void MutationDetachesTile_IsolationAfterWrite()
        {
            var img = new Image(new Size(TILE_SIZE * 2, TILE_SIZE * 2), ' ');

            var (tile00, _) = img.PixelToTile(new Point(0, 0));
            var (tile01, _) = img.PixelToTile(new Point(0, TILE_SIZE));
            var (tile10, _) = img.PixelToTile(new Point(TILE_SIZE, 0));
            
            Assert.That(Tile.InstanceCount, Is.EqualTo(1));

            tile10!.SetPixel(new Point(3, 3), 'X');
            tile00!.SetPixel(new Point(3, 3), 'Y');

            Assert.That(tile10.GetPixel(new Point(3, 3)), Is.EqualTo('X'));
            Assert.That(tile00.GetPixel(new Point(3, 3)), Is.EqualTo('Y'));
            Assert.That(tile01!.GetPixel(new Point(3, 3)), Is.EqualTo(' '));
        }

        [TestCase(0, 0)]
        [TestCase(TILE_SIZE - 1, TILE_SIZE - 1)]
        [TestCase(TILE_SIZE, TILE_SIZE)]
        [TestCase(TILE_SIZE * 2 - 1, TILE_SIZE * 2 - 1)]
        public void SetAndGetPixel_WorksAcrossTileBoundaries(int x, int y)
        {
            var img = new Image(new Size(TILE_SIZE * 3, TILE_SIZE * 3));
            img.SetPixel(new Point(x, y), 'A');
            Assert.That(img.GetPixel(new Point(x, y)), Is.EqualTo('A'));
        }
    }
}