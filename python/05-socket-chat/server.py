import socket
import threading

clients = []
lock = threading.Lock()

def fake_broadcast(message, sender_socket=None):
    with lock:
        for client in clients:
            if client != sender_socket:
                try:
                    client.send(message)
                except:
                    pass

def handle_client(client_socket, client_address):
    print(f"Подключился {client_address}")

    client_socket.send("Вы подключились к чату\n".encode('utf-8'))
    fake_broadcast(f"Клиент {client_address} вошел в чат\n".encode('utf-8'), client_socket)

    while True:
        try:
            data = client_socket.recv(1024)
            if not data:
                break

            message = f"{client_address}: {data.decode('utf-8')}"
            print(message)

            fake_broadcast(message.encode('utf-8'), client_socket)

        except:
            break

    with lock:
        if client_socket in clients:
            clients.remove(client_socket)

    client_socket.close()
    print(f"Отключился {client_address}")
    fake_broadcast(f"Клиент {client_address} вышел\n".encode('utf-8'))

server = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
server.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
server.bind(("127.0.0.1", 5001))
server.listen()

print("Сервер запущен")

while True:
    client_socket, client_address = server.accept()

    with lock:
        clients.append(client_socket)
    
    thread = threading.Thread(target=handle_client, args=(client_socket, client_address), daemon=True)
    thread.start()
