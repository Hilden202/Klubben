using System;
using System.Collections.Generic;

namespace Klubben
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Person> Club = new List<Person>();
            Random random = new Random();
            int smittadePersoner = 1; // Antal smittade från början (första personen)
            int immunaPersoner = 0;
            int tid = 0; // Starttid

            // Skapa person 0 (smittad)
            Club.Add(new Person("Person: 1"));
            Club[0].Smittad = true;

            // Skapa resten av personerna (friska)
            for (int person = 0; person < 19; person++)
            {
                Club.Add(new Person("Person: " + (person + 2)));
            }

            // Första personen går in i klubben som smittad
            Console.WriteLine(Club[0].Name + " går in i klubben som smittad!");
            Club[0].BliSmittad(tid);

            // Lista över smittsamma personer
            List<Person> smittsamma = new List<Person> { Club[0] };

            // Kör loopen tills alla är smittade OCH immuna
            while (smittadePersoner < Club.Count || immunaPersoner < Club.Count)
            {
                Console.WriteLine("\nTid: " + tid + " timmar. Tryck på en knapp för att gå vidare till nästa timme...");
                Console.ReadKey();
                Console.Clear();

                List<Person> nyaSmittsamma = new List<Person>();

                // Låt varje smittsam person smitta en annan person
                foreach (Person smittsam in smittsamma)
                {
                    List<Person> friska = Club.FindAll(p => !p.Smittad && !p.Immun);

                    if (friska.Count > 0)
                    {
                        int index = random.Next(friska.Count);
                        friska[index].BliSmittad(tid);
                        nyaSmittsamma.Add(friska[index]);
                        smittadePersoner++;
                    }
                }

                // Uppdatera smittsamma
                smittsamma.AddRange(nyaSmittsamma);

                // Kontrollera om någon blivit immun
                immunaPersoner = 0;
                foreach (Person person in Club)
                {
                    person.KontrolleraImmunitet(tid);
                    if (person.Immun)
                    {
                        immunaPersoner++;
                    }
                }

                foreach (Person person in Club)
                {
                    // Skriv ut personens namn
                    Console.Write(person.Name + ": ");

                    string status; // behöver inte stå men står nu för tydlighetens skull.

                    // Ställ in färg baserat på status
                    if (person.Immun)
                    {
                        Console.ForegroundColor = ConsoleColor.Green; // Grön för immun
                        status = "\tImmun";
                    }
                    else if (person.Smittad)
                    {
                        Console.ForegroundColor = ConsoleColor.Red; // Röd för smittad
                        status = "\tSmittad";
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White; // Vit för frisk
                        status = "\tFrisk";
                    }

                    // Skriv ut status
                    Console.WriteLine(status);

                    // Återställ färgen till standard
                    Console.ResetColor();
                }




                tid++; // Öka timmen
            }

            Console.WriteLine("Alla är nu immuna! Efter " + tid + " timmar.");
        }
    }
}
