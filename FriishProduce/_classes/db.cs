﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;

namespace FriishProduce
{
    public class Database
    {
        public string CurrentFolder(string platform) => Paths.Database + platform + "\\";

        private static JToken dbReader;
        private static List<JToken> list;

        internal string Selected;

        public Database(string platform)
        {
            bool found = false;
            list = new List<JToken>();
            dbReader = JsonConvert.DeserializeObject<JObject>(File.ReadAllText(Paths.Database + "database.json"))["database"];

            if (!string.IsNullOrWhiteSpace(platform))
            {
                foreach (JObject entry in dbReader.Children())
                    if (entry["platform"].ToString() == platform)
                    {
                        found = true;
                        list.Add(entry);
                    }

                if (!found) goto All; else { Selected = platform; return; }
            }
            else
            {
                goto All;
            }

            All:
            foreach (JObject entry in dbReader.Children())
            {
                string name = entry["platform"].ToString().ToLower();
                if (name != "msx" && name != "snes" && name != "pce") list.Add(entry);
                
                // Notes:
                // MSX WADs use a different icon with blue background
                // If using SNES/PCE, custom banner logos used for forwarders (see banner.cs) will end up stretched due to the different size of the original TPL.
                // This is embedded in the banner (.brlyt) and so would need extensive hard-coding to modify, so it is excluded.
            }
            Selected = "all";
        }

        public List<JToken> GetList()
        {
            try
            {
                return list;
            }
            catch (System.NullReferenceException)
            {
                list = new List<JToken>();
                dbReader = JsonConvert.DeserializeObject<JObject>(File.ReadAllText(Paths.Database + "database.json"))["database"];
                Selected = "all";
                var allList = new List<JToken>();
                foreach (JObject entry in dbReader.Children())
                    if (entry["platform"].ToString().ToLower() != "msx") list.Add(entry);
                return allList;
            }
        }

        /// <summary>
        /// Searches for an entry with the title parameter (game name & region) and if found, returns its upper ID, otherwise null
        /// </summary>
        public string SearchID(string title)
        {
            foreach (JObject entry in GetList())
                if (entry["title"].ToString() == title)
                    return entry["id"].ToString().ToUpper();

            return null;
        }
    }
}
