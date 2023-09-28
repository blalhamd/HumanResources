﻿

using Service.Comman.Enums;

namespace Service.Core.Models.ViewModels.Patient
{
    public class PatientLookUpViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? dateOfBirth { get; set; }
        public Gender gender { get; set; }
        public string? address { get; set; }
        public string phone { get; set; }
        public bool? HasInsurance { get; set; }
    }
}
