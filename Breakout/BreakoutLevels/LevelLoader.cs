using System.IO;
using System;
using BreakoutEntities;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using Breakout.BreakoutUtils;

namespace BreakoutLevels {
    public class LevelLoader {
        private int mapStart;
        private int mapEnd;
        private int metaStart;
        private int metaEnd;
        private int legendStart;
        private int legendEnd;
        private string currentLine;
        private string[] levelArr;
        public string[] blockTypes {get; private set;}
        public string[] blockNames {get; private set;}
        public EntityContainer<Block> blocks {get; private set;}
        private string pathLevels;
        private string pathImages;
        private const string DEFAULT_LEVEL_NAME = "default";
        string[] specialBlockType;
        string[] specialBlock;
        private int counter;
        private string powerUpBlock;
        private IImageDatabase imageDatabase;

        public LevelLoader() {
            levelArr = new string[0];
            this.pathLevels = Path.Combine("Breakout", "Assets", "Levels");
            this.pathImages = Path.Combine("Assets", "Images");
            currentLine = "";
            imageDatabase = new ImageDatabase(Path.Combine("Assets", "Images"));

            blocks = new EntityContainer<Block>();

            blockTypes = new string[0];
            blockNames = new string[0];
            counter = 0;
            powerUpBlock = "";
        }


        ///<summary>Reads a given file and separates the lines into a string array</summary>
        ///<param name = "fileName">A string representing the name of the file to read</param>
        ///<returns>The read file as a string array</returns>
        public string[] ReadLevel(string filePath) {
            // Delete any existing blocks before rendering a new level.
            blocks.Iterate(x => x.DeleteEntity());
            blocks.ClearContainer();

            string[] stringArr = new string[0];
            
            try {
                //stringArr = File.ReadAllLines(Path.Combine(pathLevels,
                //    fileName + ".txt"));
                stringArr = File.ReadAllLines(filePath);
            }
            catch (FileNotFoundException) {
                stringArr = File.ReadAllLines(Path.Combine(filePath, "..",
                    DEFAULT_LEVEL_NAME + ".txt"));
            }

            levelArr = stringArr;

            LoadMetaIndexes();
            
            PlaceBlocks();

            return stringArr;
        }

        ///<summary>
        ///Saves the index of each line that starts and ends a section in a level file
        ///<summary>
        private void LoadMetaIndexes() {
            mapStart = FindLineIndex(levelArr, "Map:");
            mapEnd = FindLineIndex(levelArr, "Map/");
            metaStart = FindLineIndex(levelArr, "Meta:");
            metaEnd = FindLineIndex(levelArr, "Meta/");
            legendStart = FindLineIndex(levelArr, "Legend:");
            legendEnd = FindLineIndex(levelArr, "Legend/");
            
            if (mapStart == -1 || mapEnd == -1
                    || legendStart == -1 || legendEnd == -1) {
                ReadLevel(DEFAULT_LEVEL_NAME);
            }
        }

        ///<summary>
        ///Returns the index of the first instance of a line containing the given substring
        ///</summary>
        ///<param name = "arr">The array to search through</param>
        ///<param name = "searchTerm">The substring to search for</param>
        ///<returns>The found index. -1 if not found</returns>
        private int FindLineIndex(string[] arr, string searchTerm) {
            int index = -1;
            int i = 0;

            while (i < arr.Length) {
                if (arr[i].Contains(searchTerm)) {
                    index = i;
                }
                i++;
            }

            return index;
        }

        ///<summary>Returns the index of the first char found in a given string</summary>
        ///<param name = "str">String to search in</param>
        ///<param name = "searchTerm">Char to search for</param>
        ///<returns>The first index of a given char in a given string. -1 if not found</returns>
        private int FirstInstanceOf(string str, char searchTerm) {
            char[] arr = str.ToCharArray();
            int i = 0;

            while (i < arr.Length - 1) {
                if (arr[i] == searchTerm) {
                    return i;
                }
                i++;
            }
            
            return -1;
        }

        ///<summary>Loads the block types and names and saves them in two arrays</summary>
        private void LoadBlockTypes() {
            int blockAmount = (legendEnd - legendStart) - 1;
            int metaLength = (metaEnd - metaStart) - 1;
            blockTypes = new string[blockAmount];
            blockNames = new string[blockAmount];

            for (int i = 0; i < blockAmount; i++) {
                currentLine = levelArr[(legendStart + 1) + i];
                blockTypes[i] = currentLine[0].ToString();
                blockNames[i] = currentLine[(FirstInstanceOf(currentLine, ')') + 2)..
                    FirstInstanceOf(currentLine, '.')].ToString();
            }


            string[] blockTypesStr = BlockTypes.GetEnumStringArray();
            specialBlockType = new string[metaLength]; // Special block type
            specialBlock = new string[metaLength]; // Block in map

            // Saves the special block type and the block in two arrays
            for (int j = 0; j < metaLength; j++) {
                currentLine = levelArr[(metaStart + 1) + j];
                int k = 0;
                foreach (string type in blockTypesStr) { // Check every meta line against every type
                    if (currentLine.Contains(type)) {
                        specialBlockType[j] = currentLine[0..FirstInstanceOf(currentLine, ':')];
                        specialBlock[j] = currentLine[ // Save data
                            (FirstInstanceOf(currentLine, ':') + 2)].ToString();
                    }
                    k++;
                }
            }
        }

