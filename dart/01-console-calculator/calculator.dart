import "dart:io";
import "dart:math";

void main() {
int choose = 1;
double a = 0;
double b = 0;

while (choose != 0) {
  print("Введите число A и число B");
  a = double.parse(stdin.readLineSync()!);
  b = double.parse(stdin.readLineSync()!);
  print("Выберите операцию: ");
  print("""1). Сложение
2). Вычитание
3). Умножение
4). Деление
5). Целочисленное деление
6). Остаток от деления
7). Возведение первого числа в степень, равную второму числу
-------------------------------------------------------------
Операции сравнения:
8). Равно
9). Не равно
10). Больше
11). Меньше
12). Больше или равно
13). Меньше или равно
-------------------------------------------------------------
14). Логическое И
15). Логическое ИЛИ
16). Логическое НЕ
0). Выход из программы""");
  choose = int.parse(stdin.readLineSync()!);
  
  switch (choose) {
    case 1: {
      print("Результат сложения: \n $a + $b = ${a + b} \n");
      sleep(Duration(seconds: 2));
      break;
    }
    case 2: {
      print("Результат вычитания: $a - $b = ${a - b} \n");
      sleep(Duration(seconds: 2));
      break;
    }
    case 3: {
      print("Результат умножения: $a * $b = ${a * b} \n");
      sleep(Duration(seconds: 2));
      break;
    }
    case 4: {
      while (b == 0) {
        print("Число B не может быть равно нулю. Введите другое число: ");
        b = double.parse(stdin.readLineSync()!);
      }
      print("Результат деления: $a / $b = ${a / b}");
      sleep(Duration(seconds: 2));
      break;
    }
    case 5: {
      while (b == 0) {
        print("Число B не может быть равно нулю. Введите другое число: ");
        b = double.parse(stdin.readLineSync()!);
      }
      print("Результат целочисленного деления: $a ~/ $b = ${a ~/ b}");
      sleep(Duration(seconds: 2));
      break;
    }
    case 6: {
      while (b == 0) {
        print("Число B не может быть равно нулю. Введите другое число: ");
        b = double.parse(stdin.readLineSync()!);
      }
      print("Остаток от деления: $a % $b = ${a % b}");
      sleep(Duration(seconds: 2));
      break;
    }
    case 7:{
      print("Результат возведения в степень: $a ^ $b = ${pow(a, b)}");
      sleep(Duration(seconds: 2));
      break;
    }
    case 8: {
      if (a == b) {print("Число А = Число B");}
      else {print("Число А != Число B");}
      sleep(Duration(seconds: 2));
      break;
      }
    case 9: {
      if (a != b) {print("Число А != Число В");}
      else {print("Число А = Число В");}
      sleep(Duration(seconds: 2));
      break;
    }
    case 10: {
      if (a > b) {print("Число А > Число В");}
      else {print("Число А < Число В");}
      sleep(Duration(seconds: 2));
      break;
    }
    case 11: {
      if (a < b) {print("Число А < Число В");}
      else {print("Число А > Число В");}
      sleep(Duration(seconds: 2));
      break;
    }
    case 12: {
      if (a >= b) {print(true);}
      else {print(false);}
      sleep(Duration(seconds: 2));
      break;
    }
    case 13: {
      if (a <= b) {print(true);}
      else {print(false);}
      sleep(Duration(seconds: 2));
      break;
    }
        case 14: {
      bool aBool = a != 0;
      bool bBool = b != 0;
      print("Логическое И: $aBool && $bBool = ${aBool && bBool}");
      sleep(Duration(seconds: 2));
      break;
    }

    case 15: {
      bool aBool = a != 0;
      bool bBool = b != 0;
      print("Логическое ИЛИ: $aBool || $bBool = ${aBool || bBool}");
      sleep(Duration(seconds: 2));
      break;
    }

    case 16: {
      bool aBool = a != 0;
      bool bBool = b != 0;
      print("Логическое НЕ: !$aBool = ${!aBool}; !$bBool = ${!bBool}");
      sleep(Duration(seconds: 2));
      break;
    }
    case 0: break;
    default: {print("Такой операции не существует.");}
    }
  }
}
