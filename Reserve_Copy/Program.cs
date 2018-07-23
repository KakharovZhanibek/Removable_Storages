using Recovery.LIB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reserve_Copy
{
    class Program
    {
        static void Main(string[] args)
        {
            Flash flash1 = new Flash(2000, TypeUSB.USB1);
            flash1.Name = "Kingston";
            flash1.Model = "KB12";

            ServiceStorage.AddFlash(flash1);
            ServiceStorage.AddFlash(new Flash(4000, TypeUSB.USB2) { Name = "Transend", Model = "B125" });
            ServiceStorage.AddFlash(new Flash(8000, TypeUSB.USB3) { Name = "Samsung", Model = "S13G" });
            ServiceStorage.AddFlash(new Flash(16000, TypeUSB.USB3) { Name = "Samsung", Model = "S13G4" });


            HDD hdd1 = new HDD(TypeUSB.USB1, 2, 256000);
            hdd1.Model = "Barracuda";
            hdd1.Name = "Seagate";

            HDD hdd2 = (new HDD(TypeUSB.USB2, 3, 84992) { Name = "Seagate", Model = "Iron Wolf" });
            HDD hdd3 = (new HDD(TypeUSB.USB1, 4, 256000) { Name = "Seagate", Model = "Sky Hawk" });

            ServiceStorage.AddHDD(hdd1);
            ServiceStorage.AddHDD(hdd2);
            ServiceStorage.AddHDD(hdd3);


            DVD dvd1 = new DVD(TypeDVD.SimpleSide) { Name = "Sony", Model = "DYK2" };
            DVD dvd2 = new DVD(TypeDVD.DoubleSide) { Name = "Panasonic", Model = "DG5S" };
            DVD dvd3 = new DVD(TypeDVD.DoubleSide) { Name = "Lightning", Model = "ESR9" };

            ServiceStorage.AddDVD(dvd1);
            ServiceStorage.AddDVD(dvd2);
            ServiceStorage.AddDVD(dvd3);

            while (true)
            {
                int x;
                Console.WriteLine("Выберите устройство, куда вы будете копировать.\n1: Флешка\n2: Жесткий диск\n3: DVD диск\n\n4: Выход");
                x = Int32.Parse(Console.ReadLine());
                if (x == 1)
                {
                    Console.Clear();
                    //ServiceStorage.printFlashes();
                    Console.WriteLine("Введите объем информации");
                    double t = double.Parse(Console.ReadLine());
                    ServiceStorage.GetCountDevice(TypeDevice.Flash, t);
                    Console.WriteLine("\nEnter any key");
                    Console.ReadKey();
                    Console.Clear();
                }
                if (x == 2)
                {
                    Console.Clear();
                    //ServiceStorage.printHDDs();
                    Console.WriteLine("Введите объем информации");
                    double s = double.Parse(Console.ReadLine());
                    ServiceStorage.GetCountDevice(TypeDevice.HDD, s);
                    Console.WriteLine("\nEnter any key");
                    Console.ReadKey();
                    Console.Clear();
                }
                if (x == 3)
                {
                    Console.Clear();
                    //ServiceStorage.printDVDs();
                    Console.WriteLine("Введите объем информации");
                    double s = double.Parse(Console.ReadLine());
                    ServiceStorage.GetCountDevice(TypeDevice.DVD, s);
                    Console.WriteLine("\nEnter any key");
                    Console.ReadKey();
                    Console.Clear();
                }
                if (x == 4)
                {
                    Console.Clear();
                    break;
                }
            }
        }
    }
}
