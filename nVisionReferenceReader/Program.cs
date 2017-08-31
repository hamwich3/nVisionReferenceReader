using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace nVisionReferenceReader
{
    class Program
    {

        public static SerialPort nVision = new SerialPort("COM3", 115200, Parity.None, 8, StopBits.One);
        static bool Lock = false;

        static void Main(string[] args)
        {
            nVision.DataReceived += NVision_DataReceived;
            nVision.Open();
            nVision.NewLine = "\r\n";
            // Send zero command
            nVision.WriteLine("MOD:ZER! 1");

            for (int x = 0; x < 150; x++)
            {
                while (Lock) ;
                // Send read command
                nVision.WriteLine("MOD:RD? 1");
                Lock = true;
            }
            Thread.Sleep(8000);
            nVision.Close();
        }

        private static void NVision_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string data = nVision.ReadLine();
            Console.WriteLine(data);
            Lock = false;
            // Returns "[PSI] | 00000000"
        }
    }
}
