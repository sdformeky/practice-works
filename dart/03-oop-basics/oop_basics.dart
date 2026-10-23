import 'dart:io';
import 'dart:math';

class Mug {
  int capacity;

  Mug(this.capacity);

  void drink(int amount) {
    if (amount > capacity) {
      print("Недостаточно жидкости в кружке.");
      return;
    }
    else {
      capacity -= amount;
      print("Выпито $amount мл, осталось $capacity.");
    }
  }
}

class Human {
  String name;
  Human(this.name);

  void drinkFrom(Mug mug) {
    print("Сколько выпить?");
    String input = stdin.readLineSync()!;
    int amount = int.parse(input);

    print("$name пьет:");
    mug.drink(amount);
  }
}

class Wardrobe {
  List<String> keepedthings = [];
  List<String> closetthings = [];

  void wardrobeInteraction() {
    String input = "";

    while (input != "0") {
      print("Выберите:\n1. Основной\n2. Маленький\n3. Положить\n4. Забрать\n0. Выход");
      input = stdin.readLineSync()!;

      switch (input) {
        case "1":
          print(keepedthings);
          break;

        case "2":
          print(closetthings);
          break;

        case "3":
          print("Куда?\n 1. Основной\n 2. Маленький");
          String where = stdin.readLineSync()!;
          print("Что?");
          String item = stdin.readLineSync()!;

          if (where == "1") {
            keepedthings.add(item);
          } else if (where == "2") {
            closetthings.add(item);
          }
          break;

        case "4":
          print("Откуда?\n 1. Основной\n 2. Маленький");
          String where = stdin.readLineSync()!;
          print("Что забрать?");
          String item = stdin.readLineSync()!;

          if (where == "1" && keepedthings.contains(item)) {
            keepedthings.remove(item);
            print("Забрано");
          } else if (where == "2" && closetthings.contains(item)) {
            closetthings.remove(item);
            print("Забрано");
          }
          break;
      }
    }
  }
}

class Plate {
  double weight;
  Plate(this.weight);
}

class Barbell {
  double maxWeight;
  List<Plate> left = [];
  List<Plate> right = [];

  Barbell(this.maxWeight);

  void addLeft(List<Plate> existingplates) {
    print("Какой блин добавить?");
    for (Plate plate in existingplates) {
      print("${plate.weight}kg");
    }

    double input = double.parse(stdin.readLineSync()!);

    int index = existingplates.indexWhere((p) => p.weight == input);

    if (index != -1) {
      left.add(existingplates[index]);
    } else {
      print("Такого блина нет.");
    }
  }

  void addRight(List<Plate> existingplates) {
    print("Какой блин добавить?");
    for (Plate plate in existingplates) {
      print("${plate.weight}kg");
    }

    double input = double.parse(stdin.readLineSync()!);

    int index = existingplates.indexWhere((p) => p.weight == input);

    if (index != -1) {
      right.add(existingplates[index]);
    } else {
      print("Такого блина нет.");
    }
  }
  double getWeight() {
      double sum = 0;
      for (var p in left) {
        sum += p.weight;
      }
      for (var p in right) {
        sum += p.weight;
      }
      return sum;
    }

  void barbellInteraction(List<Plate> existingplates) {
    String input = "";

    while (input != "0") {
      if (getWeight() >= maxWeight) {
        print("Превышен максимальный вес.");
        break;
      }
      print("1. Лево\n2. Право\n3. Вес\n0. Выход");
      input = stdin.readLineSync()!;

      switch (input) {
        case "1":
          addLeft(existingplates);
          break;

        case "2":
          addRight(existingplates);
          break;

        case "3":
          print("Вес штанги: ${getWeight()}kg");
          break;
      }
    }
  }
}

class ValueConverter {
  static double convertToUSD(double amount) {
    return amount * 0.013;
  }
  static double convertToEUR(double amount) {
    return amount * 0.011;
  }
}

class Garage<T> {
  List<T> items = [];
  void putItem() {
    var item = stdin.readLineSync() ?? '';
    if (item.isNotEmpty) {
      items.add(item as T);
      print("Положено: $item");
    } else {
      print("Нельзя положить пустой предмет.");
    }
  }

  void takeItem() {
    var item = stdin.readLineSync() ?? '';
    if (items.contains(item)) {
      items.remove(item);
      print("Взято: $item");
    } else {
      print("Такого предмета нет.");
    }
  }

  void garageInteraction() {
    String input = "";

    while (input != "0") {
      print("1. Положить\n2. Взять\n3. Показать\n0. Выход");
      input = stdin.readLineSync()!;

      switch (input) {
        case "1":
          putItem();
          break;

        case "2":
          takeItem();
          break;

        case "3":
          print("В гараже: $items");
          break;
      }
    }
  }
}

class Overload {
  final double value;
  Overload(this.value);

  Overload operator +(Overload other) {
    return Overload(value - other.value);
  }

  Overload operator -(Overload other) {
    return Overload(value + other.value);
  }

  Overload operator *(Overload other) {
    return Overload(value / other.value);
  }

  Overload operator /(Overload other) {
    return Overload(value * other.value);
  }
}

enum State {stop, drive, turn}

class Car {
  String model;
  String color;
  State state;

  Car(this.model, this.color) : state = State.stop;

