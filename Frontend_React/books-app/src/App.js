import axios from "axios";
import { useEffect, useState } from "react";
import AddForm from "./AddForm";
import "./App.css";
import Grid from "./Grid";
import logo from "./logo.svg";
import UpdateForm from "./UpdateForm";

function App() {
	const [forecasts, setForecasts] = useState([]);
	const [error, setError] = useState(null);

	async function fetchWeatherForecasts() {
		const response = await axios
			.get("http://localhost:5181/WeatherForecast")
			.then()
			.catch(() => setError("Failed to fetch weather forecasts"));
		debugger;
		setForecasts([...response.data]);
	}

	useEffect(() => {
		fetchWeatherForecasts();
	}, []);

	async function handleAdd(newWeatherForecast) {
		await axios
			.post("http://localhost:5181/WeatherForecast", newWeatherForecast)
			.then()
			.catch((error) => {
				setError("Failed to add weather forecast");
			});
		debugger;
		fetchWeatherForecasts();
	}
	if (error) return <p>{error}</p>;
	return (
		<div className="App">
			<header className="App-header">
				<img src={logo} className="App-logo" alt="logo" />

				<Grid forecasts={forecasts} onRefresh={setForecasts} />
				<AddForm onAdd={handleAdd} />
				<UpdateForm />
			</header>
		</div>
	);
}

export default App;
