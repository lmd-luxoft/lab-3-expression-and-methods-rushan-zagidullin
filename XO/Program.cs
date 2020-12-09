using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XO
{
    class Program
    {
        private static int _winner;
        private static string[] _playerNames = new string[2];
        private static char[] _playerCells = {'-', 'X', 'O'};
       static int[] _cells = new int[9];

        static void ShowCells()
        {
            Console.Clear();

            Console.WriteLine("Числа клеток:");
            Console.WriteLine("-1-|-2-|-3-");
            Console.WriteLine("-4-|-5-|-6-");
            Console.WriteLine("-7-|-8-|-9-");

            Console.WriteLine("Текущая ситуация (---пустой):");
            Console.WriteLine($"-{GetCell(0)}-|-{GetCell(1)}-|-{GetCell(2)}-");
            Console.WriteLine($"-{GetCell(3)}-|-{GetCell(4)}-|-{GetCell(5)}-");
            Console.WriteLine($"-{GetCell(6)}-|-{GetCell(7)}-|-{GetCell(8)}-");        
        }

        static char GetCell(int cell)
        {
            return _playerCells[_cells[cell]];
        }
        
        static void MakeMove(int num)
        {
            string rawCell = null;
            int cell;
            Console.Write(_playerNames[num - 1]);
            do
            {
                string message = rawCell == null
                    ? ",введите номер ячейки,сделайте свой ход:"
                    : "Введите номер правильного ( 1-9 ) или пустой ( --- ) клетки , чтобы сделать ход:";
                Console.Write(message);

                rawCell = Console.ReadLine();
            }
            while (!Int32.TryParse(rawCell, out cell) && !IsValidCell(cell));
            _cells[cell - 1] = num;
        }

        static bool IsValidCell(int cell)
        {
            return cell >= 1 && cell <= 9 && _cells[cell - 1] == _playerCells[0];
        }
        
        static int Check()
        {
            for (int i = 0; i < 3; i++)
            {
                if (IsRowWinner(i))
                    return _cells[i];
                if (IsColumnWinner(i))
                    return _cells[i];
            } 
            if (IsMainDiagonalWinner() || IsSideDiagonalWinner())
                return _cells[4];
            return 0;
        }

        static bool IsRowWinner(int row)
        {
            return _cells[row * 3] != 0 
                   && _cells[row * 3] == _cells[row * 3 + 1]
                   && _cells[row * 3 + 1] == _cells[row * 3 + 2];
        }

        static bool IsColumnWinner(int column)
        {
            return _cells[column] != 0 
                   && _cells[column] == _cells[column + 3]
                   && _cells[column + 3] == _cells[column + 6];
        }

        static bool IsMainDiagonalWinner()
        {
            return _cells[2] != 0 
                   && _cells[2] == _cells[4] 
                   && _cells[4] == _cells[6];
        }

        static bool IsSideDiagonalWinner()
        {
            return _cells[0] != 0 
                   && _cells[0] == _cells[4]
                   && _cells[4] == _cells[8];
        }

        static void Result()
        {
            if (_winner > 0)
            {
                Console.WriteLine($"{_playerNames[_winner - 1]} вы выиграли поздравляем, {_playerNames[_winner % 2]} а вы проиграли...");
            }

        }

        static void Main(string[] args)
        {
            Console.Write("Введите имя первого игрока : ");
            _playerNames[0] = Console.ReadLine();
            do
            {
                Console.Write("Введите имя второго игрока: ");
                _playerNames[1] = Console.ReadLine();
                Console.WriteLine();
            } while (_playerNames[0] == _playerNames[1]);

            ShowCells();

            for (int move = 1; move <= 9; move++)
            {
                MakeMove(move % 2 + 1);
                ShowCells();
                if (move >= 5)
                {
                    _winner = Check();
                    if (_winner > 0)
                        break;
                }
            }
            Result();
        }
    }
}
