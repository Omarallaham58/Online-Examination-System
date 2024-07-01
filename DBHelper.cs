using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;

namespace ProjectV1
{
    public class DBHelper
    {

        //SqlConnection conn;

        public DBHelper()
        {
            try
            {
                //conn = new SqlConnection(Constants.DB_CONNECTION_STRING);
                //conn = new SqlConnection(ConfigurationManager.ConnectionStrings[Constants.DB_CONNECTION_STRING].ConnectionString);
               // SqlConnection(ConfigurationManager.ConnectionStrings[Constants.DB_CONNECTION_STRING].ConnectionString);
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex.Message);
                //Response.Write(ex.Message);
            }
            }

        public Account accountExist(string username, string password)
        {
            //returns the account object if found, null otherwise
            //SqlConnection conn= new SqlConnection(Constants.DB_CONNECTION_STRING);
            Account acc = null;
            using (SqlConnection conn = new SqlConnection(Constants.DB_CONNECTION_STRING))
            {
                //  SqlConnection conn= new SqlConnection(ConfigurationManager.ConnectionStrings[Constants.DB_CONNECTION_STRING].ConnectionString);
                
                conn.Open();
                string sql = "SELECT * FROM [Account] WHERE username=@username AND password=@password";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    acc = new Account(reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
                }
                conn.Close();
            }
            return acc;

        }

