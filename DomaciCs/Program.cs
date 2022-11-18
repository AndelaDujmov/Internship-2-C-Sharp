using System.Runtime.InteropServices.ComTypes;

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
    {"Andrej Kramarić", ("DF", 67)},
    {"Joško Gvarilov", ("MF", 77)},
    {"Ivan Rakitić", ("FW", 67)},
    {"Mario Pašalić", ("MF", 87)},
    {"Lovro Majer", ("DF", 54)},
    {"Dominik Livaković", ("GK", 32)},
    {"Ante Rebić", ("FW", 63)},
    {"Josip Brekalo", ("MF", 67)},
    {"Borna Sosa", ("DF", 78)},
    {"Nikola Vlašić", ("FW", 87)},
    {"Duje Ćaleta - Car", ("MF", 56)},
    {"Dejan Lovren", ("FW", 77)},
    {"Marko Livaja", ("DF", 89)},
    {"Domagoj Vida", ("MF", 78)},
    {"Mislav Oršić", ("FW", 67)},
    {"Ante Budimir", ("FW", 97)}
};

var strijelci = new Dictionary<string, (string Position, int Rating, int Goals)>(){};

//----------- odrađivanje treninga----------------------------

int ReturnValue(int rating)
{
    Random random = new Random();
    var rankingRandom =  -0.05 +  (0.05 - (-0.05)) * random.NextDouble();
    rating = (int)(rating + (rating * rankingRandom));
    return rating;
}

void Training()
{
    var updated = new Dictionary<string, (string Position, int Rating)>();
    
    Console.WriteLine($"Odabrali ste opciju {izbornik[1]}");
    
    foreach (var dict in igrači)
    {
        updated.Add(dict.Key, (dict.Value.Position, ReturnValue(dict.Value.Rating)));
    }

    Console.WriteLine("Trening odrađen!");
    foreach (var dict in updated)
    {
        Console.WriteLine($"Igrač {dict.Key} ima ranking od {dict.Value.Rating}");
    }

    igrači = updated;
    
    Console.WriteLine("Želite li povratak na glavni meni? y/n");
    var ans = Console.ReadLine();

    if (ans.Equals("y"))
        MainMenu();
    return;
}


//-----odigraj utakmicu -------

void FindTheAttackers(Dictionary<string, (string Position, int Rating)> team)
{
    foreach (var item in team)
    {
        if (item.Value.Position.Equals("FW"))
        {
            strijelci.Add(item.Key, (item.Value.Position, item.Value.Rating, 0));
        }
    }
}

Dictionary<string, (string Position, int Rating)> CreateTheTeam()

{
    var team = new Dictionary<string, (string Position, int Rating)>();
    var GK = (Name: "", Position: "", Rating: 0);

    var strijelac = 0;
    var rezultat = 0;
    var count1 = 0;
    var count2 = 0;
    var count3 = 0;
    foreach (var player in igrači)
    {
        if (team.Count == 11)
            break;

      

        if (player.Value.Rating > GK.Rating && player.Value.Position.Equals("GK"))
        {
            GK = (player.Key, player.Value.Position, player.Value.Rating);
            team.Add(GK.Name, (GK.Position, GK.Rating));
        }

        if (count1 != 4)
        {
            if (player.Value.Position.Equals("DF"))
            {
                team.Add(player.Key, (player.Value.Position, player.Value.Rating));
                count1++;
            }
        }

        if (count2 != 3)
        {
            if (player.Value.Position.Equals("MF"))
            {
                team.Add(player.Key, (player.Value.Position, player.Value.Rating));
                count2++;
            }
        }

        if (count3 != 3)
        {
            if (player.Value.Position.Equals("FW"))
            {
                team.Add(player.Key, (player.Value.Position, player.Value.Rating));
                count3++;
            }
        }

    }
    Console.WriteLine("Želite li povratak na glavni meni? y/n");
   
    
    //kreirati funkcije za trazenje scakog od ovih igraca 

    return team;
}

