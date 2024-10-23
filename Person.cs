using System;
namespace Klubben
{
    public class Person
    {
        public string Name { get; set; }
        public bool Smittad { get; set; }
        public int SmittadNär { get; set; }
        public bool Immun { get; set; }

        public Person(string name)
        {
            Name = name;
            Smittad = false;
            SmittadNär = -1;
            Immun = false;
        }

        public void BliSmittad(int nuvarandeTid)
        {
            if (!Immun && !Smittad)
            {
                Smittad = true;
                SmittadNär = nuvarandeTid;
            }
        }

        public void KontrolleraImmunitet(int nuvarandeTid)
        {
            if (Smittad && nuvarandeTid - SmittadNär >= 5)
            {
                Smittad = false;
                Immun = true;
            }
        }
    }
}