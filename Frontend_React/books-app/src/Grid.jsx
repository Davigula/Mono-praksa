import Button from "./Button";
import { useState } from "react";

export default function Grid() {
  const [movies, setMovies] = useState(() => {
    return JSON.parse(localStorage.getItem("movies")) || [];
  });

  function removeMovie(id) {
    const updatedMovies = movies.filter((movie) => movie.id !== id);
    setMovies(updatedMovies);
    localStorage.setItem("movies", JSON.stringify(updatedMovies));
  }

  return (
    <table className="movie-table">
      <thead>
        <tr>
          <th>Title</th>
          <th>Description</th>
          <th>Actions</th>
        </tr>
      </thead>
      <tbody>
        {movies.map((movie) => (
          <tr key={movie.id}>
            <td>{movie.title}</td>
            <td>{movie.description}</td>
            <td>
              <Button button={"update"} />
              <Button
                button={"delete"}
                onButtonClicked={() => removeMovie(movie.id)}
              />
            </td>
          </tr>
        ))}
      </tbody>
    </table>
  );
}