void PlayTheGame()
{
    var team = CreateTheTeam();

    foreach (var player in team)
    {
        Console.WriteLine($"player {player.Key} on position {player.Value.Position}");
    }

    Random random = new Random();
    int index = random.Next(strijelci.Count);
    
    FindTheAttackers(team);
    var strijelac = strijelci.ElementAt(index);
    Console.WriteLine($"player {strijelac.Key} on position {strijelac.Value.Position} did the check");

    int goals1 = random.Next(1, 10);

    int Ourgoals = random.Next(1, 10); 
    
    Console.WriteLine($"tim 1 je postigao {goals1} golova, a naš tim {Ourgoals} golova");
    
    //ako gubi nas tim, mici nasem strijelcu odredeni postotak ranka!!!!
    
}

//------------statistika---------------------
void PrintPlayersNoOrder()
{
    igrači.ToList().ForEach(player => Console.WriteLine($"Igrač {player.Key}"));
    Console.WriteLine("Želite li povratak na glavni meni? y/n");
    var ans = Console.ReadLine();

    if (ans.Equals("y"))
        MainMenu();
}

void PrintPlayersAsc()
{
    var ordered = igrači.OrderBy(x => x.Value.Rating).ToDictionary(x => x.Key, x => (x.Value.Position, x.Value.Rating));
    
    ordered.ToList().ForEach(player => Console.WriteLine($"Igrač {player.Key}  s ratingom {player.Value.Rating}"));
    Console.WriteLine("Želite li povratak na glavni meni? y/n");
    var ans = Console.ReadLine();

    if (ans.Equals("y"))
        MainMenu();
}

void PrintPlayersDesc()
{
    var descending = igrači.OrderByDescending(u => u.Value.Rating)
                                                     .ToDictionary(z => z.Key, y=>(y.Value.Position, y.Value.Rating));
    descending.ToList().ForEach(player => Console.WriteLine($"Igrač {player.Key} s ratingom {player.Value.Rating}"));
    Console.WriteLine("Želite li povratak na glavni meni? y/n");
    var ans = Console.ReadLine();

    
    if (ans.Equals("y"))
        MainMenu();
}

void PrintPlayersByName()
{
    igrači.ToList().ForEach(player => Console.WriteLine($"Igrač {player.Key} na poziciji {player.Value.Position} s ratingom {player.Value.Rating}"));
    Console.WriteLine("Želite li povratak na glavni meni? y/n");
    var ans = Console.ReadLine();

    if (ans.Equals("y"))
        MainMenu();
}

void PrintPlayersByRating()
{
    Console.Clear();
    Console.WriteLine("Odabrali ste Ispis igrača po ratingu");
    Console.WriteLine("Unesite rating igrača kojeg/ih želite ispisati!");
    int ranked = 0;
    int.TryParse(Console.ReadLine(), out ranked);

    foreach (var igrač in igrači)
    {
        if (igrač.Value.Rating.Equals(ranked))
            Console.WriteLine($"Igrač s rankom {ranked} je {igrač.Key}");
    }
    
}

void PrintPlayersByPosition()
{
    Console.Clear();
    Console.WriteLine("Odabrali ste Ispis igrača po poziciji");
    Console.WriteLine("Unesite poziciju igrača kojeg/ih želite ispisati!");
    var position = Console.ReadLine().ToUpper();

    if (!position.Equals("GK") && !position.Equals("FW") && !position.Equals("DF") && !position.Equals("MF"))
    {
        Console.WriteLine($"Nema ni jednog igrača sa pozicijom {position}");
    }

    else
    {
        foreach (var igrač in igrači)
        {
            if (igrač.Value.Position.Equals(position))
                Console.WriteLine($"Igrač s pozicijom {position} je {igrač.Key}");
        }
    }
        
    Console.WriteLine("Želite li povratak na glavni meni? y/n");
    var ans = Console.ReadLine();

    if (ans.Equals("y")) // zamjeni s tenarnim operatorom
        MainMenu();
    return;

}

