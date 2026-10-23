# Practice Works

Coursework from my programming studies at college (specialty 09.02.07, Information Systems and Programming), collected in one place. Each folder is a separate practical assignment, numbered in the order I did them, from the first Python operators up to OOP, networking, multiprocessing, web and Flutter.

The programs are console apps unless stated otherwise. All user-facing text is in Russian.

**Languages:** Python · Dart / Flutter · JavaScript · C# · Java

## Contents

| # | Project | Language | What it covers |
|---|---|---|---|
| 1 | [Operators calculator](#python-01--operators-calculator) | Python | Input, arithmetic / comparison / logical / bitwise / membership / identity operators |
| 2 | [Tic-tac-toe](#python-02--tic-tac-toe) | Python | 2D lists, game loop, input validation, simple bot, writing results to a file |
| 3 | [JSON & CSV stats](#python-03--json--csv-stats) | Python | `json`, `csv.DictWriter` / `DictReader`, aggregating data with dicts |
| 4 | [Library system](#python-04--library-system) | Python | OOP: classes, inheritance, encapsulation, properties; saving state to txt/json and to pickle |
| 5 | [Socket chat](#python-05--socket-chat) | Python | TCP sockets, multithreaded server, broadcasting messages, `threading.Lock` |
| 6 | [Parallel image processing](#python-06--parallel-image-processing) | Python | Pillow, `multiprocessing.Pool`, sequential vs parallel timing |
| 7 | [Flask app with tests](#python-07--flask-app) | Python | Minimal Flask JSON endpoint, pytest with a test client |
| 8 | [Weather API client](#python-08--weather-api-client) | Python | HTTP requests to OpenWeatherMap, error handling, parsing JSON responses |
| 9 | [Console calculator](#dart-01--console-calculator) | Dart | `stdin`, `switch`, loops, arithmetic and logical operations |
| 10 | [Grade journal](#dart-02--grade-journal) | Dart | `List`, nested `Map`, averages, searching and filtering |
| 11 | [OOP basics](#dart-03--oop-basics) | Dart | Classes, inheritance, abstract classes, generics, operator overloading, static methods |
| 12 | [Notes app](#dart-04--flutter-notes) | Flutter | `StatefulWidget`, `ListView.builder`, adding / editing / deleting notes |
| 13 | [Expense tracker](#javascript-01--expense-tracker) | JavaScript | Arrays of objects, CRUD functions, grouping by category |
| 14 | [Sorting algorithms](#c-01--sorting-algorithms) | C# | Bubble, shaker, selection, insertion and merge sort |
| 15 | [Text file processing](#c-02--text-file-processing) | C# | `System.IO`, reading and analyzing files, writing results |
| 16 | [Car rental](#c-03--car-rental) | C# | Classes, inheritance, arrays, console menu |
| 17 | [WPF calculator](#c-04--wpf-calculator) | C# / WPF | XAML layout, event handlers, DEC/BIN/HEX number systems |
| 18 | [WPF library](#c-05--wpf-library) | C# / WPF | Entity Framework (database-first), SQL Server, CRUD in multiple windows |
| 19 | [Store management](#java-01--store-management) | Java | OOP: inheritance, roles, saving data to files |
| 20 | [Client-server chat with auth](#java-02--client-server-chat-with-auth) | Java | `ServerSocket`, a thread per client, registration and login |

## Repository structure

```
practice-works/
├── python/
│   ├── 01-operators-calculator/
│   ├── 02-tic-tac-toe/
│   ├── 03-json-csv-stats/
│   ├── 04-library-system/
│   ├── 05-socket-chat/
│   ├── 06-parallel-image-processing/
│   ├── 07-flask-app/
│   └── 08-weather-api/
├── dart/
│   ├── 01-console-calculator/
│   ├── 02-grade-journal/
│   ├── 03-oop-basics/
│   └── 04-flutter-notes/
├── javascript/
│   └── 01-expense-tracker/
├── csharp/
│   ├── 01-sorting-algorithms/
│   ├── 02-text-file-processing/
│   ├── 03-car-rental/
│   ├── 04-wpf-calculator/
│   └── 05-wpf-library/
└── java/
    ├── 01-store-management/
    └── 02-client-server-auth/
```

---

## Python

Requires Python 3.10+. Third-party packages are only needed for projects 6–8.

### Python 01 — Operators calculator

`python/01-operators-calculator/calculator.py`

The user picks an operator and enters two numbers. The program supports every operator group in Python: arithmetic (`+ - * / % ** //`), comparison, logical (only for 0 and 1), bitwise, membership (`in`, `not in`) and identity (`is`, `is not`).

```bash
python calculator.py
```

### Python 02 — Tic-tac-toe

`python/02-tic-tac-toe/tic_tac_toe.py`

Tic-tac-toe on a board from 3×3 up to 9×9.

- Two modes: player vs player, or player vs a bot that makes random moves on free cells.
- The first player is chosen at random.
- Checks for wins on rows, columns and both diagonals, and for a draw.
- Every result is appended to `stats/results.txt`.

```bash
python tic_tac_toe.py
```

### Python 03 — JSON & CSV stats

`python/03-json-csv-stats/json_csv_stats.py`

Writes a JSON file and an employee table to `emp_with_salary.csv`, reads the table back and calculates:

- average age by city;
- total salary by position;
- number of employees by city.

The result is saved to `stats.json`.

```bash
python json_csv_stats.py
```

### Python 04 — Library system

`python/04-library-system/`

A console library with two roles:

- **Librarian:** add and remove books, register users, view all books and users.
- **Reader:** borrow and return books, view available books and their own borrowed books.

Built around the classes `Person` → `User` / `Librarian`, plus `Book` and `Library`, with private fields and `@property`.

The same program exists in two versions that save data differently:

| File | Storage |
|---|---|
| `library_text_files.py` | `books.txt`, `users.txt` and `borrowed_books.txt` (JSON) |
| `library_pickle.py` | Everything serialized into `library.pkl` |

```bash
python library_text_files.py
python library_pickle.py
```

> For `library_text_files.py`, create the data files first. `borrowed_books.txt` should contain `{}`.

### Python 05 — Socket chat

`python/05-socket-chat/`

A TCP chat for several clients:

- **`server.py`** accepts connections on `127.0.0.1:5001` and starts a thread for each client. It broadcasts every message to everyone except the sender, and announces when users join or leave. The client list is protected by `threading.Lock`.
- **`client.py`** connects to the server. One thread receives messages while the main thread reads input and sends it.

```bash
python server.py     # in one terminal
python client.py     # in two or more other terminals
```

### Python 06 — Parallel image processing

`python/06-parallel-image-processing/image_processing.py`

Processes every image in the `imgs200/` folder: converts it to grayscale, rotates it 90° and resizes it to 800×600 with Lanczos resampling. It does this twice, sequentially and with `multiprocessing.Pool`, and prints how long each run took so the speedup is visible.

```bash
pip install pillow
# put images into imgs200/
python image_processing.py
```

### Python 07 — Flask app

`python/07-flask-app/`

A minimal Flask service with one endpoint, `GET /`, that returns `{"message": "Hello, DevOps!"}`. It comes with a pytest test that uses Flask's test client.

```bash
pip install flask pytest
python app.py        # http://localhost:5000
pytest               # run the test
```

### Python 08 — Weather API client

`python/08-weather-api/weather.py`

Requests the current weather in Moscow from the OpenWeatherMap API and prints temperature, "feels like", description, humidity, pressure and wind speed. Network and API errors are handled separately.

```bash
pip install requests
export OPENWEATHER_API_KEY=<your key>   # free key at openweathermap.org
python weather.py
```

---

## Dart

Requires the [Dart SDK](https://dart.dev/get-dart) 3.x. The notes app requires [Flutter](https://docs.flutter.dev/get-started/install).

### Dart 01 — Console calculator

`dart/01-console-calculator/calculator.dart`

A looped menu with 16 operations on two numbers:

- arithmetic, including integer division (`~/`), remainder and `pow`;
- comparisons;
- logical AND / OR / NOT.

It re-asks for B when dividing by zero. `0` exits.

```bash
dart run calculator.dart
```

### Dart 02 — Grade journal

`dart/02-grade-journal/grade_journal.dart`

A group grade book stored as `Map<String, Map<String, List<int>>>` (student → subject → grades). It prints:

- each student's grades with averages;
- the average per subject and per student;
- the best student and the subject with the lowest average;
- the group's overall average;
- students with no 2s, and students with only 4s and 5s.

```bash
dart run grade_journal.dart
```

### Dart 03 — OOP basics

`dart/03-oop-basics/oop_basics.dart`

A set of small OOP tasks in one file:

| Class | Topic |
|---|---|
| `Mug`, `Human` | Objects interacting with each other |
| `Wardrobe` | Interactive menu over a list of items |
| `Plate`, `Barbell` | Composition |
| `ValueConverter`, `Converter` | Static methods |
| `Garage<T>` | Generics |
| `Overload` | Operator overloading (`+ - * /`) |
| `Car` | Enum-based state (stop / drive / turn) controlled through a menu |
| `Figure` → `Circle`, `Rectangle`, `Triangle`, `Figures` | Abstract class, inheritance, polymorphism |
| `Cutlery` → `Fork`, `Spoon`, `Knife`, `Table` | Abstract class, moving objects between lists |

```bash
dart run oop_basics.dart
```

### Dart 04 — Flutter notes

`dart/04-flutter-notes/`

A Flutter notes app. You can add a note, tap ✏️ to edit it (the text goes back into the input field) or 🗑️ to delete it. The list is built with `ListView.builder` and the state lives in a `StatefulWidget`.

```bash
cd dart/04-flutter-notes
flutter create .     # generate platform folders (android/, web/, ...)
flutter run
```

---

## JavaScript

### JavaScript 01 — Expense tracker

`javascript/01-expense-tracker/expense_tracker.js`

An expense tracker that works on an array of objects. Its functions:

- add an expense (with input validation);
- list all expenses;
- calculate the total;
- filter and total by category;
- search by title;
- delete by ID;
- print statistics per category.

```bash
node expense_tracker.js
```

---

## C#

Requires the [.NET 8 SDK](https://dotnet.microsoft.com/download). The WPF apps (04, 05) run on Windows only.

### C# 01 — Sorting algorithms

`csharp/01-sorting-algorithms/`

Five sorting algorithms implemented by hand on an `int[]` array: bubble, shaker (cocktail), selection, insertion and merge sort.

```bash
dotnet run --project "csharp/01-sorting-algorithms/pw 5.5 c#"
```

### C# 02 — Text file processing

`csharp/02-text-file-processing/`

A menu-driven program that works with a text file the user picks (folder + file name):

1. find out the type of a character;
2. check whether the file contains a given text;
3. find the length of the longest word;
4. print the file without spaces.

Each result is written to a new file in the same folder.

### C# 03 — Car rental

`csharp/03-car-rental/`

A car rental fleet built on a `Transport` → `CarRent` class hierarchy. You can:

- add cars;
- list all cars;
- list cars available for rent;
- list cars that need refueling or service (based on mileage and fuel level).

### C# 04 — WPF calculator

`csharp/04-wpf-calculator/`

A desktop calculator in WPF:

- arithmetic, square root, sign change, backspace and clear;
- input from both the on-screen buttons and the keyboard;
- display in **DEC / BIN / HEX**;
- configurable rounding precision.

### C# 05 — WPF library

`csharp/05-wpf-library/`

A library management app in WPF on a SQL Server database, accessed through Entity Framework (database-first, `.edmx` model). It has separate windows for **books, genres, readers and loans**, each with a list plus add / edit / delete dialogs.

Set your SQL Server connection in `App.config` (replace `YOUR_PASSWORD`).

---

## Java

Requires JDK 17+.

### Java 01 — Store management

`java/01-store-management/`

A console store with two roles:

- **Employee:** manages products (add, remove, change price, restock).
- **Customer:** tops up a balance and buys products, paying from that balance.

Users and products are saved to text files and loaded on start (`FileManager`).

```bash
cd java/01-store-management && javac -d out src/*.java && java -cp out Main
```

### Java 02 — Client-server chat with auth

`java/02-client-server-auth/`

A TCP chat with registration and login:

- **`server/`:** `ServerSocket` with a separate handler thread per client. `AuthService` stores users in a file and checks logins and passwords.
- **`client/`:** logs in or registers, then sends messages. A background thread prints incoming messages.

```bash
cd java/02-client-server-auth/server && javac -d out src/*.java && java -cp out Main   # terminal 1
cd java/02-client-server-auth/client && javac -d out src/*.java && java -cp out Main   # terminal 2+
```

---

## Other projects

Bigger projects live in their own repositories:

- [uspehNeizbejenboot](https://github.com/sdformeky/uspehNeizbejenboot): an online school built as Spring Cloud microservices (Java, Spring Boot, Eureka, Gateway, JWT, PostgreSQL).
- [rimweb](https://github.com/sdformeky/rimweb): a lab equipment inventory system (Python, FastAPI, SQLAlchemy, Jinja2, REST API, JWT auth).
- [online-shop-dotnet](https://github.com/sdformeky/online-shop-dotnet): an online shop on ASP.NET Core, both as an MVC site and as a REST Web API (EF Core, SQL Server).
- [crm-spring](https://github.com/sdformeky/crm-spring): a CRM on Spring Boot, Thymeleaf and PostgreSQL.
- [police-reports-db](https://github.com/sdformeky/police-reports-db): a police reports database as a Dart/SQLite console app and a Node.js/Express/PostgreSQL web app.
- [flutter-ui-screens](https://github.com/sdformeky/flutter-ui-screens): four mobile screens recreated in Flutter from design mockups.
