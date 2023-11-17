using PcMan.Controller;
using PcMan.Model.Characters;
using PcMan.Model.Interfaces;
using PcMan.View;
using static PcMan.Model.GameController;

namespace PcMan.Model.Scenes
{
    internal class LevelScene : Scene
    {
        private int levelIndex;

        // Create a 2d array of Cells
        public Cell[,] Cells;

        private Random random;
        private List<Wall> walls;
        private List<ICollectable> collectables;
        private List<IUpdatable> updatables;
        private List<IUpdatable> toRemove;
        private List<IUpdatable> toAdd;
        private Player player;

        private LevelData LevelData;

        private int lives;
        private int score;

        public LevelScene(GameController gameController, ConsoleController consoleController, ConsoleView consoleView, int levelId) : base(gameController, consoleController, consoleView)
        {
            this.gameController = gameController;
            this.consoleController = consoleController;
            this.consoleView = consoleView;
            this.levelIndex = levelId;

            // Initialize fields from the Game class
            LevelData = new LevelData();
            random = new Random();
            walls = new List<Wall>();
            collectables = new List<ICollectable>();
            updatables = new List<IUpdatable>();
            toRemove = new List<IUpdatable>();
            toAdd = new List<IUpdatable>();
            lives = 3;
            score = 0;
        }

        public void SetupLevel()
        {
            // Create cells for all the positions
            Cells = new Cell[GameController.CurrentGame.Height, GameController.CurrentGame.Width];

            // Create a new cell for each position
            for (int top = 0; top < GameController.CurrentGame.Height; top++)
            {
                for (int left = 0; left < GameController.CurrentGame.Width; left++)
                {
                    Cells[top, left] = new Cell(top, left, consoleView);
                }
            }
            
            ClearLevel();
            player = new Player(5, 5, consoleController);
            updatables.Add(player);

            // Load level data
            LevelData.LoadLevel(this, levelIndex);

            UpdateScore();
            UpdateLives();
        }

        public override void Update(TimeSpan timeElapsed)
        {
            // Remove all the updatables that are marked for removal
            foreach (IUpdatable updatable in toRemove)
            {
                updatables.Remove(updatable);
            }
            toRemove.Clear();

            // Add all the updatables that are marked for addition
            foreach (IUpdatable updatable in toAdd)
            {
                updatables.Add(updatable);
            }
            toAdd.Clear();

            // Loop trough all updatables and call Update-method
            foreach (IUpdatable updatable in updatables)
            {
                if (updatable == null)
                {
                    continue;
                }
                updatable.Update(timeElapsed);
            }
        }


        public void GameOver()
        {
            gameController.lastLevel = levelIndex;
            gameController.lastScore = score;
            gameController.SetState(GameState.HighScores);
        }

        public void AddUpdatable(IUpdatable updatable)
        {
            // Add the updatable to the list
            toAdd.Add(updatable);
        }

        public void RemoveUpdatable(IUpdatable updatable)
        {
            // Remove the updatable from the list
            toRemove.Add(updatable);
        }

        public Cell GetCell(int top, int left)
        {
            // Check if the position is inside the game
            if (top < 0 || top >= GameController.CurrentGame.Height - 1 || left < 0 || left >= GameController.CurrentGame.Width - 1)
            {
                return null;
            }

            // Return the cell at the position
            return Cells[top, left];
        }

        public int GetLives()
        {
            return lives;
        }

        public void AddLive()
        {
            lives++;
            UpdateLives();
        }

        public int GetScore()
        {
            return score;
        }

        public void AddScore()
        {
            score++;
            UpdateScore();
        }

        public void UpdateLives()
        {
            // Set cursor position left from the play area, 2nd row
            Console.SetCursorPosition(GameController.CurrentGame.Width + 1, 1);

            // Set color to red
            Console.ForegroundColor = ConsoleColor.Red;

            // Write the score
            Console.WriteLine("♥" + lives);
        }

        public void UpdateScore()
        {
            // Set cursor position left from the play area, top row
            Console.SetCursorPosition(GameController.CurrentGame.Width + 1, 0);

            // Set color to yellow
            Console.ForegroundColor = ConsoleColor.Yellow;

            // Write the score
            Console.WriteLine("$" + score);
        }

        internal void AddEnemy(Type enemy)
        {
            // Create a new instance of the enemy
            IUpdatable updatable = (IUpdatable)Activator.CreateInstance(enemy);

            // Add the enemy to the list of updatables
            AddUpdatable(updatable);
        }

        public void AddEnemy(IUpdatable enemy)
        {
            // Add the enemy to the list of updatables
            AddUpdatable(enemy);
        }

        public void ClearLevel()
        {
            // Go trough all the updatables, if it is a Character, kill it
            foreach (IUpdatable updatable in updatables)
            {
                if (updatable is Character)
                {
                    Character character = (Character)updatable;
                    character.Die();
                }
            }

            // Clear updatables
            updatables.Clear();

            // Clear all collectables
            foreach (ICollectable collectable in collectables)
            {
                collectable.Remove();
            }

            // Clear collectables
            collectables.Clear();

            // Go trough the list of walls
            foreach (Wall wall in walls)
            {
                // Get the cell at the position of the wall, make it walkable
                GetCell(wall.Top, wall.Left).MakeEnterable();
            }

            // Clear the list of walls
            walls.Clear();

        }

        public Player GetPlayer()
        {
            return player;
        }

        internal void AddWall(Wall wall)
        {
            // Make the cell a wall
            Cells[wall.Top, wall.Left].MakeWall();

            // Add the wall to the list of walls
            walls.Add(wall);
        }

        internal Wall GetWall(int top, int left)
        {
            // Loop trough all the walls
            foreach (Wall wall in walls)
            {
                // Check if the wall is at the position
                if (wall.Top == top && wall.Left == left)
                {
                    return wall;
                }
            }

            // No wall found
            return null;
        }

        public void AddCollectable(ICollectable collectable)
        {
            // Add the collectable to the list of collectables
            collectables.Add(collectable);
        }
    }
}