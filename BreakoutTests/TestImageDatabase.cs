using NUnit.Framework;
using System.IO;
using DIKUArcade.GUI;
using DIKUArcade.Graphics;
using Breakout.BreakoutUtils;


namespace BreakoutTests
{
    public class TestImageDatabase

    {
        private IImageDatabase _imageDatabaseBreakout = new ImageDatabase(Path.Combine("..", "Breakout", "Assets", "Images"));
        private IImageDatabase _imageDatabaseBreakoutTest = new ImageDatabase(Path.Combine("Assets", "Images"));

 

        [SetUp]
        public void Setup()

        {
            Window.CreateOpenGLContext();
        }

 
        [TestCase("thisfails.png", false)]
        [TestCase("ball2.png", true)]
        [TestCase("yellow-block-damaged.png", true)]


        public void TestHasImage(string imageFileName, bool expectedOutcome)
        {
            if (expectedOutcome)
            {
                Assert.IsTrue(_imageDatabaseBreakout.HasImage(imageFileName));
                Assert.IsTrue(_imageDatabaseBreakoutTest.HasImage(imageFileName));
            }
            else
            {
                Assert.IsFalse(_imageDatabaseBreakout.HasImage(imageFileName));
                Assert.IsFalse(_imageDatabaseBreakoutTest.HasImage(imageFileName));
            }
        }


        [TestCase("yellow-block-damaged.png")]
        public void TestGetImageNotNull(string imageFileName)
        {
            IBaseImage image = _imageDatabaseBreakout.GetImage(imageFileName);
            Assert.NotNull(image);
        }

 
        [TestCase("doesnotexist.png")]
        public void TestGetImageNotExisting(string imageFileName)
        {
            try
            {
                IBaseImage image = _imageDatabaseBreakout.GetImage(imageFileName);  
                Assert.Fail();
            }
            
            catch { }
        }
    }    
}
