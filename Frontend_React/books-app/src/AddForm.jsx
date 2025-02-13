import { useState } from "react";
import Button from "./Button";

export default function AddForm({ onAdd }) {
	const [formData, setFormData] = useState({
		temperatureC: "",
		temperatureF: "",
		summary: "",
	});

	function handleChange(e) {
		const { name, value } = e.target;
		setFormData((prevData) => ({
			...prevData,
			[name]: value,
		}));
	}

	function handleSubmit(e) {
		e.preventDefault();
		onAdd(formData);
		setFormData({
			temperatureC: "",
			temperatureF: "",
			summary: "",
		});
	}

	return (
		<form className="add" onSubmit={handleSubmit}>
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
				<Button button="Add" onButtonClicked={handleSubmit} />
			</div>
		</form>
	);
}
