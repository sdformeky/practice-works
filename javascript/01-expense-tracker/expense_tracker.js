let expenses = []
let idCounter = 1

function addExpense(title, amount, category) {
    if (title == "" || category == "" || amount <= 0) {
        console.log("Некорректный ввод")
        return
    }

    let expense = {
        id: idCounter,
        title: title,
        amount: amount,
        category: category
    }

    idCounter = idCounter + 1
    expenses.push(expense)
}

function printAllExpenses() {
    if (expenses.length == 0) {
        console.log("Список пуст")
        return
    }

    console.log("Все расходы:")
    for (let i = 0; i < expenses.length; i++) {
        console.log(
            "ID: " + expenses[i].id +
            " | Название: " + expenses[i].title +
            " | Сумма: " + expenses[i].amount +
            " | Категория: " + expenses[i].category
        )
    }
}

function getTotalAmount() {
    let total = 0

    for (let i = 0; i < expenses.length; i++) {
        total = total + expenses[i].amount
    }

    console.log("Общая сумма расходов: " + total)
    return total
}

function getExpensesByCategory(category) {
    let total = 0

    console.log("Категория: " + category)
    for (let i = 0; i < expenses.length; i++) {
        if (expenses[i].category == category) {
            console.log(expenses[i])
            total = total + expenses[i].amount
        }
    }

    console.log("Итого по категории: " + total)
    return total
}

function findExpenseByTitle(text) {
    for (let i = 0; i < expenses.length; i++) {
        if (expenses[i].title.indexOf(text) != -1) {
            console.log("Найдено:")
            console.log(expenses[i])
            return expenses[i]
        }
    }

    console.log("Не найдено")
    return null
}

function deleteExpenseById(id) {
    for (let i = 0; i < expenses.length; i++) {
        if (expenses[i].id == id) {
            expenses.splice(i, 1)
            console.log("Удалено")
            return
        }
    }

    console.log("ID не найден")
}

function printCategoryStats() {
    let stats = {}

    for (let i = 0; i < expenses.length; i++) {
        let cat = expenses[i].category

        if (stats[cat] == undefined) {
            stats[cat] = 0
        }

        stats[cat] = stats[cat] + expenses[i].amount
    }

    console.log("Статистика по категориям:")
    for (let key in stats) {
        console.log(key + ": " + stats[key])
    }
}


addExpense("Продукты", 1200, "Еда")
addExpense("Такси", 600, "Транспорт")
addExpense("Кофе", 300, "Еда")

printAllExpenses()
getTotalAmount()
getExpensesByCategory("Еда")
findExpenseByTitle("Так")
deleteExpenseById(1)
printAllExpenses()
printCategoryStats()