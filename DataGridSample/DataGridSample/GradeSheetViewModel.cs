using Syncfusion.Maui.Data;
using Syncfusion.Maui.DataGrid;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;

namespace DataGridSample
{
    public class GradeSheetViewModel : INotifyPropertyChanged
    {
        private bool isOpenForFilterColumns;
        public bool IsOpenForFilterColumns
        {
            get { return isOpenForFilterColumns; }
            set
            {
                isOpenForFilterColumns = value;
                OnPropertyChanged(nameof(IsOpenForFilterColumns));
            }
        }

        private bool isOpenForSearch;
        public bool IsOpenForSearch
        {
            get { return isOpenForSearch; }
            set
            {
                isOpenForSearch = value;
                OnPropertyChanged(nameof(IsOpenForSearch));
            }
        }

        private string? filtertext = string.Empty;
        public string? FilterText
        {
            get { return filtertext; }
            set
            {
                filtertext = value;
                OnFilterTextChanged();
                OnPropertyChanged("FilterText");
            }
        }

        internal delegate void FilterChanged();

        internal FilterChanged? Filtertextchanged;

        private void OnFilterTextChanged()
        {
            if(Filtertextchanged != null)
            {
                Filtertextchanged();
            }
        }

        private GridColumn? selectedColumn;
        public GridColumn? SelectedColumn
        {
            get { return selectedColumn; }
            set
            {
                selectedColumn = value;
                OnPropertyChanged(nameof(SelectedColumn));
            }
        }

        private string? selectedcondition = "Contains";
        public string? SelectedCondition
        {
            get { return selectedcondition; }
            set
            {
                selectedcondition = value;
                OnPropertyChanged(nameof(SelectedCondition));
            }
        }

        public List<GridColumn> GridColumnsForFilter { get; set; }
        public List<string> SearchConditions { get; set; }
        public ICommand FilterColumns { get ; set; }
        public ICommand ClearFilter { get; set; }

        private bool isOpenForGroupColumns;
        public bool IsOpenForGroupColumns
        {
            get { return isOpenForGroupColumns; }
            set
            {
                isOpenForGroupColumns = value;
                OnPropertyChanged(nameof(IsOpenForGroupColumns));
            }
        }

        private GridColumn? selectedGroupColumn;
        public GridColumn? SelectedGroupColumn
        {
            get { return selectedGroupColumn; }
            set
            {
                selectedGroupColumn = value;
                ExecuteAddGrouping();
                OnPropertyChanged(nameof(SelectedGroupColumn));
            }
        }

        private GroupColumnDescriptionCollection groupColumns;
        public GroupColumnDescriptionCollection GroupColumnDescriptions
        {
            get { return groupColumns; }
            set
            {
                groupColumns = value;
                OnPropertyChanged(nameof(GroupColumnDescriptions));
            }
        }

        public List<GridColumn> GridColumnsForGroup {  get; set; }
        public ICommand GroupColumns {  get; set; }
        public ICommand ClearGroup { get; set; }

        private void ExecuteAddGrouping()
        {
            if(SelectedGroupColumn != null)
            {
                GroupColumnDescriptions.Clear();
                var groupColumnDescription = new GroupColumnDescription()
                {
                    ColumnName = SelectedGroupColumn.Name
                };

                GroupColumnDescriptions.Add(groupColumnDescription);
            }
        }

        public List<GridColumn> GridColumns { get; set; }
        private bool isOpenForSortColumns;
        public bool IsOpenForSortColumns
        {
            get {  return isOpenForSortColumns; } 
            set 
            { 
                isOpenForSortColumns = value; 
                OnPropertyChanged(nameof(IsOpenForSortColumns));
            }
        }

        private bool isOnState;
        public bool IsOnState
        {
            get { return isOnState; }
            set
            {
                isOnState = value;
                ExecuteAddSorting();
                OnPropertyChanged(nameof(IsOnState));
            }
        }

        private GridColumn? selectedSortColumn;
        public GridColumn? SelectedSortColumn
        {
            get { return selectedSortColumn; }
            set
            {
                selectedSortColumn = value;
                ExecuteAddSorting();
                OnPropertyChanged(nameof(SelectedSortColumn));
            }
        }