void PrintPlayers()
{
    var input = 0;
    Console.Clear();
    Console.WriteLine("Dobrodošli na izbornik Ispis svih igrača, odaberite jednu od sljedećih opcija:");
    Console.WriteLine("1 - Ispis igrača bez redoslijeda");
    Console.WriteLine("2 - Ispis po ratingu uzlazno");
    Console.WriteLine("3 - Ispis po ratingu silazno");
    Console.WriteLine("4 - Ispis igrača po imenu i prezimenu");
    Console.WriteLine("5 - Ispis igrača po ratingu");
    Console.WriteLine("6 - Ispis igrača po poziciji");
    Console.WriteLine("7 - Ispis trenutnih prvih 11 igrača");
    Console.WriteLine("8 - Ispis strijelaca i koliko golova imaju");
    Console.WriteLine("9 - Ispis svih rezultata ekipe");
    Console.WriteLine("10 - Ispis rezultat svih ekipa");
    Console.WriteLine("11 - Ispis tablice grupe");
    
    int.TryParse(Console.ReadLine(), out input);

    switch (input)
    {
        case 1:
            Console.WriteLine("Izabrali ste opciju Ispis igrača bez redoslijeda");
            PrintPlayersNoOrder();
            break;
        case 2:
            Console.WriteLine("Izabrali ste opciju Ispis igrača uzlazno");
            PrintPlayersAsc();
            break;
        case 3:
            Console.WriteLine("Izabrali ste opciju Ispis igrača silazno");
            PrintPlayersDesc();
            break;
        case 4:
            Console.WriteLine("Izabrali ste opciju Ispis igrača po Imenu i prezimenu");
            PrintPlayersByName();
            break;
        case 5:
            Console.WriteLine("Izabrali ste opciju Ispis igrača po Ratingu");
            PrintPlayersByRating();
            break;
        case 6:
            Console.WriteLine("Izabrali ste opciju Ispis igrača po Poziciji");
            PrintPlayersByPosition();
            break;
        case 7:
            break;
        case 8:
            break;
        case 9:
            break;
        case 10:
            break;
    }
    Console.WriteLine("Želite li povratak na glavni meni? y/n");
    var ans = Console.ReadLine();

    if (ans.Equals("y"))
        MainMenu();
    return;
}

void Statistics()
{
    var input = 0;
    Console.WriteLine($"Dobrodošli na izbornik {izbornik[3]}, izaberite jednu od ponuđenih opcija:");
    Console.WriteLine("1 - Ispis svih igrača");
    int.TryParse(Console.ReadLine(), out input);

    if (input is 1)
        PrintPlayers();
    else
    {
        Console.WriteLine("Niste izabrali ni jednu od ponuđenih opcija"); 
        Console.WriteLine("Želite li povratak na glavni meni? y/n");
        var ans = Console.ReadLine();

        if (ans.Equals("y"))
            MainMenu();
        return;
    }
}

//----------------kontrola igrača-----------------
void EnterThePlayer()
{
    var ime = "";
    var prezime = "";
    var position = "";
    var rating = 0;
    Console.WriteLine("Unesite ime igrača!");
    ime = Console.ReadLine();
    Console.WriteLine("Unesite prezime igrača!");
    prezime = Console.ReadLine();
    var fullName = ime.Trim() + ' ' + prezime.Trim();
    Console.WriteLine("Unesite poziciju igrača: GK, DF, MF ili FW!");
    position = Console.ReadLine().ToUpper();

    if (!position.Equals("GK") && !position.Equals("DF") && !position.Equals("MF") && !position.Equals("FW"))
    {
        Console.WriteLine("Nije točno unesena pozicija, mora biti jedna od navedenih!");
        
        Console.WriteLine("Unesite poziciju igrača: GK, DF, MF ili FW!");
        position = Console.ReadLine().ToUpper();
    }

    Console.WriteLine("Unesite rating igrača od 1 do 100!");
    int.TryParse(Console.ReadLine(), out rating);

    if (rating < 1 || rating > 100)
    {
          
        Console.WriteLine("Želite li povratak na glavni meni? y/n");
        var ans = Console.ReadLine();

        if (ans.Equals("y"))
            MainMenu();
        return;       
    }

    if (igrači.Count is 26)
    
        Console.WriteLine("Previše igrača uneseno! Oslobodite prostor.");
    
    else
    {
        if (igrači.ContainsKey(fullName))
            Console.WriteLine("Ovaj igrač već postoji!");
        

        igrači.Add(fullName, (position, rating));
        
        Console.WriteLine("Želite li povratak na glavni meni? y/n");
        var ans = Console.ReadLine();

        if (ans.Equals("y"))
            MainMenu();
        return;
    }
}

