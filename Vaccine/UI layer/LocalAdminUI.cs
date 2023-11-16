

namespace Project
{
    public  class LocalAdminUI 
    {
        IVaccineControllerForAdmin vaccineControllerObj = new VaccineController();
        IAppointmentControllerForAdmin appointmentControllerObj = new AppointmentController();
        GlobalAdminUI globalAdminObj = new GlobalAdminUI();
        public void LocalAdminUIChoose(VaccineCenter vaccineCenterObj, Admin admin)
        {
           
            while (true)
            {
                Choose.LocalAdminUIChoose();
                LocalAdminChoose option=(LocalAdminChoose)Validation.IntValidate();
                Console.ResetColor();
                
                switch (option)
                {
                    case LocalAdminChoose.ViewAvailableVaccines:
                        ViewVaccinesInCenter(vaccineCenterObj);
                        break;
                    
                    case LocalAdminChoose.IncrementVaccines:
                        IncreaseVaccineCount(vaccineCenterObj);    
                        break;
                    
                    case LocalAdminChoose.DecrementVaccines:
                        DecreaseVaccineCount(vaccineCenterObj);
                        break;
                    
                    case LocalAdminChoose.ViewAppointments:    
                        ViewAppointments(admin,vaccineCenterObj);
                        break;
                    
                    case LocalAdminChoose.AddVaccineToCenter:
                        AddVaccineToCenter(vaccineCenterObj);
                        break;
                    
                    case LocalAdminChoose.ViewPatientPastRecord:
                        ViewPastRecordOfPatient(admin,vaccineCenterObj);
                        break;

                    case LocalAdminChoose.Exit:
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine(Message.printInvalidChoice);
                        continue;
                }
                Choose.HelperChoose();
                var inputValue= (HelperChoose)Validation.IntValidate();

                switch (inputValue)
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
            while (true) { 
                Console.WriteLine(Message.inputVaccinationCenterName);
                var vaccineCenterName = Console.ReadLine();

                Console.WriteLine(Message.inputVaccinationCenterAddress);
                var vaccineCenterAddress = Console.ReadLine();

                VaccineCenter vaccineCenterObj = AuthManager<User>.AuthMInstance.ValidateVaccineCenter(admin,vaccineCenterName,vaccineCenterAddress);
            
                if (vaccineCenterObj != null)
            { LocalAdminUIChoose(vaccineCenterObj, admin); }
            
                else
            {
                Console.WriteLine(Message.printInvalidChoice);
                    continue;
            }
            }
        }
         void ViewVaccinesInCenter(VaccineCenter vaccineCenterObj)
        {
            foreach (var vaccine in vaccineCenterObj.vaccines)
            {
                Console.WriteLine(vaccine.VName +" : "+vaccine.VCount);
            }
        }
        void InputVaccineDetails(out int vaccineCount,out Vaccine vaccineObj)
        {
            while (true)
            {
                Console.Write("\n" + Message.inputIncrementVaccineName);
                string vaccineName = Console.ReadLine();

                Console.Write("\n" + Message.inputUpdateVaccineCount);
                vaccineCount=Validation.IntValidate();

                vaccineObj = AuthManager<Admin>.AuthMInstance.FindVaccine(vaccineName);
                if (vaccineObj == null)
                { Console.WriteLine(Message.printUnavailableVaccine); continue; }
            }
        }

        void IncreaseVaccineCount(VaccineCenter vaccineCenterObj)
        {
            int vaccineCount;
            Vaccine vaccineObj;

            ViewVaccinesInCenter(vaccineCenterObj);
            InputVaccineDetails(out vaccineCount,out vaccineObj);
            
            
             if (vaccineControllerObj.UpdateVaccine(vaccineObj, "increase", vaccineCount)==false)
                    Console.WriteLine(Message.printNoIncrementOFVaccine);
             else
                    Console.WriteLine(Message.printIncrementOFVaccine);       
        }
         void DecreaseVaccineCount(VaccineCenter vaccineCenterObj)
        {
            int vaccineCount;
            Vaccine vaccineObj;
           
            ViewVaccinesInCenter(vaccineCenterObj);
            InputVaccineDetails(out vaccineCount,out vaccineObj);
            
            if (vaccineControllerObj.UpdateVaccine(vaccineObj, "decrease", vaccineCount) == false)
                Console.WriteLine(Message.printNoDecrementOFVaccine);
            else 
                Console.WriteLine(Message.printDecrementOFVaccine);
        }
         void AddVaccineToCenter(VaccineCenter vaccineCenter)
        {
            while (true)
            {
                
                List<Vaccine> vaccineList =globalAdminObj.GloballyViewVaccine();
                
                Console.Write(Message.inputAddVaccineName);
                var vaccineName = Console.ReadLine();
                
                var vaccineObj = vaccineList.Find(vaccine => vaccine.VName.ToLower().Equals(vaccineName.ToLower()));
                if (vaccineObj==null)
                {
                    Console.WriteLine(Message.printInvalidChoice);
                    continue;
                }
                
                Console.WriteLine(Message.inputAddVaccineToCenterCount);
                int doses = Validation.IntValidate();
                
                var vaccine=vaccineCenter.vaccines.Find(v=>v.VName.ToLower().Equals(vaccineName.ToLower()));
                if (vaccine !=null) { Console.WriteLine(Message.printVAaccinePresentAlready); 
                    continue; }
              
                vaccineObj.VCount = doses;
                vaccineCenter.vaccines.Add(vaccineObj);
                
                if ( vaccineControllerObj.VaccineAddInCenter(vaccineCenter) == false)
                { Console.WriteLine(Message.printVaccineNotAdded);
                continue; }
                else break;
            }
            Console.WriteLine(Message.printVaccineAdded);

        }

         void ViewAppointments(Admin admin, VaccineCenter vaccineCenterObj)
        {
            List<Appointment> AppointmentList = appointmentControllerObj.ViewAppointment(admin, vaccineCenterObj.VcName);
            
            if (AppointmentList.Count == 0) 
            {
                Console.WriteLine(Message.printNoAppointment); 
                return; 
            }
            
            Console.WriteLine(Message.printViewAppointments);
            
            foreach (Appointment appointment in AppointmentList)
                {
                    if(appointment.Date>=DateTime.Now.Date)
                    Console.WriteLine(appointment.VName + ", " + appointment.PatientPhoneNo + ", " + appointment.Date);
                }
        }
         void ViewPastRecordOfPatient(Admin admin, VaccineCenter vaccineCenterObj)
        {
            List<Appointment> AppointmentList = appointmentControllerObj.ViewAppointment(admin, vaccineCenterObj.VcName);

            var appointments = new List<Appointment>();
            foreach (Appointment appointment in AppointmentList)
            {
                if (appointment.Date < DateTime.Now.Date)
                {
                    Console.WriteLine(appointment.VName + ", " + appointment.PatientPhoneNo + ", " + appointment.Date);
                    appointments.Add(appointment);
                }
            }
            if(appointments.Count==0)
                Console.WriteLine(Message.printNoPastRecords);
        }
    }
}
