using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransponderReceiver;

namespace TransponderReceiver
{ 
    public class RawTransponderDataEventArgs : EventArgs
    {
        private ITransponderReceiver TransponderData;
        public RawTransponderDataEventArgs(ITransponderReceiver TransponderData)
        {
            this.TransponderData = TransponderData;
            this.TransponderData.TransponderDataReady += ReceiverOnTransponderDataReady;
        }
        //public List<string> TransponderData { get; }
    }

    private void ReceiverOnTransponderDataReady(object sender, RawTransponderDataEventArgs e)

    public interface ITransponderReceiver
    {
        event EventHandler<RawTransponderDataEventArgs> TransponderDataReady;
    }

}

class Program
{
    static void Main(string[] args)
    {
        foreach (var data in TransponderDataReady.TransponderData)
        {
            System.Console.WriteLine($"Transponderdata {data}");
        }
    }
}