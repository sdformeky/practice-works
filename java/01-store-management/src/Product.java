public class Product {
    private int id;
    private String name;
    private String category;
    private double price;
    private int quantity;

    public Product(int id, String name, String category, double price, int quantity) {
        this.id = id;
        this.name = name;
        this.category = category;
        this.price = price;
        this.quantity = quantity;
    }

    public int getId() { return id; }
    public String getName() { return name; }
    public String getCategory() { return category; }
    public double getPrice() { return price; }
    public int getQuantity() { return quantity; }

    public void changePrice(double newPrice) {
        if (newPrice < 0) {
            System.out.println("Цена не может быть отрицательной");
            return;
        }
        price = newPrice;
    }

    public void addQuantity(int amount) {
        if (amount <= 0) return;
        quantity += amount;
    }

    public boolean sell(int amount) {
        if (amount > quantity) {
            System.out.println("Недостаточно товара на складе");
            return false;
        }
        quantity -= amount;
        return true;
    }

    @Override
    public String toString() {
        return id + ". " + name + " | " + category +
                " | Цена: " + String.format("%.2f", price) +
                " руб. | Кол-во: " + quantity;
    }
}
