using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Recovery.LIB
{
    public class HDD : Storage
    {
        static int Hcounter = 0;
        public HDD() : this(TypeUSB.USB1, 0, 0.0) { }
        public HDD(TypeUSB TypeUSB, int KolRazd, double ObemRazd)
        {
            this.TypeUSB = TypeUSB;
            this.KolRazd = KolRazd;
            this.ObemRazd = ObemRazd;
            FreeMemory = KolRazd * ObemRazd;
            Hcounter++;
        }
        public TypeUSB TypeUSB { get; set; }
        public int KolRazd { get; set; }
        public double ObemRazd { get; set; }
        public double FreeMemory { get; set; }

        public override double GetFreeMemory()
        {
            return KolRazd * ObemRazd;
        }
        public override double GetMemory()
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
        public override string ToString()
        {
            return string.Format("Type USB: {0}\tPart count: {1}\tPart capacity: {2}\n\nFree space: {3}", TypeUSB, KolRazd, ObemRazd, FreeMemory);
        }
    }
}
