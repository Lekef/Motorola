using System;
using System.Linq; // elementAT
using System.IO; //wejscia i wysjca
using System.Diagnostics; // do stopera

namespace Hangman
{
    class Program
    {
        static void Main(string[] args)
        {
            int grac; //jesli grac TAK to gramy dalej
            int licznik = 0; // licznik dobrych liter
            DateTime date1; //start
            DateTime date2; //koniec
            Stopwatch stopWatch = new Stopwatch();
            do
            {
                stopWatch.Start();
                date1 = DateTime.Now;
                int HP = 5; // delkaracja poczatkowej ilosci prob
                bool win = false; // zmienna czy wygrano
                string kraj;
                string stolica;
                string two = File.ReadAllText(@"C:\Users\Wicu\Desktop\countries_and_capitals.txt"); // odczytywanie z pliku jednego stringa
                Random ir = new Random(); // losowanie
                int k = ir.Next(0, 366);
                string[] twos = two.Split('|', '\n'); // obcinanie jednego stringa na stolice i panstwa
                if (k % 2 == 0) // jesli parzysta wylosowana to jest to kraj a stolica jest jeden wieksza
                {
                    kraj = twos[k];
                    stolica = twos[k + 1];
                }
                else // jesli nieparzysta wylosowana to jest to stolica a kraj jest jeden mniejsza
                {
                    kraj = twos[k - 1];
                    stolica = twos[k];
                }
                string Trimstolica = stolica.Trim(); // wycinanie spacji z nazwy stolicy
                int stolicalen = Trimstolica.Length; // dlugosc nazwy stolicy bez spacji
                char[] kol = new char[stolicalen];
                string malastolicatrim = Trimstolica.ToLower();
                for (int i = 0; i < stolicalen; i++) //wypelnienie chara samymi kreskami
                {
                    kol[i] = '_';
                }
                Console.WriteLine("Pozostala liczba szans " + HP);

                int bledyCount = HP; // zmienna liczaca aktualne HP
                int bledy = 0; // zmienna liczaca ilosc bledow
                char[] bledyChars = new char[bledyCount]; // tablica na błędne litery
                bool game = true; // zmienna od ktorej zalezy czy glowna petla gry sie wykonuje
                Console.WriteLine("\nPowodzenia!\n");
                while (game) // pętla gry
                {
                    Console.WriteLine();
                    Console.WriteLine(kol);
                    Console.WriteLine();
                    Console.Write("Podane błędne litery: ");
                    Console.Write(bledyChars);
                    Console.WriteLine("Pozostało prób: {0}", bledyCount - bledy);
                    if (bledy == HP - 1)
                    {
                        Console.WriteLine("To stolica: {0}!\n", kraj);
                        Console.ReadLine();
                    }
                    Console.Write("Chcesz podać slowo czy litere? ");
                    string wybor = Console.ReadLine();
                    if (wybor == "slowo") // jesli wybrano slowo
                    {
                        Console.WriteLine("Podaj slowo: ");
                        string slowo = Console.ReadLine();
                        if (slowo == malastolicatrim) // i jest ono dobre
                        {
                            game = false;
                            Console.WriteLine("\nWygrales! Haslo to {0}!", Trimstolica);
                        }
                        else // a jesli jest zle
                        {
                            bledy += 2;
                        }
                    }
                    else // jesli wybrano cos innego niz slowo, mozna dodac else if i slowo oraz else i jesli cos inego to zapetlic w do while
                    {
                        Console.WriteLine("\nPodaj literę: ");
                        string s = Console.ReadLine();
                        char c;
                        if (s.Length > 0) // sprawdzenie czy wprowadzono jakikowiek znak
                            c = s.ElementAt(0); // bierzemy pierwsza litere/ zabezpieczenie na wpisanie wiecej liter
                        else // jesli nie wprowadzono znaku
                            continue; bool charFound = false; // zmienna czy litera znajduje sie w slowie
                        for (int i = 0; i < stolicalen; i++) // sprawdzanie czy w slowie jest podana litera
                        {
                            if (c == malastolicatrim.ElementAt(i)) // jesli jest
                            {
                                licznik += 1;
                                kol[i] = c;
                                charFound = true;
                            }
                        }
                        bool bledyCharFound = false; // zmienna od blednej litery
                        if (!charFound) // jesli nie ma litery
                        {
                            bledy += 1;
                            for (int i = 0; i < bledyChars.Length; i++) //czy bledna litera juz byla
                            {
                                if (bledyChars[i] == c) // jesli tak to nic nie rob
                                {
                                    bledyCharFound = true;
                                    break;
                                }
                            }
                            if (!bledyCharFound) // jesli nie
                            {
                                for (int i = 0; i < bledyChars.Length; i++)
                                {
                                    if (bledyChars[i] == 0)
                                    {
                                        bledyChars[i] = c;
                                        break;
                                    }

                                }
                            }
                        }
                        for (int i = 0; i < stolicalen; i++) // sprawdzanie czy wygrana
                        {
                            if (kol[i] == '_')
                            {
                                win = false;
                                break;
                            }
                            win = true;
                        }
                    }
                    if (bledy == HP || bledy == HP + 1) // sprawdzanie czy przegrana
                    {
                        Console.WriteLine("\nPrzegrales! Haslo to: {0}!\n", Trimstolica);
                        game = false;
                    }
                    if (win) // wygrana
                    {
                        game = false;
                        Console.WriteLine("\nWygrales! Haslo to {0}!", Trimstolica);
                    }
                }
                do
                {
                    stopWatch.Stop();
                    date2 = DateTime.Now;
                    TimeSpan ts = date2 - date1; // obliczanie czasu
                    Console.WriteLine("Odgadnięte litery: " + licznik);
                    Console.WriteLine("Czas: " + ts.TotalSeconds);
                    if (win == true)
                    {
                        Console.WriteLine("Podaj swoj nick: ");
                        string nick = Console.ReadLine();
                        File.AppendAllText(@"C:\Users\Wicu\Desktop\Tablicawynikow.txt", nick + " | " + date1 + " | " + ts + " | " + Trimstolica + "\n"); //zapis w pliku
                    }
                    Console.WriteLine("\nTablica wynikow:\n");
                    Console.WriteLine("Nick | Data | Czas | Stolica");
                    string bestscore = File.ReadAllText(@"C:\Users\Wicu\Desktop\Tablicawynikow.txt");
                    /*
                    Tu powinno być sortowanie wynikow po najlepszych a nastepnie petla wypisujaca 10 najlepszycch
                    */
                    Console.WriteLine(bestscore);
                    Console.WriteLine("Czy chesz zagrac jeszcze raz?");
                    Console.WriteLine("TAK/NIE");
                    string d = Console.ReadLine();
                    if (d == "TAK")
                    {
                        grac = 1;
                        stopWatch.Reset();
                        Console.Clear();
                    }
                    else if (d == "NIE")
                    {
                        grac = 0;
                    }
                    else
                    {
                        grac = 2;
                        Console.WriteLine("Nieznana komenda");
                    }

                }
                while (grac == 2);
                Console.Clear();
            } while (grac == 1);

        }
    }
}