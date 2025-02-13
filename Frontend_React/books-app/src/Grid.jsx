import axios from "axios";
import { useEffect, useState } from "react";
import Button from "./Button";
import UpdateForm from "./UpdateForm";

export default function Grid({ forecasts, onRefresh }) {
	const [loading, setLoading] = useState(true);
	const [error, setError] = useState(null);
	const [editWeatherForecast, setEditWeatherForecast] = useState(null);

	useEffect(() => {
		axios
			.get("http://localhost:5181/WeatherForecast")
			.then((response) => {
				onRefresh(response.data);
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
		const weatherForecastToEdit = forecasts.find(
			(weatherForecast) => weatherForecast.id === id
		);
		setEditWeatherForecast(weatherForecastToEdit);
	}

	async function handleUpdate(updatedWeatherForecast) {
		await axios

			.put(
				`http://localhost:5181/WeatherForecast/${updatedWeatherForecast.id}`,
				updatedWeatherForecast
			)
			.then(() => {
				const updatedWeatherForecasts = forecasts.map((weatherForecast) =>
					weatherForecast.id === updatedWeatherForecast.id
						? updatedWeatherForecast
						: weatherForecast
				);
				onRefresh(updatedWeatherForecasts);
				setEditWeatherForecast(null);
			})
			.catch((error) => {
				setError("Failed to update weather forecast");
			});
	}

	async function removeWeatherForecast(id) {
		await axios

			.delete(`http://localhost:5181/WeatherForecast/${id}`)
			.then(() => {
				const updatedWeatherForecasts = forecasts.filter(
					(weatherForecast) => weatherForecast.id !== id
				);
				onRefresh(updatedWeatherForecasts);
			})
			.catch((error) => {
				setError("Failed to delete weather forecast");
			});
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
					{forecasts.map((weatherForecast) => (
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
