import { useEffect, useState } from "react";
import Button from "./Button";

export default function UpdateForm({ weatherForecast, onUpdate }) {
	const [formData, setFormData] = useState({
		temperatureC: "",
		temperatureF: "",
		summary: "",
		id: 0,
	});

	useEffect(() => {
		if (weatherForecast) {
			setFormData({
				temperatureC: weatherForecast.temperatureC,
				temperatureF: weatherForecast.temperatureF,
				summary: weatherForecast.summary,
				id: weatherForecast.id,
			});
		}
	}, [weatherForecast]);

	function handleChange(e) {
		const { name, value } = e.target;
		setFormData((prev) => ({ ...prev, [name]: value }));
	}

	function handleSubmit(e) {
		e.preventDefault();
		onUpdate(formData);
	}

	if (!weatherForecast) {
		return null;
	}

	return (
		<>
			<form className="add">
				<label htmlFor="temperatureC">TemperatureC:</label>
				<input
					type="text"
					name="temperatureC"
					id="tempC"
					value={formData.temperatureC}
					onChange={handleChange}
				></input>
				<label htmlFor="temperatureF">TemperatureF:</label>
				<input
					type="text"
					name="temperatureF"
					id="tempF"
					value={formData.temperatureF}
					onChange={handleChange}
				></input>
				<label htmlFor="summary">Summary:</label>
				<textarea
					name="summary"
					id="summary"
					value={formData.summary}
					onChange={handleChange}
				></textarea>
				<div>
					<Button button={"Edit"} onButtonClicked={handleSubmit} />
				</div>
			</form>
		</>
	);
}
