import json

class Person:
    def __init__(self, name):
        self._name = name

    @property
    def name(self):
        return self._name

    def menu(self):
        pass


class Book:
    def __init__(self, title, author, available=True):
        self.__title = title
        self.__author = author
        self.__available = available

    @property
    def title(self):
        return self.__title

    @property
    def author(self):
        return self.__author

    @property
    def is_available(self):
        return self.__available

    def take_book(self):
        if self.__available:
            self.__available = False
            return True
        return False

    def return_book(self):
        self.__available = True

    def __str__(self):
        status = "Доступна" if self.__available else "Выдана"
        return f"'{self.__title}' — {self.__author} [{status}]"


class User(Person):
    def __init__(self, name):
        super().__init__(name)
        self.borrowed_books = []

    def menu(self):
        print(f"\n--- Меню пользователя: {self.name} ---")
        print("1. Посмотреть доступные книги\n2. Взять книгу\n3. Вернуть книгу\n4. Мои книги\n0. Выход")


class Librarian(Person):
    def menu(self):
        print(f"\n--- Меню библиотекаря: {self.name} ---")
        print("1. Добавить книгу\n2. Удалить книгу\n3. Зарегистрировать пользователя\n4. Список пользователей\n5. Все книги\n0. Выход")


class Library:
    def __init__(self):
        self.__books = []
        self.__users = []

    def load_data(self):
        try:
            with open("books.txt", "r", encoding="utf-8") as f:
                for line in f:
                    t, a, s = line.strip().split(";")
                    self.__books.append(Book(t, a, s == "1"))

            with open("users.txt", "r", encoding="utf-8") as f:
                for line in f:
                    self.__users.append(User(line.strip()))

            with open("borrowed_books.txt", "r", encoding="utf-8") as f:
                borrowed_data = json.load(f)
                for book_title, info in borrowed_data.items():
                    borrower_name = info.get("borrower")
                    user = self.find_user(borrower_name)
                    if user:
                        for book in self.__books:
                            if book.title == book_title:
                                user.borrowed_books.append(book)
                                book.take_book()
                                break
        except FileNotFoundError:
            pass

    def save_data(self):
        with open("books.txt", "w", encoding="utf-8") as f:
            for b in self.__books:
                status = "1" if b.is_available else "0"
                f.write(f"{b.title};{b.author};{status}\n")

        with open("users.txt", "w", encoding="utf-8") as f:
            for u in self.__users:
                f.write(f"{u.name}\n")

        # Сохраняем забранные книги в JSON формате
        borrowed_data = {}
        for user in self.__users:
            for book in user.borrowed_books:
                borrowed_data[book.title] = {
                    "borrower": user.name,
                    "author": book.author,
                    "status": "borrowed"
                }

        with open("borrowed_books.txt", "w", encoding="utf-8") as f:
            json.dump(borrowed_data, f, ensure_ascii=False, indent=2)

    def add_book(self, title, author):
        self.__books.append(Book(title, author))
        print("Книга добавлена!")

    def remove_book(self, title):
        original_count = len(self.__books)
        self.__books = [b for b in self.__books if b.title.lower() != title.lower()]
        print("Книга удалена!" if len(self.__books) < original_count else "Книга не найдена.")

    def find_user(self, name):
        for u in self.__users:
            if u.name.lower() == name.lower():
                return u
        return None

    def register_user(self, name):
        if self.find_user(name):
            print("Такой пользователь уже есть.")
        else:
            self.__users.append(User(name))
            print("Пользователь зарегистрирован.")

    def show_users(self):
        print("\nСписок пользователей:")
        for u in self.__users: print(f"- {u.name}")

    def show_books(self, only_available=False):
        print("\nСписок книг:")
        for b in self.__books:
            if only_available and not b.is_available:
                continue
            print(b)

    def issue_book(self, user, title):
        for b in self.__books:
            if b.title.lower() == title.lower():
                if b.take_book():
                    user.borrowed_books.append(b)
                    print(f"Вы взяли книгу: {b.title}")
                else:
                    print("Книга уже выдана.")
                return
        print("Книга не найдена.")

    def return_book(self, user, title):
        for b in user.borrowed_books:
            if b.title.lower() == title.lower():
                b.return_book()
                user.borrowed_books.remove(b)
                print(f"Книга '{title}' возвращена.")
                return
        print("У вас нет такой книги.")

def main():
    lib = Library()
    lib.load_data()

    print("Добро пожаловать в Систему Управления Библиотекой!")
    print("Выберите роль:\n1. Библиотекарь\n2. Пользователь")
    role_choice = input("> ")

    if role_choice == "1":
        cmd = "-1"
        person = Librarian("Главный Библиотекарь")
        while cmd != "0":
            person.menu()
            cmd = input("> ")
            if cmd == "1":
                lib.add_book(input("Название: "), input("Автор: "))
            elif cmd == "2":
                lib.remove_book(input("Название для удаления: "))
            elif cmd == "3":
                lib.register_user(input("Имя нового пользователя: "))
            elif cmd == "4":
                lib.show_users()
            elif cmd == "5":
                lib.show_books()
            elif cmd == "0":
                break
    else:
        cmd = "-1"
        name = input("Введите ваше имя: ")
        user = lib.find_user(name)
        if not user:
            print("Вы не зарегистрированы. Обратитесь к библиотекарю.")
            return

        while cmd != "0":
            user.menu()
            cmd = input("> ")
            if cmd == "1":
                lib.show_books(only_available=True)
            elif cmd == "2":
                lib.issue_book(user, input("Название книги: "))
            elif cmd == "3":
                lib.return_book(user, input("Название книги: "))
            elif cmd == "4":
                print("\nВаши книги:")
                for b in user.borrowed_books: print(f"- {b}")
            elif cmd == "0":
                break

    lib.save_data()
    print("Данные сохранены. До свидания!")

main()