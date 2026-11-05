const taskInput = document.getElementById('todo-input');
const addButton = document.getElementById('add');
const taskList = document.getElementById('task-list');
const counter = document.getElementById('counter');

let taskName = ""
let count =0

function addTask() {
   taskName = taskInput.value.trim();
   if (taskName === "") return;

   const li = document.createElement('li');
   li.textContent = taskName;
   li.classList.add('task-item');
   taskList.appendChild(li);
   const deleteButton = document.createElement('button');
   deleteButton.textContent = "Delete";
   deleteButton.classList.add('delete-btn');
   li.appendChild(deleteButton);

   
   count++;
   counter.textContent = `${count} tasks left`;
   taskInput.value = "";
   taskInput.focus();
}

addButton.addEventListener('click', addTask);

taskInput.addEventListener('keydown', (e) => {
    if (e.key === 'Enter') addTask();
});

taskList.addEventListener('click', (e) => {
    if (!e.target.classList.contains('delete-btn')) return;
    deleteTask(e.target.parentElement);
});

function deleteTask(li) {
    taskList.removeChild(li);
    count--;
    counter.textContent = `${count} tasks left`;
}