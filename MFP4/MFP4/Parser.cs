using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Parser
{
    public partial class Parser
    {
        public string[] Strukturieren(string Therm)
        {
            string Formatierung = null;
            string[] Struktur = null;
            int KlammerIndex = 0;
            string msg = "";

            for (int i = 0; i < Therm.Length; i++)
            {
                var c = Therm[i];

                //Formatierung von Zeichen, welche in korrekte mathematische Form gebracht werden müssen: '.'

                if (c == '.') //Formatierung von Dezimaltrennzeichen '.' --> ','
                {
                    Formatierung += ",";
                }
                if (c == ':') //Formatierung von Divisions-Operator ':' --> '/'
                {
                    Formatierung += "/";
                }
                if (c == ' ') //Entfernen von ungewünschten Leerzeichen
                {
                    continue;
                }

                //Übernahme von Ziffern und Kommas

                if (Char.IsNumber(c) == true || c == ',')
                {
                    Formatierung += c;
                }

                //Formatierung von Klammern und Wurzel

                if (c == '(' || c == 'V') //offene Klammer
                {   
                    if (c == '(')
                    {
                        KlammerIndex++;
                    }
                    Formatierung += c;
                    Formatierung += " ";
                    
                }
                if (c == ')') //geschlossene Klammer
                {
                    Formatierung += " ";
                    Formatierung += c;
                    KlammerIndex--;
                }

                //Formatierung von Operatoren

                if (c == '-') //'-' bietet viele Variationen, wie es zu deuten ist, entweder als simpler Operator oder als Negator oder als Vorzeichen einer Zahl
                {
                    if (i == 0)
                    {
                        if (Therm[i + 1] == '(' || Therm[i + 1] == '-' || Therm[i + 1] == 'V')
                        {
                            Formatierung += "-1";
                            Formatierung += " ";
                            Formatierung += "*";
                            Formatierung += " ";
                        }
                        if (Char.IsNumber(Therm[i + 1]))
                        {
                            Formatierung += c;
                        }
                    }
                    if (i > 0)
                    {
                        if ((Therm[i + 1] == '(' || Therm[i + 1] == '-' || Therm[i + 1] == 'V') && Therm[i - 1] != ')' && Char.IsNumber(Therm[i - 1]) == false)
                        {
                            Formatierung += "-1";
                            Formatierung += " ";
                            Formatierung += "*";
                            Formatierung += " ";
                        }
                        else if (Char.IsNumber(Therm[i - 1]) == false && Char.IsNumber(Therm[i + 1]) == true && Therm[i - 1] != ')')
                        {
                            Formatierung += c;
                        }
                        else if (Therm[i - 1] == ')' && Therm[i + 1] == '(')
                        {
                            Formatierung += " ";
                            Formatierung += "-";
                            Formatierung += " ";
                        }
                        else
                        {
                            Formatierung += " ";
                            Formatierung += "-";
                            Formatierung += " ";
                        }
                    }
                }
                if (c == '+' || c == '*' || c == '/' || c == '^')
                {
                    Formatierung += " ";
                    Formatierung += c;
                    Formatierung += " ";
                }
            }
            if (KlammerIndex == 0)
            {
                Struktur = Formatierung.Split(' ');
            }
            if (KlammerIndex != 0)
            {
                if (KlammerIndex == 1)
                {
                    msg = " geschlossene Klammer fehlt!";
                }
                if (KlammerIndex > 1)
                {
                    msg = " geschlossene Klammern fehlen!";
                }
                if (KlammerIndex == -1)
                {
                    msg = " offene Klammer fehlt!";
                }
                if (KlammerIndex < -1)
                {
                    msg = " offene Klammern fehlen!";
                }
                MessageBox.Show(Math.Abs(KlammerIndex).ToString() + msg, "SYNTAXFEHLER!");
            }

            return Struktur;

        }
        public string[] Ausklammern(string[] Struktur)
        {
            bool z = false;
            int i = 0;
            int k = 0;
            string s;

            if (Struktur.Contains("("))
            {
                do
                {
                    s = Struktur[i];
                    if (s != ")")
                    {
                        i++;
                    }
                    else if (s == ")")
                    {

                        k = i;
                        bool zz = false;

                        do
                        {
                            s = Struktur[k];

                            if (s != "(")
                            {
                                k--;
                            }
                            else if (s == "(")
                            {
                                string[] Struktur1 = Struktur.Where((source, index) => index < k).ToArray();
                                string[] Struktur2 = Struktur.Where((source, index) => index > i).ToArray();
                                Struktur = Struktur.Where((source, index) => index > k && index < i).ToArray();
                                string[] TeilErgebnis = new string[] { Berechnen(Struktur).ToString() };
                                string[] neueStruktur = new string[Struktur1.Length + TeilErgebnis.Length + Struktur2.Length];
                                Array.Copy(Struktur1, neueStruktur, Struktur1.Length);
                                Array.Copy(TeilErgebnis, 0, neueStruktur, Struktur1.Length, TeilErgebnis.Length);
                                Array.Copy(Struktur2, 0, neueStruktur, Struktur1.Length + TeilErgebnis.Length, Struktur2.Length);
                                Struktur = neueStruktur;
                                zz = true;
                                z = true;
                            }
                        }
                        while (zz == false);
                    }
                }
                while (z == false);
            }
                            
            return Struktur;
        }
        public string Berechnen(string[] Struktur)
        {
            int OperatorenIndex = 0;
            string Ergebnis = null;

            if (Struktur.Contains("(") && Struktur.Length > 2)
            {
                do
                {
                    Struktur = Ausklammern(Struktur);
                }
                while (Struktur.Contains("("));
                
                Ergebnis = Berechnen(Struktur);
            }
            else
            {   

                for (int n = 0; n < Struktur.Length; n++)
                {
                    string s = Struktur[n];
                    if (s == "+" || s == "-" || s == "*" || s == "/" || s == "^" || s == "V")
                    {
                        OperatorenIndex++;
                    }
                }
                try
                {
                    do
                    {
                        for (int i = 0; i < Struktur.Length; i++)
                        {
                            string t = Struktur[i];

                            if (Struktur.Contains("^"))
                            {
                                if (t == "^")
                                {
                                    Struktur[i] = Math.Pow(Convert.ToDouble(Struktur[i - 1]), Convert.ToDouble(Struktur[i + 1])).ToString();
                                    Struktur = Struktur.Where((source, index) => index != i - 1 && index != i + 1).ToArray();
                                    OperatorenIndex--;
                                    i = 0;
                                }
                                else
                                    continue;

                            }
                            if (Struktur.Contains("V"))
                            {
                                if (t == "V")
                                {
                                    if (Convert.ToDouble(Struktur[i + 1]) > 0)
                                    {
                                        Struktur[i] = Math.Pow(Convert.ToDouble(Struktur[i + 1]), 0.5).ToString();
                                        Struktur = Struktur.Where((source, index) => index != i + 1).ToArray();
                                        OperatorenIndex--;
                                        i = 0;
                                    }
                                    else
                                    {
                                        MessageBox.Show("Der Bereich der komplexen Zahlen ist nicht abgedeckt! (Wurzel aus negativen Zahlen)", "SYNTAXFEHLER");
                                        Struktur = null;
                                    }
                                }
                                else
                                    continue;

                            }
                            if (Struktur.Contains("/"))
                            {
                                if (t == "/")
                                {
                                    if (Convert.ToDouble(Struktur[i + 1]) != 0)
                                    {
                                        Struktur[i] = (Convert.ToDouble(Struktur[i - 1]) / Convert.ToDouble(Struktur[i + 1])).ToString();
                                        Struktur = Struktur.Where((source, index) => index != i - 1 && index != i + 1).ToArray();
                                        OperatorenIndex--;
                                        i = 0;
                                    }
                                    else
                                    {
                                        MessageBox.Show("Division durch 0 ist nicht möglich!", "SYNTAXFEHLER");
                                        Struktur = null;
                                    }
                                }
                                else
                                    continue;

                            }
                            if (Struktur.Contains("*"))
                            {
                                if (t == "*")
                                {
                                    Struktur[i] = (Convert.ToDouble(Struktur[i - 1]) * Convert.ToDouble(Struktur[i + 1])).ToString();
                                    Struktur = Struktur.Where((source, index) => index != i - 1 && index != i + 1).ToArray();
                                    OperatorenIndex--;
                                    i = 0;
                                }
                                else
                                    continue;

                            }
                            if (Struktur.Contains("-"))
                            {
                                if (t == "-")
                                {
                                    Struktur[i] = (Convert.ToDouble(Struktur[i - 1]) - Convert.ToDouble(Struktur[i + 1])).ToString();
                                    Struktur = Struktur.Where((source, index) => index != i - 1 && index != i + 1).ToArray();
                                    OperatorenIndex--;
                                    i = 0;
                                }
                                else
                                    continue;

                            }
                            if (Struktur.Contains("+"))
                            {
                                if (t == "+")
                                {
                                    Struktur[i] = (Convert.ToDouble(Struktur[i - 1]) + Convert.ToDouble(Struktur[i + 1])).ToString();
                                    Struktur = Struktur.Where((source, index) => index != i - 1 && index != i + 1).ToArray();
                                    OperatorenIndex--;
                                    i = 0;
                                }
                                else
                                    continue;

                            }
                            if (OperatorenIndex == 0)
                            {
                                Ergebnis = Struktur[0];
                            }

                        }
                    }
                    while (OperatorenIndex > 0);
                }                
                catch
                {
                    MessageBox.Show("Bitte überprüfen Sie Ihre Eingabe!", "SYNTAXFEHLER");
                }
            }
            
            return Ergebnis;

        }
    }
}