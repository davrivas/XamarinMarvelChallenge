using System;

namespace XamarinMarvelChallenge.Model.App
{
    public class AppMenuItem
    {
        public string Name { get; private set; }
        public string Icon { get; private set; }
        public Type Destination { get; private set; }

        public AppMenuItem(string name, string icon, Type destination)
        {
            Name = name;
            Icon = icon;
            Destination = destination;
        }
    }
}
