using System.ComponentModel;

namespace DataGridSample
{
    public class Grade
    {
        private int _id;
        private string _studentName;
        private string _subjectName;
        private double _assignmentScore;
        private double _quizScore;
        private double _examScore;
        private double _projectScore;
        private string _comments;

        public int ID
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
                OnPropertyChanged(nameof(ID));
            }
        }

        public string StudentName
        {
            get
            {
                return _studentName;
            }
            set
            {
                _studentName = value;
                OnPropertyChanged(nameof(StudentName));
            }
        }

        public string SubjectName
        {
            get
            {
                return _subjectName;
            }
            set
            {
                _subjectName = value;
                OnPropertyChanged(nameof(SubjectName));
            }
        }

        public double AssignmentScore
        {
            get
            {
                return _assignmentScore;
            }
            set
            {
                _assignmentScore = value;
                OnPropertyChanged(nameof(AssignmentScore));
            }
        }

        public double QuizScore
        {
            get
            {
                return _quizScore;
            }
            set
            {
                _quizScore = value;
                OnPropertyChanged(nameof(QuizScore));
            }
        }

        public double ExamScore
        {
            get
            {
                return _examScore;
            }
            set
            {
                _examScore = value;
                OnPropertyChanged(nameof(ExamScore));
            }
        }

        public double ProjectScore
        {
            get
            {
                return _projectScore;
            }
            set
            {
                _projectScore = value;
                OnPropertyChanged(nameof(ProjectScore));
            }
        }

        public string Comments
        {
            get
            {
                return _comments;
            }
            set
            {
                _comments = value;
                OnPropertyChanged(nameof(Comments));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public double CalculateFinalGrade()
        {
            // Example calculation - adjust based on actual needs and weightage
            return (AssignmentScore + QuizScore + ExamScore + ProjectScore) / 4;
        }
    }
}
