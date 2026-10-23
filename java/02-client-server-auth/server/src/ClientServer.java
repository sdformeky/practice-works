import java.io.*;
import java.net.ServerSocket;
import java.net.Socket;
import java.time.LocalDateTime;
import java.util.*;
import java.time.format.DateTimeFormatter;

public class ClientServer {

    private static final int PORT = 3030;

    private static BufferedWriter logGenWriter;
    static {
        try {
            logGenWriter = new BufferedWriter(new FileWriter("generalLog.txt", true));
        } catch (IOException e) {
            throw new RuntimeException(e);
        }
    }

    private static BufferedWriter logGen2Writer;
    static {
        try {
            logGen2Writer = new BufferedWriter(new FileWriter("general2Log.txt", true));
        } catch (IOException e) {
            throw new RuntimeException(e);
        }
    }

    private static BufferedWriter logLarpWriter;
    static {
        try {
            logLarpWriter = new BufferedWriter(new FileWriter("larpLog.txt", true));
        } catch (IOException e) {
            throw new RuntimeException(e);
        }
    }

    private static AuthService authService = new AuthService();

    private static Map<String, Set<ClientHandler>> channels = new HashMap<>();
    private static Map<String, BufferedWriter> logWriters = new HashMap<>();
    private static Map<String, BufferedReader> logReaders = new HashMap<>();

    private static Map<String, ClientHandler> clients = new HashMap<>();

    public static void main(String[] args) throws IOException {

        initializechat();
        System.out.println("Chat server started...");

        try (ServerSocket serverSocket = new ServerSocket(PORT)) {
            while (true) {
                new ClientHandler(serverSocket.accept()).start();
            }
        }
    }

    private static void initializechat() {

        channels.put("general", new HashSet<>());
        channels.put("general2", new HashSet<>());
        channels.put("larp", new HashSet<>());

        logWriters.put("general", logGenWriter);
        logWriters.put("general2", logGen2Writer);
        logWriters.put("larp", logLarpWriter);

    }

    private static class ClientHandler extends Thread {

        private String currentChannel;
        private Socket socket;
        private PrintWriter out;
        private BufferedReader in;
        private String username;

        public ClientHandler(Socket socket) {
            this.socket = socket;
        }

        public void run() {

            try {

                in = new BufferedReader(new InputStreamReader(socket.getInputStream()));
                out = new PrintWriter(socket.getOutputStream(), true);

                String message;

                out.println("1 - Вход");
                out.println("2 - Регистрация");

                String choice = in.readLine();

                if (choice.equals("1")) {

                    out.println("Логин:");
                    String login = in.readLine();

                    out.println("Пароль:");
                    String password = in.readLine();

                    if (authService.authenticate(login, password)) {

                        out.println("Успешный вход");
                        username = login;

                        synchronized (clients) {
                            clients.put(username, this);
                        }

                    } else {
                        out.println("Неверный логин или пароль");
                        socket.close();
                        return;
                    }
                }

                if (choice.equals("2")) {

                    out.println("Введите логин:");
                    String login = in.readLine();

                    out.println("Введите пароль:");
                    String password = in.readLine();

                    if (authService.register(login, password)) {

                        out.println("Регистрация успешна");
                        username = login;

                        synchronized (clients) {
                            clients.put(username, this);
                        }

                    } else {
                        out.println("Пользователь уже существует");
                        socket.close();
                        return;
                    }
                }

                out.println("Выберите канал: \n1. Основной\n2. Основной, но покруче\n3. ларп");
                String channelChoice = in.readLine();

                switch (channelChoice) {
                    case "1" -> currentChannel = "general";
                    case "2" -> currentChannel = "general2";
                    case "3" -> currentChannel = "larp";
                }

                synchronized (channels) {
                    channels.get(currentChannel).add(this);
                }

                synchronized (logReaders) {
                    loadChat(currentChannel, logReaders);
                }

                while ((message = in.readLine()) != null) {

                    if (message.startsWith("/pm ")) {
                        handleDM(message);
                        continue;
                    }

                    message = LocalDateTime.now()
                            .format(DateTimeFormatter.ofPattern("HH:mm:ss dd.MM.yyyy"))
                            + " от " + username + ": " + message;

                    System.out.println("В канале: " + currentChannel + " " + message);

                    synchronized (logWriters) {
                        writeLog(currentChannel, message);
                    }

                    broadcast(message, this);
                }

            } catch (IOException e) {
                e.printStackTrace();
            } finally {

                try {
                    socket.close();
                } catch (IOException e) {
                    e.printStackTrace();
                }


                if (username != null) {
                    synchronized (clients) {
                        clients.remove(username);
                    }
                }
            }
        }

        private void handleDM(String message) {

            String[] parts = message.split(" ", 3);

            if (parts.length < 3) {
                out.println("Формат: /pm user message");
                return;
            }

            String targetUser = parts[1];
            String text = parts[2];

            ClientHandler target;

            synchronized (clients) {
                target = clients.get(targetUser);
            }

            if (target == null) {
                out.println("Пользователь не найден");
                return;
            }

            String formatted = "[DM] " + username + ": " + text;

            target.out.println(formatted);
            this.out.println(formatted);
        }

        private void broadcast(String message, ClientHandler sender) {

            synchronized (channels) {

                for (ClientHandler client : channels.get(currentChannel)) {
                    if (client != sender) {
                        client.out.println(message);
                    }
                }
            }
        }

        private void writeLog(String currentChannel, String message) throws IOException {
            logWriters.get(currentChannel).write(message);
            logWriters.get(currentChannel).newLine();
            logWriters.get(currentChannel).flush();
        }

        private void loadChat(String currentChannel, Map<String, BufferedReader> logReaders) throws IOException {

            BufferedReader reader = new BufferedReader(
                    new FileReader(currentChannel + "Log.txt")
            );

            String line;

            while ((line = reader.readLine()) != null) {
                out.println(line);
            }

            reader.close();
        }
    }
}