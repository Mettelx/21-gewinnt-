
using System;

namespace _21_gewinnt
{
    internal class Program
    {
       
        static int iKonto = 1000;
        static Random rnd = new Random();

        static void Main(string[] args)
        {
            Spieler spieler = new();

            Console.WriteLine("========================");
            Console.WriteLine("       BLACKJACK");
            Console.WriteLine("========================");

            
            if (!SpielerEingabe(spieler))
            {
                return;
            }

            Console.WriteLine("\r\nWillkommen, " + spieler.sUser + "!");

            bool bProgrammLäuft = true;

          
            while (bProgrammLäuft)
            {
                int iAuswahl = Menue();

                if (iAuswahl == 1)
                {
                    Spielregeln();

                    Console.WriteLine("\r\n========================");
                    Console.WriteLine("[1] Zurück zum Menü");
                    Console.WriteLine("[2] Direkt zum Spiel");
                    Console.WriteLine("[0] Programm beenden");
                    Console.WriteLine("========================");

                    Console.Write("Deine Auswahl: ");
                    int iRegelWahl = Convert.ToInt32(Console.ReadLine());

                    if (iRegelWahl == 1)
                    {
                        continue;
                    }
                    else if (iRegelWahl == 2)
                    {
                        SpielStarten();
                       }
                    else if (iRegelWahl == 0)
                    {
                        bProgrammLäuft = false;
                    }
                    else
                    {
                        Console.WriteLine("Ungültige Eingabe!");
                    }
                }
                else if (iAuswahl == 2)
                {
                    SpielStarten();
                }
                else if (iAuswahl == 0)
                {
                    bProgrammLäuft = false;
                }
                else
                {
                    Console.WriteLine("Ungültige Eingabe!");
                }
            }

            Console.WriteLine("\r\nAuf Wiedersehen!");
            KontostandAnzeigen();
        }

  
        static bool SpielerEingabe(Spieler spieler)
        {
            Console.Write("Gib deinen Namen ein: ");
            spieler.sName = Console.ReadLine();

            Console.Write("Gib dein Alter an: ");
            spieler.iAlter = Convert.ToInt32(Console.ReadLine());

            if (spieler.iAlter < 18)
            {
                Console.WriteLine("Du darfst das nicht spielen!");
                return false;
            }

            Console.Write("Gib deinen Username ein: ");
            spieler.sUser = Console.ReadLine();

            if (spieler.sName == spieler.sUser)
            {
                Console.WriteLine("Gib einen anderen Username ein!");
                return false;
            }

            return true;
        }


        static int Menue()
        {
            Console.WriteLine("\r\n========== HAUPTMENÜ ==========");
            Console.WriteLine("[1] Zu den Spielregeln");
            Console.WriteLine("[2] Zum Spiel");
            Console.WriteLine("[0] Programm beenden");
            Console.WriteLine("===============================");

            Console.Write("Deine Auswahl: ");

            return Convert.ToInt32(Console.ReadLine());
        }

