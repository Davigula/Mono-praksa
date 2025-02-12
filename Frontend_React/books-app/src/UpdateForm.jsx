import Button from "./Button";
import { useState } from "react";

export default function UpdateForm() {
  const [movies, setMovies] = useState(
    JSON.parse(localStorage.getItem("movies")) || []
  );
  function removeMovie(movieToRemove) {
    const updatedMovies = movies.filter((movie) => movie !== movieToRemove);
    setMovies(updatedMovies);
    localStorage.setItem("movies", JSON.stringify(updatedMovies));
  }

  return (
    <>
      <form className="add">
        <label for="title">Title:</label>
        <input type="text" name="title" id="title"></input>
        <label for="description">Description:</label>
        <textarea name="description" id="description"></textarea>
        <div>
          <Button button={"Edit"} />
        </div>
      </form>
      
    </>
  );
}
