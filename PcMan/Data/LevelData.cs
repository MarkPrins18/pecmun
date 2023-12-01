using System;
using System.Collections.Generic;
using System.Xml.Linq;
using PcMan.Model.Characters;
using PcMan.Model.Interfaces;
using PcMan.Model.Scenes;
using System.Net.Http;
using PcMan.Model.Collectables;

namespace PcMan.Model
{
    /// <summary>
    /// LevelData class loads and manages level data from an external source.
    /// </summary>
    public class LevelData
    {
        private string levelsUrl;
        private XDocument levelsDocument;
        private Dictionary<int, int> levelIndexToIdMapping;

        /// <summary>
        /// Initializes a new instance of the LevelData class and loads the levels.
        /// </summary>
        public LevelData()

        {
            levelsUrl = "http://localhost/levelgenerator/LevelGenerator/api.php";
            levelIndexToIdMapping = new Dictionary<int, int>();

            LoadLevels();
            GetLevelList();
        }

        /// <summary>
        /// Loads level data from the external source.
        /// </summary>
        private void LoadLevels()
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync(levelsUrl).Result;
                if (response.IsSuccessStatusCode)
                {
                    string xmlContent = response.Content.ReadAsStringAsync().Result;
                    levelsDocument = XDocument.Parse(xmlContent);
                }
                else
                {
                    throw new HttpRequestException($"Failed to load levels from '{levelsUrl}'. Status code: {response.StatusCode}");
                }
            }
        }

        /// <summary>
        /// Gets the number of levels available.
        /// </summary>
        /// <returns>An integer representing the number of levels.</returns>
        public int GetNumberOfLevels()
        {
            return levelsDocument.Root.Elements("level").Count();
        }

        /// <summary>
        /// Gets a list of available levels with their numbers, names, and descriptions.
        /// </summary>
        /// <returns>A List of tuples containing the level number, name, and description.</returns>
        public List<(int levelNumber, string name, string description)> GetLevelList()
        {
            var levels = new List<(int, string, string)>();

            int index = 1;
            foreach (var levelElement in levelsDocument.Root.Elements("level"))
            {
                int levelId = int.Parse(levelElement.Attribute("id").Value);
                levelIndexToIdMapping[index] = levelId;

                string name = levelElement.Element("name").Value;
                string description = levelElement.Element("description").Value;

                levels.Add((index, name, description));
                index++;
            }

            return levels;
        }

        /// <summary>
        /// Loads the specified level into the given LevelScene.
        /// </summary>
        /// <param name="levelScene">A LevelScene instance where the level should be loaded.</param>
        /// <param name="levelIndex">An integer representing the index of the level to be loaded.</param>
        internal void LoadLevel(LevelScene levelScene, int levelIndex)
        {

            int levelId = GetLevelIdFromIndex(levelIndex);
            XDocument levelDocument;

            // Fetch the level XML from the API
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync($"{levelsUrl}?levelId={levelId}").Result;
                if (response.IsSuccessStatusCode)
                {
                    string xmlContent = response.Content.ReadAsStringAsync().Result;
                    levelDocument = XDocument.Parse(xmlContent);
                }
                else
                {
                    throw new HttpRequestException($"Failed to load level with id '{levelId}' from '{levelsUrl}'. Status code: {response.StatusCode}");
                }
            }

            var levelElement = levelDocument.Root;

            if (levelElement == null)
            {
                throw new InvalidOperationException("Level " + GetLevelIdFromIndex(levelIndex) + " not found.");
            }

            // Extract gridWidth and gridHeight
            int gridWidth = int.Parse(levelElement.Attribute("gridWidth").Value);
            int gridHeight = int.Parse(levelElement.Attribute("gridHeight").Value);

            // Set window size
            GameController.CurrentGame.SetWindow(gridWidth, gridHeight);

            foreach (var wallElement in levelElement.Element("walls").Elements("wall"))
            {
                int top = int.Parse(wallElement.Attribute("top").Value);
                int left = int.Parse(wallElement.Attribute("left").Value);
                var wall = new Wall(top, left);
                levelScene.AddWall(wall);
            }

            foreach (var enemyElement in levelElement.Element("enemies").Elements("enemy"))
            {
                string type = enemyElement.Attribute("type").Value;
                int top = int.Parse(enemyElement.Attribute("top").Value);
                int left = int.Parse(enemyElement.Attribute("left").Value);
                Type enemyType = Type.GetType("PcMan.Model.Characters." + type);
                var enemy = (IUpdatable)Activator.CreateInstance(enemyType, top, left);
                levelScene.AddEnemy(enemy);
            }

            foreach (var collectableElement in levelElement.Element("collectables").Elements("collectable"))
            {
                string type = collectableElement.Attribute("type").Value;

                int top = int.Parse(collectableElement.Attribute("top").Value);
                int left = int.Parse(collectableElement.Attribute("left").Value);

                if (type == "Key")
                {
                    int wallTop = int.Parse(collectableElement.Attribute("wallTop").Value);
                    int wallLeft = int.Parse(collectableElement.Attribute("wallLeft").Value);

                    var wall = ((LevelScene)GameController.CurrentScene).GetWall(wallTop, wallLeft);
                    var key = new Key(top, left, wall);

                    levelScene.AddCollectable(key);
                }
                else
                {
                    Type collectableType = Type.GetType("PcMan.Model.Collectables." + type);
                    var collectable = (ICollectable)Activator.CreateInstance(collectableType, top, left);
                    levelScene.AddCollectable(collectable);
                }
            }
        }
        
        /// <summary>
        /// Gets the level ID from the given level index.
        /// </summary>
        /// <param name="index">An integer representing the index of the level.</param>
        /// <returns>An integer representing the level ID.</returns>
        public int GetLevelIdFromIndex(int index)
        {
            return levelIndexToIdMapping[index];
        }
    }
}
