namespace HomeEasy.RpiAPI.Model
{
	using Microsoft.AspNetCore.Components;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.Device.Gpio;
	using System.Diagnostics.Eventing.Reader;
	using System.Net.NetworkInformation;

	public class Node
	{
		private readonly Dictionary<int, GpioPin> inputs;
		private readonly Dictionary<int, GpioPin> outputs;
		private GpioController controller;


		public Node(int[] _inputs, int[] _outputs, int startRangeInput, int startRangeOutput) 
		{
			inputs = new Dictionary<int, GpioPin>();
			outputs = new Dictionary<int, GpioPin>();
			controller = new GpioController();

			foreach (var input in _inputs) 
			{
				inputs.Add(startRangeInput, controller.OpenPin(input, PinMode.Input));
				startRangeInput++;
			}
			foreach (var output in _outputs)
			{
				outputs.Add(startRangeOutput, controller.OpenPin(output, PinMode.Output, PinValue.Low));
				startRangeOutput++;
			}
			
		}
		public void switchState(int pinNumber)
		{
			var pinGpio = outputs[pinNumber];
			PinValue currentValue = controller.Read(pinGpio.);
			PinValue newValue = (currentValue == PinValue.High) ? PinValue.Low : PinValue.High;

			controller.Write(pin, newValue);

			Console.WriteLine($"Lamp is {(newValue == PinValue.High ? "on" : "off")}");

			using (GpioController controller = new GpioController())
			{
				int lampPin = 17; // GPIO pin number

				controller.OpenPin(lampPin, PinMode.Output);

				while (true)
				{
					Console.ReadLine(); // Wait for Enter key press
					ToggleLamp(controller, lampPin);
				}
			}

			static void ToggleLamp(GpioController controller, int pin)
			{
				PinValue currentValue = controller.Read(pin);
				PinValue newValue = (currentValue == PinValue.High) ? PinValue.Low : PinValue.High;

				controller.Write(pin, newValue);

				Console.WriteLine($"Lamp is {(newValue == PinValue.High ? "on" : "off")}");
			}
		}
	}
}
