using System.ComponentModel;

namespace DataGridSample
{
    public class Grade : INotifyPropertyChanged
    {
        private int _id;
        private string _studentName = string.Empty;
        private string _subjectName = string.Empty;
        private double _assignmentScore;
        private double _quizScore;
        private double _examScore;
        private double _projectScore;
        private string _comments = string.Empty;

        public int ID
        {
            get => _id;
            set
            {
                if (_id == value) return;
                _id = value;
                OnPropertyChanged(nameof(ID));
            }
        }

        public string StudentName
        {
            get => _studentName;
            set
            {
                if (_studentName == value) return;
                _studentName = value;
                OnPropertyChanged(nameof(StudentName));
            }
        }

        public string SubjectName
        {
            get => _subjectName;
            set
            {
                if (_subjectName == value) return;
                _subjectName = value;
                OnPropertyChanged(nameof(SubjectName));
            }
        }

        public double AssignmentScore
        {
            get => _assignmentScore;
            set
            {
                if (_assignmentScore == value) return;
                _assignmentScore = value;
                OnPropertyChanged(nameof(AssignmentScore));
            }
        }

        public double QuizScore
        {
            get => _quizScore;
            set
            {
                if (_quizScore == value) return;
                _quizScore = value;
                OnPropertyChanged(nameof(QuizScore));
            }
        }

        public double ExamScore
        {
            get => _examScore;
            set
            {
                if (_examScore == value) return;
                _examScore = value;
                OnPropertyChanged(nameof(ExamScore));
            }
        }

        public double ProjectScore
        {
            get => _projectScore;
            set
            {
                if (_projectScore == value) return;
                _projectScore = value;
                OnPropertyChanged(nameof(ProjectScore));
            }
        }

        public string Comments
        {
            get => _comments;
            set
            {
                if (_comments == value) return;
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
            return (AssignmentScore + QuizScore + ExamScore + ProjectScore) / 4.0;
        }
    }
}
