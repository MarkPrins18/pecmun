using PcMan.Controller;
using PcMan.Model;
using PcMan.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PcMan.Model.Scenes
{
    public abstract class Scene
    {
        protected GameController gameController;
        protected ConsoleController consoleController;
        protected ConsoleView consoleView;

        public Scene(GameController gameController, ConsoleController consoleController, ConsoleView consoleView)
        {
            this.gameController = gameController;
            this.consoleController = consoleController;
            this.consoleView = consoleView;
        }

        public abstract void Update(TimeSpan deltaTime);

    }
}