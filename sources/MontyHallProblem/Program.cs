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
using DustInTheWind.ConsoleTools.Spinners;

namespace MontyHallProblem
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            const int totalGameCount = 10_000_000;

            PlayGames(totalGameCount, GameStrategy.AlwaysSwitch);
            Console.WriteLine();
            PlayGames(totalGameCount, GameStrategy.AlwaysKeep);
        }

        private static void PlayGames(int totalGameCount, GameStrategy gameStrategy)
        {
            string strategyName = GetStrategyName(gameStrategy);
            Console.WriteLine($"Playing {totalGameCount:N0} games applying strategy: {strategyName}");

            GameSet gameSet = new GameSet(totalGameCount, gameStrategy);
            Spinner.Run(() =>
            {
                gameSet.PlayAllGames();
            });

            Console.WriteLine($"Winnings count: {gameSet.WinningGamesCount:N0} from {totalGameCount:N0}.");
        }

        private static string GetStrategyName(GameStrategy gameStrategy)
        {
            return gameStrategy == GameStrategy.AlwaysSwitch
                ? "'always switch'"
                : "'always keep'";
        }
    }
}