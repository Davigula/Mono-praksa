import axios from "axios";
import { useState } from "react";
import AddForm from "./AddForm";
import "./App.css";
import Grid from "./Grid";
import logo from "./logo.svg";
import UpdateForm from "./UpdateForm";

function App() {
	const [weatherForecasts, setWeatherForecasts] = useState([]);
	const [error, setError] = useState(null);

	function handleAdd(newWeatherForecast) {
		axios
			.post("http://localhost:5181/WeatherForecast", newWeatherForecast)
			.then((response) => {
				setWeatherForecasts((prevForecasts) => [
					...prevForecasts,
					response.data,
				]);
			})
			.catch((error) => {
				setError("Failed to add weather forecast");
			});
	}
	return (
		<div className="App">
			<header className="App-header">
				<img src={logo} className="App-logo" alt="logo" />

				<Grid />
				<AddForm onAdd={handleAdd} />
				<UpdateForm />
			</header>
		</div>
	);
}

export default App;