        private SortColumnDescriptionCollection sortColumns;
        public SortColumnDescriptionCollection SortColumnDescriptions
        {
            get { return sortColumns; }
            set
            {
                sortColumns = value;
                OnPropertyChanged(nameof (SortColumnDescriptions));
            }
        }

        public List<GridColumn> GridColumnsForSort {  get; set; }

        public ICommand SortColumns {  get; set; }
        public ICommand ClearSort { get; set; }

        private void InitializeCommands()
        {
            FilterColumns = new Command(ExecuteFilterColumns);
            ClearFilter = new Command(ExecuteClearFilter);
            GroupColumns = new Command(ExecuteGroupColumns);
            ClearGroup = new Command(ExecuteClearGroups);
            SortColumns = new Command(ExecuteSortColumns);
            ClearSort = new Command(ExecuteClearSorts);
        }

        private void ExecuteClearFilter(object obj)
        {
            SelectedCondition = string.Empty;
            SelectedColumn = null;
            FilterText = string.Empty;
            Application.Current?.Dispatcher.Dispatch(() =>
            {
                IsOpenForFilterColumns = false;
            });
        }

        private void PopulateSearchCriteria()
        {
            SearchConditions = new List<string>
            {
                "Contains",
                "Equals",
                "Does Not Equal"
            };
        }

        private void ExecuteFilterColumns(object obj)
        {
            IsOpenForFilterColumns = true;
        }

        private void ExecuteClearGroups()
        {
            if(GroupColumnDescriptions == null)
            {
                return;
            }
            GroupColumnDescriptions.Clear();
            SelectedGroupColumn = null;
            Application.Current?.Dispatcher.Dispatch(() =>
            {
                IsOpenForGroupColumns = false;
            });
        }

        private void ExecuteGroupColumns()
        {
            IsOpenForGroupColumns = true;
        }

        private void ExecuteClearSorts(object obj)
        {
            if(SortColumnDescriptions == null)
            {
                return;
            }

            SortColumnDescriptions.Clear();
            SelectedSortColumn = null;
            Application.Current?.Dispatcher.Dispatch(() =>
            {
                IsOpenForSortColumns = false;
            });
        }

        private void ExecuteSortColumns(object obj)
        {
            IsOpenForSortColumns = true;
        }

        private void InitializeSortGroupColumnDescriptions()
        {
            SortColumnDescriptions = new SortColumnDescriptionCollection();
            GroupColumnDescriptions = new GroupColumnDescriptionCollection();
        }

        private void PopulateColumnNames()
        {
            var type = typeof(Grade);
            var properties = type.GetProperties();
            GridColumns = new List<GridColumn>();
            GridColumnsForFilter = new List<GridColumn>();
            GridColumnsForGroup = new List<GridColumn>();
            GridColumnsForSort = new List<GridColumn>();
            GridColumnsForFilter.Add(new GridColumn()
            {
                Name = "All Columns",
                DisplayName = "All Columns"
            });
            columns.ForEach(o =>
            {
                var column = new GridColumn()
                {
                    Name = o.MappingName,
                    DisplayName = o.HeaderText
                };
                GridColumns.Add(column);
            });

            GridColumnsForFilter.AddRange(GridColumns);
            GridColumnsForGroup.AddRange(GridColumns);
            GridColumnsForSort.AddRange(GridColumns);
            GridColumns.RemoveAt(0);
            GridColumnsForFilter.Remove(GridColumnsForFilter.FirstOrDefault(o => o.Name == "StudentID"));
            SelectedColumn = GridColumnsForFilter.FirstOrDefault();
            SelectedGroupColumn = GridColumnsForGroup.FirstOrDefault();
            selectedSortColumn = GridColumnsForSort.FirstOrDefault();
        }

        private void ExecuteAddSorting()
        {
            if (SelectedSortColumn != null)
            {
                SortColumnDescriptions.Clear();
                var sortColumnDescription = new SortColumnDescription() { ColumnName = this.SelectedSortColumn.Name, SortDirection = IsOnState ? ListSortDirection.Ascending : ListSortDirection.Descending };
                SortColumnDescriptions.Add(sortColumnDescription);
            }
        }

        private ColumnCollection columns;
        public ColumnCollection Columns
        {
            get
            {
                return columns;
            }
            set
            {
                columns = value;
                OnPropertyChanged(nameof(Columns));
            }
        }
        public GradeSheetViewModel()
        {
            PopulateCollection();
            InitializeColumns();
            InitializeCommands();
            PopulateSearchCriteria();
            InitializeSortGroupColumnDescriptions();
            PopulateColumnNames();
        }

