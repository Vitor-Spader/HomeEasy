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
            if (!outputs.ContainsKey(pinNumber))
            {
				throw new KeyNotFoundException();
            }
			controller.Write(outputs[pinNumber].PinNumber, 
				controller.Read(outputs[pinNumber].PinNumber) == PinValue.High ? PinValue.Low : PinValue.High);
		}
		public void OnState(int pinNumber)
		{
			if (!outputs.ContainsKey(pinNumber))
			{
				throw new KeyNotFoundException();
			}
			controller.Write(outputs[pinNumber].PinNumber, PinValue.High);
		}
		public void OffState(int pinNumber)
		{
			if (!outputs.ContainsKey(pinNumber))
			{
				throw new KeyNotFoundException();
			}
			controller.Write(outputs[pinNumber].PinNumber, PinValue.Low);
		}
	}
}
