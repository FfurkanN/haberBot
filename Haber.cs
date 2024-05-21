using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Firestore;

namespace haberBot
{
    [FirestoreData]
    public class Haber
    {
        [FirestoreProperty]
        public string HaberBasligi { get; set; }
        [FirestoreProperty]
        public string HaberIcerigi { get; set; }
        [FirestoreProperty]
        public string Tarih { get; set; }
    }
}
