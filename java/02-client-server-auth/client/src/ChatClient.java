import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.PrintWriter;
import java.net.Socket;

public class ChatClient {
    // Адрес сервера и порт для подключения
    private static final String SERVER_ADDRESS = "localhost";
    private static final int SERVER_PORT = 3030;

    public static void main(String[] args) {
        try (Socket socket = new Socket(SERVER_ADDRESS, SERVER_PORT)) {
            // Создаем и запускаем новый поток для обработки входящих сообщений от сервера
            new Thread(new IncomingMessageHandler(socket)).start();

            // Создаем PrintWriter для отправки сообщений на сервер
            PrintWriter out = new PrintWriter(socket.getOutputStream(), true);
            // Создаем BufferedReader для чтения ввода с консоли
            BufferedReader consoleInput = new BufferedReader(new InputStreamReader(System.in));

            String userInput;
            // Читаем ввод пользователя из консоли и отправляем его на сервер
            while ((userInput = consoleInput.readLine()) != null) {
                out.println(userInput); // Отправляем введенное сообщение на сервер
            }
        } catch (IOException e) {
            // Обработка исключений при работе с сетью
            e.printStackTrace();
        }
    }

    // Вложенный класс для обработки входящих сообщений от сервера
    public static class IncomingMessageHandler implements Runnable {
        private Socket socket; // Сокет для общения с сервером

        public IncomingMessageHandler(Socket socket) {
            this.socket = socket; // Инициализация сокета
        }

        @Override
        public void run() {
            try (BufferedReader in = new BufferedReader(new InputStreamReader(socket.getInputStream()))) {
                String message;
                // Читаем сообщения от сервера до тех пор, пока они не закончатся
                while ((message = in.readLine()) != null) {
                    System.out.println(message); // Выводим полученное сообщение на консоль
                }
            } catch (IOException e) {
                // Обработка исключений при работе с вводом-выводом
                e.printStackTrace();
            }
        }
    }
}