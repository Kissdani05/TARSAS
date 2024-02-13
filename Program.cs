using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        string[] osvenyek = File.ReadAllLines("osvenyek.txt");
        string[] dobasok = File.ReadAllLines("dobasok.txt");

        Console.WriteLine($"Ösvények száma: {osvenyek.Length}");
        Console.WriteLine($"Dobások száma: {dobasok[0].Split(' ').Length}");

        int maxHossz = 0;
        int maxIndex = -1;
        for (int i = 0; i < osvenyek.Length; i++)
        {
            if (osvenyek[i].Length > maxHossz)
            {
                maxHossz = osvenyek[i].Length;
                maxIndex = i;
            }
        }
        Console.WriteLine($"A leghosszabb ösvény sorszáma: {maxIndex + 1}, mezőinek száma: {maxHossz}");

        Console.Write("Kérem az ösvény sorszámát: ");
        int osvenySorszam = int.Parse(Console.ReadLine());
        Console.Write("Kérem a játékosok számát (2-5 között): ");
        int jatekosokSzama = int.Parse(Console.ReadLine());

        string kivalasztottOsvény = osvenyek[osvenySorszam - 1]; 

        Dictionary<char, int> mezokTipusai = new Dictionary<char, int>();

        foreach (char mezo in kivalasztottOsvény)
        {
            if (!mezokTipusai.ContainsKey(mezo))
            {
                mezokTipusai[mezo] = 1;
            }
            else
            {
                mezokTipusai[mezo]++;
            }
        }

        Console.WriteLine("Mezők statisztikája:");
        foreach (var par in mezokTipusai)
        {
            Console.WriteLine($"{par.Key}: {par.Value} db");
        }
        using (StreamWriter sw = new StreamWriter("kulonleges.txt"))
        {
            for (int i = 0; i < kivalasztottOsvény.Length; i++)
            {
                if (kivalasztottOsvény[i] != 'M')
                {
                    sw.WriteLine($"{i + 1}\t{kivalasztottOsvény[i]}");
                }
            }
        }
        int[] jatekosokPozicioi = new int[jatekosokSzama];
        string[] dobasokTomb = dobasok[0].Split(' ');
        int korokSzama = dobasokTomb.Length / jatekosokSzama;
        int legmesszebb = 0;
        int legmesszebbJatekosIndex = -1;

        for (int k = 0; k < korokSzama; k++)
        {
            for (int j = 0; j < jatekosokSzama; j++)
            {
                int dobas = int.Parse(dobasokTomb[k * jatekosokSzama + j]);
                jatekosokPozicioi[j] += dobas;
                if (jatekosokPozicioi[j] > legmesszebb)
                {
                    legmesszebb = jatekosokPozicioi[j];
                    legmesszebbJatekosIndex = j;
                }
            }
        }

        Console.WriteLine($"Legmesszebb jutó játékos sorszáma: {legmesszebbJatekosIndex + 1}, a dobások {korokSzama}. körében");

        for (int k = 0; k < korokSzama; k++)
        {
            for (int j = 0; j < jatekosokSzama; j++)
            {
                int dobas = int.Parse(dobasokTomb[k * jatekosokSzama + j]);
                jatekosokPozicioi[j] += dobas;

            }
        }

        int nyertesIndex = 0;
        for (int j = 1; j < jatekosokSzama; j++)
        {
            if (jatekosokPozicioi[j] > jatekosokPozicioi[nyertesIndex])
            {
                nyertesIndex = j;
            }
        }

        Console.WriteLine($"A nyertes játékos sorszáma: {nyertesIndex + 1}");
        for (int j = 0; j < jatekosokSzama; j++)
        {
            if (j != nyertesIndex)
            {
                Console.WriteLine($"A játékos {j + 1} a {jatekosokPozicioi[j]} mezőn áll.");
            }
        }

    }
}
