// Monty Hall Problem
// Copyright (C) 2021 Dust in the Wind
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.ComponentModel;
using System.Linq;
using DustInTheWind.MontyHallProblem.Utils;

namespace DustInTheWind.MontyHallProblem.GameModel
{
    internal class GameSet
    {
        private readonly int totalGameCount;
        private readonly GameStrategy gameStrategy;

        public int WinningGamesCount { get; private set; }

        public GameSet(int totalGameCount, GameStrategy gameStrategy)
        {
            if (!Enum.IsDefined(typeof(GameStrategy), gameStrategy))
                throw new InvalidEnumArgumentException(nameof(gameStrategy), (int)gameStrategy, typeof(GameStrategy));

            this.totalGameCount = totalGameCount;
            this.gameStrategy = gameStrategy;
        }

        public void PlayAllGames()
        {
            WinningGamesCount = Enumerable.Range(0, totalGameCount)
                .Select(x =>
                {
                    MontyHallGame game = PlayGame();
                    return game.DidPlayerWin();
                })
                .Count(x => x);
        }

        private MontyHallGame PlayGame()
        {
            MontyHallGame game = new MontyHallGame();

            ThreadSafeRandom random = new ThreadSafeRandom();
            int doorNumber = random.Next(3) + 1;
            game.PickDoor(doorNumber);

            bool doSwitch = gameStrategy == GameStrategy.AlwaysSwitch;
            game.SwitchDoor(doSwitch);

            return game;
        }
    }
}