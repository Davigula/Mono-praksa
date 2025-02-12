import Button from "./Button";
import { useState } from "react";

export default function AddForm() {
  
  const [formData, setFormData] = useState({
    title: "",
    description: "",
  });

  function findMaxId(movies) {
    let max = 0;
    movies.forEach((p) => {
      if (p.id > max) {
        max = p.id;
      }
    });
    return max;
  }

  function handleChange(e) {
    setFormData((prevData) => ({
      ...prevData,
      [e.target.name]: e.target.value,
    }));
  }

  function addTask() {
    let movieList = JSON.parse(localStorage.getItem("movies")) || [];
    const movie = {
      title: formData.title,
      description: formData.description,
      id: findMaxId(movieList) + 1,
    };
    const updateMovies = [...movieList, movie];
   

    localStorage.setItem("movies", JSON.stringify(updateMovies));
  
    setFormData({
      title: "",
      description: "",
    });
  }

  return (
    <form className="add">
      <label for="title">Title:</label>
      <input
        type="text"
        name="title"
        id="title"
        value={formData.title}
        onChange={handleChange}
      />
      <label for="description">Description:</label>
      <textarea
        name="description"
        id="description"
        value={formData.description}
        onChange={handleChange}
      />
      <div>
        <Button button="Add" onButtonClicked={addTask} />
      </div>
    </form>
  );
}
