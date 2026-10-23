import java.util.ArrayList;

public class Store {
    private ArrayList<Product> products = new ArrayList<>();

    public void addProduct(Product product) { products.add(product); }

    public void showProducts() {
        if (products.isEmpty()) {
            System.out.println("Список товаров пуст");
            return;
        }
        System.out.println("\n--- Товары в магазине ---");
        for (Product p : products) {
            System.out.println(p);
        }
    }

    public Product findById(int id) {
        for (Product p : products) {
            if (p.getId() == id) return p;
        }
        return null;
    }

    public void removeProduct(int id) {
        Product p = findById(id);
        if (p != null) {
            products.remove(p);
            System.out.println("Товар удалён");
        } else {
            System.out.println("Товар не найден");
        }
    }

    public ArrayList<Product> getProducts() { return products; }
}
