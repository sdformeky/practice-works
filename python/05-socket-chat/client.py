import socket
import threading

def receive_messages(sock):
    while True:
        try:
            data = sock.recv(1024)
            if not data:
                break
            print(data.decode('utf-8'))
        except:
            break

def send_messages(sock):
    while True:
        try:
            message = input()
            sock.send(message.encode('utf-8'))
        except:
            break


client = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
client.connect(("127.0.0.1", 5001))
threading.Thread(target=receive_messages, args=(client,), daemon=True).start()
send_messages(client)
