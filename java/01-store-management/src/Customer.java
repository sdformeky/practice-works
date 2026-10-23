// Наследник User — покупатель
public class Customer extends User {
    private double balance;

    public Customer(String login, String password, double balance) {
        super(login, password, "USER");
        this.balance = balance;
    }

    public double getBalance() { return balance; }

    public void addBalance(double amount) {
        if (amount > 0) balance += amount;
    }

    public boolean charge(double amount) {
        if (amount > balance) {
            System.out.println("Недостаточно средств. Баланс: " + balance);
            return false;
        }
        balance -= amount;
        return true;
    }

    // Переопределение #3 — toString() с балансом покупателя
    @Override
    public String toString() {
        return "Покупатель: " + getLogin() + " | Баланс: " + String.format("%.2f", balance) + " руб.";
    }

    // Реализация абстрактного метода — меню для покупателя
    @Override
    public void printMenu() {
        System.out.println("\n=== МЕНЮ ПОКУПАТЕЛЯ ===");
        System.out.println("1. Показать товары");
        System.out.println("2. Купить товар");
        System.out.println("3. Пополнить баланс");
        System.out.println("4. Мой профиль");
        System.out.println("0. Выход");
    }
}