        ///<summary>Places the loaded blocks into the game</summary>
        private void PlaceBlocks() {
            LoadBlockTypes();

            // First loop iterates through the lines of the .txt file.
            // Second loop iterates through the characters in each line.
            // Block Amount Y outside loops to not waste resources.
            float amountOfBlocksY = (mapEnd - 1) - (mapStart + 1);
            for (int i = mapStart + 1; i < mapEnd; i++) {
                // Block Amount X outside 2nd for-loop as it only uses indexer i
                float amountOfBlocksX = levelArr[i].Length;
                if (amountOfBlocksY > 24 || amountOfBlocksX > 12) { // If block amounts are too big
                        ReadLevel(DEFAULT_LEVEL_NAME); // Load default
                        break;
                }

                for (int j = 0; j < levelArr[i].Length; j++) {
                    string type = levelArr[i][j].ToString();   // Saves the block type to search for
                    
                    // Gets the size for each block dependant on the total amount of blocks in level
                    float xLength = 1.0f / amountOfBlocksX;
                    float yHeight = 1.0f / (amountOfBlocksY + 1);

                    try { // Try to read level. On error, load default
                        if (levelArr[i][j] != '-') { // If a space is not "empty" do...
                            string tmpType = Array.Exists(specialBlock, x => x == type) ?
                                tmpType = specialBlockType[Array.FindIndex( // Assigns special type
                                specialBlock, x => x == type)] : tmpType = "Default";


                            Block block;

                            Random rand = new Random();

                            // Assign random power-up enum to block if it matches metadata
                            PowerUp.PowerUpType powerUpType = levelArr[i][j].ToString() ==
                                powerUpBlock ? (PowerUp.PowerUpType)rand.Next(Enum.GetValues(
                                    typeof(PowerUp.PowerUpType)).Length-1) :
                                        PowerUp.PowerUpType.None;


                            switch (tmpType) {
                                case "Hardened":
                                    block = new HardenedBlock(
                                        new StationaryShape(0.0f + (xLength * (float)j),
                                        1.0f - (yHeight * (float)i), xLength, yHeight), // Size & position
                                        //new Image(Path.Combine(pathImages, // Image and name
                                        //blockNames[Array.FindIndex(blockTypes, x => x == type)] + ".png")),
                                        imageDatabase.GetImage(blockNames[Array.FindIndex(blockTypes, x => x == type)] + ".png"),
                                        blockNames[Array.FindIndex(blockTypes, x => x == type)],
                                        (BlockTypes.BlockType)Enum.Parse(typeof(BlockTypes.BlockType),
                                            tmpType), powerUpType);
                                    break;
                                
                                case "Unbreakable":
                                    block = new UnbreakableBlock(
                                        new StationaryShape(0.0f + (xLength * (float)j),
                                        1.0f - (yHeight * (float)i), xLength, yHeight), // Size & position
                                        //new Image(Path.Combine(pathImages, // Image and name
                                        //blockNames[Array.FindIndex(blockTypes, x => x == type)] + ".png")),
                                        imageDatabase.GetImage(blockNames[Array.FindIndex(blockTypes, x => x == type)] + ".png"),
                                        blockNames[Array.FindIndex(blockTypes, x => x == type)],
                                        (BlockTypes.BlockType)Enum.Parse(typeof(BlockTypes.BlockType),
                                            tmpType), powerUpType);
                                    break;
                                
                                case "PowerUp":
                                    block = new PowerUpBlock(
                                        new StationaryShape(0.0f + (xLength * (float)j),
                                        1.0f - (yHeight * (float)i), xLength, yHeight), // Size & position
                                        imageDatabase.GetImage(blockNames[Array.FindIndex(blockTypes, x => x == type)] + ".png"),
                                        blockNames[Array.FindIndex(blockTypes, x => x == type)],
                                        (BlockTypes.BlockType)Enum.Parse(typeof(BlockTypes.BlockType),
                                            tmpType), powerUpType);
                                    break;

                                default:
                                    block = new DefaultBlock(
                                        new StationaryShape(0.0f + (xLength * (float)j),
                                        1.0f - (yHeight * (float)i), xLength, yHeight), // Size & position
                                        imageDatabase.GetImage(blockNames[Array.FindIndex(blockTypes, x => x == type)] + ".png"),
                                        blockNames[Array.FindIndex(blockTypes, x => x == type)],
                                        (BlockTypes.BlockType)Enum.Parse(typeof(BlockTypes.BlockType),
                                            tmpType), powerUpType);
                                    break;
                            }

                            blocks.AddEntity(block);

                        }
                    }
                    catch (IndexOutOfRangeException) {
                        ReadLevel(DEFAULT_LEVEL_NAME);
                    }
                }
            }
        }
    }
}