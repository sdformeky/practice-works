import json
import csv

countryjson = {'страна':'Нигерия', 'столица':'Абуджа', 'население':'230.000.000'}
with open('countryjson', 'w', encoding='utf-8') as jsonf:
    json.dump(countryjson, jsonf, indent = 2, ensure_ascii=False)

emp_w_salary = [
    {'Имя': 'Павел', 'Возраст': 30, 'Город': 'Москва', 'Должность' : 'Дев', 'Зарплата': 120000},
    {'Имя': 'Никита', 'Возраст': 25, 'Город': 'Санкт-Петербург', 'Должность' : 'Дев', 'Зарплата': 120000},
    {'Имя': 'Александр', 'Возраст': 22, 'Город': 'Казань', 'Должность' : 'Уборщик', 'Зарплата': 45000},
    {'Имя': 'Иван', 'Возраст': 23, 'Город': 'Казань', 'Должность' : 'Дев', 'Зарплата': 120000},
    {'Имя': 'Тимофей', 'Возраст': 27, 'Город': 'Москва', 'Должность' : 'Миддл дев', 'Зарплата': 200000},
]

with open('emp_with_salary.csv', 'w', encoding='utf-8', newline='') as csvf:
    writer = csv.DictWriter(csvf, fieldnames = ['Имя', 'Возраст', 'Город', 'Должность', 'Зарплата'])
    writer.writeheader()
    writer.writerows(emp_w_salary)

age_sum = {}
age_count = {}
salary_sum_by_position = {}
count_by_city = {}
with open('emp_with_salary.csv', 'r', encoding='utf-8') as csvf:
    reader = csv.DictReader(csvf)
    for row in reader:
        city = row['Город']
        age = int(row['Возраст'])
        position = row['Должность']
        salary = int(row['Зарплата'])

        age_sum[city] = age_sum.get(city, 0) + age
        age_count[city] = age_count.get(city, 0) + 1

        salary_sum_by_position[position] = (
            salary_sum_by_position.get(position, 0) + salary
        )

        count_by_city[city] = count_by_city.get(city, 0) + 1

avg_age_by_city = {
    city: round(age_sum[city] / age_count[city])
    for city in age_sum
}

stats = {
    "средний возраст по городам": avg_age_by_city,
    "сумма зарплат по должностям": salary_sum_by_position,
    "количество по городам": count_by_city
}

with open('stats.json', 'w', encoding='utf-8') as jsonf:
    json.dump(stats, jsonf, ensure_ascii=False, indent=2)

