using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace efCore.Data
{
    public class Kurs
    {
        [Key]
        public int KursId {get;set;}
        public string? Baslik {get;set;}
        public int? OgretmenId {get;set;}
        public Ogretmen Ogretmen {get;set;} = null!;
        public ICollection<KursKayit> KursKayitlari {get;set;} = new List<KursKayit>();
    }
}