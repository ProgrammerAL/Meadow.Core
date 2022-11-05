﻿using Meadow;
using Meadow.Foundation.ICs.ADC;
using Meadow.Pinouts;
using System;
using System.Threading.Tasks;

namespace Ads1015_Sample
{
    public class MeadowApp : App<MeadowForLinux<RaspberryPi>>
    {
        private Ads1x15 _adc;

        public MeadowApp()
        {
            Initialize();
            _ = TestSpeed();
            _ = TakeMeasurements();
        }

        void Initialize()
        {
            Console.WriteLine("Initialize hardware...");
            _adc = new Ads1015(
                Device.CreateI2cBus(1, new Meadow.Units.Frequency(1, Meadow.Units.Frequency.UnitType.Megahertz)),
                Ads1x15.Addresses.Default,
                Ads1x15.MeasureMode.Continuous,
                Ads1x15.ChannelSetting.A0SingleEnded,
                Ads1015.SampleRateSetting.Sps3300);

            _adc.Gain = Ads1x15.FsrGain.TwoThirds;

        }

        async Task TestSpeed()
        {
            var totalSamples = 1000;

            var start = Environment.TickCount;
            long sum = 0;

            for(var i = 0; i < totalSamples; i++)
            {
                sum += await _adc.ReadRaw();
            }

            var end = Environment.TickCount;

            var mean = sum / (double)totalSamples;
            Console.WriteLine($"{totalSamples} reads in {end - start} ticks gave a raw mean of {mean:0.00}");
        }

        async Task TakeMeasurements()
        {
            var i = 0;

            while(true)
            {
                try
                {
                    var value = await _adc.Read();
                    Console.WriteLine($"ADC Reading {++i}: {value.Volts}V");
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                await Task.Delay(5000);
            }
        }
    }
}