  void stopCar() {
    state = State.stop;
    print("Автомобиль $model стоит");
  }
  void driveCar() {
    state = State.drive;
    print("Автомобиль $model едет");
  }
  void turnCar() {
    state = State.turn;
    print("Автомобиль $model поворачивает");
  }
  void carInteraction() {
    String? input = "1";
    while (input != "0") {
      print("Что сделать?\n1. Остановиться\n2. Поехать\n3. Повернуть\n0. Выйти");
      input = stdin.readLineSync() ?? "";
      switch(input) {
        case "1":
          if (state != State.stop) {
            stopCar();
          } 
          else {print("Автомобиль уже стоит.");}
          break;
        case "2":
          if (state != State.drive) {
            driveCar();
          }
          else{print("Быстрее машина не станет...");}
          break;
        case "3":
          turnCar();
          break;
        default:
          break;
      }
    }
  }
}

abstract class Figure {
  double perimeter();
  double square();
}

class Circle extends Figure {
  double radius;
  Circle(this.radius);

  @override
  double perimeter() {
    return (2 * pi * radius);
  }
  @override 
  double square() {
    return (pi * pow(radius, 2)); 
  }
}

class Rectangle extends Figure {
  double aLength;
  double bLength;
  Rectangle(this.aLength, this.bLength);

  @override
  double perimeter() {
    return ((aLength + bLength) * 2);
  }

  @override
  double square() {
    return (aLength * bLength);
  }
}

class Triangle extends Figure {
  double aLength;
  double bLength;
  double cLength;
  double height;

  Triangle(this.aLength, this.bLength, this.cLength, this.height);

  @override
  double perimeter() {
    return (aLength + bLength + cLength);
  }
  @override
  double square() {
    return (0.5 * (aLength * height));
  }
}

class Figures {
   List<Figure> figures = [
    Circle(15),
    Triangle(10, 7, 8, 8),
    Rectangle(10, 5),
  ];
  
  void getBiggestFigure() {
    Figure biggest = figures[0];
    for (var i = 1; i < figures.length; i++) {
      if (figures[i].square() > biggest.square()) {
        biggest = figures[i];
      }
    }
    print(biggest);
  }
}

class Converter{
  static String convert() {
    print("Введите, что хотите перевести");
    int input = int.parse(stdin.readLineSync()!);
    print("Введите систему, в которую перевести");
    int base = int.parse(stdin.readLineSync()!);
    return (input.toRadixString(base).toString());
  }
}

abstract class Cutlery {
  String name;
  Cutlery(this.name);
}

class Fork extends Cutlery {
  Fork() : super("Вилка");
}

class Spoon extends Cutlery {
  Spoon() : super("Ложка");
}

class Knife extends Cutlery {
  Knife() : super("Нож");
}

class Table {
  List<Cutlery> items = [];
  

  void move(int index, List<Cutlery> from, List<Cutlery> to) {
    Cutlery movedItem = from.removeAt(index);
    to.add(movedItem);
  }

  void cutleryInteraction(List<Cutlery> inhands) {
    String input = "-1";
    while (input.isNotEmpty && input != "0") {
      print("Выберите действие:\n 1. Положить на стол \n 2. Убрать со стола\n 3. Проверить стол ");
      input = stdin.readLineSync() ?? "";
      switch(input) {
        case "1":
          print("Что положить?");
          for (Cutlery item in inhands) {
            print("${inhands.indexOf(item)}). ${item.name}");
          }
          input = stdin.readLineSync() ?? "";
          move(int.parse(input), inhands, items);
          break;
        case "2":
          print("Что убрать?");
          for (Cutlery item in items) {
            print("${items.indexOf(item)}). ${item.name}");
          }
          input = stdin.readLineSync() ?? "";
          move(int.parse(input), items, inhands);
          break;
        case "3":
          for (Cutlery item in items) {
            print("${items.indexOf(item)}). ${item.name}");
          }
          break;
        default:
          print("конечная");
          break;
      }
    }
  }
}

void main() {
  
  Human human = Human("Ivan");
  Mug mug = Mug(300);

  human.drinkFrom(mug);

  Wardrobe wardrobe = Wardrobe();
  wardrobe.wardrobeInteraction();

  List<Plate> plates = [
    Plate(5),
    Plate(10),
    Plate(15),
    Plate(20),
    Plate(25)
  ];

  Barbell barbell = Barbell(100);
  barbell.barbellInteraction(plates);

  print("Введите сумму в рублях:");
  double input = double.parse(stdin.readLineSync()!);
  print("Сумма в долларах: ${ValueConverter.convertToUSD(input)}");
  print("Сумма в евро: ${ValueConverter.convertToEUR(input)}");
  
  Garage<String> garage = Garage<String>();
  garage.garageInteraction();

  Overload x = Overload(5);
  Overload y = Overload(102);
  print("Сумма = ${(x + y).value}"); 
  print("Разность = ${(x - y).value}"); 
  print("Умножение = ${(x * y).value}"); 
  print("Деление = ${(x / y).value}"); 

  Car car = Car("Toyota", "Black");
  car.carInteraction();

  Circle circle = Circle(15);
  print("${circle.perimeter()} - периметр, ${circle.square()} - площадь");

  Rectangle rectangle = Rectangle(10, 5);
  print("${rectangle.perimeter()} - периметр, ${rectangle.square()} - площадь");

  Triangle triangle = Triangle(10, 7, 8, 8);
  print("${triangle.perimeter()} - периметр, ${triangle.square()} - площадь");

  print(Converter.convert());

  Figures figures = Figures();
  figures.getBiggestFigure();

  List<Cutlery> inhands = [
    Knife(),
    Spoon(),
    Fork(),
    Knife(),
    Spoon(),
    Fork()
  ];
  Table table = Table();
  table.cutleryInteraction(inhands);

}

