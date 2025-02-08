var selectedTask = null;
var movies = [];


function addTask() {
  const movie = {
    title: document.getElementById("title").value,
    description: document.getElementById("description").value,
  };

  movies.push(movie);

  const taskList = document.getElementById("taskList");
  const taskDiv = document.createElement("div");
  

  const taskTitle = document.createElement("h3");
  taskTitle.innerText = movie.title;

  const taskDescription = document.createElement("p");
  taskDescription.innerText = movie.description;

  const editButton = document.createElement("button");
  editButton.innerText = "Edit";
  editButton.onclick = function () {
    editTask(taskDiv, movie);
  };

  const deleteButton = document.createElement("button");
  deleteButton.innerText = "Delete";
  deleteButton.onclick = function () {
    removeTask(taskDiv, movie);
  };

  taskDiv.appendChild(taskTitle);
  taskDiv.appendChild(taskDescription);
  taskDiv.appendChild(editButton);
  taskDiv.appendChild(deleteButton);

  taskList.appendChild(taskDiv);
  
  document.getElementById("title").value = "";
  document.getElementById("description").value = "";
}


function removeTask(taskDiv) {
  if (confirm("Do you want to delete this task?")) {
    
    taskDiv.remove();
  }
}

function editTask(taskDiv) {
  selectedTask = taskDiv;

  document.getElementById("title").value = selectedTask.querySelector("h3").innerText;
  document.getElementById("description").value = selectedTask.querySelector("p").innerText;

}

function updateTask() {
    if (!selectedTask) {
        alert("No task selected for update!");
        return;
    }
    selectedTask.querySelector("h3").innerText = document.getElementById("title").value;
    selectedTask.querySelector("p").innerText = document.getElementById("description").value;

    selectedTask = null;
}
