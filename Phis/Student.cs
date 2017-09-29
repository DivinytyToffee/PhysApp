using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Phis
{
    [Serializable]
    public class Student 
    {
        
        public string Name { get;  set; }
        public string LastName { get; set; }

        private const int Visit = 18;

        private double _balls;

        public double ResVisit { get; set; } = 0;
        public double ResPod { get; set; } = 0;
        public double ResPrig { get; set; } = 0;
        public double ResBeg3Km { get; set; } = 0;
        public double ResBeg100M { get; set; } = 0;
        public double Lichnostnie { get; set; } = 0;
        public double Balls
        {
            get
            {
                CheckRes();
                return Math.Round(_balls, 2);
            }

        }

        public Student(string name, string lname)
        {
            Name = name;
            LastName = lname;
        }
        
        public void CheckRes()
        {
            _balls = CheckResPod + CheckResPrig + CheckResBeg3Km + CheckResBeg100M + CheckVisit + CheckLich;
        }
        private double CheckResPod
        {
            get
            {
                if (!(ResPod > 0)) return 0;
                if (ResPod >= 10)
                    return 2;
                if ((ResPod < 10) && (ResPod >= 5))
                    return 1.5;
                return ResPod < 5 ? 1 : 0;
            }
        }
        private double CheckResPrig
        {
            get
            {
                if (!(ResPrig > 0)) return 0;
                if (ResPrig >= 180)
                    return 2;
                if ((ResPrig < 180) && (ResPrig >= 150))
                    return 1.5;
                return ResPrig < 150 ? 1 : 0;
            }
        }
        private double CheckResBeg3Km
        {
            get
            {
                if (!(ResBeg3Km > 0)) return 0;
                if (ResBeg3Km > 460)
                    return 1;
                if ((ResBeg3Km <= 460) && (ResBeg3Km > 400))
                    return 1.5;
                return ResBeg3Km <= 400 ? 2 : 0;
            }
        }
        private double CheckResBeg100M
        {
            get
            {
                if (!(ResBeg100M > 0)) return 0;
                if (ResBeg100M > 14.8 && ResBeg100M <= 15.1)
                    return 1;
                if (ResBeg100M > 13.5 && ResBeg100M <= 14.8)
                    return 1.5;
                return ResBeg100M <= 13.5 ? 2 : 0;
            }
        }
        private double CheckVisit
        {
            get
            {
                if (ResVisit > Visit)
                    ResVisit = 18;
                if (!(((ResVisit/Visit)*100) > 0)) return 0;
                if (((ResVisit / Visit) * 100) < 60)
                    return (((ResVisit / Visit) ) * (6 + 5));

                if ((((ResVisit / Visit) * 100) >= 60) && ((ResVisit / Visit) * 100) < 75)
                {
                    return (((ResVisit / Visit) ) * (7 + 6));
                }
                if (((ResVisit / Visit) * 100) >= 75)
                {
                    return (((ResVisit / Visit) ) * (9 + 8));
                }
                return 0;


            }
        }
        private double CheckLich
        {
            get
            {
                
                if (Lichnostnie < 10 && Lichnostnie > 0)
                    return Lichnostnie;
                if (!(Lichnostnie >= 10)) return 0;
                Lichnostnie = 10;
                return 10;
            }
        }


        public static void Save( List<Student> tmp,  string filename)
        {
            if (tmp == null) throw new ArgumentNullException(nameof(tmp));
            if (filename == null) throw new ArgumentNullException(nameof(filename));
            BinaryFormatter bit = new BinaryFormatter();
            using (Stream fStream = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                bit.Serialize(fStream, tmp);
            }
        }
        public static List<Student> Open(string filename)
        {
            if (filename == null) throw new ArgumentNullException(nameof(filename));
            BinaryFormatter bit = new BinaryFormatter();
            using (Stream fStream = File.OpenRead(filename))
            {
                List<Student> tmp = (List<Student>)bit.Deserialize(fStream);
                return tmp;
            }
            
        }
        public override string ToString()
        {
            return LastName + "\t " + Name;
        }
    }
}