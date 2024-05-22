using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Firestore;

namespace haberBot
{
    [FirestoreData]
    public class News
    {
        [FirestoreProperty]
        public string newsHeader { get; set; }
        [FirestoreProperty]
        public string newsText { get; set; }
        [FirestoreProperty]
        public string newsDate { get; set; }
        [FirestoreProperty]
        public Dictionary<string,int> frequency { get; set; }
    }
}
