

namespace Project
{
    
    public class PatientUI : User {
        IVaccineControllerForPatient vaccineControllerObj = new VaccineController();
     
        IAppointmentControllerForPatient appointmentControllerObj = new AppointmentController();
        AppointmentUI appointmentObj = new AppointmentUI();
        public void PatientUIChoose(Patient patientObj)
        {
            
            while (true)
            {
                
               Console.ForegroundColor = ConsoleColor.Gray;
               Choose.PatientUIChoose();
                PatientChoose input = (PatientChoose)Validation.IntValidate() ;
                Console.ResetColor();
                switch (input)
                {
                    case PatientChoose.SelectVaccines:
                        SelectVaccine(patientObj);
                        continue;

                    case PatientChoose.ViewPastRecords:
                         PastRecordOfPatient(patientObj);
                        continue;


                    case PatientChoose.ViewAppointments:
                        
                        appointmentObj.ViewAppointments(patientObj);
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

        public void SelectVaccine(Patient patientObj)
        {
            var VaccineList=ViewAgeWiseVaccines(patientObj);
            VaccineIsAvailable(VaccineList, patientObj);
        }
        public List<Vaccine> ViewAgeWiseVaccines(Patient patientObj)
        {
            Console.Write(Message.inputAge);
            int age = Validation.IntValidate();

            Console.Write(Message.printAvailableVaccine);
            List<Vaccine> VaccineList = ViewVaccineAgeWise(age);

            Console.ForegroundColor = ConsoleColor.Blue;
            Helper(patientObj);
            return VaccineList;        
        }
           
        public void PastRecordOfPatient(Patient patientObj)

        {
            List<Appointment> pastRecords = appointmentControllerObj.ViewAppointment(patientObj);
            var recordList= pastRecords.FindAll(pastRecord => pastRecord.Date < DateTime.Now.Date);
            if (recordList.Count == 0)
            {
                Console.WriteLine(Message.printNoPastRecords);
                return;
            }
            foreach (var record in recordList)
                {
                Console.WriteLine("Vaccine : " +record.VName + " on date : "+record.Date);
                Console.WriteLine(",at " + record.VcName + ", " + record.Address);
            }
        }
        public List<Vaccine> ViewVaccineAgeWise(int age)
        {
            List<Vaccine> VaccineList = vaccineControllerObj.ViewVaccineByAge(age);
            if (VaccineList.Count == 0)

            {
                Console.WriteLine(Message.printUnavailableVaccine);
                return null;
            }
                foreach(var vaccine in VaccineList) { Console.WriteLine(vaccine.VName); }
            return VaccineList;
        }

        public void Helper(Patient patientObj)
        {
            Choose.VaccineSelectChoose();
            while (true)
            {
                VaccineSelect input = (VaccineSelect)Validation.IntValidate();

                if (input.Equals(VaccineSelect.Exit))
                {
                    Environment.Exit(0);
                }

                else if (input.Equals(VaccineSelect.Back))
                    PatientUIChoose(patientObj);

                else if (input.Equals(VaccineSelect.SelectVaccine))
                {
                    return;
                }
                else continue;
            }
            
        }
        public void VaccineIsAvailable(List<Vaccine> vaccineAges,Patient patientObj) {
            while (true)
            {

                Console.Write(Message.inputVaccineName);
                var vaccineName = Console.ReadLine();
                
                if (!vaccineAges.Any(v => v.VName.ToLower().Equals(vaccineName.ToLower())))
                {
                    Console.WriteLine(Message.printVaccineAboveAge);
                    continue;
                }
                List<VaccineCenter> vaccineCenterList = vaccineControllerObj.ViewCenterByVaccine(vaccineName);
                if (vaccineCenterList.Count == 0)
                {
                    Console.WriteLine(Message.printUnavailableVaccine); continue;
                }
                Console.WriteLine(Message.printAvailableVaccineCenterWise);
               foreach(var vaccineCenter in vaccineCenterList)
                Console.WriteLine(vaccineCenter.VcName + " : " + vaccineCenter.Address);
                ChooseVaccineCenter(vaccineCenterList, patientObj, vaccineName);
                break;
            }
        }
        public  void ChooseVaccineCenter(List<VaccineCenter> VaccineCenterList,Patient patientObj,string vaccineN)
            { while(true)
            { Console.Write(Message.printChooseVC);
                    var centerName = Console.ReadLine();
                Console.Write(Message.printChooseVCAddress);
                var centerAddress = Console.ReadLine();
                var vaccineCenterFind = VaccineCenterList.Find(v => v.VcName.ToLower().Equals(centerName.ToLower()) && v.Address.ToLower().Equals(centerAddress.ToLower()));
                if (vaccineCenterFind==null)
                    {
                        Console.WriteLine(Message.printInvalidChoice);
                        continue;
                    }
                    else
                    {
                        Console.WriteLine(Message.inputBookAppointment);
                        var appointmentUI = new AppointmentUI();
                        var appointment = new Appointment(patientObj.PhoneNo, vaccineN, DateTime.Now, centerName,centerAddress);
                        appointmentUI.BookAppointment(appointment);
                    
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
    
