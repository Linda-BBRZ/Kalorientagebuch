using System.ComponentModel.DataAnnotations;

namespace Kalorientagebuch.Models
{
    public class User
    {
        public int Id { get; set; }

        [MaxLength(25)]
        public string Name { get; set; }

        [MaxLength(255)]
        public string PasswordHash { get; set; }

        public DateOnly Birthday { get; set; }

        public double Weight { get; set; }

        public int Height { get; set; }

        public string Gender { get; set; }

        public byte[]? Picture { get; set; }

        public  List<Entry> Entries { get; set; } = new List<Entry>();

        public ActivityLevel? ActivityLevel { get; set; }

        public int? ActivityLevelId { get; set; }

        /* GRUNDUMSATZ BERECHNEN (vgl. https://www.omnicalculator.com/de/gesundheit/grundumsatz-harris-benedict-formel)
         * Für Männer: BMR = 66,5 + (13,75 × Gewicht in kg) + (5,003 × Größe in cm) - (6,75 × Alter)
         * Für Frauen: BMR = 655,1 + (9,563 × Gewicht in kg) + (1,850 × Körpergröße in cm) - (4,676 × Alter)
         * 
         * Um deinen TDEE (Total Daily Energy Expenditure) zu ermitteln, multiplizierst du deinen BMR mit dem 
         * entsprechenden Aktivitätsfaktor (PAL) wie folgt:
         * überwiegend sitzende Tätigkeit (wenig oder keine Bewegung): Kalorien = BMR × 1,2;
         * leicht aktiv (leichte Bewegung/Sport 1-3 Tage/Woche): Kalorien = Grundumsatz × 1,375;
         * mäßig aktiv (moderate Bewegung/Sport an 3-5 Tagen/Woche): Kalorien = Grundumsatz × 1,55;
         * sehr aktiv (anstrengende Bewegung/Sport an 6-7 Tagen pro Woche): Kalorien = Grundumsatz × 1,725 und
         * wenn du besonders aktiv bist (sehr viel Bewegung/Sport und eine körperliche Arbeit): Kalorien = Grundumsatz × 1,9.
         */
    }
}