        private ObservableCollection<Grade> grades;

        public ObservableCollection<Grade> Grades
        {
            get
            {
                return grades;
            }
            set
            {
                grades = value;
                OnPropertyChanged(nameof(Grades));
            }
        }

        string[] studentNames = { "Alice Johnson", "Bob Smith", "Charlie Brown", "Diana Prince", "Evan Davis",
                              "Faith Wilson", "George Harris", "Helen Moore", "Ian Clark", "Jenny Lewis",
                              "Kyle Robinson", "Laura Scott", "Martin King", "Nina Adams", "Oscar Perez",
                              "Paula Turner", "Quincy Bell", "Rachel Cox", "Steven Wright", "Tracy Mills" };

        int[] studentIDs = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };

        string[] subjectNames = { "Math", "Science", "History", "English", "Art" };

        // Arrays for grade properties (length should match the number of students * subjects for expanded data)
        int[] assignmentScores = { 90, 78, 85, 92, 88, 95, 80, 86, 81, 82, 88, 79, 84, 92, 80, 90, 83, 87, 91, 85 };
        int[] quizScores = { 85, 82, 80, 88, 80, 90, 75, 87, 84, 79, 91, 82, 88, 90, 81, 92, 86, 89, 87, 88 };
        int[] examScores = { 88, 80, 87, 91, 85, 93, 79, 89, 86, 81, 89, 83, 87, 95, 83, 88, 85, 86, 88, 84 };
        int[] projectScores = { 92, 85, 90, 95, 87, 97, 83, 88, 90, 84, 92, 85, 89, 94, 86, 91, 88, 90, 93, 89 };
        string[] comments = {
        "Excellent performance", "Needs improvement", "Good effort", "Outstanding", "Satisfactory",
        "Great participation", "Study more", "Consistent work", "Keep up the good work", "Focus more",
        "Excellent progress", "Shows interest", "Needs more practice", "Impressive work", "Catch up on assignments",
        "Solid understanding", "Achieved beyond expectations", "Room for improvement", "Good participation", "Well done" };

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private void PopulateCollection()
        {
            // Populate Grades collection
            Grades = new ObservableCollection<Grade>();
            for (int i = 0; i < studentNames.Length; i++)
            {
                Grades.Add(new Grade
                {
                    ID = studentIDs[i],
                    StudentName = studentNames[i],
                    SubjectName = subjectNames[i % subjectNames.Length],
                    AssignmentScore = assignmentScores[i],
                    QuizScore = quizScores[i],
                    ExamScore = examScores[i],
                    ProjectScore = projectScores[i],
                    Comments = comments[i],
                });
            }
        }

        private void InitializeColumns()
        {
            columns = new ColumnCollection();
            var studentNameIdUnboundColumn = new DataGridUnboundColumn
            {
                MappingName = "StudentID",
                HeaderText = "Student ID"
            };
            columns.Add(studentNameIdUnboundColumn);
            var studentIdColumn = new DataGridTextColumn
            {
                MappingName = "ID",
                HeaderText = "ID"
            };
            columns.Add(studentIdColumn);
            var studentNameColumn = new DataGridTextColumn
            {
                MappingName = "StudentName",
                HeaderText = "Student Name"
            };
            columns.Add(studentNameColumn);
            var courseNameColumn = new DataGridTextColumn
            {
                MappingName = "SubjectName",
                HeaderText = "Course Name"
            };
            columns.Add(courseNameColumn);
            var assignmentScoreColumn = new DataGridNumericColumn
            {
                MappingName = "AssignmentScore",
                HeaderText = "Assignment Score"
            };
            columns.Add(assignmentScoreColumn);
            var quizScoreColumn = new DataGridNumericColumn
            {
                MappingName = "QuizScore",
                HeaderText = "Quiz Score"
            };
            columns.Add(quizScoreColumn);
            var examScoreColumn = new DataGridNumericColumn
            {
                MappingName = "ExamScore",
                HeaderText = "Exam Score"
            };
            columns.Add(examScoreColumn);
            var projectScoreColumn = new DataGridNumericColumn
            {
                MappingName = "ProjectScore",
                HeaderText = "Project Score"
            };
            columns.Add(projectScoreColumn);
            var gradeColumn = new DataGridUnboundColumn
            {
                MappingName = "Grade",
                HeaderText = "Grade"
            };
            columns.Add(gradeColumn);
            var commentsColumn = new DataGridTextColumn
            {
                MappingName = "Comments",
                HeaderText = "Comments"
            };
            columns.Add(commentsColumn);
        }

