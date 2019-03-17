using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.ComponentModel;

namespace Sales_And_Inventory_MIS.Models
{
    public class Product
    {
        [Key]
        public int id { get; set; }

        public string name { get; set; }
        
        [Required(ErrorMessage = "Please Enter Value for this")]
        public int price { get; set; }


        [Required(ErrorMessage = "Please Enter Value for this")]
        public int Emp_Charges { get; set; }

        public string description { get; set; }


        [Required(ErrorMessage = "Please Enter Value for this")]
        public Boolean borderframe { get; set; }
        public int BorderFrame_quantity { get; set; }

        [Required(ErrorMessage = "Please Enter Value for this")]
        public Boolean Acrylic { get; set; }
        public int shell_quantity { get; set; }


        [Required(ErrorMessage = "Please Enter Value for this")]
        [DisplayName("Raison(Kgs)")]
        public float raison { get; set; }

        [Required(ErrorMessage = "Please Enter Value for this")]
        [DisplayName("Mat300(Kgs)")]
        public float Mat300 { get; set; }

        [Required(ErrorMessage = "Please Enter Value for this")]
        [DisplayName("Mat450(Kgs)")]
        public float Mat450 { get; set; }

        [Required(ErrorMessage = "Please Enter Value for this")]
        [DisplayName("Cobalt(mgs)")]
        public float cobalt { get; set; }

        [Required(ErrorMessage = "Please Enter Value for this")]
        [DisplayName("Mekb(grams)")]
        public float Mekb { get; set; }

        [Required(ErrorMessage = "Please Enter Value for this")]
        [DisplayName("Titanium(grams)")]
        public float Titanium { get; set; }

        [Required(ErrorMessage = "Please Enter Value for this")]
        [DisplayName("Bursh(Quantity)")]
        public float bursh { get; set; }

        //************************************

        [Required(ErrorMessage = "Please Enter Value for this")]
        [DisplayName("Jelcoat(kgs)")]
        public float JelCoat { get; set; }

        [Required(ErrorMessage = "Please Enter Value for this")]
        [DisplayName("Aerosel(grams)")]
        public float Aerosel { get; set; }

        [Required(ErrorMessage = "Please Enter Value for this")]
        [DisplayName("Brush Cleaner (grams)")]
        public float BrushCleaner { get; set; }

        [Required(ErrorMessage = "Please Enter Value for this")]
        [DisplayName("(Plastic(meters)")]
        public float plastic { get; set; }

        [Required(ErrorMessage = "Please Enter Value for this")]
        [DisplayName("Colour (mgs)")]
        public float colour { get; set; }
        
        [Required(ErrorMessage = "Please Enter Value for this")]
        [DisplayName("Plastic Paris(Kgs)")]
        public float Plasticparis { get; set; }


        [Required(ErrorMessage = "Please Enter Value for this")]
        [DisplayName("Nut(Quantity)")]
        public float nut { get; set; }

        [Required(ErrorMessage = "Please Enter Value for this")]
        [DisplayName("Bolt(Quantity)")]
        public float bolts { get; set; }


        [Required(ErrorMessage = "Please Enter Value for this")]
        [DisplayName("Plastic Bolts(Quantity)")]
        public float plastic_nut_Bolts { get; set; }

        [Required(ErrorMessage = "Please Enter Value for this")]
        [DisplayName("Khrpaichy(Quantity)")]
        public float khrpaichy { get; set; }

        [Required(ErrorMessage = "Please Enter Value for this")]
        [DisplayName("Polish(Price)")]
        public float Polish { get; set; }


        [Required(ErrorMessage = "Please Enter Value for this")]
        [DisplayName("Kapra (Quantity)")]
        public float Kapra { get; set; }


        [Required(ErrorMessage = "Please Enter Value for this")]
        [DisplayName("Bowl(Quantity)")]
        public float bowl { get; set; }

        [Required(ErrorMessage = "Please Enter Value for this")]
        [DisplayName("Cutting Cream(Price)")]
        public float cuttingcream { get; set; }


        [Required(ErrorMessage = "Please Enter Value for this")]

        [DisplayName("Dori(Quantity)")]
        public float dori { get; set; }


        [Required(ErrorMessage = "Please Enter Value for this")]
        [DisplayName("Cement(Kgs)")]
        public float cement { get; set; }



    }
}