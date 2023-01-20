using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Quera {
    public static class Program {
        public static class Configs {
            public enum PrintTypes {
                Console,
                File
            };

            public const bool IsDebug = false;
            public const PrintTypes PrintInDebugMode = PrintTypes.Console;
            public const string OutputFilePath = "output.txt";
        }

        public static void Main() {
            var data = new Data();
            var identicalService = new Services.IdenticalService(data);
            var studentService = new Services.StudentService(data, identicalService);
            var professorService = new Services.ProfessorService(data, identicalService);
            var classService = new Services.ClassService(data, studentService, professorService);
            var markService = new Services.MarkService(data, studentService, professorService, classService);

            while (true) {
                var inputs = Console.ReadLine()?.Trim().Split(' ');
                if (inputs == null || !inputs.Any()) continue;
                if (inputs.First().ToLower() == "end") break;

                Result result;
                var command = inputs.First().ToLower();
                switch (command) {
                    case "register_student":
                        result = studentService.Register(new Models.Student(inputs[1], inputs[2],
                            Convert.ToInt32(inputs[3]), inputs[4]));
                        break;
                    case "register_professor":
                        result = professorService.Register(new Models.Professor(inputs[1],
                            inputs[2], inputs[3]));
                        break;
                    case "make_class":
                        result = classService.Create(new Models.Class(inputs[1],
                            inputs[2], inputs[3]));
                        break;
                    case "add_student":
                        result = classService.AddStudent(inputs[1], inputs[2]);
                        break;
                    case "add_professor":
                        result = classService.SetProfessor(inputs[1], inputs[2]);
                        break;
                    case "student_status":
                        result = studentService.Status(inputs[1]);
                        break;
                    case "professor_status":
                        result = professorService.Status(inputs[1]);
                        break;
                    case "class_status":
                        result = classService.Status(inputs[1]);
                        break;
                    case "set_final_mark":
                        result = markService.SetFinalMark(inputs[1], inputs[2], inputs[3], Convert.ToInt32(inputs[4]));
                        break;
                    case "mark_student":
                        result = markService.GetStudentMark(inputs[1], inputs[2]);
                        break;
                    case "mark_list":
                        result = markService.GetClassMarkList(inputs[1]);
                        break;
                    case "average_mark_professor":
                        result = markService.AverageProfessorMarks(inputs[1]);
                        break;
                    case "average_mark_student":
                        result = markService.AverageStudentMarks(inputs[1]);
                        break;
                    case "top_student":
                        result = markService.GetTopStudentName(inputs[1], Convert.ToInt32(inputs[2]));
                        break;
                    case "top_mark":
                        result = markService.GetTopMarkOfClassStudents(inputs[1]);
                        break;
                    default:
                        result = Result.Fail("The command is not supported.");
                        break;
                }

                if (result != null)
                    Utility.PrintLine(result.Message);
            }
        }
    }

    public class Services {
        public class StudentService {
            private readonly Data _data;
            private readonly IdenticalService _identicalService;

            public StudentService(Data data, IdenticalService identicalService) {
                _data = data;
                _identicalService = identicalService;
            }

            public Result Register(Models.Student student) {
                if (!_identicalService.IsFree(student.IdenticalNumber))
                    return Result.Fail("this identical number previously registered");

                _data.Students.Add(student);
                _identicalService.Add(student.IdenticalNumber);
                return Result.Ok("welcome to golestan");
            }

            public bool Exists(string studentId) =>
                _data.Students.Exists(student => student.IdenticalNumber == studentId);

            public Models.Student Find(string studentId) =>
                _data.Students.Find(student => student.IdenticalNumber == studentId);

            public Result Status(string studentId) {
                var student = Find(studentId);
                if (student == null)
                    return Result.Fail("invalid student");

                var classes = GetClassNames(studentId);

                var status = $"{student.Name} {student.EnteringYear} {student.Field} {string.Join(" ", classes)}";
                return Result.Ok(status);
            }

            private IEnumerable<string> GetClassNames(string studentId) =>
                from cs in _data.ClassStudents
                join @class in _data.Classes on cs.ClassId equals @class.ClassId
                where cs.StudentId == studentId
                select @class.Name;
        }

        public class IdenticalService {
            private readonly Data _data;

            public IdenticalService(Data data) {
                _data = data;
            }

            public bool IsFree(string identicalNumber) =>
                !_data.IdenticalNumbers.Exists(identical => identical == identicalNumber);

            public void Add(string identicalNumber) {
                _data.IdenticalNumbers.Add(identicalNumber);
            }
        }

        public class ClassService {
            private readonly Data _data;
            private readonly StudentService _studentService;
            private readonly ProfessorService _professorService;

            public ClassService(Data data, StudentService studentService, ProfessorService professorService) {
                _data = data;
                _studentService = studentService;
                _professorService = professorService;
            }

            public Result Create(Models.Class @class) {
                if (Exists(@class.ClassId))
                    return Result.Fail("this class id previously used");

                _data.Classes.Add(@class);
                return Result.Ok("class added successfully");
            }

            public bool Exists(string classId) =>
                _data.Classes.Exists(@class => @class.ClassId == classId);

            public Models.Class Find(string classId) => _data.Classes.Find(@class => @class.ClassId == classId);

            public Result AddStudent(string studentId, string classId) {
                var student = _studentService.Find(studentId);
                if (student == null)
                    return Result.Fail("invalid student");

                var targetClass = Find(classId);
                if (targetClass == null)
                    return Result.Fail("invalid class");

                if (student.Field != targetClass.Field)
                    return Result.Fail("student field is not match");
                if (IsStudentRegistered(classId, studentId))
                    return Result.Fail("student is already registered");

                _data.ClassStudents.Add(new Models.ClassStudent(classId, studentId));
                return Result.Ok("student added successfully to the class");
            }

            public Result SetProfessor(string professorId, string classId) {
                var professor = _professorService.Find(professorId);
                if (professor == null)
                    return Result.Fail("invalid professor");

                var targetClass = Find(classId);
                if (targetClass == null)
                    return Result.Fail("invalid class");

                if (professor.Field != targetClass.Field)
                    return Result.Fail("professor field is not match");
                if (targetClass.ProfessorId != null)
                    return Result.Fail("this class has a professor");

                targetClass.ProfessorId = professorId;
                return Result.Ok("professor added successfully to the class");
            }

            public bool IsStudentRegistered(string classId, string studentId) =>
                _data.ClassStudents.Exists(classStudent =>
                    classStudent.ClassId == classId && classStudent.StudentId == studentId);

            public Result Status(string classId) {
                var @class = Find(classId);
                if (@class == null)
                    return Result.Fail("invalid class");

                var professorName = @class.ProfessorId == null
                    ? null
                    : _professorService.Find(@class.ProfessorId)?.Name;
                professorName = professorName ?? "None";

                var classes = GetStudents(classId).Select(student => student.Name);

                var status = $"{professorName} {string.Join(" ", classes)}";
                return Result.Ok(status);
            }

            public IEnumerable<Models.Student> GetStudents(string classId) =>
                from cs in _data.ClassStudents
                join student in _data.Students on cs.StudentId equals student.IdenticalNumber
                where cs.ClassId == classId
                select student;
        }

        public class ProfessorService {
            private readonly Data _data;
            private readonly IdenticalService _identicalService;

            public ProfessorService(Data data, IdenticalService identicalService) {
                _data = data;
                _identicalService = identicalService;
            }

            public Result Register(Models.Professor professor) {
                if (!_identicalService.IsFree(professor.IdenticalNumber))
                    return Result.Fail("this identical number previously registered");

                _data.Professors.Add(professor);
                _identicalService.Add(professor.IdenticalNumber);
                return Result.Ok("welcome to golestan");
            }

            public Models.Professor Find(string professorId) =>
                _data.Professors.Find(professor => professor.IdenticalNumber == professorId);

            public Result Status(string professorId) {
                var professor = Find(professorId);
                if (professor == null)
                    return Result.Fail("invalid professor");

                var classes = GetClasses(professorId).Select(@class => @class.Name);

                var status = $"{professor.Name} {professor.Field} {string.Join(" ", classes)}";
                return Result.Ok(status);
            }

            private IEnumerable<Models.Class> GetClasses(string professorId) =>
                _data.Classes.Where(@class => @class.ProfessorId != null && @class.ProfessorId == professorId);

            public bool Exists(string professorIdenticalNum) =>
                _data.Professors.Exists(professor => professor.IdenticalNumber == professorIdenticalNum);
        }

        public class MarkService {
            private readonly Data _data;
            private readonly StudentService _studentService;
            private readonly ProfessorService _professorService;
            private readonly ClassService _classService;

            public MarkService(Data data, StudentService studentService, ProfessorService professorService,
                ClassService classService) {
                _data = data;
                _studentService = studentService;
                _professorService = professorService;
                _classService = classService;
            }

            public Result SetFinalMark(string professorIdenticalNum, string studentIdenticalNum, string classId,
                int mark) {
                if (!_professorService.Exists(professorIdenticalNum))
                    return Result.Fail("invalid professor");
                if (!_studentService.Exists(studentIdenticalNum))
                    return Result.Fail("invalid student");
                var @class = _classService.Find(classId);
                if (@class == null)
                    return Result.Fail("invalid class");
                if (@class.ProfessorId != professorIdenticalNum)
                    return Result.Fail("professor class is not match");
                if (!_classService.IsStudentRegistered(classId, studentIdenticalNum))
                    return Result.Fail("student did not registered");

                CreateOrUpdate(professorIdenticalNum, studentIdenticalNum, classId, mark);
                return Result.Ok("student final mark added or changed");
            }

            private void CreateOrUpdate(string professorId, string studentId, string classId, int mark) {
                var markIndex = FindIndex(professorId, studentId, classId);
                if (markIndex >= 0)
                    Update(markIndex, mark);
                else
                    Create(professorId, studentId, classId, mark);
            }

            private void Update(int markIndex, int mark) {
                _data.StudentMarks[markIndex].Mark = mark;
            }

            private void Create(string professorId, string studentId, string classId, int mark) {
                _data.StudentMarks.Add(new Models.StudentMark(studentId, professorId, classId, mark));
            }

            private int FindIndex(string professorId, string studentId, string classId) =>
                _data.StudentMarks.FindIndex(studentMark => studentMark.ProfessorId == professorId
                                                            && studentMark.StudentId == studentId &&
                                                            studentMark.ClassId == classId);


            public Result GetStudentMark(string studentId, string classId) {
                if (!_studentService.Exists(studentId))
                    return Result.Fail("invalid student");
                if (!_classService.Exists(classId))
                    return Result.Fail("invalid class");
                if (!_classService.IsStudentRegistered(classId, studentId))
                    return Result.Fail("student did not registered");

                var mark = _data.StudentMarks.SingleOrDefault(studentMark =>
                    studentMark.StudentId == studentId && studentMark.ClassId == classId)?.Mark;

                return Result.Ok(mark == null ? "None" : mark.ToString());
            }

            public Result GetClassMarkList(string classId) {
                var @class = _classService.Find(classId);
                if (@class == null)
                    return Result.Fail("invalid class");
                if (@class.ProfessorId == null)
                    return Result.Fail("no professor");

                var markList = from student in _classService.GetStudents(classId)
                    join studentMark in _data.StudentMarks on new
                            {StudentId = student.IdenticalNumber, ClassId = classId} equals new
                            {StudentId = studentMark.StudentId, ClassId = studentMark.ClassId} into
                        studentMarks
                    from studentMark in studentMarks.DefaultIfEmpty()
                    select studentMark;

                var marks = markList.Select(studentMark => studentMark == null ? "None" : studentMark.Mark.ToString())
                    .ToList();

                return Result.Ok(!marks.Any() ? "no student" : string.Join(" ", marks));
            }

            public Result AverageProfessorMarks(string professorId) {
                if (!_professorService.Exists(professorId))
                    return Result.Fail("invalid professor");

                var studentMarks = _data.StudentMarks
                    .Where(studentMark => studentMark.ProfessorId == professorId)
                    .ToList();
                if (!studentMarks.Any())
                    return Result.Ok("None");
                var average = studentMarks.Average(studentMark => studentMark.Mark)
                    .ToString("F2");
                return Result.Ok(average);
            }

            public Result AverageStudentMarks(string studentId) {
                if (!_studentService.Exists(studentId))
                    return Result.Fail("invalid student");

                var studentMarks = _data.StudentMarks
                    .Where(studentMark => studentMark.StudentId == studentId)
                    .ToList();
                if (!studentMarks.Any())
                    return Result.Ok("None");
                var average = studentMarks.Average(studentMark => studentMark.Mark)
                    .ToString("F2");
                return Result.Ok(average);
            }

            public Result GetTopStudentName(string field, int enteringYear) {
                var students = (from studentMark in _data.StudentMarks
                        join student in _data.Students on studentMark.StudentId equals student.IdenticalNumber
                        where student.Field == field && student.EnteringYear == enteringYear
                        select new {student.Name, studentMark.StudentId, studentMark.Mark})
                    .ToList();
                var orderedStudents = students
                    .GroupBy(student => student.StudentId)
                    .Select(grouping => new {Average = grouping.Average(stu => stu.Mark), Name = grouping.First().Name})
                    .OrderByDescending(student => student.Average);
                var topStudent = orderedStudents.FirstOrDefault();

                return Result.Ok(topStudent == null ? "None" : topStudent.Name);
            }

            public Result GetTopMarkOfClassStudents(string classId) {
                if (!_classService.Exists(classId))
                    return Result.Fail("invalid class");

                var studentMarks = (from studentMark in _data.StudentMarks
                        where studentMark.ClassId == classId
                        orderby studentMark.Mark descending
                        select studentMark.Mark)
                    .ToList();
                return Result.Ok(!studentMarks.Any() ? "None" : studentMarks.First().ToString());
            }
        }
    }

    public class Data {
        public List<Models.Student> Students { get; } = new List<Models.Student>();
        public List<string> IdenticalNumbers { get; } = new List<string>();
        public List<Models.Professor> Professors { get; } = new List<Models.Professor>();
        public List<Models.Class> Classes { get; } = new List<Models.Class>();
        public List<Models.StudentMark> StudentMarks { get; } = new List<Models.StudentMark>();
        public List<Models.ClassStudent> ClassStudents { get; } = new List<Models.ClassStudent>();
    }

    public class Models {
        public class Student {
            public Student(string name, string identicalNumber, int enteringYear, string field) {
                Name = name;
                IdenticalNumber = identicalNumber;
                EnteringYear = enteringYear;
                Field = field;
            }

            public string Name { get; set; }
            public string IdenticalNumber { get; set; }
            public int EnteringYear { get; set; }
            public string Field { get; set; }
        }

        public class Professor {
            public Professor(string name, string identicalNumber, string field) {
                Name = name;
                IdenticalNumber = identicalNumber;
                Field = field;
            }

            public string Name { get; set; }
            public string IdenticalNumber { get; set; }
            public string Field { get; set; }
        }

        public class Class {
            public Class(string name, string classId, string field) {
                Name = name;
                ClassId = classId;
                Field = field;
            }

            public string Name { get; set; }
            public string ClassId { get; set; }
            public string Field { get; set; }
            public string ProfessorId { get; set; }
        }

        public class StudentMark {
            public StudentMark(string studentId, string professorId, string classId, int mark) {
                StudentId = studentId;
                ProfessorId = professorId;
                ClassId = classId;
                Mark = mark;
            }

            public string StudentId { get; set; }
            public string ProfessorId { get; set; }
            public string ClassId { get; set; }
            public int Mark { get; set; }
        }


        public class ClassStudent {
            public ClassStudent(string classId, string studentId) {
                ClassId = classId;
                StudentId = studentId;
            }

            public string ClassId { get; set; }
            public string StudentId { get; set; }
        }
    }

    public class Result {
        public bool Success { get; set; }
        public string Message { get; set; }

        private Result(string message) {
            Message = message;
        }

        public static Result Ok(string message) => new Result(message) {Success = true};

        public static Result Fail(string message) => new Result(message) {Success = false};
    }

    public static class Utility {
        public static void PrintLine(string content) {
            if (!Program.Configs.IsDebug) {
                Console.WriteLine(content);
            }
            else if (Program.Configs.PrintInDebugMode == Program.Configs.PrintTypes.Console) {
                var defaultColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(content);
                Console.ForegroundColor = defaultColor;
            }
            else if (Program.Configs.PrintInDebugMode == Program.Configs.PrintTypes.File) {
                File.AppendAllText(Program.Configs.OutputFilePath, content + '\n');
            }
            else {
                Console.WriteLine($"Error! type of {nameof(Program.Configs.PrintInDebugMode)} is not supported.");
            }
        }
    }
}