        public User getUserByAccountId(int id)
        {
            User user = null;
            using (SqlConnection conn = new SqlConnection(Constants.DB_CONNECTION_STRING))
            {
                //SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings[Constants.DB_CONNECTION_STRING].ConnectionString);
               
                conn.Open();
                string sql = "SELECT * FROM [User] WHERE account_id=@id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    user = new User(reader.GetInt32(0), this.getAccountById(id), reader.GetString(2), reader.GetString(3), reader.GetDateTime(4), reader.GetString(5), reader.GetString(6), reader.GetInt32(7));
                }
                conn.Close();
            }
            return user;
        }

        public Account getAccountById(int id)
        {
            Account acc = null;
            // SqlConnection conn = new SqlConnection(Constants.DB_CONNECTION_STRING);
            using (SqlConnection conn = new SqlConnection(Constants.DB_CONNECTION_STRING))
            {
                // SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings[Constants.DB_CONNECTION_STRING].ConnectionString);
                
                conn.Open();
                string sql = "SELECT * FROM [Account] WHERE id=@id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    acc = new Account(id, reader.GetString(1), reader.GetString(2));
                }
                conn.Close();
            }
            return acc;
        }
        

        public bool UserExist(User user)
        {
            int exist = 0;
            using (SqlConnection conn = new SqlConnection(Constants.DB_CONNECTION_STRING))
            {
                conn.Open();
                string sql = "SELECT COUNT(*) FROM [User] WHERE firstName=@firstName AND lastName=@lastName and email=@email";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@firstName", user.firstName);
                cmd.Parameters.AddWithValue("@lastName", user.lastName);
                cmd.Parameters.AddWithValue("@email", user.email);
                exist = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                conn.Close();

            }

            return (exist != 0);

        }


        public int AddAccount(Account account)
        {

            //returns the id of the inserted id on success, -1 on failure
            int id = -1;
            using (SqlConnection conn = new SqlConnection(Constants.DB_CONNECTION_STRING))
            {
                conn.Open();

                string sql = "INSERT INTO [Account](username,password) OUTPUT INSERTED.id" +
                    " VALUES (@username,@password)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@username", account.username);
                cmd.Parameters.AddWithValue("@password", account.password);
                id = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                conn.Close();

            }
            return id;
        }

        public bool AddUser(User user)
        {
            int added = 0;
            using (SqlConnection conn = new SqlConnection(Constants.DB_CONNECTION_STRING))
            {
                conn.Open();

                string sql = "INSERT INTO [User](account_id,firstName,lastName,dob,email,phoneNumber,isTeacher)" +
                    " OUTPUT INSERTED.id VALUES (@account_id,@firstName,@lastName,@dob,@email,@phoneNumber,@isTeacher)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@account_id", user.account.id);
                cmd.Parameters.AddWithValue("@firstName", user.firstName);
                cmd.Parameters.AddWithValue("@lastName", user.lastName);
                cmd.Parameters.AddWithValue("@dob", user.dob.Date);
                cmd.Parameters.AddWithValue("@email", user.email);
                cmd.Parameters.AddWithValue("@phoneNumber", user.phoneNb);
                cmd.Parameters.AddWithValue("@isTeacher", user.isTeacher);
                added = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                conn.Close();

            }

            return (added != 0);

        }

        public bool CourseExist(Course course)
        {
            int exist = 0;
            using (SqlConnection conn = new SqlConnection(Constants.DB_CONNECTION_STRING))
            {
                conn.Open();
                string sql = "SELECT COUNT(*) FROM [Course] WHERE code=@code";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@code", course.code);
                exist = Convert.ToInt32(cmd.ExecuteScalar().ToString());

                conn.Close();
            }

            return (exist != 0);

        }

        public bool AddCourse(Course course)
        {
            int added = 0;
            using (SqlConnection conn = new SqlConnection(Constants.DB_CONNECTION_STRING))
            {
                conn.Open();
                string sql = "INSERT INTO [Course](teacher_id,code,name,credits,nbStudents) " +
                    "VALUES(-1,@code,@name,@credits,0)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                //cmd.Parameters.AddWithValue("@teacher_id", course.teacher.id);
                cmd.Parameters.AddWithValue("@code", course.code);
                cmd.Parameters.AddWithValue("@name", course.name);
                cmd.Parameters.AddWithValue("@credits", course.credit);
                //added = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                //int.TryParse(cmd.ExecuteScalar().ToString(), out added);
                added = cmd.ExecuteNonQuery();

                conn.Close();

            }

            return (added != 0);

        }

        public Course getCourseByCode(string code)
        {
            Course course = null;
            using (SqlConnection conn = new SqlConnection(Constants.DB_CONNECTION_STRING))
            {
                conn.Open();
                string sql = "SELECT * FROM [Course] WHERE [code]=@code";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@code", code);
                SqlDataReader reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    User teacher = this.getUserById(reader.GetInt32(1));
                    course=new Course(reader.GetInt32(0),teacher,reader.GetString(2),reader.GetString(3),reader.GetInt32(4),reader.GetInt32(5));
                }

                conn.Close();
            }

            return course;

        }

        public User getUserById(int id)
        {
            User user = null;
            using (SqlConnection conn = new SqlConnection(Constants.DB_CONNECTION_STRING))
            {

                conn.Open();
                string sql = "SELECT * FROM [User] WHERE id=@id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    user = new User(id, this.getAccountById(reader.GetInt32(1)), reader.GetString(2), reader.GetString(3), reader.GetDateTime(4), reader.GetString(5), reader.GetString(6), reader.GetInt32(7));
                }
                conn.Close();
            }

            return user;
        }

        public bool updateCourse(Course course)
        {
            int updated = 0;
            using (SqlConnection conn = new SqlConnection(Constants.DB_CONNECTION_STRING))
            {
                conn.Open();
                string sql = "UPDATE [Course] SET teacher_id=@teacher_id, code=@code, name=@name, " +
                    "credits=@credits, nbStudents=@nbStudents WHERE id=@id";
                SqlCommand cmd=new SqlCommand(sql,conn);
                cmd.Parameters.AddWithValue("@teacher_id", course.teacher.id);
                cmd.Parameters.AddWithValue("@code", course.code);
                cmd.Parameters.AddWithValue("@name", course.name);
                cmd.Parameters.AddWithValue("@credits", course.credit);
                cmd.Parameters.AddWithValue("@nbStudents", course.nbStudents);
                cmd.Parameters.AddWithValue("@id", course.id);
                updated=cmd.ExecuteNonQuery();
                conn.Close();
            }

            return (updated != 0);
        }

        public Course getCourseById(int id)
        {
            Course course = null;
            using (SqlConnection conn = new SqlConnection(Constants.DB_CONNECTION_STRING))
            {
                conn.Open();
                string sql = "SELECT * FROM [Course] WHERE id=@id";
                SqlCommand cmd= new SqlCommand(sql,conn);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader reader=cmd.ExecuteReader();
                while(reader.Read())
                {
                    course=new Course(id,this.getUserById(reader.GetInt32(1)),reader.GetString(2),reader.GetString(3),reader.GetInt32(4),reader.GetInt32(5));
                }
                conn.Close();
            }

            return course;
        }

        public bool hasDoneExam(User student,Course course)
        {
            int done = 0;
            using (SqlConnection conn = new SqlConnection(Constants.DB_CONNECTION_STRING))
            {
                conn.Open();
                string sql = @"SELECT COUNT(e.id) FROM [Exam] e, [Assignment] a WHERE e.course_id=a.course_id
                            AND a.course_id=@course_id AND a.student_id=@student_id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@course_id",course.id);
                cmd.Parameters.AddWithValue("@student_id", student.id);
                done=Convert.ToInt32(cmd.ExecuteScalar().ToString());
                    

                conn.Close();
            }

            return (done != 0);
        }

        public List<Exam> getExamsByCourse(Course course)
        {
            List<Exam> exams = new List<Exam>();
            //List<Question> q = null;
            using (SqlConnection conn = new SqlConnection(Constants.DB_CONNECTION_STRING))
            {
                conn.Open();

                string sql = "SELECT * FROM [Exam] WHERE course_id=@course_id ORDER BY date DESC";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@course_id", course.id);
                SqlDataReader reader=cmd.ExecuteReader();
                while(reader.Read())
                {
                   // Course c = this.getCourseById(reader.GetInt32(1));
                    exams.Add( new Exam(reader.GetInt32(0), course, reader.GetDateTime(2), reader.GetInt32(3), reader.GetFloat(4), null, reader.GetInt32(5)));
                    //exams.Add(new Exam(reader.GetInt32(0), this.getCourseById(reader.GetInt32(1)),reader.GetDateTime(2),reader.GetInt32(3),reader.GetFloat(4),null,reader.GetInt32(5)));
                }
                

                conn.Close();
            }
            //return exams;
            return (exams.Count>0)?exams:null;
        }

        public List<Grade> getGradesByStudent(User student)
        {
            List<Grade> grades = new List<Grade>();
            using (SqlConnection conn = new SqlConnection(Constants.DB_CONNECTION_STRING))
            {

                conn.Open();

                string sql = "SELECT * FROM [Grade] WHERE student_id=@student_id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@student_id", student.id);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    grades.Add(new Grade(reader.GetInt32(0), this.getExamById(reader.GetInt32(1)), this.getUserById(reader.GetInt32(2)), reader.GetFloat(3)));
                }

                conn.Close();

            }

            return (grades.Count>0)?grades:null;

        }

        public Exam getExamById(int id)
        {
            Exam exam = null;
            using (SqlConnection conn = new SqlConnection(Constants.DB_CONNECTION_STRING))
            {
                conn.Open();
                string sql = "SELECT * FROM [Exam] WHERE id=@id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Course course = this.getCourseById(reader.GetInt32(1));
                    List<Question> questions = this.getQuestionsByCourseId(course.id);
                    exam = new Exam(reader.GetInt32(0),course,reader.GetDateTime(2),reader.GetInt32(3),reader.GetFloat(4),questions,reader.GetInt32(5));
                }
                conn.Close();
            }

            return exam;
        }



        /*------------------------------------------------------------YEHYA----------------------------------------------*/

        public bool addQuestion(Question question)
        {
            int updated = -1;//added(OMAR)
            int question_id;
            int correct_option_id = -1; //set to -1 since later will find the correct option id when inserting the options
            using (SqlConnection conn = new SqlConnection(Constants.DB_CONNECTION_STRING))
            {
                conn.Open();
                string sql = "INSERT INTO Question(course_id,text,correct_option_id,grade) " +
                          "OUTPUT INSERTED.id " +
                          "VALUES(@course_id,@text,@correct_option_id,@grade)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@course_id", question.course.id);
                cmd.Parameters.AddWithValue("@text", question.text);
                cmd.Parameters.AddWithValue("@correct_option_id", correct_option_id);
                cmd.Parameters.AddWithValue("@grade", question.grade);

                //executing and getting the question id value to set to the options
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    question_id = (int)result;
                }
                else
                {
                    return false;//added (OMAR)
                    //throw new Exception("Failed to insert a Question and retrieve question_id.");
                }

                foreach (Option option in question.options)
                {
                    sql = "INSERT INTO [Option] (question_id,text,isCorrect) " +
                          "OUTPUT INSERTED.id " +
                          "VALUES(@question_id,@text,@isCorrect)";

                    cmd = new SqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@question_id", question_id);
                    cmd.Parameters.AddWithValue("@text", option.text);
                    cmd.Parameters.AddWithValue("@isCorrect", option.isCorrect);

                    //getting the inserted option id each time to set the correct one to the question's correct_option_id
                    int option_id = (int)cmd.ExecuteScalar();

                    if(option_id==0) return false;//ADDED(OMAR)
                    
                    if (option.isCorrect == 1)
                    {
                        correct_option_id = option_id;
                    }
                }

                //update the question with correct_option_id
                sql = "UPDATE Question SET correct_option_id = @correct_option_id" +
                      " WHERE id = @question_id";
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@question_id", question_id);
                cmd.Parameters.AddWithValue("@correct_option_id", correct_option_id);
                updated=cmd.ExecuteNonQuery();

                conn.Close();
                //return true;
            }
            return (updated >= 0);//ADDED(OMAR)
        }


        public List<Option> getOptionsByQuestionId(int question_id)
        {
            List<Option> options = new List<Option>();
            using (SqlConnection conn = new SqlConnection(Constants.DB_CONNECTION_STRING))
            {
                conn.Open();
                string sql = "SELECT * FROM [Option] WHERE question_id=@question_id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@question_id", question_id);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Option option = new Option(reader.GetInt32(0), question_id, reader.GetString(2), reader.GetInt32(3));
                    options.Add(option);
                }
                conn.Close();
            }
            return (options.Count>0)?options:null;
        }

        public List<Question> getQuestionsByCourseId(int course_id)
        {
            List<Question> questions = new List<Question>();
            using (SqlConnection conn = new SqlConnection(Constants.DB_CONNECTION_STRING))
            {
                conn.Open();
                string sql = "SELECT * FROM [Question] WHERE course_id=@course_id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@course_id", course_id);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Question question = new Question(reader.GetInt32(0), this.getCourseById(reader.GetInt32(1)), reader.GetString(2),this.getOptionsByQuestionId(reader.GetInt32(0)), reader.GetFloat(4));
                    questions.Add(question);
                }
                conn.Close();
            }
            return (questions.Count>0)?questions:null;
        }


        public Question getQuestionById(int id)
        {
            Question question = null;
            using (SqlConnection conn = new SqlConnection(Constants.DB_CONNECTION_STRING))
            {
                conn.Open();
                string sql = "SELECT * FROM [Question] WHERE id=@id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    question = new Question(id, null, reader.GetString(2), this.getOptionsByQuestionId(id), reader.GetFloat(4));
                }
                conn.Close();
            }
            return question;
        }

        public bool addExam(Exam exam)
        {
            int exam_id, val = 0;
            using (SqlConnection conn = new SqlConnection(Constants.DB_CONNECTION_STRING))
            {
                conn.Open();
                string sql = "INSERT INTO Exam(course_id,date,duration,grade,nbQuestions) " +
                          "OUTPUT INSERTED.id " +
                          "VALUES(@course_id,@date,@duration,@grade,@nbQuestions)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@course_id", exam.course.id);
                cmd.Parameters.AddWithValue("@date", exam.date.Date);
                cmd.Parameters.AddWithValue("@duration", exam.duration);
                cmd.Parameters.AddWithValue("@grade", exam.grade);
                cmd.Parameters.AddWithValue("@nbQuestions", exam.nbQuestions);

                //get the exam's id to set to the ExamQuestions corresponding to this exam
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    exam_id = (int)result;
                }
                else
                {
                    //throw new Exception("Failed to insert Exam record and retrieve exam_id.");
                    return false;
                }

                for (int j = 0; j < exam.nbQuestions; j++)
                {
                    sql = "INSERT INTO ExamQuestion(question_id,exam_id) " +
                          "OUTPUT INSERTED.id " +
                          "VALUES(@question_id,@exam_id)";

                    cmd = new SqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@question_id", exam.questions[j].id);
                    cmd.Parameters.AddWithValue("@exam_id", exam_id);

                    val = cmd.ExecuteNonQuery();

                    if (val <= 0)
                    {
                        //throw new Exception("Failed to insert ExamQuestion record.");
                        return false;
                    }
                }
                conn.Close();
               // return (val != 0);
            }
            return (val != 0);
        }

        public int getNbQuestionsPerCourse(int course_id)
        {
            int result = 0;
            using (SqlConnection conn = new SqlConnection(Constants.DB_CONNECTION_STRING))
            {
                conn.Open();
                string sql = "SELECT COUNT(*) FROM [Question] WHERE course_id=@course_id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@course_id", course_id);
                result = (int)cmd.ExecuteScalar();
                conn.Close();
            }
            return result;
        }

        public bool removeQuestion(int id)
        {
            int removed = 0;//(ADDED(OMAR))
            using (SqlConnection conn = new SqlConnection(Constants.DB_CONNECTION_STRING))
            {
                conn.Open();
                string sql = "SELECT COUNT(*) FROM [ExamQuestion] WHERE question_id=@question_id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@question_id", id);
                int count = (int)cmd.ExecuteScalar();

                //if there exists exams having this question then it won't be deleted
                if (count > 0)
                {
                    return false;
                }

                sql = "DELETE FROM [Option] WHERE question_id=@id";
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
               removed= cmd.ExecuteNonQuery();//ADDED(OMAR)
                if (removed <= 0) return false;
                sql = "DELETE FROM [Question] WHERE id = @id";
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                removed=cmd.ExecuteNonQuery();//ADDED(OMAR)
                conn.Close();
            }
            //return true;
            return (removed > 0);
        }



        public bool updateQuestion(Question question)
        {
            int updated = -1;
            int correct_option_id = -1;
            using (SqlConnection conn = new SqlConnection(Constants.DB_CONNECTION_STRING))
            {
                conn.Open();
                string sql;
                SqlCommand cmd;
                List<Option> options = question.options;
                foreach (Option option in options)
                {
                    sql = "UPDATE [Option] SET text = @text , isCorrect = @isCorrect WHERE id = @id";
                    cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@text", option.text);
                    cmd.Parameters.AddWithValue("@isCorrect", option.isCorrect);
                    cmd.Parameters.AddWithValue("@id", option.id);
                    cmd.ExecuteNonQuery();
                    if (option.isCorrect == 1)
                    {
                        correct_option_id = option.id;
                    }
                }


                sql = "UPDATE [Question] SET text = @text, grade = @grade, correct_option_id = @correct_option_id WHERE id = @id";
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@text", question.text);
                //cmd.Parameters.AddWithValue("@grade", Convert.ToFloat(question.grade));
                cmd.Parameters.AddWithValue("@grade", Convert.ToDouble(question.grade));
                cmd.Parameters.AddWithValue("@correct_option_id", correct_option_id);
                cmd.Parameters.AddWithValue("@id", question.id);
                updated=cmd.ExecuteNonQuery();

                conn.Close();
            }
            //return true;
            return (updated >= 0);
        }


        /*--------------------------------------Abdulrahman------------------------------------------------------------*/

        public List<Exam> getExamsByStudentId(int student_id)
        {
            List<Exam> exams = new List<Exam>();
            using (SqlConnection conn = new SqlConnection(Constants.DB_CONNECTION_STRING))
            {

                conn.Open();
                string sql = "SELECT E.* FROM [Exam] E, [User] U, Assignment A " +
                    "WHERE U.id=@student_id AND U.id=A.student_id AND A.course_id=E.course_id AND A.hasPassed=0";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@student_id", student_id);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Course course = this.getCourseById(reader.GetInt32(1));
                    exams.Add(new Exam(reader.GetInt32(0), course, reader.GetDateTime(2), reader.GetInt32(3), reader.GetFloat(4), null, reader.GetInt32(5)));
                }
                conn.Close();
            }
            return exams;
        }


        public int addGrade(Grade grade)
        {
            int rowsAffected = 0;
            int passed;
            //if the student passed the exam
            if (grade.value >= 50) passed = 1;
            else passed = 0;
            using (SqlConnection conn = new SqlConnection(Constants.DB_CONNECTION_STRING))
            {
                conn.Open();
                string sql = "INSERT INTO Grade(exam_id, student_id, value) VALUES(@exam_id, @student_id, @value);" +
                    "UPDATE Assignment SET hasPassed=@passed WHERE student_id=@student_id AND course_id=@course_id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@exam_id", grade.exam.id);
                cmd.Parameters.AddWithValue("@student_id", grade.student.id);
                cmd.Parameters.AddWithValue("@value", Math.Round(grade.value, 2));
                cmd.Parameters.AddWithValue("@passed", passed);
                cmd.Parameters.AddWithValue("@course_id", grade.exam.course.id);
                 rowsAffected = cmd.ExecuteNonQuery();
                conn.Close();
            }

            return rowsAffected;
        }

        public int isDoneExam(int exam_id, int student_id)
        {
            int isDone = 0;
            using (SqlConnection conn = new SqlConnection(Constants.DB_CONNECTION_STRING))
            {
                conn.Open();
                string sql = "SELECT 1 FROM Grade WHERE exam_id=@exam_id AND student_id=@student_id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@exam_id", exam_id);
                cmd.Parameters.AddWithValue("@student_id", student_id);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    isDone = reader.GetInt32(0);
                }
                conn.Close();
            }
            return isDone;
        }

    }
}