        public bool FilterRecords(object obj)
        {
            if(SelectedColumn == null && string.IsNullOrEmpty(SelectedCondition))
            {
                return true;
            }

            double res;
            bool checkNumeric = double.TryParse(FilterText, out res);
            var item = obj as Grade;
            if (item != null && SelectedColumn != null)
            {
                if(checkNumeric && SelectedColumn.Name!.Equals("All Columns") && SelectedCondition!.Equals("Contains"))
                {
                    bool result = MakeNumericFilter(item, SelectedColumn.Name, SelectedCondition);
                    return result;
                }
                else if(SelectedColumn.Name!.Equals("All Columns"))
                {
                    if (item.ID!.ToString().ToLower().Contains(FilterText!.ToLower()) ||
                    item.StudentName!.ToString().ToLower().Contains(FilterText.ToLower()) ||
                    item.SubjectName!.ToString().ToLower().Contains(FilterText.ToLower()) ||
                    item.AssignmentScore!.ToString().ToLower().Contains(FilterText.ToLower()) ||
                    item.QuizScore!.ToString().ToLower().Contains(FilterText.ToLower()) ||
                    item.ExamScore!.ToString().ToLower().Contains(FilterText.ToLower()) ||
                    item.ProjectScore!.ToString().ToLower().Contains(FilterText.ToLower()) ||
                    item.Comments!.ToString().ToLower().Contains(FilterText.ToLower()))
                    {
                        return true;
                    }
                    return false;
                }
                else
                {
                    bool result = MakeStringFilter(item, SelectedColumn.Name, SelectedCondition!);
                    return result;
                }
            }

            return false;
        }

        private bool MakeStringFilter(Grade item, string name, string? selectedCondition)
        {
            var value = item.GetType().GetProperty(name);
            var exactValue = value!.GetValue(item, null);
            exactValue = exactValue!.ToString()!.ToLower();
            string text = FilterText!.ToLower();
            var methods = typeof(string).GetMethods();

            if (methods.Count() != 0)
            {
                if (selectedCondition == "Contains")
                {
                    var methodInfo = methods.FirstOrDefault(method => method.Name == selectedCondition);
                    bool result = (bool)methodInfo!.Invoke(exactValue!, new object[]
                    {
                        text 
                    })!;
                    return result;
                }
                else if(exactValue.ToString() == text.ToString())
                {
                    bool result = string.Equals(exactValue.ToString(), text.ToString());
                    if(selectedCondition == "Equals")
                    {
                        return result;
                    }
                    else if(selectedCondition == "Does Not Equal")
                    {
                        return false;
                    }
                }
                else if(selectedCondition == "Does Not Equal")
                {
                    return true;
                }
                return false;
            }
            else
            {
                return false;
            }
        }

        private bool MakeNumericFilter(Grade item, string option, string condition)
        {
            var value = item.GetType().GetProperty(option);
            var exactValue = value!.GetValue(item, null);
            double res;
            bool checkNumeric = double.TryParse(exactValue!.ToString(), out res);
            if(checkNumeric)
            {
                switch(condition)
                {
                    case "Equals":
                        try
                        {
                            if(exactValue.ToString() == FilterText)
                            {
                                if(Convert.ToDouble(exactValue) == Convert.ToDouble(FilterText))
                                {
                                    return true;
                                }
                            }
                        }
                        catch(Exception ex)
                        {
                            Debug.WriteLine(ex.Message);
                        }
                        break;
                    case "Does Not Equal":
                        try
                        {
                            if(Convert.ToDouble(FilterText) != Convert.ToDouble(exactValue))
                            {
                                return true;
                            }
                        }
                        catch(Exception ex)
                        {
                            Debug.WriteLine(ex.Message);
                            return true;
                        }
                        break;
                }
            }

            return false;
        }
    }

    public class GridColumn : INotifyPropertyChanged
    {
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        internal void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