        static void Spielregeln()
        {
            Console.WriteLine("\r\n========== SPIELREGELN ==========");
            Console.WriteLine("1. Ziel ist es, 21 Punkte zu erreichen.");
            Console.WriteLine("2. Du spielst gegen den Dealer.");
            Console.WriteLine("3. Bildkarten zählen 10 Punkte.");
            Console.WriteLine("4. Ein Ass zählt 1 oder 11 Punkte.");
            Console.WriteLine("5. Wer über 21 Punkte kommt, verliert.");
            Console.WriteLine("=================================");
        }

        
        static void SpielStarten()
        {
            int iNeustart = 9;

            while (iNeustart == 9 && iKonto > 0)
            {
                Console.WriteLine("\r\n========== BLACKJACK ==========");

                KontostandAnzeigen();

                Console.Write("Wie viel möchtest du setzen? ");
                int iEinsatz = Convert.ToInt32(Console.ReadLine());

                if (iEinsatz <= 0 || iEinsatz > iKonto)
                {
                    Console.WriteLine("Ungültiger Einsatz!");
                    continue;
                }

                iKonto -= iEinsatz;

               
                int iKarte1 = KarteZiehen();
                int iKarte2 = KarteZiehen();

                int iPunkte = iKarte1 + iKarte2;

                Console.WriteLine("Deine erste Karte: " + iKarte1);
                Console.WriteLine("Deine zweite Karte: " + iKarte2);
                Console.WriteLine("Deine Punkte: " + iPunkte);

               
                iPunkte = SpielerZug(iPunkte);

                bool bGewonnen = false;
                bool bUnentschieden = false;

                if (iPunkte > 21)
                {
                    Console.WriteLine("\r\nBUST! Du hast verloren!");
                }
                else
                {
               
                    int iDealerPunkte = DealerZug();

                    
                    int iErgebnis = ErgebnisBerechnen(
                        iPunkte, iDealerPunkte);

                    if (iErgebnis == 1)
                    {
                        bGewonnen = true;
                    }
                    else if (iErgebnis == 0)
                    {
                        bUnentschieden = true;
                    }
                }

                
                if (bGewonnen)
                {
                    iKonto += iEinsatz * 2;
                    Console.WriteLine("Du hast " + iEinsatz + " € gewonnen!");
                }
                else if (bUnentschieden)
                {
                    iKonto += iEinsatz;
                    Console.WriteLine("Dein Einsatz wurde zurückgegeben.");
                }
                else
                {
                    Console.WriteLine("Du hast deinen Einsatz verloren!");
                }

                KontostandAnzeigen();

                if (iKonto <= 0)
                {
                    Console.WriteLine("\r\nDu hast kein Geld mehr!");
                    Console.WriteLine("GAME OVER!");
                    break;
                }

                Console.WriteLine("\r\n========================");
                Console.WriteLine("[9] Neue Runde starten");
                Console.WriteLine("[0] Zurück zum Hauptmenü");
                Console.WriteLine("========================");

                Console.Write("Deine Auswahl: ");
                iNeustart = Convert.ToInt32(Console.ReadLine());
            }

            if (iKonto <= 0)
            {
                Console.WriteLine("Dein Guthaben ist aufgebraucht!");
            }
        }

        
        static int KarteZiehen()
        {
            return rnd.Next(2, 12);
        }

       
        static int SpielerZug(int iPunkte)
        {
            int iWahl = 0;

            while (iWahl != 2 && iPunkte < 21)
            {
                Console.WriteLine("\r\n[1] Karte ziehen");
                Console.WriteLine("[2] Stehen bleiben");

                Console.Write("Deine Auswahl: ");
                iWahl = Convert.ToInt32(Console.ReadLine());

                if (iWahl == 1)
                {
                    int iNeueKarte = KarteZiehen();

                    iPunkte += iNeueKarte;

                    Console.WriteLine("Du hast eine " + iNeueKarte + " gezogen!");
                    Console.WriteLine("Deine Punkte: " + iPunkte);
                }
                else if (iWahl == 2)
                {
                    Console.WriteLine("Du bleibst bei " + iPunkte + " Punkten.");
                }
                else
                {
                    Console.WriteLine("Ungültige Eingabe!");
                }
            }

            return iPunkte;
        }

        
        static int DealerZug()
        {
            Console.WriteLine("\r\n========== DEALER ==========");

            int iDealerKarte1 = KarteZiehen();
            int iDealerKarte2 = KarteZiehen();

            int iDealerPunkte = iDealerKarte1 + iDealerKarte2;

            Console.WriteLine("Dealer erste Karte: " + iDealerKarte1);
            Console.WriteLine("Dealer zweite Karte: " + iDealerKarte2);
            Console.WriteLine("Dealer Punkte: " + iDealerPunkte);

            while (iDealerPunkte < 17)
            {
                int iNeueDealerKarte = KarteZiehen();

                iDealerPunkte += iNeueDealerKarte;

                Console.WriteLine("Dealer zieht: " + iNeueDealerKarte);
                Console.WriteLine("Dealer Punkte: " + iDealerPunkte);
            }

            return iDealerPunkte;
        }

       
        static int ErgebnisBerechnen(int iPunkte, int iDealerPunkte)
        {
            Console.WriteLine("\r\n========== ERGEBNIS ==========");

            Console.WriteLine("Deine Punkte: " + iPunkte);
            Console.WriteLine("Dealer Punkte: " + iDealerPunkte);

            if (iDealerPunkte > 21)
            {
                Console.WriteLine("Dealer hat über 21!");
                Console.WriteLine("DU HAST GEWONNEN!");
                return 1;
            }
            else if (iPunkte > iDealerPunkte)
            {
                Console.WriteLine("DU HAST GEWONNEN!");
                return 1;
            }
            else if (iPunkte < iDealerPunkte)
            {
                Console.WriteLine("DEALER HAT GEWONNEN!");
                return -1;
            }
            else
            {
                Console.WriteLine("UNENTSCHIEDEN!");
                return 0;
            }
        }

     
        static void KontostandAnzeigen()
        {
            Console.WriteLine("Dein Kontostand: " + iKonto + " €");
        }
    }
}
