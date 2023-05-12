/*
using NUnit.Framework;
using System.IO;
using Breakout;
using BreakoutLevels;
using System;
using DIKUArcade.GUI;

namespace BreakoutTests;

public class TestLevelLoader {
    private LevelLoader loader;
    private string pathLevels = Path.Combine("..", "..", "..", "Assets", "Levels");
    private string pathImages = Path.Combine("..", "..", "..", "Assets", "Images");

    [SetUp]
    public void Setup() {
        Window.CreateOpenGLContext();
        
        loader = new LevelLoader();
    }

    [TestCase("level1")]
    [TestCase("level2")]
    [TestCase("level3")]
    [TestCase("level4")]
    public void TestReadFile(string fileName) {
        string[] fileLines = loader.ReadLevel(Path.Combine(pathLevels, fileName + ".txt"));

        string[] actualLines = File.ReadAllLines(Path.Combine(pathLevels, fileName + ".txt"));
        

        int i = 0;
        foreach (var line in fileLines) {
            Assert.AreEqual(line, actualLines[i]);

            i++;
        }
    }

    [TestCase("invalidLevelName")]
    public void TestDefaultLevel(string fileName) {
        string[] invalidFileLines = loader.ReadLevel(Path.Combine(pathLevels, fileName + ".txt"));

        string[] actualDefaultLines = File.ReadAllLines(Path.Combine(pathLevels, "default" + ".txt"));

        int i = 0;
        foreach (var line in invalidFileLines) {
            Assert.AreEqual(line, actualDefaultLines[i]);

            i++;
        }
    }
}
*/