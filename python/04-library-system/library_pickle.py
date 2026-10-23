import pickle
import os


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
    FILE = "library.pkl"

    def __init__(self): 
        self.__books = []
        self.__users = []

    def load_data(self):
        if os.path.exists(self.FILE):
            try:
                with open(self.FILE, "rb") as f:
                    data = pickle.load(f)
                    self.__books = data.get("books", [])
                    self.__users = data.get("users", [])
            except (pickle.PickleError, EOFError, FileNotFoundError):
                print("Ошибка загрузки данных.")
                self.__books = []
                self.__users = []

    def save_data(self):
        data = {
            "books": self.__books,
            "users": self.__users
        }
        try:
            with open(self.FILE, "wb") as f:
                pickle.dump(data, f)
        except (pickle.PickleError, OSError):
            print("Ошибка сохранения данных.")

    def add_book(self, title, author):
        self.__books.append(Book(title, author))
        print("Книга добавлена!")

    def remove_book(self, title):
        book_to_remove = None

        for b in self.__books:
            if b.title.lower() == title.lower():
                book_to_remove = b
                break

        if not book_to_remove:
            print("Книга не найдена.")
            return

        self.__books.remove(book_to_remove)

        for user in self.__users:
            if book_to_remove in user.borrowed_books:
                user.borrowed_books.remove(book_to_remove)
        print("Книга удалена!")
    def show_books(self, only_available=False):
        print("\nСписок книг:")
        for b in self.__books:
            if only_available and not b.is_available:
                continue
            print(b)

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
        for u in self.__users:
            print(f"- {u.name}")

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
    role = input("> ")

    if role == "1":
        person = Librarian("Главный Библиотекарь")
        cmd = "-1"

        while cmd != "0":
            person.menu()
            cmd = input("> ")

            if cmd == "1":
                lib.add_book(input("Название: "), input("Автор: "))
            elif cmd == "2":
                lib.remove_book(input("Название: "))
            elif cmd == "3":
                lib.register_user(input("Имя: "))
            elif cmd == "4":
                lib.show_users()
            elif cmd == "5":
                lib.show_books()

    else:
        name = input("Введите ваше имя: ")
        user = lib.find_user(name)

        if not user:
            print("Вы не зарегистрированы.")
            return

        cmd = "-1"
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
                for b in user.borrowed_books:
                    print(f"- {b}")

    lib.save_data()
    print("Данные сохранены. До свидания!")
    
    with open("library.pkl", "rb") as f:
        data = f.read(40)
        print(data)




main()
