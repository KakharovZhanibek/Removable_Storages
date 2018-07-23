using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Recovery.LIB
{
    public class Flash : Storage
    {
        static int Fcounter = 0;
        public Flash() : this(0, TypeUSB.USB1) { }
        public Flash(double Memory, TypeUSB TypeUSB)
        {
            this.Memory = Memory;
            this.TypeUSB = TypeUSB;
            FreeMemory = Memory;
            Fcounter++;
        }
        public TypeUSB TypeUSB { get; set; }
        public double Memory { get; set; }

        public override double GetMemory()
        {
            return Memory;
        }
        public override double GetFreeMemory()
        {
            return FreeMemory;
        }
        public override bool CopyData(double memoryData)
        {
            FreeMemory -= memoryData;
            if (FreeMemory < 0)
                FreeMemory = 0;
            //Memory += memoryData;
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

        public override TimeSpan GetTimeToCopy(double memoryData)
        {
            TimeSpan ts = new TimeSpan();
            return TimeSpan.FromSeconds(memoryData / (int)TypeUSB);
        }
        public double FreeMemory { get; set; }


        public override string ToString()
        {
            return string.Format(" Capacity: {0}\tType USB: {1}\tFree Space: {2}", Memory, TypeUSB, FreeMemory);
        }

    }
}
