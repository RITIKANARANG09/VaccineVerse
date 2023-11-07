
using Vaccine.Model;

namespace Project
{
    
    public class PatientUI : User {
        VaccineController vaccineControllerObj = new VaccineController();
        PatientController patientControllerObj = new PatientController();
        AppointmentController appointmentControllerObj = new AppointmentController();
        AppointmentUI appointmentObj = new AppointmentUI();
        public void choose(Patient patientObj)
        {
            
            while (true)
            {
                
               Console.ForegroundColor = ConsoleColor.Gray;
               Choose.PatientUIChoose();
                PatientChoose input = (PatientChoose)Validation.IntValidate() ;
                Console.ResetColor();
                switch (input)
                {
                    case PatientChoose.ViewVaccines:
                        chooseVC(patientObj);
                        continue;

                    case PatientChoose.ViewPastRecords:
                         PastRecordOfPatient(patientObj);
                        continue;


                    case PatientChoose.ViewAppointments:
                        
                        appointmentObj.viewAppointments(patientObj);
                        continue;

                    case PatientChoose.CancelAppointment:
                        appointmentObj.CancelAppointment(patientObj);
                        continue;

                    case PatientChoose.Exit:
                        Environment.Exit(0);
                        break;
                    
                    default:
                        Console.WriteLine(Message.printInvalidChoice);
                        continue;
                }
                break;
            }
            
        }

        public void chooseVC(Patient patientObj)
        {
            var vaccineAges=ViewAgeWiseVaccines(patientObj);
            VaccineIsAvailable(vaccineAges,patientObj);
        }
        public List<string> ViewAgeWiseVaccines(Patient patientObj)
        {
            Console.Write(Message.inputAge);
            int age = Validation.IntValidate();

            Console.Write(Message.printAvailableVaccine);
            List<string> vaccineAges = ViewVaccineAgeWise(age);

            Console.ForegroundColor = ConsoleColor.Blue;
            Helper(patientObj);
            return vaccineAges;        
        }
           
        public void PastRecordOfPatient(Patient patientObj)

        {
            List<Appointment> pastRecords = appointmentControllerObj.ViewAppointment(patientObj);
            var recordList= pastRecords.FindAll(pastRecord => pastRecord.dt < DateTime.Now.Date);
            if (recordList.Count == 0)
            {
                Console.WriteLine(Message.printNoPastRecords);
                return;
            }
            foreach (var record in recordList)
                {
                Console.WriteLine("Vaccine : " +record.VName + " on date : "+record.dt);
            }
        }
        public List<string> ViewVaccineAgeWise(int age)
        {
            List<string> vaccineList = vaccineControllerObj.ViewVaccineByAge(age);
            if (vaccineList.Count == 0)

            {
                Console.WriteLine(Message.printUnavailableVaccine);
                return null;
            }
                foreach(var vaccine in vaccineList) { Console.WriteLine(vaccine); }
            return vaccineList;
        }

        public void Helper(Patient patientObj)
        {
            Choose.VaccineSelectChoose();
        ChooseVaccineOption: VaccineSelect input = (VaccineSelect)Validation.IntValidate();

            if (input == VaccineSelect.Exit)
            { Environment.Exit(0); }

            else if (input == VaccineSelect.Back)
                choose(patientObj);
            else if (input == VaccineSelect.SelectVaccine)
            {
                return;
            }
            else goto ChooseVaccineOption;
        }
        public void VaccineIsAvailable(List<string> vaccineAges,Patient patientObj) {
            while (true)
            {

                Console.Write(Message.inputVaccineName);
                var vaccineName = Console.ReadLine();
                if (!vaccineAges.Contains(vaccineName))
                {
                    Console.WriteLine(Message.printVaccineAboveAge);
                    continue;
                }
                var type = vaccineControllerObj.FindParticularVaccineCenterWise(vaccineName);
                if (type.Count == 0)
                {
                    Console.WriteLine(Message.printUnavailableVaccine); continue;
                }
                Console.WriteLine(Message.printAvailableVaccineCenterWise);
                string types = string.Join(", ", type);
                Console.WriteLine(types);
                ChooseVaccineCenter(type, patientObj, vaccineName);
                break;
            }
        }
        public  void ChooseVaccineCenter(List<string> type,Patient patientObj,string vaccineN)
            { while(true)
            { Console.Write(Message.printChooseVC);
                    var centerName = Console.ReadLine();
                    if (!type.Contains(centerName))
                    {
                        Console.WriteLine(Message.printInvalidChoice);
                        continue;
                    }
                    else
                    {
                        Console.WriteLine(Message.inputBookAppointment);
                        AppointmentUI appointmentUI = new AppointmentUI();
                        Appointment appointment = new Appointment(patientObj.phoneNo, vaccineN, DateTime.Now, centerName);
                        appointmentUI.bookAppointment(appointment);
                    
                    }
                    break;
                }
            }
           
        }
    }
        /*public static void GetCertificate(string pn,string vname)
        {
            var vaccineCenter=VaccineCenterController.GiveCertificateToPatient(pn, vname);
            Console.WriteLine($"You got vaccinated by {vname} in ");
            foreach(var read in  vaccineCenter)
            {
                Console.Write(read.VcName+" : ");
                foreach (var record in read.appointmentDate)
                {
                    if(record.patientPhoneNo == pn && record.VName==vname)
                        Console.Write(record.dt);
                }
                Console.WriteLine();

            }
        }
       *//* public static void GetCertificateByPhoneN(string pn)
        {
            Console.WriteLine();
        }*/
    
