namespace HomeEasy.RpiAPI.Model
{
	using System.Collections.Generic;
	using System.Device.Gpio;

	//This class control the gpio, inputs and outputs
	public class ControlerGpio
	{
		private readonly Dictionary<int, GpioPin> inputs;
		private readonly Dictionary<int, GpioPin> outputs;
		private GpioController controller;


		public ControlerGpio(int[] _inputs, int[] _outputs, int startRangeInput, int startRangeOutput) 
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
				throw new KeyNotFoundException();

			controller.Write(outputs[pinNumber].PinNumber, 
				controller.Read(outputs[pinNumber].PinNumber) == PinValue.High ? PinValue.Low : PinValue.High);
		}
		public void OnState(int pinNumber)
		{
			if (!outputs.ContainsKey(pinNumber))
				throw new KeyNotFoundException();

			controller.Write(outputs[pinNumber].PinNumber, PinValue.High);
		}
		public void OffState(int pinNumber)
		{
			if (!outputs.ContainsKey(pinNumber))
				throw new KeyNotFoundException();
			
			controller.Write(outputs[pinNumber].PinNumber, PinValue.Low);
		}
		public bool GetState(int pinNumber)
		{
			if (!inputs.ContainsKey(pinNumber))
				throw new KeyNotFoundException();
			//Return true if the input is active
			return controller.Read(inputs[pinNumber].PinNumber) == PinValue.High;
		}
	}
}
