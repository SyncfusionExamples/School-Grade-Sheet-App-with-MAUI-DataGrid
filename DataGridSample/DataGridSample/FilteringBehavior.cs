using Syncfusion.Maui.DataGrid;

namespace DataGridSample
{
    public class FilteringBehavior : Behavior<SfDataGrid>
    {
        SfDataGrid dataGrid;
        GradeSheetViewModel gradeSheetViewModel;

        protected override void OnAttachedTo(SfDataGrid bindable)
        {
            base.OnAttachedTo(bindable);
            dataGrid = bindable;
            dataGrid.Loaded += DataGridLoaded;
        }

        private void DataGridLoaded(object? sender, EventArgs e)
        {
            gradeSheetViewModel = dataGrid.BindingContext as GradeSheetViewModel;
            gradeSheetViewModel.Filtertextchanged += OnFilterChanged;
        }

        public void OnFilterChanged()
        {
            if(dataGrid!.View != null)
            {
                dataGrid.View.Filter = gradeSheetViewModel!.FilterRecords;
                dataGrid.View.RefreshFilter();
            }
        }

        protected override void OnDetachingFrom(SfDataGrid bindable)
        {
            base.OnDetachingFrom(bindable);
            dataGrid.Loaded -= DataGridLoaded;
            gradeSheetViewModel.Filtertextchanged -= OnFilterChanged;
            gradeSheetViewModel = null;
        }
    }
}
