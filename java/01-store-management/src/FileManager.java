import java.io.*;

public class FileManager {

    public static void saveProducts(Store store) {
        try (FileWriter writer = new FileWriter("products.txt")) {
            for (Product p : store.getProducts()) {
                writer.write(
                        p.getId() + "," +
                        p.getName() + "," +
                        p.getCategory() + "," +
                        p.getPrice() + "," +
                        p.getQuantity() + "\n"
                );
            }
        } catch (IOException e) {
            System.out.println("Ошибка сохранения товаров");
        }
    }

    public static void loadProducts(Store store) {
        File file = new File("products.txt");
        if (!file.exists()) return;

        try (BufferedReader reader = new BufferedReader(new FileReader(file))) {
            String line;
            while ((line = reader.readLine()) != null) {
                String[] p = line.split(",");
                store.addProduct(new Product(
                        Integer.parseInt(p[0]),
                        p[1], p[2],
                        Double.parseDouble(p[3]),
                        Integer.parseInt(p[4])
                ));
            }
        } catch (IOException e) {
            System.out.println("Ошибка чтения товаров");
        }
    }

    //Employee -> EMPLOYEE,login,password,department
    //Customer -> CUSTOMER,login,password,balance
    public static void saveUsers(UserManager um) {
        try (FileWriter writer = new FileWriter("users.txt")) {
            for (User u : um.getUsers()) {
                if (u instanceof Employee) {
                    Employee e = (Employee) u;
                    writer.write("EMPLOYEE," + e.getLogin() + "," + e.getPassword() + "," + e.getDepartment() + "\n");
                } else if (u instanceof Customer) {
                    Customer c = (Customer) u;
                    writer.write("CUSTOMER," + c.getLogin() + "," + c.getPassword() + "," + c.getBalance() + "\n");
                }
            }
        } catch (IOException e) {
            System.out.println("Ошибка сохранения пользователей");
        }
    }

    public static void loadUsers(UserManager um) {
        File file = new File("users.txt");
        if (!file.exists()) return;

        try (BufferedReader reader = new BufferedReader(new FileReader(file))) {
            String line;
            while ((line = reader.readLine()) != null) {
                String[] parts = line.split(",");
                if (parts[0].equals("EMPLOYEE")) {
                    um.addUser(new Employee(parts[1], parts[2], parts[3]));
                } else if (parts[0].equals("CUSTOMER")) {
                    um.addUser(new Customer(parts[1], parts[2], Double.parseDouble(parts[3])));
                }
            }
        } catch (IOException e) {
            System.out.println("Ошибка чтения пользователей");
        }
    }
}
