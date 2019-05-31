﻿using System;
using Meadow;
using Meadow.Hardware;
using Meadow.Devices;
using System.Threading;

namespace Basic_AnalogReads
{
    public class AnalogReadApp : Appp<<F7Micro, AnalogReadApp>
    {
        IAnalogInputPort analogIn;

        public AnalogReadApp()
        {
            Console.WriteLine("Starting App");
            analogIn = Device.CreateAnalogInputPort(Device.Pins.A02);
            Console.WriteLine("Analog port created");
            this.StartReading();
        }

        protected void StartReading()
        {
            float voltage;
            for (int i = 0; i < 10; i++) {
                Console.WriteLine(i);
            //while (true) {
                voltage = analogIn.Read();
                Console.WriteLine("Voltage: " + voltage.ToString());
                Thread.Sleep(500);
            }
        }
    }
}