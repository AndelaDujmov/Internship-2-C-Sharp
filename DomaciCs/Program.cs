var izbornik = new Dictionary<int, string>()
{
    {1, "Odradi trening"},
    {2, "Odigraj utakmicu"},
    {3, "Statistika"},
    {4, "Kontrola igrača"},
    {0, "Izlaz iz aplikacije"}
};

var igrači = new Dictionary<string, (string Position, int Rating)>()
    {};


Dictionary<string, (string Position, int Rating)> EnterThePlayer(Dictionary<string, (string Position, int Rating)> dictionary
)
{
    var ime = "";
    var prezime = "";
    var position = "";
    var rating = 0;
    Console.WriteLine("Unesite ime igrača!");
    ime = Console.ReadLine();
    Console.WriteLine("Unesite ime igrača!");
    prezime = Console.ReadLine();
    var fullName = ime.Trim() + "" + prezime.Trim();
    Console.WriteLine("Unesite poziciju igrača: GK, DF, MF ili FW!");
    position = Console.ReadLine();

  
    Console.WriteLine("Unesite rating igrača od 1 do 100!");
    int.TryParse(Console.ReadLine(), out rating);
    
    if (rating < 1 || rating > 100)
        return dictionary;

    if (dictionary.Count is 0)
        dictionary.Add(fullName, (position, rating));
    else
    {
        foreach (var item in dictionary)
        {
            if(!item.Key.Contains(fullName))
                dictionary.Add(fullName, (position, rating));
        }
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

