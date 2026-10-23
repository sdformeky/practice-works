import os
import random


if not os.path.exists("stats"):
    os.mkdir("stats")

def save_result(text):
    with open("stats/results.txt", "a", encoding="utf-8") as f:
        f.write(text + "\n")


def print_board(board):
    n = len(board)
    print("  " + " ".join(str(i + 1) for i in range(n)))
    for i in range(n):
        print(i + 1, " ".join(board[i]))


def check_win(board, p):
    n = len(board)

    for row in board:
        if all(x == p for x in row):
            return True

    for col in range(n):
        ok = True
        for row in range(n):
            if board[row][col] != p:
                ok = False
                break
        if ok:
            return True

    if all(board[i][i] == p for i in range(n)):
        return True
    if all(board[i][n - 1 - i] == p for i in range(n)):
        return True

    return False


def check_draw(board):
    for row in board:
        for cell in row:
            if cell == ".":
                return False
    return True


def play():
    size = input("Введите размер поля (3–9): ")
    while not size.isdigit() or not (3 <= int(size) <= 9):
        size = input("Неверно! Введите число 3–9: ")
    size = int(size)

    board = [["." for _ in range(size)] for _ in range(size)]

    mode = input("Режим:\n 1 - Игрок против игрока\n2 - Игрок против бота: ")
    while mode not in ("1", "2"):
        mode = input("Введите 1 или 2: ")

    current = random.choice(["X", "O"])
    print(f"Первым ходит {current}")

    while True:
        print_board(board)
        if current == "X" or mode == "1":

            row = input("Введите номер строки: ")
            while not row.isdigit() or not (1 <= int(row) <= size):
                row = input("Неверно! Введите номер строки: ")
            r = int(row) - 1

            col = input("Введите номер столбца: ")
            while not col.isdigit() or not (1 <= int(col) <= size):
                col = input("Неверно! Введите номер столбца: ")
            c = int(col) - 1

            if board[r][c] != ".":
                print("Клетка занята, попробуйте другой ход!")
                continue
        else:
            free = []
            for i in range(size):
                for j in range(size):
                    if board[i][j] == ".":
                        free.append((i, j))
            r, c = random.choice(free)
            print("Бот сделал ход.")

        board[r][c] = current

        if check_win(board, current):
            print_board(board)
            print(f"{current} победил!")
            save_result(f"Победил {current} на поле {size}x{size}")
            break

        if check_draw(board):
            print_board(board)
            print("Ничья!")
            save_result(f"Ничья на поле {size}x{size}")
            break

        current = "O" if current == "X" else "X"


def main():
    while True:
        play()
        again = input("Сыграть ещё раз? (y/n): ").lower()
        if again != "y":
            print("Выход из игры...")
            break


main()
