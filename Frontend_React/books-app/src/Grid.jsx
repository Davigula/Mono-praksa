import axios from "axios";
import { useEffect, useState } from "react";
import Button from "./Button";
import UpdateForm from "./UpdateForm";

export default function Grid() {
	const [weatherForecasts, setWeatherForecasts] = useState([]);
	const [loading, setLoading] = useState(true);
	const [error, setError] = useState(null);
	const [editWeatherForecast, setEditWeatherForecast] = useState(null);

	useEffect(() => {
		axios
			.get("http://localhost:5181/WeatherForecast")
			.then((response) => {
				setWeatherForecasts(response.data);
				// localStorage.setItem("WeatherForecasts", JSON.stringify(response.data));
				setLoading(false);
			})
			.catch((error) => {
				debugger;
				setError("Failed to fetch data");
				setLoading(false);
			});
	}, []);

	if (loading) return <p>Loading ...</p>;
	if (error) return <p>{error}</p>;

	function updateWeatherForecast(id) {
		// stavi u edit formu, tako da jednom kad u edit formi stisnem gumb edit, ažurira mi se
		const weatherForecastToEdit = weatherForecasts.find(
			(weatherForecast) => weatherForecast.id === id
		);
		setEditWeatherForecast(weatherForecastToEdit);
	}

	function handleUpdate(updatedWeatherForecast) {
		const updatedWeatherForecasts = weatherForecasts.map((weatherForecast) =>
			weatherForecast.id === updatedWeatherForecast.id
				? updatedWeatherForecast
				: weatherForecast
		);
		setWeatherForecasts(updatedWeatherForecasts);
		setEditWeatherForecast(null);
	}

	function removeWeatherForecast(id) {
		const updatedWeatherForecasts = weatherForecasts.filter(
			(weatherForecast) => weatherForecast.id !== id
		);
		setWeatherForecasts(updatedWeatherForecasts);
	}

	return (
		<>
			{editWeatherForecast && (
				<UpdateForm
					weatherForecast={editWeatherForecast}
					onUpdate={handleUpdate}
				/>
			)}
			<table className="WeatherForecast-table">
				<thead>
					<tr>
						<th>TemperatureC</th>
						<th>TemperatureF</th>
						<th>Summary</th>
						<th>Actions</th>
					</tr>
				</thead>
				<tbody>
					{weatherForecasts.map((weatherForecast) => (
						<tr key={weatherForecast.id}>
							<td>{weatherForecast.temperatureC}</td>
							<td>{weatherForecast.temperatureF}</td>
							<td>{weatherForecast.summary}</td>
							<td>
								<Button
									button={"update"}
									onButtonClicked={() =>
										updateWeatherForecast(weatherForecast.id)
									}
								/>
								<Button
									button={"delete"}
									onButtonClicked={() =>
										removeWeatherForecast(weatherForecast.id)
									}
								/>
							</td>
						</tr>
					))}
				</tbody>
			</table>
		</>
	);
}
