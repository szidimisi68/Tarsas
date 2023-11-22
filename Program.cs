using System;
using System.Threading.Channels;

namespace MyApp // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //1. feladat
            List<string> osvenyek = new List<string>();
            File.ReadAllLines("C:\\teszt\\osvenyek.txt").ToList().ForEach(x => osvenyek.Add(x));
            List<string> dobasok = new List<string>();
            File.ReadAllLines("C:\\teszt\\dobasok.txt")[0].Split(" ").ToList().ForEach(x => dobasok.Add(x));
            
            //2. feladat
            Console.WriteLine($"2. feladat\nA dobások száma: {dobasok.Count()}\nAz ösvények száma: {osvenyek.Count()}");
            
            //3. feladat
            Console.WriteLine($"\n3. feladat\nAz egyik leghosszabb a(z) {osvenyek.IndexOf(osvenyek.MaxBy(x => x.Length))+1}. ösvény, hossza: {osvenyek.MaxBy(x=> x.Length).Length}");
            
            //4. feladat
            Console.Write("\n4. feladat\nAdja meg egy ösvény sorszámát! ");
            int osvenyindex = Convert.ToInt32(Console.ReadLine())-1;
            int jatekosokSzama = 0;
            do
            {
                Console.Write("Adja meg a játékosok számát! ");
                jatekosokSzama = Convert.ToInt32(Console.ReadLine());
            }
            while (jatekosokSzama < 2 || jatekosokSzama > 5);

            //5. feladat
            Console.WriteLine("\n5. feladat");
            osvenyek[osvenyindex].ToList().GroupBy(x => x).ToList().ForEach(x => Console.WriteLine($"{x.Key}: {x.Count()} darab"));

            //6. feladat
            List<string> kulonleges = new List<string>();
            int index = 1;
            foreach (char adat in osvenyek[osvenyindex])
            {
                if (adat == 'E')
                {
                    kulonleges.Add($"{index}\tE");
                }

                else if (adat == 'V')
                {
                    kulonleges.Add($"{index}\tV");
                }
                index++;
            }
            File.WriteAllLines("kulonleges.txt", kulonleges);

            //7. feladat
            int cel = osvenyek[osvenyindex].Length;
            List<int> jatekosok = new List<int>();
            int nyertesJatekosSima = 0;
            for (int i = 0; i < jatekosokSzama; i++)
            {
                jatekosok.Add(0);
            }
            int dobas;
            int dobasIndex = 0;
            int kor = 0;
            while (nyertesJatekosSima == 0 && dobasIndex < dobasok.Count())
            {
                kor++;
                for (int jatekosIndex = 0; jatekosIndex < jatekosok.Count(); jatekosIndex++)
                {
                    dobas = Convert.ToInt32(dobasok[dobasIndex]);
                    jatekosok[jatekosIndex] += Convert.ToInt32(dobas);
                    if (jatekosok[jatekosIndex] >= cel)
                    {
                        nyertesJatekosSima = jatekosIndex+1;
                    }
                    dobasIndex++;
                }
            }

            Console.WriteLine($"\n7. feladat\nA játék a(z) {kor}.körben fejeződött be. A legtávolabb jutó(k) sorszáma: {nyertesJatekosSima}");

            //8. feladat
            for (int i = 0; i < jatekosokSzama; i++)
            {
                jatekosok[i] = 0;
            }
            dobasIndex = 0;
            bool jatek = true;
            while (jatek && dobasIndex < dobasok.Count())
            {
                for (int jatekosIndex = 0; jatekosIndex < jatekosok.Count(); jatekosIndex++)
                {
                    dobas = Convert.ToInt32(dobasok[dobasIndex]);
                    if (jatekosok[jatekosIndex] + dobas < cel)
                    {
                        if (osvenyek[osvenyindex][jatekosok[jatekosIndex]+dobas-1] == 'M')
                        {
                            jatekosok[jatekosIndex] += dobas;
                        }
                        else if (osvenyek[osvenyindex][jatekosok[jatekosIndex] + dobas-1] == 'E')
                        {
                            jatekosok[jatekosIndex] += dobas * 2;
                        }
                    }
                    else
                    {
                        jatekosok[jatekosIndex] += dobas;
                    }
                    if (jatekosok[jatekosIndex] >= cel)
                    {
                        jatek = false;
                    }
                    dobasIndex++;
                }
               
            }

            List<int> nyertesek = new List<int>();
            List<string> vesztesek = new List<string>();
            int vizsgalt = 1;
            foreach (int jatekos in jatekosok)
            {
                if (jatekos >= cel)
                {
                    nyertesek.Add(vizsgalt);
                }
                else
                {
                    vesztesek.Add($"{vizsgalt}. játékos, {jatekos}. mező");
                }
                vizsgalt++;
            }
            Console.Write("\n8. feladat\nNyertes(ek): ");
            nyertesek.ForEach(x => Console.Write($"{x} "));
            Console.WriteLine("\nA többiek pozíciója:");
            vesztesek.ForEach(x => Console.WriteLine(x));

        }
    }
}