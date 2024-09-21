using System;
using System.Text;

namespace lab2
{
    internal class NimGame
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.GetEncoding(1251);

            bool playAgain;
            do
            {
                Console.WriteLine("Ласкаво просимо до гри Нім!");

                Console.WriteLine("Оберіть варіант гри (1 - Пряма гра, 2 - Зворотна гра): ");
                int gameMode = int.Parse(Console.ReadLine());

                Console.WriteLine("Оберіть кількість монет (N): ");
                int N = int.Parse(Console.ReadLine());

                Console.WriteLine("Введіть максимальну кількість монет, яку можна взяти за хід (k): ");
                int k = int.Parse(Console.ReadLine());

                int currentPlayer;

                // Перевірка виграшної позиції для прямої гри
                if (gameMode == 1 && N % (k + 1) != 0)
                {
                    currentPlayer = 1; // Комп'ютер ходить першим у виграшній позиції
                    Console.WriteLine("Комп'ютер ходить першим.");
                }
                // Перевірка виграшної позиції для зворотної гри
                else if (gameMode == 2 && (N - 1) % (k + 1) != 0)
                {
                    currentPlayer = 1; // Комп'ютер ходить першим у виграшній позиції
                    Console.WriteLine("Комп'ютер ходить першим.");
                }
                else
                {
                    Random random = new Random();
                    currentPlayer = random.Next(1, 3); // Випадковий вибір: хто ходить першим
                    Console.WriteLine(currentPlayer == 1 ? "Комп'ютер ходить першим." : "Користувач ходить першим.");
                }

                while (N > 0)
                {
                    Console.WriteLine($"\nЗалишилось монет: {N}");

                    if (currentPlayer == 1) // Хід комп'ютера
                    {
                        Console.WriteLine("Комп'ютер робить хід.");
                        int move = ComputerMove(N, k, gameMode);
                        N -= move;
                        Console.WriteLine($"Комп'ютер бере {move} монет.");

                        if (N == 0)
                        {
                            Console.WriteLine(gameMode == 1 ? "Комп'ютер переміг!" : "Комп'ютер програв!");
                            break;
                        }
                        currentPlayer = 2; // Передача ходу користувачу
                    }
                    else // Хід користувача
                    {
                        Console.WriteLine("Користувач робить хід.");
                        int userMove = UserMove(N, k);
                        N -= userMove;

                        if (N == 0)
                        {
                            Console.WriteLine(gameMode == 1 ? "Користувач переміг!" : "Користувач програв!");
                            break;
                        }
                        currentPlayer = 1; // Передача ходу комп'ютеру
                    }
                }

                // Запит на повторну гру
                Console.WriteLine("Хочете зіграти ще раз? (y/n): ");
                string response = Console.ReadLine().ToLower();
                playAgain = response == "y" || response == "yes";

            } while (playAgain);

            Console.WriteLine("Дякуємо за гру!");
        }

        // Функція ходу комп'ютера
        static int ComputerMove(int N, int k, int gameMode)
        {
            int move;
            if (gameMode == 1) // Пряма гра
            {
                move = N % (k + 1); // Оптимальний хід для виграшу
                if (move == 0) move = 1; // Якщо немає виграшного ходу, взяти мінімум
            }
            else // Зворотна гра
            {
                move = (N - 1) % (k + 1); // Оптимальний хід для виграшу
                if (move == 0) move = k; // Якщо немає виграшного ходу, взяти максимальну кількість монет
            }
            return move;
        }

        // Функція ходу користувача
        static int UserMove(int N, int k)
        {
            int move = 0;
            while (true)
            {
                Console.WriteLine($"Скільки монет ви хочете взяти? (від 1 до {Math.Min(k, N)}): ");
                move = int.Parse(Console.ReadLine());

                if (move >= 1 && move <= Math.Min(k, N)) // Перевірка на коректність ходу
                    break;
                else
                    Console.WriteLine($"Невірний хід. Ви можете взяти від 1 до {Math.Min(k, N)} монет.");
            }
            return move;
        }
    }
}
