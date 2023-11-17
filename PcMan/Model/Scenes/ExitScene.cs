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
    internal class ExitScene : Scene
    {
        private TimeSpan timeElapsed;
        
        public ExitScene(GameController gameController, ConsoleController consoleController, ConsoleView consoleView) : base(gameController, consoleController, consoleView)
        {
            this.gameController = gameController;
            this.consoleController = consoleController;
            this.consoleView = consoleView;

            timeElapsed = TimeSpan.Zero;
        }

        public override void Update(TimeSpan deltaTime)
        {
            // Show goodbye message for 2 seconds, using timeElapsed and deltaTime
            timeElapsed += deltaTime;
            
            consoleView.Show("Goodbye...", 25, 1, ConsoleColor.Red);

            // Print "Press any key to exit" in dark grey
            consoleView.Show("  Press any key to exit  ", 49, 1, ConsoleColor.DarkGray);

            // If the user presses any key, or 2 seconds have passed, exit the game
            if (consoleController.WasKeyPressed() || timeElapsed.TotalSeconds > 2)
            {
                gameController.SetState(GameState.Closed);
            }
        }
    }
}
