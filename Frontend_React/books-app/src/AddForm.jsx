import axios from "axios";
import { useState } from "react";
import Button from "./Button";

export default function AddForm({ onAdd }) {
	const [formData, setFormData] = useState({
		temperatureC: "",
		temperatureF: "",
		summary: "",
		id: 0,
	});

	function handleChange(e) {
		const { name, value } = e.target;
		setFormData((prevData) => ({
			...prevData,
			[name]: value,
		}));
	}

	function addTask() {
		axios
			.post("http://localhost:5181/WeatherForecast", formData)
			.then((response) => {
				onAdd(response.data);
				setFormData({
					temperatureC: "",
					temperatureF: "",
					summary: "",
				});
			})
			.catch((error) => {
				console.error("Failed to add weather forecast:", error);
			});
	}

	return (
		<form className="add">
			<label htmlFor="temperatureC">TemperatureC:</label>
			<input
				type="text"
				name="temperatureC"
				id="tempC"
				value={formData.temperatureC}
				onChange={handleChange}
			/>
			<label htmlFor="temperatureF">TemperatureF:</label>
			<input
				type="text"
				name="temperatureF"
				id="tempF"
				value={formData.temperatureF}
				onChange={handleChange}
			/>
			<label htmlFor="summary">Summary:</label>
			<textarea
				name="summary"
				id="summary"
				value={formData.summary}
				onChange={handleChange}
			/>
			<div>
				<Button button="Add" onButtonClicked={addTask} />
			</div>
		</form>
	);
}
