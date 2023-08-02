using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityCompetition.Core.Contracts;
using UniversityCompetition.Models;
using UniversityCompetition.Models.Contracts;
using UniversityCompetition.Repositories;
using UniversityCompetition.Utilities.Messages;

namespace UniversityCompetition.Core
{
    public class Controller : IController
    {
        private readonly SubjectRepository subjects;
        private readonly StudentRepository students;
        private readonly UniversityRepository universities;

        private int subjectId = 1;
        private int studentId = 1;
        private int universityId = 1;

        public Controller()
        {
            this.subjects = new SubjectRepository();
            this.students = new StudentRepository();
            this.universities = new UniversityRepository();
        }

        public string AddStudent(string firstName, string lastName)
        {
            IStudent studentExist = students.Models.FirstOrDefault(s => s.FirstName == firstName && s.LastName == lastName);

            if (studentExist != null)
            {
                return string.Format(OutputMessages.AlreadyAddedStudent, firstName, lastName);
            }

            IStudent student = new Student(studentId, firstName, lastName);
            this.studentId++;
            this.students.AddModel(student);

            return string.Format(OutputMessages.StudentAddedSuccessfully, firstName, lastName, nameof(StudentRepository));
        }

        public string AddSubject(string subjectName, string subjectType)
        {
            

            if (subjectType != nameof(EconomicalSubject) && subjectType != nameof(HumanitySubject) && subjectType != nameof(TechnicalSubject))
            {
                return string.Format(OutputMessages.SubjectTypeNotSupported, subjectType);
            }

            ISubject subjectExist = subjects.Models.FirstOrDefault(s => s.Name == subjectName);

            if (subjectExist != null)
            {
                return string.Format(OutputMessages.AlreadyAddedSubject, subjectName);
            }

            ISubject subject;
            if (subjectType == nameof(EconomicalSubject))
            {
                subject = new EconomicalSubject(subjectId, subjectName);
            }
            else if (subjectType == nameof(HumanitySubject))
            {
                subject = new HumanitySubject(subjectId, subjectName);
            }
            else
            {
                subject = new TechnicalSubject(subjectId, subjectName);
            }

            this.subjectId++;
            subjects.AddModel(subject);
            return string.Format(OutputMessages.SubjectAddedSuccessfully, subjectType, subjectName, nameof(SubjectRepository)); 
        }

        public string AddUniversity(string universityName, string category, int capacity, List<string> requiredSubjects)
        {
            IUniversity universityExist = universities.Models.FirstOrDefault(u => u.Name == universityName);

            if (universityExist != null)
            {
                return string.Format(OutputMessages.AlreadyAddedUniversity, universityName);
            }

            List<int> currentUnivercitySubjectsId = new List<int>();

            foreach (var subject in requiredSubjects)
            {
                int currentSubject = subjects.Models.FirstOrDefault(s => s.Name == subject).Id;
                currentUnivercitySubjectsId.Add(currentSubject);
            }

            IUniversity university = new University(universityId, universityName, category, capacity, currentUnivercitySubjectsId);
            this.universityId++;
            this.universities.AddModel(university);

            return string.Format(OutputMessages.UniversityAddedSuccessfully, universityName, nameof(UniversityRepository));
        }

        public string ApplyToUniversity(string studentName, string universityName)
        {
            string[] name = studentName.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            string firstName = name[0];
            string lastName = name[1];

            IStudent student = students.FindByName(studentName);

            if (student == null)
            {
                return string.Format(OutputMessages.StudentNotRegitered, firstName, lastName);
            }

            IUniversity university = universities.FindByName(universityName);

            if (university == null)
            {
                return string.Format(OutputMessages.UniversityNotRegitered, universityName);
            }
                        
            foreach (var exam in university.RequiredSubjects)
            {
                bool examIsCovered = student.CoveredExams.Contains(exam);
                if (!examIsCovered)
                {
                    return string.Format(OutputMessages.StudentHasToCoverExams, studentName, universityName);
                }                
            }

            if (student.University != null && student.University.Name == universityName)
            {
                return string.Format(OutputMessages.StudentAlreadyJoined, firstName, lastName, universityName);
            }

            student.JoinUniversity(university);
            return string.Format(OutputMessages.StudentSuccessfullyJoined, firstName, lastName, universityName);
        }

        public string TakeExam(int studentId, int subjectId)
        {
            IStudent studentExist = students.Models.FirstOrDefault(s => s.Id == studentId);

            if (studentExist == null)
            {
                return string.Format(OutputMessages.InvalidStudentId);
            }

            ISubject subjectExist = subjects.Models.FirstOrDefault(s => s.Id == subjectId);

            if (subjectExist == null)
            {
                return string.Format(OutputMessages.InvalidSubjectId);
            }

            var isExamAlreadyCovered = studentExist.CoveredExams.Any(ce => ce == subjectId);

            if (isExamAlreadyCovered)
            {
                return string.Format(OutputMessages.StudentAlreadyCoveredThatExam, studentExist.FirstName, studentExist.LastName, subjectExist.Name);
            }

            ISubject currentSubject = subjects.FindById(subjectId);
            studentExist.CoverExam(currentSubject);            

            return string.Format(OutputMessages.StudentSuccessfullyCoveredExam, studentExist.FirstName, studentExist.LastName, subjectExist.Name);
        }

        public string UniversityReport(int universityId)
        {
            IUniversity university = universities.FindById(universityId);
            StringBuilder sb = new StringBuilder();
            int attendances = this.students.Models.Where(u => u.University == university).Count();

            sb.AppendLine($"*** {university.Name} ***");
            sb.AppendLine($"Profile: {university.Category}");
            sb.AppendLine($"Students admitted: {attendances}");
            sb.AppendLine($"University vacancy: {university.Capacity - attendances}");

            return sb.ToString().TrimEnd();
        }
    }
}
