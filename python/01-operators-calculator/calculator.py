print("Выберите операцию:")
print("Арифметические операции: (+); (-); (*); (/); (%); (^); (//);")
print("Операторы сравнения: (==); (!=); (>=); (<=); (>) ; (<)")
print("Логические операции(работает только с булевыми значениями - 0 и 1): (and); (or); (not)")
print("Побитовые операции: (&); (|); (^); (>>); (<<); (~)")
print("Операторы принадлежности: (in); (not in)")
print("Операторы тождественности: (is); (is not)")
operation = input("Введите операцию: ")
a = float(input("Введите первое число: "))
b = float(input("Введите второе число: "))



if operation == "+":
    print("Ответ: " + (a + b))
elif operation == "-":
    print("Ответ: " + (a - b))
elif operation == "*":
    print("Ответ: " + (a * b))
elif operation == "/":
    if b != 0:
        print("Ответ: " + (a / b))
elif operation == "%":
    if b != 0:
        print("Ответ: " + (a % b))
elif operation == "^":
    print("Ответ: " + (a ** b))
elif operation == "//":
    if b != 0:
        print("Ответ: " + (a // b))



elif operation == "==":
    if a == b:
        print("Ответ: тождественны")
    else:
        print("Ответ: не тождественны")
elif operation == "!=":
    if a != b:
        print("Ответ: не равны")
    else:
        print("Ответ: равны")
elif operation == ">=":
    if a >= b:
        print("Ответ: первое число больше или равно второму")
    else:
        print("Ответ: первое число меньше второго")
elif operation == "<=":
    if a <= b:
        print("Ответ: первое число меньше или равно второму")
    else:
        print("Ответ: первое число больше второго")
elif operation == ">":
    if a > b:
        print("Ответ: первое число больше второго")
    else:
        print("Ответ: первое число меньше или равно второму")
elif operation == "<":
    if a < b:
        print("Ответ: первое число меньше второго")
    else:
        print("Ответ: первое число больше или равно второму")



elif operation == "and" or operation == "or" or operation == "not":
    if (a == 0 or a == 1) and (b == 0 or b == 1):
        if operation == "and":
            print("Ответ: " + str(a and b))
        elif operation == "or":
            print("Ответ: " + str(a or b))
        elif operation == "not":
            print("Ответ: " + str(not a))
    else:
        print("Ошибка: логические операции работают только с булевыми значениями - 0 и 1")



elif operation == "&":
    print("Ответ: " + (int(a) & int(b)))
elif operation == "|":
    print("Ответ: " + (int(a) | int(b)))    
elif operation == "^":
    print("Ответ: " + (int(a) ^ int(b)))
elif operation == ">>":
    print("Ответ: " + (int(a) >> int(b)))
elif operation == "<<":
    print("Ответ: " + (int(a) << int(b)))
elif operation == "~":
    print("Ответ: " + (~int(a)))



elif operation == "in":
    if str(a) in str(b):
        print("Ответ: первое число содержится во втором")
    else:
        print("Ответ: первое число не содержится во втором")
elif operation == "not in":
    if str(a) not in str(b):
        print("Ответ: первое число не содержится во втором")
    else:
        print("Ответ: первое число содержится во втором")



elif operation == "is":
    if a is b:
        print("Ответ: тождественны")
    else:
        print("Ответ: не тождественны")
elif operation == "is not":
    if a is not b:
        print("Ответ: не тождественны")
    else:
        print("Ответ: тождественны")

else:
    print("Такой операции нет")







