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
using System.Linq;

namespace MontyHallProblem
{
    internal class MontyHallGame
    {
        private readonly ThreadSafeRandom random = new ThreadSafeRandom();
        private GameState state;

        private readonly int carDoorNumber;
        private int playerDoorNumber;
        private int openedDoorNumber;

        public MontyHallGame()
        {
            random.Next(2);

            carDoorNumber = random.Next(3) + 1;
            state = GameState.WaitingPlayerToPickDoor;
        }

        public void PickDoor(int doorNumber)
        {
            if (state != GameState.WaitingPlayerToPickDoor)
                throw new Exception("It is not the players turn to pick a door.");

            if (doorNumber <= 0 || doorNumber >= 4)
                throw new ArgumentOutOfRangeException(nameof(doorNumber));

            playerDoorNumber = doorNumber;
            HostOpensAGoatDoor();
            state = GameState.WaitingPlayerToSwitchDoors;
        }

        private void HostOpensAGoatDoor()
        {
            int[] availableGoatDoorNumbers = Enumerable.Range(1, 3)
                .Where(x => x != carDoorNumber && x != playerDoorNumber)
                .ToArray();

            switch (availableGoatDoorNumbers.Length)
            {
                case 1:
                    openedDoorNumber = availableGoatDoorNumbers[0];
                    break;

                case 2:
                    bool chooseFirstGoat = random.Next(2) > 0;
                    openedDoorNumber = chooseFirstGoat
                        ? availableGoatDoorNumbers[0]
                        : availableGoatDoorNumbers[1];
                    break;

                default:
                    throw new Exception("The available doors with goats were calculated calculated wrong.");
            }
        }

        public void SwitchDoor(bool doSwitch)
        {
            if (doSwitch)
            {
                playerDoorNumber = Enumerable.Range(1, 3)
                    .First(x => x != playerDoorNumber && x != openedDoorNumber);
            }

            state = GameState.Finished;
        }

        public bool DidPlayerWin()
        {
            if (state != GameState.Finished)
                throw new Exception("The game is not finished yet.");

            return playerDoorNumber == carDoorNumber;
        }
    }
}