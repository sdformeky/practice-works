import java.util.Scanner;

public class Main {
    static Scanner scanner = new Scanner(System.in);
    static Store store = new Store();
    static UserManager users = new UserManager();

    public static void main(String[] args) {
        FileManager.loadProducts(store);
        FileManager.loadUsers(users);

        //админ создается чобы крута
        if (!users.exists("admin")) {
            users.addUser(new Employee("admin", "admin", "Управление"));
            FileManager.saveUsers(users);
        }

        User currentUser = authMenu();

        //полиморфизм легендарнейший
        currentUser.printMenu();

        if (currentUser instanceof Employee) {
            runEmployeeSession((Employee) currentUser);
        } else if (currentUser instanceof Customer) {
            runCustomerSession((Customer) currentUser);
        }

        FileManager.saveProducts(store);
        FileManager.saveUsers(users);
        System.out.println("До свидания!");
    }

    //авторизация + рег
    private static User authMenu() {
        User currentUser = null;
        while (currentUser == null) {
            System.out.println("\n=== МАГАЗИН ===");
            System.out.println("1. Вход");
            System.out.println("2. Регистрация (только для покупателей)");
            System.out.print("Выбор: ");
            int choice = scanner.nextInt();
            scanner.nextLine();

            if (choice == 1) {
                currentUser = login();
            } else if (choice == 2) {
                register();
            }
        }
        return currentUser;
    }

    private static User login() {
        System.out.print("Логин: ");
        String login = scanner.nextLine();
        System.out.print("Пароль: ");
        String password = scanner.nextLine();

        User user = users.authenticate(login, password);
        if (user == null) {
            System.out.println("Неверный логин или пароль");
        } else {
            System.out.println("Добро пожаловать, " + user + "!");
        }
        return user;
    }

    private static void register() {
        System.out.print("Логин: ");
        String login = scanner.nextLine();

        if (users.exists(login)) {
            System.out.println("Пользователь с таким логином уже существует");
            return;
        }

        System.out.print("Пароль: ");
        String password = scanner.nextLine();

        //рег кастомера
        users.registerCustomer(login, password);
        FileManager.saveUsers(users);
        System.out.println("Регистрация успешна! Стартовый баланс: 1000.00 руб.");
    }

    private static void runEmployeeSession(Employee employee) {
        int c = -1;
        while (c != 0) {
            System.out.print("Выбор: ");
            c = scanner.nextInt();
            scanner.nextLine();

            switch (c) {
                case 1 -> addProduct();
                case 2 -> store.showProducts();
                case 3 -> removeProduct();
                case 4 -> changePrice();
                case 5 -> addQuantity();
                case 6 -> {
                    FileManager.saveProducts(store);
                    FileManager.saveUsers(users);
                    System.out.println("Сохранено");
                }
                case 7 -> showEmployees();
                case 8 -> addEmployee();
                case 0 -> System.out.println("Выход из системы...");
                default -> System.out.println("Неверный выбор");
            }
        }
    }

    private static void addProduct() {
        System.out.print("ID: ");
        int id = scanner.nextInt(); scanner.nextLine();
        System.out.print("Название: ");
        String name = scanner.nextLine();
        System.out.print("Категория: ");
        String cat = scanner.nextLine();
        System.out.print("Цена: ");
        double price = scanner.nextDouble();
        System.out.print("Количество: ");
        int q = scanner.nextInt(); scanner.nextLine();
        store.addProduct(new Product(id, name, cat, price, q));
        System.out.println("Товар добавлен");
    }

    private static void removeProduct() {
        store.showProducts();
        System.out.print("Введите ID товара для удаления: ");
        store.removeProduct(scanner.nextInt());
        scanner.nextLine();
    }

    private static void changePrice() {
        store.showProducts();
        System.out.print("ID товара: ");
        int id = scanner.nextInt(); scanner.nextLine();
        Product p = store.findById(id);
        if (p == null) { System.out.println("Товар не найден"); return; }
        System.out.print("Новая цена: ");
        p.changePrice(scanner.nextDouble());
        scanner.nextLine();
        System.out.println("Цена изменена");
    }

    private static void addQuantity() {
        store.showProducts();
        System.out.print("ID товара: ");
        int id = scanner.nextInt(); scanner.nextLine();
        Product p = store.findById(id);
        if (p == null) { System.out.println("Товар не найден"); return; }
        System.out.print("Количество: ");
        p.addQuantity(scanner.nextInt());
        scanner.nextLine();
        System.out.println("Количество обновлено");
    }

    private static void showEmployees() {
        System.out.println("\n--- Список сотрудников ---");
        for (Employee e : users.getEmployees()) {
            System.out.println(e); //переопределение
        }
    }

    private static void addEmployee() {
        System.out.print("Логин нового сотрудника: ");
        String login = scanner.nextLine();
        if (users.exists(login)) { System.out.println("Логин занят"); return; }
        System.out.print("Пароль: ");
        String password = scanner.nextLine();
        System.out.print("Отдел: ");
        String dept = scanner.nextLine();
        users.addUser(new Employee(login, password, dept));
        FileManager.saveUsers(users);
        System.out.println("Сотрудник добавлен");
    }

    private static void runCustomerSession(Customer customer) {
        int c = -1;
        while (c != 0) {
            System.out.print("Выбор: ");
            c = scanner.nextInt();
            scanner.nextLine();

            switch (c) {
                case 1 -> store.showProducts();
                case 2 -> buyProduct(customer);
                case 3 -> topUpBalance(customer);
                case 4 -> System.out.println(customer);
                case 0 -> System.out.println("Выход из системы...");
                default -> System.out.println("Неверный выбор");
            }
        }
    }

    private static void buyProduct(Customer customer) {
        store.showProducts();
        System.out.print("ID товара: ");
        int id = scanner.nextInt(); scanner.nextLine();
        Product p = store.findById(id);
        if (p == null) { System.out.println("Товар не найден"); return; }

        System.out.print("Количество: ");
        int qty = scanner.nextInt(); scanner.nextLine();

        double total = p.getPrice() * qty;
        System.out.println("Итого к оплате: " + String.format("%.2f", total) + " руб.");
        System.out.println("Ваш баланс: " + String.format("%.2f", customer.getBalance()) + " руб.");

        if (customer.charge(total)) {
            if (p.sell(qty)) {
                System.out.println("Покупка успешна! Остаток баланса: " + String.format("%.2f", customer.getBalance()) + " руб.");
                FileManager.saveUsers(users);
                FileManager.saveProducts(store);
            } else {
                customer.addBalance(total);
            }
        }
    }

    private static void topUpBalance(Customer customer) {
        System.out.print("Сумма пополнения: ");
        double amount = scanner.nextDouble();
        scanner.nextLine();
        if (amount <= 0) { System.out.println("Сумма должна быть положительной"); return; }
        customer.addBalance(amount);
        FileManager.saveUsers(users);
        System.out.println("Баланс пополнен. Текущий баланс: " + String.format("%.2f", customer.getBalance()) + " руб.");
    }
}
