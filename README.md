# **Human Resources Management System**

## **Overview**

The **Human Resources Management System** is a comprehensive solution for managing various aspects of healthcare operations, including doctors, patients, appointments, prescriptions, and billing. The system is designed to enhance efficiency, streamline data management, and improve overall productivity in healthcare facilities.

---
## **Database Design**
![image](https://github.com/user-attachments/assets/6f2ce813-df19-492f-8efe-8542191b101e)

---

## **Key Features**

### **Doctors Management**
- Manage doctor profiles, including:
  - **Name**, **specialty**, **license details**, and **contact information**.
  - Track **creation**, **modification**, and **deletion metadata** for audit purposes.
- Support logical deletion for better data handling.

### **Patient Records**
- Comprehensive patient records, including:
  - **Name**, **date of birth**, **gender**, **address**, and **phone number**.
  - Indicate insurance status with the **HasInsurance** field.
- Lifecycle metadata for tracking changes and deletions.

### **Appointments**
- Manage appointments between doctors and patients:
  - Capture **date/time**, **status**, and associated **doctor** and **patient**.
  - Maintain appointment history with metadata for audits.

### **Prescriptions**
- Generate and manage prescriptions:
  - Include details like **medication**, **dosage**, **frequency**, and **duration**.
  - Link prescriptions to specific appointments.

### **Billing**
- Manage billing operations:
  - Track **amount**, **date/time**, and associated **patient**.
  - Store lifecycle metadata for audit purposes.

---

### **Entity Relationships**
- **Doctors** ↔ **Appointments**: A doctor can have multiple appointments.
- **Patients** ↔ **Appointments**: A patient can book multiple appointments.
- **Appointments** ↔ **Prescriptions**: Each appointment can have multiple prescriptions.
- **Patients** ↔ **Bills**: A patient can have multiple bills.

### **Key Tables**
- **Doctors**:
  - Fields: `Name`, `Specialty`, `License`, `Contact`.
- **Patients**:
  - Fields: `Name`, `DateOfBirth`, `Gender`, `HasInsurance`, `Address`, `Phone`.
- **Appointments**:
  - Fields: `DateTime`, `Status`, `PatientId`, `DoctorId`.
- **Prescriptions**:
  - Fields: `Medication`, `Dosage`, `Frequency`, `Duration`, `AppointmentId`.
- **Bills**:
  - Fields: `Amount`, `DateTime`, `PatientId`.

---

## **Technologies Used**

- **Backend**:
  - **ASP.NET Core**: For creating RESTful APIs and handling business logic.
  - **Entity Framework Core**: For database operations and ORM functionality.
- **Database**:
  - **SQL Server**: For structured data storage.
- **Version Control**:
  - **Git**: For source code management.
  - **GitHub**: For collaboration and code hosting.

---

## **Key Use Cases**

1. **Manage Doctors**: Create, update, or remove doctor profiles.
2. **Maintain Patient Records**: Store detailed patient information.
3. **Schedule Appointments**: Book and manage appointments for patients.
4. **Generate Prescriptions**: Record prescriptions linked to appointments.
5. **Track Billing**: Manage and track financial transactions for patient services.

---

## **Getting Started**

### **Clone the Repository**
```bash
git clone https://github.com/blalhamd/HumanResources.git

