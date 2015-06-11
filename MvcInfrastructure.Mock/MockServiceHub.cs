using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using Bk.Core;

namespace MvcInfrastructure.Mock
{
    public class MockServiceHub
    {
        private static Random r = new Random();
        private static IGame GetGame()
        {
            var game = new Mock<IGame>();
            game.Setup(g => g.Name).Returns("魔兽世界" + r.NextDouble());
            game.Setup(g => g.Description).Returns("好游戏" + r.NextDouble());
            return game.Object;
        }

        private static IGameService gameService;
        public static IGameService GetGameService()
        {
            return gameService;
        }

        public static void Init()
        {
            var mgameService = new Mock<IGameService>();
            mgameService.Setup(s => s.GetGames(It.IsAny<bool>(), It.IsAny<bool>())).Returns(Enumerable.Repeat(GetGame(), 10).ToArray());
            gameService = mgameService.Object;
        }
    }
}
