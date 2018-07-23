using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Recovery.LIB
{

    public class DVD : Storage
    {
        static int Dcounter = 0;
        public DVD() : this(TypeDVD.SimpleSide) { }
        public DVD(TypeDVD TypeDVD)
        {
            this.TypeDVD = TypeDVD;
            this.SpeedOfRead = 1.32;
            this.SpeedOfWrite = 1.32;
            FreeMemory = (int)TypeDVD;
            Dcounter++;
        }
        public double SpeedOfRead { get; set; }
        public double SpeedOfWrite { get; set; }
        public TypeDVD TypeDVD { get; set; }
        public double FreeMemory { get; set; }

        public override bool CopyData(double memoryData)
        {
            if (FreeMemory < 0)
                FreeMemory = 0;
            Console.WriteLine("Идет копирование");
            for (int i = 0; i < GetTimeToCopy(memoryData).Seconds; i++)
            {
                Thread.Sleep(GetTimeToCopy(memoryData).Seconds);
                Thread.Sleep(1000);
                Console.Write(".");
            }
            Console.WriteLine("\nКопирование завершено успешно!\n");
            return true;
        }

        public override double GetFreeMemory()
        {
            return FreeMemory;
        }

        public override double GetMemory()
        {
            return (int)TypeDVD;
        }

        public override TimeSpan GetTimeToCopy(double memoryData)
        {
            TimeSpan ts = new TimeSpan();
            return TimeSpan.FromSeconds(memoryData / (SpeedOfRead + SpeedOfWrite));
        }
    }
}
