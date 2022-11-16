var izbornik = new Dictionary<int, string>()
{
    {1, "Odradi trening"},
    {2, "Odigraj utakmicu"},
    {3, "Statistika"},
    {4, "Kontrola igrača"},
    {0, "Izlaz iz aplikacije"}
};

var igrači = new Dictionary<string, (string Position, int Rating)>()
{
    {"Luka Modrić", ("MF", 89)},
    {"Marcelo Brozović", ("DF", 77)},
    {"Mateo Kovačić", ("MF", 78)},
    {"Ivan Perišić", ("MF", 76)},
    {"Andrej Kramarić", ("MF", 67)},
    {"Joško Gvarilov", ("MF", 77)},
    {"Ivan Rakitić", ("FW", 67)},
    {"Mario Pašalić", ("MF", 87)},
    {"Lovro Majer", ("DF", 54)},
    {"Dominik Livaković", ("GK", 32)},
    {"Ante Rebić", ("FW", 63)},
    {"Josip Brekalo", ("MF", 67)},
    {"Borna Sosa", ("DF", 78)},
    {"Nikola Vlašić", ("FW", 87)}
    
};


int returnValue(int rating)
{
    Random random = new Random();
    var rankingRandom =  -0.05 +  (0.05 - (-0.05)) * random.NextDouble();
    rating = (int)(rating + (rating * rankingRandom));
    return rating;
}

Dictionary<string, (string Position, int Rating)> Training(Dictionary<string, (string Position, int Rating)> dictionary)
{
    foreach (var player in dictionary.Keys)
    {
        Console.WriteLine("Trening u toku...");
        //player.Rating = returnValue(player.Rating);
    }

    Console.WriteLine("Trening uspješno odrađen!");
    foreach (var player in dictionary)
    {
        Console.WriteLine($"{player.Key} ima trenutan rating od {player.Value.Rating}");
    }

    return dictionary;
}


Dictionary<string, (string Position, int Rating)> EnterThePlayer(Dictionary<string, (string Position, int Rating)> dictionary)
{
    var ime = "";
    var prezime = "";
    var position = "";
    var rating = 0;
    Console.WriteLine("Unesite ime igrača!");
    ime = Console.ReadLine();
    Console.WriteLine("Unesite ime igrača!");
    prezime = Console.ReadLine();
    var fullName = ime.Trim() + ' ' + prezime.Trim();
    Console.WriteLine("Unesite poziciju igrača: GK, DF, MF ili FW!");
    position = Console.ReadLine();


    Console.WriteLine("Unesite rating igrača od 1 do 100!");
    int.TryParse(Console.ReadLine(), out rating);

    if (rating < 1 || rating > 100)
        return dictionary;

    if (dictionary.Count is 26)
    {
        Console.WriteLine("Previše igrača uneseno! Oslobodite prostor.");
        return dictionary;
    }
    else
    {
        if (dictionary.ContainsKey(fullName))
        {
            Console.WriteLine("Ovaj igrač već postoji!");
            return dictionary;
        }

        dictionary.Add(fullName, (position, rating));
    }

    return dictionary;
}

Console.WriteLine("Dobrodošli na glavni izbornik! Izaberite jednu od navedenih opcija!");
foreach (var i in izbornik)
{
    Console.WriteLine($"{i.Key} - {i.Value}");
}

var izbor = 0;
int.TryParse(Console.ReadLine(), out izbor);

switch (izbor)
{
    case 1:
        Console.WriteLine($"Odabrali ste izbor {izbornik[1]}");
        igrači = Training(igrači);
        break;
    case 0:
        Console.WriteLine($"Odabrali ste izbor {izbornik[0]}");
        break;
    case 2: 
        Console.WriteLine($"Odabrali ste izbor {izbornik[2]}");
        break;
    case 3: 
        Console.WriteLine($"Odabrali ste izbor {izbornik[3]}");
        break;
    case 4:
        Console.Clear();
        Console.WriteLine($"Dobrodošli na izbornik {izbornik[4]}");
        Console.WriteLine("1 - Unos novog igrača");
        Console.WriteLine("2 - Brisanje igrača");
        Console.WriteLine("3 - Uređivanje igrača");
        var izbor4 = 0;
        int.TryParse(Console.ReadLine(), out izbor4);

        if (izbor4 is 1)
        {
            Console.WriteLine("Izabrali ste Unos novog igrača");
            igrači = EnterThePlayer(igrači);
        }
        else if (izbor4 is 2)
            Console.WriteLine("Izabrali ste Brisanje igrača");
        else if(izbor4 is 3)
            Console.WriteLine("Izabrali ste Uređivanje igrača");
        else
            Console.WriteLine("Niste izabrali nijednu opciju! Pokušajte ponovo.");
        break;
    default:
        Console.WriteLine("Niste odabrali ni jedan od ponuđenih izbora! Pokušajte ponovo.");
        break;
}

