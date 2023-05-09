using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Lab3.TehnologiiWeb
{
    public partial class Form1 : Form
    {
        private List<Student> studenti = new List<Student>();
        private List<Grupa> grupe = new List<Grupa>();
        private List<Profesor> profesori = new List<Profesor>();

        public Form1()
        {
            InitializeComponent();

            // Adăugați date de test pentru studenti, grupe și profesori
            grupe.Add(new Grupa { ID = 1, Denumire = "Grupa 1" });
            grupe.Add(new Grupa { ID = 2, Denumire = "Grupa 2" });

            studenti.Add(new Student { ID = 1, Nume = "Ion Popescu", GrupaID = 1 });
            studenti.Add(new Student { ID = 2, Nume = "Maria Ionescu", GrupaID = 2 });

            profesori.Add(new Profesor { ID = 1, Nume = "Profesor Popescu", Materie = "Informatica" });
            profesori.Add(new Profesor { ID = 2, Nume = "Profesor Ionescu", Materie = "Matematica" });
            comboBox1.SelectedIndexChanged += new EventHandler(comboBox1_SelectedIndexChanged);
            comboBox1.Items.AddRange(new string[] {
                "Toți studenții din grupa 1",
                "Profesorii care predau studenții din anul curent",
                "Studenții grupați după grupă",
                "Numărul de studenți per grupă",
                "Studenți și grupele lor",
                "Profesori și numărul de studenți",
                "Studenți cu nume lung",
                "Studenți și lungimea numelui de familie",
                "Studenți și profesori ordonați",
                "Numărul de studenți cu nume scurt",
                "Primii trei studenți ordonați după nume",
                "Studenți fără primul",
                "Studenți cu litera A în nume",
                "Profesori cu cel puțin doi studenți",
                "Studenți cu cel puțin un coleg"
            });

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    var studentsInGroup = StudentiDinGrupa(1);
                    DisplayResult(studentsInGroup);
                    break;
                case 1:
                    var studentsNumeIncepandCuM = StudentiNumeIncepandCuM();
                    DisplayResult(studentsNumeIncepandCuM);
                    break;
                case 2:
                    var studentsGrupatiDupaGrupa = StudentiGrupatiDupaGrupa();
                    DisplayResult(studentsGrupatiDupaGrupa.SelectMany(group => group));
                    break;
                case 3:
                    var numarStudentiPerGrupa = NumarStudentiPerGrupa();
                    DisplayResult(numarStudentiPerGrupa.Cast<object>());
                    break;
                case 4:
                    var studentiSiGrupeleLor = StudentiSiGrupeleLor();
                    DisplayResult(studentiSiGrupeleLor.Cast<object>());
                    break;
                case 5:
                    var profesoriSiNumarulDeStudenti = ProfesoriSiNumarulDeStudenti();
                    DisplayResult(profesoriSiNumarulDeStudenti.Cast<object>());
                    break;
                case 6:
                    var studentiCuNumeLung = StudentiCuNumeLung();
                    DisplayResult(studentiCuNumeLung);
                    break;
                case 7:
                    var studentiSiLungimeaNumeFamilie = StudentiSiLungimeaNumeFamilie();
                    DisplayResult(studentiSiLungimeaNumeFamilie.Cast<object>());
                    break;
                case 8:
                    var studentiSiProfesoriOrdonati = StudentiSiProfesoriOrdonati();
                    DisplayResult(studentiSiProfesoriOrdonati.Cast<object>());
                    break;
                case 9:
                    var numarStudentiCuNumeScurt = NumarStudentiCuNumeScurt();
                    MessageBox.Show("Numarul de studenti cu nume scurt: " + numarStudentiCuNumeScurt);
                    break;
                case 10:
                    var primiiTreiStudentiOrdonatiDupaNume = PrimiiTreiStudentiOrdonatiDupaNume();
                    DisplayResult(primiiTreiStudentiOrdonatiDupaNume);
                    break;
                case 11:
                    var studentiFaraPrimul = StudentiFaraPrimul();
                    DisplayResult(studentiFaraPrimul);
                    break;
                case 12:
                    var studentiCuLiteraAInNume = StudentiCuLiteraAInNume();
                    DisplayResult(studentiCuLiteraAInNume);
                    break;
                case 13:
                    var profesoriCuCelPutinDoiStudenti = ProfesoriCuCelPutinDoiStudenti();
                    DisplayResult(profesoriCuCelPutinDoiStudenti);
                    break;
                case 14:
                    var studentiCuCelPutinUnColeg = StudentiCuCelPutinUnColeg();
                    DisplayResult(studentiCuCelPutinUnColeg);
                    break;
                default:
                    break;
            }
        }


        private void DisplayResult(IEnumerable<object> items)
        {
            listBoxStudenti.Items.Clear();
            foreach (var item in items)
            {
                listBoxStudenti.Items.Add(item.ToString());
            }
        }




        // 1
        private IEnumerable<Student> StudentiDinGrupa(int grupaID)
        {
            var studentiDinGrupa = from student in studenti
                                   where student.GrupaID == grupaID
                                   select student;

            return studentiDinGrupa;
        }

        // 2

        private IEnumerable<Student> StudentiNumeIncepandCuM()
        {
            var result = from student in studenti
                         where student.Nume.StartsWith("M")
                         select student;
            return result;
        }

        // 3
        private IEnumerable<IGrouping<int, Student>> StudentiGrupatiDupaGrupa()
        {
            var result = from student in studenti
                         group student by student.GrupaID;
            return result;
        }

        // 4
        private IEnumerable<KeyValuePair<int, int>> NumarStudentiPerGrupa()
        {
            var result = from student in studenti
                         group student by student.GrupaID
                         into studentGroup
                         select new KeyValuePair<int, int>(studentGroup.Key, studentGroup.Count());
            return result;
        }

        // 5
        private IEnumerable<(Student, Grupa)> StudentiSiGrupeleLor()
        {
            var result = from student in studenti
                         join grupa in grupe on student.GrupaID equals grupa.ID
                         select (student, grupa);
            return result;
        }

        // 6
        private IEnumerable<(Profesor, int)> ProfesoriSiNumarulDeStudenti()
        {
            var result = from profesor in profesori
                         join grupa in grupe on profesor.ID equals grupa.ID
                         join student in studenti on grupa.ID equals student.GrupaID into studentGroup
                         select (profesor, studentGroup.Count());
            return result;
        }

        // 7
        private IEnumerable<Student> StudentiCuNumeLung()
        {
            var result = from student in studenti
                         let numeFamilie = student.Nume.Split(' ')[1]
                         where numeFamilie.Length > 6
                         select student;
            return result;
        }

        // 8
        private IEnumerable<(Student, int)> StudentiSiLungimeaNumeFamilie()
        {
            var result = from student in studenti
                         let numeFamilie = student.Nume.Split(' ')[1]
                         select (student, numeFamilie.Length);
            return result;
        }

        // 9
        private IEnumerable<(Student, Profesor)> StudentiSiProfesoriOrdonati()
        {
            var result = from student in studenti
                         join grupa in grupe on student.GrupaID equals grupa.ID
                         join profesor in profesori on grupa.ID equals profesor.ID
                         orderby student.Nume, profesor.Nume
                         select (student, profesor);
            return result;
        }

        // 10
        private int NumarStudentiCuNumeScurt()
        {
            var result = (from student in studenti
                          let numeFamilie = student.Nume.Split(' ')[1]
                          where numeFamilie.Length < 6
                          select student).Count();
            return result;
        }

        // 11
        private IEnumerable<Student> PrimiiTreiStudentiOrdonatiDupaNume()
        {
            var result = (from student in studenti
                          orderby student.Nume
                          select student).Take(3);
            return result;
        }

        // 12
        private IEnumerable<Student> StudentiFaraPrimul()
        {
            var result = studenti.Skip(1);
            return result;
        }

        // 13
        private IEnumerable<Student> StudentiCuLiteraAInNume()
        {
            var result = from student in studenti
                         let numeFamilie = student.Nume.Split(' ')[1]
                         where numeFamilie.Contains("a")
                         select student;
            return result;
        }

        // 14
        private IEnumerable<Profesor> ProfesoriCuCelPutinDoiStudenti()
        {
            var result = from profesor in profesori
                         join grupa in grupe on profesor.ID equals grupa.ID
                         join student in studenti on grupa.ID equals student.GrupaID into studentGroup
                         where studentGroup.Count() >= 2
                         select profesor;
            return result;
        }

        // 15
        private IEnumerable<Student> StudentiCuCelPutinUnColeg()
        {
            var result = from student in studenti
                         join coleg in studenti on student.GrupaID equals coleg.GrupaID
                         where student.ID != coleg.ID
                         select student;
            return result.Distinct();
        }


    }

}
