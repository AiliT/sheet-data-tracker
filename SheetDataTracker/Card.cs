using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace SheetDataTracker
{
    public class Card : INotifyPropertyChanged
    {
        public string Name { get; set; } = "";
        public ObservableCollection<IDataRow> DataRows { get; } = new ObservableCollection<IDataRow>();

        public event PropertyChangedEventHandler? PropertyChanged;

        public RelayCommand<object> DeleteRowCommand { get; private set; }

        public Card()
        {
            DataRows.CollectionChanged += (o, eventArgs) => NotifyPropertyChanged(nameof(DataRows));
            
            DeleteRowCommand = new RelayCommand<object>((obj) => DataRows.Remove(obj as IDataRow));
        }

        public void AddDataRow()
        {
            DataRows.Add(new DataRow());
        }

        public void AddConstantDataRow()
        {
            DataRows.Add(new ConstantDataRow());
        }

        public void UpdateVariableValues()
        {
            foreach (var dataRow in DataRows)
            {
                if (dataRow is DataRow variableRow)
                    variableRow.Update();
            }
        }

        public void Reset()
        {
            foreach (var dataRow in DataRows)
            {
                if (dataRow is DataRow variableRow)
                    variableRow.Reset();
            }
        }

        public void NotifyPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
