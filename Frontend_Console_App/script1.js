var selectedTask = null;

function saveMovies(movies) {
  localStorage.setItem("movies", JSON.stringify(movies));
}

function loadMovies() {
  let movies = JSON.parse(localStorage.getItem("movies")) || [];
  const taskList = document.getElementById("taskList");
  taskList.innerHTML = '';
  movies.forEach((movie) => {
    const row = addRow(movie);

    taskList.appendChild(row);
  });
}

function addTask() {
    
  const movie = {
    title: document.getElementById("title").value,
    description: document.getElementById("description").value,
  };

  const movieList = JSON.parse(localStorage.getItem("movies")) || [];
  movieList.push(movie);
  saveMovies(movieList);
  loadMovies();
}

function addRow(movie) {
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
  return taskDiv;
}

function removeTask(taskDiv, movie) {
  if (confirm("Do you want to delete this task?")) {
    movies = movies.filter((m) => m !== movie);
    saveMovies();
    taskDiv.remove();
  }
}

function editTask(taskDiv, movie) {
  selectedTask = { taskDiv, movie };

  document.getElementById("title").value = movie.title;
  document.getElementById("description").value = movie.description;
}

function updateTask() {
  if (!selectedTask) {
    alert("No task selected for update!");
    return;
  }

  selectedTask.movie.title = document.getElementById("title").value;
  selectedTask.movie.description = document.getElementById("description").value;

  selectedTask.taskDiv.querySelector("h3").innerText = selectedTask.movie.title;
  selectedTask.taskDiv.querySelector("p").innerText =
    selectedTask.movie.description;

  saveMovies();

  selectedTask = null;
}

window.onload = function () {
  loadMovies();
};
