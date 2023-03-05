using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace SheetDataTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public int RoundCounter { get; set; } = 1;

        public ObservableCollection<Card> Cards { get; set; } = new ObservableCollection<Card>();

        public RelayCommand AddCardCommand { get; private set; }
        public RelayCommand ResetCommand { get; private set; }
        public RelayCommand NextRoundCommand { get; private set; }
        public RelayCommand ResetRoundsCommand { get; private set; }
        public RelayCommand<object> DeleteCardCommand { get; private set; }
        public RelayCommand<object> AddVariableRowCommand { get; private set; }
        public RelayCommand<object> AddConstantRowCommand { get; private set; }


        public MainWindow()
        {
            InitializeComponent();

            AddCardCommand = new RelayCommand(() => Cards.Add(new Card()));
            ResetCommand = new RelayCommand(() =>
            {
                foreach (var card in Cards)
                {
                    card.Reset();
                }
                //OnPropertyChanged(nameof(Cards));
            });
            NextRoundCommand = new RelayCommand(() =>
            {
                RoundCounter++;
                OnPropertyChanged(nameof(RoundCounter));

                foreach (var card in Cards)
                {
                    card.UpdateVariableValues();
                }
            });
            ResetRoundsCommand = new RelayCommand(() =>
            {
                RoundCounter = 1;
                OnPropertyChanged(nameof(RoundCounter));
            });
            DeleteCardCommand = new RelayCommand<object>((obj) => Cards.Remove(obj as Card));
            AddVariableRowCommand = new RelayCommand<object>((obj) => (obj as Card).AddDataRow());
            AddConstantRowCommand = new RelayCommand<object>((obj) => (obj as Card).AddConstantDataRow());

            DataContext = this;
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void TextBox_KeyEnterUpdate(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TextBox tBox = (TextBox)sender;
                DependencyProperty prop = TextBox.TextProperty;

                BindingExpression binding = BindingOperations.GetBindingExpression(tBox, prop);
                if (binding != null) { binding.UpdateSource(); }
            }
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Dispatcher.BeginInvoke(new Action(() => textBox.SelectAll()));
        }
    }
}
