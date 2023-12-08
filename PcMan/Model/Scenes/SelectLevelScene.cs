using PcMan.Controller;
using PcMan.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PcMan.Model.GameController;

namespace PcMan.Model.Scenes
{
    internal class SelectLevelScene : Scene
    {
        public SelectLevelScene(GameController gameController, ConsoleController consoleController, ConsoleView consoleView) : base(gameController, consoleController, consoleView)
        {
            this.gameController = gameController;
            this.consoleController = consoleController;
            this.consoleView = consoleView;
        }

        public override void Update(TimeSpan deltaTime)
        {
            consoleView.Show("Select a level", 23, 1, ConsoleColor.Red);

            consoleView.Show("1. First level", 26, 1, ConsoleColor.White);

            ConsoleKey lastKey = consoleController.GetLastKey(true);

            if (lastKey == ConsoleKey.D1)
            {
                gameController.SetState(GameState.Playing);
            }
        }
    }
}