void DeleteThePlayer()
{
    var newDict = new Dictionary<string, (string Position, int Rating)>();
    
    Console.WriteLine("Unesite Ime igrača kojeg želite izbrisati!");
    var name = Console.ReadLine();
    Console.WriteLine("Unesite Prezime igrača kojeg želite izbrisati!");
    var surname = Console.ReadLine();
    var fullName = name.Trim() + ' ' + surname.Trim();

    foreach (var element in igrači)
    {
        if (element.Key.Equals(fullName))
            igrači.Remove(element.Key);
    }
    
    Console.WriteLine("Želite li povratak na glavni meni? y/n");
    var ans = Console.ReadLine();

    if (ans.Equals("y"))
        MainMenu();
    
    
}

//-----uređivanje------

void EditThePlayerRank()
{
    Console.WriteLine("Odabrali ste opciju Uredi Rank igrača");
    var updated = new Dictionary<string, (string Position, int Rating)>();

    Console.WriteLine("Unesite ime igrača kojeg  biste uredili");
    var ime = Console.ReadLine();
    Console.WriteLine("Unesite prezime igrača kojeg biste uredili!");
    var prezime = Console.ReadLine();
   
    var fullname = ime.Trim() + ' ' + prezime.Trim();

    foreach (var element in igrači)
    {
        if (element.Key.Equals(fullname))
        {
            Console.WriteLine("Unesite novi rank igrača");
            var rank = 0;
            int.TryParse(Console.ReadLine(), out rank);
            if (rank >= 1 && rank <=100)
                updated.Add(element.Key, (element.Value.Position, rank));
            else
            {
                Console.WriteLine("Rank izvan predviđenih granica! Pokušati ponovo!");
                EditThePlayer();
            }
            
        }
        else
            updated.Add(element.Key, (element.Value.Position, element.Value.Rating));
        
        
    }

    igrači = updated;
    
    Console.WriteLine("Želite li povratak na glavni meni? y/n");
    var ans = Console.ReadLine();

    if (ans.Equals("y"))
        MainMenu();
}

void EditThePlayerPosition()
{
    Console.WriteLine("Odabrali ste opciju Uredi poziciju igrača (GK, DF, MF ili FW)");
    var updated = new Dictionary<string, (string Position, int Rating)>();

    Console.WriteLine("Unesite ime igrača kojeg  biste uredili");
    var ime = Console.ReadLine();
    Console.WriteLine("Unesite prezime igrača kojeg biste uredili!");
    var prezime = Console.ReadLine();

    var fullname = ime.Trim() + ' ' + prezime.Trim();

    foreach (var element in igrači)
    {
        if (element.Key.Equals(fullname))
        {
            Console.WriteLine("Unesite novu poziciju igrača");
            var pozicija = Console.ReadLine().ToUpper();
            if (pozicija.Equals("GK") || pozicija.Equals("DF") || pozicija.Equals("MF") || pozicija.Equals("FW"))
                updated.Add(element.Key, (pozicija, element.Value.Rating));
            else
            {
                Console.WriteLine("Pogrešno unesena pozicija! Pokušati ponovo!");
                EditThePlayer();
            }
            
        }
        else
            updated.Add(element.Key, (element.Value.Position, element.Value.Rating));
    }

    igrači = updated;
    
    Console.WriteLine("Želite li povratak na glavni meni? y/n");
    var ans = Console.ReadLine();

    if (ans.Equals("y"))
        MainMenu();

}

