using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace Sales_And_Inventory_MIS.Models
{
    public static class Switch
    {
        public static List<int> idsToSwitch;
    }
    public static class Material
    {
        public static float raison =0;
        public static float mat300 =0;
        public static float cobalt =0;
        public static float mekb =0;
        public static float titanium =0;
        public static float bursh =0;

        public static float mat450 = 0;
        public static float jelcoat = 0;
        public static float aerosel = 0;
        public static float brushcleaner = 0;
        public static float plastic = 0;
        public static float colour = 0;
        public static float plasticparis = 0;
        public static float bolt = 0;
        public static float nut = 0;
        public static float plasticbolts = 0;
        public static float kharpaichy = 0;
        public static float polish = 0;
        public static float kapra = 0;
        public static float bowl = 0;
        public static float cuttingcream = 0;
        public static float dori = 0;
        public static float cement = 0;
        
        public static void readFromFile()
        {

            if (File.Exists("D:\\dont open me\\material.bin"))
            {
                using (BinaryReader binWriter =
                new BinaryReader(File.Open("D:\\dont open me\\material.bin", FileMode.Open)))
                {
                    Material.aerosel = binWriter.ReadSingle();
                    Material.bolt = binWriter.ReadSingle();
                    Material.bowl = binWriter.ReadSingle();
                    Material.brushcleaner = binWriter.ReadSingle();
                    Material.bursh = binWriter.ReadSingle();
                    Material.cement = binWriter.ReadSingle();
                    Material.cobalt = binWriter.ReadSingle();
                    Material.colour = binWriter.ReadSingle();
                    Material.cuttingcream = binWriter.ReadSingle();
                    Material.dori = binWriter.ReadSingle();
                    Material.jelcoat = binWriter.ReadSingle();
                    Material.kapra = binWriter.ReadSingle();
                    Material.kharpaichy = binWriter.ReadSingle();
                    Material.mat300 = binWriter.ReadSingle();
                    Material.mat450 = binWriter.ReadSingle();
                    Material.mekb = binWriter.ReadSingle();
                    Material.nut = binWriter.ReadSingle();
                    Material.plastic = binWriter.ReadSingle();
                    Material.plasticbolts = binWriter.ReadSingle();
                    Material.plasticparis = binWriter.ReadSingle();
                    Material.polish = binWriter.ReadSingle();
                    Material.raison = binWriter.ReadSingle();
                    Material.titanium = binWriter.ReadSingle();

                    binWriter.Close();

                }
            }

        }

        public static void writeOnFile()
        {
            using (BinaryWriter binWriter =
    new BinaryWriter(File.Open("D:\\dont open me\\material.bin", FileMode.Create)))
            {
                binWriter.Write(aerosel);
                binWriter.Write(bolt);
                binWriter.Write(bowl);
                binWriter.Write(brushcleaner);
                binWriter.Write(bursh);
                binWriter.Write(cement);
                binWriter.Write(cobalt);
                binWriter.Write(colour);
                binWriter.Write(cuttingcream);
                binWriter.Write(dori);
                binWriter.Write(Material.jelcoat);
                binWriter.Write(Material.kapra);
                binWriter.Write(Material.kharpaichy);
                binWriter.Write(Material.mat300);
                binWriter.Write(Material.mat450);
                binWriter.Write(Material.mekb);
                binWriter.Write(Material.nut);
                binWriter.Write(Material.plastic);
                binWriter.Write(Material.plasticbolts);
                binWriter.Write(Material.plasticparis);
                binWriter.Write(Material.polish);
                binWriter.Write(Material.raison);
                binWriter.Write(Material.titanium);

                binWriter.Close();
            }
           
        }


    }
    public static class Global
    {

        public static int Money = 10000;
        
    }
}