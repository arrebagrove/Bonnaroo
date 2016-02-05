using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace Bonnaroo.Collections
{

    public class lineupArtists
    {
        public static RootObjectArtist artistDataDeserializer(string response)
        {
            RootObjectArtist rartist = JsonConvert.DeserializeObject<RootObjectArtist>(response);
            return rartist;
        }
        public static string artistDataSerializer(RootObjectArtist rartist)
        {
            string response = JsonConvert.SerializeObject(rartist);
            return response;
        }
    }
    [DataContract]
    public class lineupArt
    {
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string link {get; set; }
        [DataMember]
        public string sublink { get; set; }
        [DataMember]
        public string artistPhotoPath { get; set; }
    }
    [DataContract]
    public class RootObjectArtist
    {
        [DataMember]
        public List<lineupArt> art { get; set; }
    }
}