void EditThePlayerName()
{
    
    Console.WriteLine("Odabrali ste opciju Uredi ime i prezime igrača");
    var updated = new Dictionary<string, (string Position, int Rating)>();
    
    Console.WriteLine("Unesite ime igrača kojeg  biste uredili");
    var ime = Console.ReadLine();
    Console.WriteLine("Unesite prezime igrača kojeg biste uredili!");
    var prezime = Console.ReadLine();

    var fullname = ime.Trim() + ' ' + prezime.Trim();

    if (!igrači.ContainsKey(fullname))
    {
        Console.WriteLine("Nije moguće pronaći ime igrača! Pokušajte ponovo!");
        EditThePlayer();
    }

    foreach (var element in igrači)
    {
        if (element.Key.Equals(fullname))
        {
            Console.WriteLine("Unesite novo ime igrača");
            ime = Console.ReadLine();
            Console.WriteLine("Unesite novo prezime igrača!");
            prezime = Console.ReadLine();
            fullname = ime.Trim() + ' ' + prezime.Trim();
            
            updated.Add(fullname, (element.Value.Position, element.Value.Rating));
        }
        else
            updated.Add(element.Key, (element.Value.Position, element.Value.Rating));
    }

    igrači = updated;

    Console.WriteLine("Želite li povratak na glavni meni? y/n");
    var ans = Console.ReadLine();

    if (ans.Equals("y"))
        MainMenu();


}

void EditThePlayer()
{
    Console.WriteLine("Dobrodošli na izbornik Uređivanja igrača! Izaberite jedno od ponuđenih opcija:");
    Console.WriteLine("1 - Uredi ime i prezime igrača");
    Console.WriteLine("2 - Uredi poziciju igrača (GK, DF, MF ili FW)");
    Console.WriteLine("3 - Uredi rating igrača (od 1 do 100)");

    var izbor = 0;
    int.TryParse(Console.ReadLine(), out izbor);

    switch (izbor)
    {
        case 1:
            EditThePlayerName();
            break;
        case 2:
            EditThePlayerPosition();
            break;
        case 3:
            EditThePlayerRank();
            break;
        default:
            Console.WriteLine("Niste odabrali ni jedan od opcija!");
            Console.WriteLine("Želite li povratak na glavni izbornik? y/n");
            var ans = Console.ReadLine();
            if (ans.Equals('y'))
                MainMenu();
            break;
    }
}

//-----glavni izbornik-------

void MainMenu(){
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
        Console.Clear();
        Console.WriteLine($"Odabrali ste izbor {izbornik[1]}");
        Training();
        break;
    case 0:
        Console.WriteLine($"Odabrali ste izbor {izbornik[0]}");
        break;
    case 2:
        Console.Clear();
        Console.WriteLine($"Odabrali ste izbor {izbornik[2]}");
        PlayTheGame();
        break;
    case 3:
        Console.Clear();
        Console.WriteLine($"Odabrali ste izbor {izbornik[3]}");
        Statistics();
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
            EnterThePlayer();
        }
        else if (izbor4 is 2)
        {
            Console.WriteLine("Izabrali ste Brisanje igrača");
            DeleteThePlayer();
        }
        else if (izbor4 is 3)
        {
            Console.WriteLine("Izabrali ste Uređivanje igrača");
            EditThePlayer();
        }
        else
            Console.WriteLine("Niste izabrali nijednu opciju! Pokušajte ponovo.");

        break;
    default:
        Console.WriteLine("Niste odabrali ni jedan od ponuđenih izbora! Pokušajte ponovo.");
        break;
}

}

MainMenu();

