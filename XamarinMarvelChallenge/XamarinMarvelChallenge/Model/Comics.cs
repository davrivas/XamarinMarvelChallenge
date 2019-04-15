﻿using System.Collections.Generic;

namespace XamarinMarvelChallenge.Model
{
    public class Comics
    {
        public int available { get; set; }
        public string collectionURI { get; set; }
        public List<object> items { get; set; }
        public int returned { get; set; }
    }
}