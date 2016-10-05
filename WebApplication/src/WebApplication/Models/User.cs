using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class Familie
    {
        public string kinder { get; set; }
        public string stand { get; set; }
    }

    public class Sozial
    {
        public Familie familie { get; set; }
        public string stand { get; set; }
    }

    public class Addresse
    {
        public string straße { get; set; }
        public string postleitzahl { get; set; }
        public string stadt { get; set; }
    }

    public class Person
    {
        public string vorname { get; set; }
        public Addresse addresse { get; set; }
        public string geburtsdatum { get; set; }
        public string nachname { get; set; }

    }

    public class Karriere
    {
        public string einkommen { get; set; }
        public string sektor { get; set; }
    }

    public class Kontakt
    {
        public string handynr { get; set; }
    }

    public class User
    {
        public List<string> charakter { get; set; }
        public string ID { get; set; }
        public Sozial sozial { get; set; }
        public Person Person { get; set; }
        public List<Chat> ChatMessages { get; set; }
        public Karriere karriere { get; set; }
        public String Chat_s;
        public Kontakt Kontakt;
        public Deduction Deduction;
    }

    public class Deduction
    {
        public int value;
    }

    public class Chat
    {

        public Chat()
        {
            this.created = DateTime.Now;
        }

        public Chat(String message) 
        {
            this.created = DateTime.Now;
            this.message = message;
        }

        public String message;
        public DateTime created;
    }

    public class MockUser
    {
        public User User;

        private static MockUser instance;


        private MockUser()
        {
            this.User = new User();
            buildUser();
        }

        public static MockUser Instance
        {
         get
         {
                if (instance == null)
                {
                    instance = new MockUser();
                }
                return instance;
            }
        }

        private void buildUser()
        {
            this.User.ID = "111";
            this.User.Person = new Person();
            this.User.Person.vorname = "Markus";
            this.User.Person.nachname = "Jansen";
            this.User.Person.geburtsdatum = "12.07.1960";

            this.User.Kontakt = new Kontakt();
            this.User.Kontakt.handynr = "";

            this.User.Person.addresse = new Addresse();
            this.User.Person.addresse.postleitzahl = "53429";
            this.User.Person.addresse.stadt = "DD";
            this.User.Person.addresse.straße = "Königsstraße 1";

            this.User.charakter = new List<string>(new String[] { "sparsam", "verantwortungsbewusst", "zukunfsorientiert" });
            this.User.ChatMessages = new List<Chat>( );
            this.User.ChatMessages.Add(new Chat("Hallo"));
            this.User.ChatMessages.Add(new Chat("Bitte Hilfe"));
            this.User.ChatMessages.Add(new Chat("Autoooo"));

            this.User.karriere = new Karriere();
            this.User.karriere.einkommen = "10000";
            this.User.karriere.sektor = "Internationaler Handel";

            this.User.sozial = new Sozial();
            this.User.sozial.stand = "mittelstand";

            this.User.sozial.familie = new Familie();
            this.User.sozial.familie.stand = "verheiratet";
            this.User.sozial.familie.kinder = "2";

            this.User.Chat_s = "";

            this.User.Deduction = new Deduction();
            this.User.Deduction.value = 1337;
        }
    }
}
