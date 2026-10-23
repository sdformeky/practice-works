import 'package:flutter/material.dart';

void main() {
  runApp(const MainApp());
}

class MainApp extends StatelessWidget {
  const MainApp({super.key});

  @override
  Widget build(BuildContext context) {
    return const MaterialApp(
      home: NotesScreen(),
    );
  }
}

class NotesScreen extends StatefulWidget {
  const NotesScreen({super.key});

  @override
  State<NotesScreen> createState() => _NotesScreenState();
}

class _NotesScreenState extends State<NotesScreen> {
  List<String> notes = [];
  TextEditingController controller = TextEditingController();
  int editIndex = -1;

  void addNote() {
    String text = controller.text;
    if (text.isEmpty) {
      return;
    }
    setState(() {
      if (editIndex == -1) {
        notes.add(text);
      }
      else {
        notes[editIndex] = text;
        editIndex = -1;
      }
    });
    controller.clear();
  }

  void editNote(int index) {
    setState(() {
      editIndex = index;
      controller.text = notes[index];
    });
  }

  void deleteNote(int index) {
    setState(() {
      notes.removeAt(index);
      editIndex = -1;
      controller.clear();
    });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Заметки'),
      ),
      body: Padding(
        padding: EdgeInsets.only(left: 10, right: 10, top: 10, bottom: 10),
          child: Column(
            children: [
              TextField(
                controller: controller,
              ),
              ElevatedButton(
                  onPressed: addNote,
                  child: const Text("Сохранить")
              ),
              Expanded(
                  child: ListView.builder(
                    itemCount: notes.length,
                    itemBuilder: (context, index) {
                      return Card(
                        child: ListTile(
                          title: Text(notes[index]),
                          trailing: Row(
                            mainAxisSize: MainAxisSize.min,
                            children: [
                              IconButton(
                                  onPressed: () => editNote(index),
                                  icon: const Icon(Icons.edit)
                              ),
                              IconButton(
                                  onPressed: () => deleteNote(index),
                                  icon: const Icon(Icons.delete))
                            ],
                          ),
                        ),
                      );
                    }
                )
              )
            ],
          )
        )
    );
  }
}

