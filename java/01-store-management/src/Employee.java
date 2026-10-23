public class Employee extends User {
    private String department;

    public Employee(String login, String password, String department) {
        super(login, password, "ADMIN");
        this.department = department;
    }

    public String getDepartment() { return department; }

    @Override
    public String toString() {
        return "Сотрудник: " + getLogin() + " | Отдел: " + department;
    }

    @Override
    public void printMenu() {
        System.out.println("\n=== МЕНЮ СОТРУДНИКА ===");
        System.out.println("1. Добавить товар");
        System.out.println("2. Показать все товары");
        System.out.println("3. Удалить товар");
        System.out.println("4. Изменить цену товара");
        System.out.println("5. Добавить количество товара");
        System.out.println("6. Сохранить изменения");
        System.out.println("7. Показать всех сотрудников");
        System.out.println("8. Добавить сотрудника");
        System.out.println("0. Выход");
    }
}
