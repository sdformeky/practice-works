void main() {

  List<String> students = [
    "Анна",
    "Мария",
    "Иван",
    "Дана",
    "Олег",
    "София"
  ];

  List<String> subjects = [
    "Математика",
    "Физика",
    "Информатика",
    "История"
  ];

  Map<String, Map<String, List<int>>> grades = {

    "Анна": {
      "Математика": [5, 5, 4, 5],
      "Физика": [4, 4, 5, 4],
      "Информатика": [5, 5, 5, 4],
      "История": [3, 4, 4, 3]
    },

    "Мария": {
      "Математика": [4, 4, 5, 4],
      "Физика": [5, 5, 4, 5],
      "Информатика": [5, 4, 5, 5],
      "История": [4, 4, 4, 5]
    },

    "Иван": {
      "Математика": [3, 4, 3, 4],
      "Физика": [4, 3, 4, 3],
      "Информатика": [4, 4, 3, 4],
      "История": [3, 3, 4, 3]
    },

    "Дана": {
      "Математика": [5, 5, 5, 5],
      "Физика": [5, 4, 5, 5],
      "Информатика": [5, 5, 5, 5],
      "История": [4, 5, 4, 5]
    },

    "Олег": {
      "Математика": [2, 3, 3, 2],
      "Физика": [3, 3, 3, 3],
      "Информатика": [3, 4, 3, 3],
      "История": [3, 3, 3, 3]
    },

    "София": {
      "Математика": [4, 5, 4, 5],
      "Физика": [4, 4, 5, 4],
      "Информатика": [5, 5, 5, 5],
      "История": [5, 5, 4, 5]
    }
  };

  print("ЖУРНАЛ УСПЕВАЕМОСТИ\n");

  print("Список студентов:");
  for (int i = 0; i < students.length; i++) {
    print("${i + 1}. ${students[i]}");
  }

  print("\nСписок предметов:");
  for (var subject in subjects) {
    print(subject);
  }

  print("\nОценки студентов:");

  grades.forEach((student, subjectsMap) {

    print("\n$student");

    subjectsMap.forEach((subject, marks) {
      print("$subject: $marks (средний: ${average(marks).toStringAsFixed(2)})");
    });

  });

  print("\nСредний балл по предметам:");

  Map<String, double> subjectAvg = {};

  for (var subject in subjects) {

    List<int> allMarks = [];

    for (var student in students) {
      allMarks.addAll(grades[student]![subject]!);
    }

    double avg = average(allMarks);
    subjectAvg[subject] = avg;

    print("$subject: ${avg.toStringAsFixed(2)}");
  }

  print("\nСредний балл студентов:");

  Map<String, double> studentAvg = {};

  for (var student in students) {

    List<int> allMarks = [];

    for (var subject in subjects) {
      allMarks.addAll(grades[student]![subject]!);
    }

    double avg = average(allMarks);
    studentAvg[student] = avg;

    print("$student: ${avg.toStringAsFixed(2)}");
  }

  var bestStudent =
      studentAvg.entries.reduce((a, b) => a.value > b.value ? a : b);

  print("\nЛучший студент:");
  print("${bestStudent.key} — ${bestStudent.value.toStringAsFixed(2)}");

  var worstSubject =
      subjectAvg.entries.reduce((a, b) => a.value < b.value ? a : b);

  print("\nПредмет с наименьшим средним баллом:");
  print("${worstSubject.key} — ${worstSubject.value.toStringAsFixed(2)}");

  List<int> allMarks = [];

  grades.forEach((student, subjects) {
    subjects.forEach((subject, marks) {
      allMarks.addAll(marks);
    });
  });

  print("\nОбщий средний балл группы:");
  print(average(allMarks).toStringAsFixed(2));

  print("\nСтуденты без оценок 2:");

  for (var student in students) {

    bool hasTwo = false;

    for (var subject in subjects) {
      if (grades[student]![subject]!.contains(2)) {
        hasTwo = true;
      }
    }

    if (!hasTwo) {
      print(student);
    }
  }

  print("\nСтуденты с оценками не ниже 4:");

  for (var student in students) {

    bool good = true;

    for (var subject in subjects) {
      for (var mark in grades[student]![subject]!) {
        if (mark < 4) {
          good = false;
        }
      }
    }

    if (good) {
      print(student);
    }
  }
}

double average(List<int> marks) {

  int sum = 0;

  for (var m in marks) {
    sum += m;
  }

  return sum / marks.length;
}