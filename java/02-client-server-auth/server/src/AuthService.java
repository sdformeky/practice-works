import java.io.*;
import java.util.ArrayList;
import java.util.List;

public class AuthService {

    private final String path = "users.txt";

    public boolean register(String login, String password) {

        if (userExists(login)) {
            return false;
        }

        try (BufferedWriter writer = new BufferedWriter(new FileWriter(path, true)))
        {

            writer.write(login + ":" + password);
            writer.newLine();

            return true;

        } catch (IOException e) {
            e.printStackTrace();
        }

        return false;
    }

    public boolean authenticate(String login, String password) {

        List<User> users = loadUsers();

        for (User user : users) {

            if (user.getLogin().equals(login) && user.getPassword().equals(password)) {

                return true;
            }
        }

        return false;
    }

    private boolean userExists(String login) {

        List<User> users = loadUsers();

        for (User user : users) {

            if (user.getLogin().equals(login)) {
                return true;
            }
        }

        return false;
    }

    private List<User> loadUsers() {

        List<User> users = new ArrayList<>();

        File file = new File(path);

        if (!file.exists()) {
            return users;
        }

        try (BufferedReader reader = new BufferedReader(new FileReader(path))) {

            String line;

            while ((line = reader.readLine()) != null) {

                String[] parts = line.split(":");

                if (parts.length == 2) {

                    users.add(
                            new User(parts[0], parts[1])
                    );
                }
            }

        } catch (IOException e) {
            e.printStackTrace();
        }

        return users;
    }
}