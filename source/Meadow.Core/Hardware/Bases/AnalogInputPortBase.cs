using System;
using System.Threading.Tasks;

namespace Meadow.Hardware
{
    /// <summary>
    /// Provides a base implementation for much of the common tasks of 
    /// implementing IAnalogInputPort
    /// </summary>
    public abstract class AnalogInputPortBase : AnalogPortBase, IAnalogInputPort
    {
        public override PortDirectionType Direction => PortDirectionType.Input;

        protected AnalogInputPortBase(IAnalogChannelInfo channelInfo)
            : base (channelInfo)
        {
        }

        public abstract Task<float> Read(int sampleCount, int sampleInterval);
        //public abstract Task<float> ReadVoltage(int sampleCount, int sampleInterval, float referenceVoltage);
    }
}
