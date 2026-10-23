import java.util.ArrayList;

public class UserManager {
    private ArrayList<User> users = new ArrayList<>();

    public void addUser(User user) {
        users.add(user);
    }

    public User authenticate(String login, String password) {
        for (User u : users) {
            if (u.getLogin().equals(login) && u.getPassword().equals(password)) {
                return u;
            }
        }
        return null;
    }

    // Регистрация доступна только как Customer — Employee создаётся только сотрудником
    public boolean registerCustomer(String login, String password) {
        if (exists(login)) return false;
        users.add(new Customer(login, password, 1000.0)); //стартовый баланс 1000 руб.
        return true;
    }

    public boolean exists(String login) {
        for (User u : users) {
            if (u.getLogin().equals(login)) return true;
        }
        return false;
    }

    public ArrayList<User> getUsers() { return users; }

    // Только сотрудники
    public ArrayList<Employee> getEmployees() {
        ArrayList<Employee> result = new ArrayList<>();
        for (User u : users) {
            if (u instanceof Employee) result.add((Employee) u);
        }
        return result;
    }
}
