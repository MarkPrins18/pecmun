using PcMan.Controller;
using PcMan.View;

namespace PcMan.Model.Scenes
{
    internal class NewHighScoreScene : Scene
    {
        private object score;
        private object level;

        public NewHighScoreScene(GameController gameController, ConsoleController consoleController, ConsoleView consoleView) : base(gameController, consoleController, consoleView)
        {
            this.gameController = gameController;
            this.consoleController = consoleController;
            this.consoleView = consoleView;
        }

        public override void Update(TimeSpan deltaTime)
        {
            throw new NotImplementedException();
        }
    }
}