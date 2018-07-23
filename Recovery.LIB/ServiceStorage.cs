using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recovery.LIB
{
    public enum TypeDevice
    {
        Flash, DVD, HDD
    }

    public class ServiceStorage
    {
        static List<Flash> Flashes;
        static List<HDD> HDDs;
        static List<DVD> DVDs;
        static ServiceStorage()
        {
            Flashes = new List<Flash>();
            HDDs = new List<HDD>();
            DVDs = new List<DVD>();
        }
        public static void AddFlash(Flash flash)
        {
            Flashes.Add(flash);
        }
        public static void AddHDD(HDD hdd)
        {
            HDDs.Add(hdd);
        }
        public static void AddDVD(DVD dvd)
        {
            DVDs.Add(dvd);
        }

        public static void printFlashes()
        {
            if (Flashes != null)
            {
                foreach (var item in Flashes)
                {
                    Console.WriteLine(item);
                }
            }
        }
        public static void printDVDs()
        {
            if (DVDs != null)
            {
                foreach (var item in DVDs)
                {
                    Console.WriteLine(item);
                }
            }
        }
        public static void printHDDs()
        {
            if (HDDs != null)
            {
                foreach (var item in HDDs)
                {
                    Console.WriteLine(item);
                }
            }
        }

        static double totalMemory;
        public static double GetMemoryDevice()
        {
            totalMemory = Flashes.Sum(s => s.GetMemory()/*Лямбда выражение*/);
            Console.WriteLine("Объем всех носителей = {0}", totalMemory);
            return totalMemory;
        }
        public static double GetFreeMemoryDevice()
        {
            totalMemory = Flashes.Sum(s => s.GetFreeMemory()/*Лямбда выражение*/);
            Console.WriteLine("Свободный объем всех носителей = {0}", totalMemory);
            return totalMemory;
        }
        public static void GetCountDevice(TypeDevice typeDevice, double sizeData)
        {
            /*double total = 0;*/
            switch (typeDevice)
            {
                case TypeDevice.Flash:
                    {
                        int i = 1;
                        double[] total = new double[Flashes.Count]; int c = 0;
                        foreach (Flash item in Flashes)
                        {
                            double x = sizeData / item.Memory;
                            total[c] = Math.Floor(sizeData / item.Memory);
                            if (x > (int)total[c])
                                total[c]++;
                            if (total[c] == 0)
                                total[c]++;
                            Console.WriteLine("{0}. {1} ({2}) - {3}Mb \t - {4}штук",
                                i++, item.Name, item.Model, item.Memory, total[c]);
                            c++;
                        }
                        Console.WriteLine("Введите тип флешки");
                        i = Int32.Parse(Console.ReadLine());
                        GetTimeToCopy(typeDevice, i, sizeData, total);
                    }
                    break;
                case TypeDevice.DVD:
                    {
                        int i = 0;
                        double[] total = new double[DVDs.Count]; int c = 0;
                        foreach (DVD item in DVDs)
                        {
                            double y = (int)item.TypeDVD * 1024;
                            double x = sizeData / (int)item.TypeDVD;
                            total[c] = Math.Floor(sizeData / y);
                            if (x > (int)total[c])
                                total[c]++;
                            if (total[c] == 0)
                                total[c]++;
                            Console.WriteLine("{0}. {1} ({2}) - {3}Mb \t - {4}штук",
                                i++, item.Name, item.Model, y, total[c]);
                            c++;
                        }
                        Console.WriteLine("Введите тип диска");
                        i = Int32.Parse(Console.ReadLine());
                        GetTimeToCopy(typeDevice, i, sizeData, total);
                    }
                    break;
                case TypeDevice.HDD:
                    {
                        int i = 1;
                        double[] total = new double[HDDs.Count]; int c = 0;
                        foreach (HDD item in HDDs)
                        {
                            double Memory = item.KolRazd * item.ObemRazd;
                            double x = sizeData / Memory;
                            total[c] = Math.Floor(sizeData / Memory);
                            if (x > (int)total[c])
                                total[c]++;
                            if (total[c] == 0)
                                total[c]++;
                            Console.WriteLine("{0}. {1} ({2}) - {3}Mb \t - {4}штук",
                                i++, item.Name, item.Model, item.KolRazd * item.ObemRazd, total[c]);
                            c++;
                        }
                        Console.WriteLine("Введите тип жесткого диска");
                        i = Int32.Parse(Console.ReadLine());
                        GetTimeToCopy(typeDevice, i, sizeData, total);
                    }
                    break;
                default:
                    break;
            }
        }
        public static void GetTimeToCopy(TypeDevice typeDevice, int choise, double sizeData, double[] total)
        {

            switch (typeDevice)
            {
                case TypeDevice.Flash:
                    {
                        Flash chFlash = Flashes[choise - 1];

                        Console.WriteLine("/////////////////////////////////////");
                        Flashes[choise - 1].StorageInfo();
                        Console.WriteLine(Flashes[choise - 1]);
                        Console.WriteLine("\n/////////////////////////////////////");

                        Flash[] flashesD = new Flash[(int)total[choise - 1]];
                        TimeSpan d = new TimeSpan();
                        for (int i = 0; i < (int)total[choise - 1]; i++)
                        {

                            Flash temp = new Flash(chFlash.Memory, chFlash.TypeUSB);
                            flashesD[i] = temp;
                            d += flashesD[i].GetTimeToCopy(sizeData);
                            flashesD[i].CopyData(sizeData);
                            if (sizeData >= flashesD[i].Memory)
                                sizeData -= flashesD[i].Memory;

                        }
                        Console.WriteLine("На копирования ушло {0} сек.", d.Seconds);
                    }
                    break;
                case TypeDevice.DVD:
                    {
                        DVD chDVD = DVDs[choise - 1];
                        Console.WriteLine("/////////////////////////////////////");
                        DVDs[choise - 1].StorageInfo();
                        Console.WriteLine(DVDs[choise - 1]);
                        Console.WriteLine("\n/////////////////////////////////////");
                        DVD[] dvdsD = new DVD[(int)total[choise - 1]];
                        TimeSpan d = new TimeSpan();

                        for (int i = 0; i < (int)total[choise - 1]; i++)
                        {
                            DVD temp = new DVD(chDVD.TypeDVD);

                            dvdsD[i] = temp;
                            d += dvdsD[i].GetTimeToCopy(sizeData);
                            dvdsD[i].CopyData(sizeData);

                            if (sizeData >= (int)dvdsD[i].TypeDVD)
                                sizeData -= (int)dvdsD[i].TypeDVD;
                        }
                        Console.WriteLine("На копирования ушло {0} сек.", d.Seconds);
                    }
                    break;
                case TypeDevice.HDD:
                    {
                        HDD chHDD = HDDs[choise - 1];
                        Console.WriteLine("/////////////////////////////////////");
                        HDDs[choise - 1].StorageInfo();
                        Console.WriteLine(HDDs[choise - 1]);
                        Console.WriteLine("\n/////////////////////////////////////");
                        double x = chHDD.KolRazd * chHDD.ObemRazd;
                        HDD[] hddsD = new HDD[(int)total[choise - 1]];
                        TimeSpan d = new TimeSpan();

                        for (int i = 0; i < (int)total[choise - 1]; i++)
                        {
                            HDD temp = new HDD(chHDD.TypeUSB, chHDD.KolRazd, chHDD.ObemRazd);
                            hddsD[i] = temp;
                            d += hddsD[i].GetTimeToCopy(sizeData);
                            hddsD[i].CopyData(sizeData);

                            if (sizeData >= hddsD[i].KolRazd * hddsD[i].ObemRazd)
                                sizeData -= hddsD[i].KolRazd * hddsD[i].ObemRazd;
                        }
                        Console.WriteLine("На копирования ушло {0} сек.", d.Seconds);

                    }
                    break;
                default:
                    break;
            }

        }
    }
}
