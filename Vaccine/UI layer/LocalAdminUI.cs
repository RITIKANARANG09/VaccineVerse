
using Vaccine.Model;

namespace Project
{
    public  class LocalAdminUI 
    {
        VaccineController vaccineControllerObj = new VaccineController();
        PatientController patientControllerObj = new PatientController();
        AppointmentController appointmentControllerObj = new AppointmentController();
        GlobalAdminUI globalAdminObj = new GlobalAdminUI();
        public void choose(VaccineCenter vaccineCenterObj, Admin admin)
        {
           
            while (true)
            {
                Choose.LocalAdminUIChoose();
                LocalAdminChoose ip=(LocalAdminChoose)Validation.IntValidate();
                Console.ResetColor();
                
                switch (ip)
                {
                    case LocalAdminChoose.ViewAvailableVaccines:
                        LocallyViewVaccine(vaccineCenterObj);
                        break;
                    
                    case LocalAdminChoose.IncrementVaccines:
                        IncreaseVaccineCount(vaccineCenterObj);    
                        break;
                    
                    case LocalAdminChoose.DecrementVaccines:
                        DecreaseVaccineCount(vaccineCenterObj);
                        break;
                    
                    case LocalAdminChoose.Appointments:    
                        ViewAppointments(admin,vaccineCenterObj);
                        break;
                    
                    case LocalAdminChoose.AddVaccineToCenter:
                        AddVaccineToCenter(vaccineCenterObj);
                        break;
                    
                    case LocalAdminChoose.ViewPatientPastRecord:
                        ViewPastRecord(admin,vaccineCenterObj);
                        break;
                    
                    default:
                        Console.WriteLine(Message.printInvalidChoice);
                        continue;
                }
                Choose.HelperChoose();
                var inputVal= (HelperChoose)Validation.IntValidate();

                switch (inputVal)
                {
                    case HelperChoose.Back:
                        continue;
                   
                    case HelperChoose.Exit:
                        Environment.Exit(0);
                        break;
                    
                    default:
                        Console.WriteLine(Message.printInvalidChoice);
                            continue;
                }
            }

        }
        public void AdminLogin(Admin admin)
        {
           Label: Console.WriteLine(Message.inputVaccinationCenter);
            string vaccineCenter = Console.ReadLine();
            
            VaccineCenter vaccineCenterObj = AuthManager<User>.AuthMInstance.VerifyVaccineCenter(admin,vaccineCenter);
            if (vaccineCenterObj != null)
            { choose(vaccineCenterObj, admin); }
            else
            {
                Console.WriteLine(Message.printInvalidChoice);
                goto Label;
            }
        }
        public void LocallyViewVaccine(VaccineCenter vaccineCenterObj)
        {
            foreach (var vaccine in vaccineCenterObj.vaccines)
            {
                Console.WriteLine(vaccine.vname +" : "+vaccine.vcount);
            }
        }
        public void IncreaseVaccineCount(VaccineCenter vaccineCenterObj)
        {
              
            LocallyViewVaccine(vaccineCenterObj);
            Console.Write("\n" + Message.inputIncrementVaccineName);
            string vaccineName = Console.ReadLine();
           
            Console.Write("\n"+Message.inputUpdateVaccineCount);
            int vaccineCount = Validation.IntValidate();
            
            var vaccineObj=vaccineCenterObj.vaccines.Find(vaccine=>vaccine.vname==vaccineName);
            if (vaccineObj == null) { Console.WriteLine(Message.printUnavailableVaccine); return; }
            
                if (vaccineControllerObj.UpdateVaccine(vaccineObj, "increase", vaccineCount) == false)
                    Console.WriteLine(Message.printNoIncrementOFVaccine);
                else
                    Console.WriteLine(Message.printIncrementOFVaccine);       
        }
        public void DecreaseVaccineCount(VaccineCenter vaccineCenterObj)
        {
            label: Console.WriteLine("\n" + Message.inputDecrementVaccineName);
            string vaccineName = Console.ReadLine();
            Console.Write("\n" + Message.inputDecrementVaccineCount);
            int vaccineCount = Validation.IntValidate();
            var vaccineObj = vaccineCenterObj.vaccines.Find(vaccine => vaccine.vname == vaccineName);
            if(vaccineObj == null) { Console.WriteLine(Message.printUnavailableVaccine); goto label; }
            if (vaccineControllerObj.UpdateVaccine(vaccineObj, "decrease", vaccineCount) == false)
                Console.WriteLine(Message.printNoDecrementOFVaccine);
            else Console.WriteLine(Message.printDecrementOFVaccine);
        }
        public void AddVaccineToCenter(VaccineCenter vaccineCenter)
        {
            while (true)
            {
                
                List<Vaccine> vaccineList =globalAdminObj.GloballyViewVaccine();
                Console.Write(Message.inputAddVaccineToCenter);
                var addVaccines = Console.ReadLine();
                var vaccineObj = vaccineList.Find(vaccine => vaccine.vname == addVaccines);
                if (vaccineObj==null)
                {
                    Console.WriteLine(Message.printInvalidChoice);
                    continue;
                }
                
                Console.WriteLine(Message.inputAddVaccineToCenterCount);
                int doses = Validation.IntValidate();
                bool isVaccinePresent = false;
                var vaccine=vaccineCenter.vaccines.Find(v=>v.vname == addVaccines);
                if (vaccine != null) { Console.WriteLine(Message.printVAaccinePresentAlready); continue; }
                if ( vaccineControllerObj.VaccineAddInCenter(vaccineCenter,vaccineObj,doses) == false)
                { Console.WriteLine(Message.printVaccineNotAdded);
                continue; }
                else break;
            }
            Console.WriteLine(Message.printVaccineAdded);

        }

        public void ViewAppointments(Admin admin, VaccineCenter vaccineCenterObj)
        {
            List<Appointment> AppointmentList = appointmentControllerObj.ViewAppointment(admin, vaccineCenterObj.VcName);
            if (AppointmentList.Count == 0) {Console.WriteLine(Message.printNoAppointment); return; }
            Console.WriteLine(Message.printViewAppointments);
            foreach (Appointment appointment in AppointmentList)
                {
                    if(appointment.dt>=DateTime.Now.Date)
                    Console.WriteLine(appointment.VName + ", " + appointment.patientPhoneNo + ", " + appointment.dt);
                }
        }
        public void ViewPastRecord(Admin admin, VaccineCenter vaccineCenterObj)
        {
            List<Appointment> AppointmentList = appointmentControllerObj.ViewAppointment(admin, vaccineCenterObj.VcName);

            foreach (Appointment appointment in AppointmentList)
            {
                if (appointment.dt < DateTime.Now.Date)
                    Console.WriteLine(appointment.VName + ", " + appointment.patientPhoneNo + ", " + appointment.dt);
            }
        }
    }
}
