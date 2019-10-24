using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CascadingPicker
{
    public partial class MainPage : ContentPage
    {
        string CurrentItem;
        public MainPage()
        {
            InitializeComponent();
        }

        private void picker_SelectionChanged(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            if (picker.ItemsSource != null && e.NewValue is IList && (picker.ItemsSource as IList).Count > 1 && CurrentItem != (e.NewValue as IList)[0].ToString())
            {
                //Updated the second column collection based on first column selected value.
                (picker.ItemsSource as ObservableCollection<object>).RemoveAt(1);
                (picker.ItemsSource as ObservableCollection<object>).Add(GetCountry((e.NewValue as IList)[0].ToString()));
            }
        }

        public ObservableCollection<object> GetCountry(string CountryName)
        {
            CurrentItem = CountryName;
            ObservableCollection<object> selectedCountries = new ObservableCollection<object>();
            if (CountryName == "UK")
            {
                selectedCountries.Add("London");
                selectedCountries.Add("Manchester");
                selectedCountries.Add("Cambridge");
                selectedCountries.Add("Edinburgh");
                selectedCountries.Add("Glasgow");
                selectedCountries.Add("Birmingham");
            }
            else if (CountryName == "USA")
            {
                selectedCountries.Add("New York");
                selectedCountries.Add("Seattle");
                selectedCountries.Add("Wasington");
                selectedCountries.Add("Chicago");
                selectedCountries.Add("Boston");
                selectedCountries.Add("Los Angles");
            }
            else if (CountryName == "UAE")
            {
                selectedCountries.Add("Dubai");
                selectedCountries.Add("Abu Dhabi");
                selectedCountries.Add("Fujairah");
                selectedCountries.Add("Sharjah");
                selectedCountries.Add("Ajman");
                selectedCountries.Add("AL Ain");
            }
            else if (CountryName == "India")
            {
                selectedCountries.Add("Mumbai");
                selectedCountries.Add("Bengaluru");
                selectedCountries.Add("Chennai");
                selectedCountries.Add("Pune");
                selectedCountries.Add("Jaipur");
                selectedCountries.Add("Delhi");
            }
            else
            {
                selectedCountries.Add("Berlin");
                selectedCountries.Add("Munich");
                selectedCountries.Add("Frankfurt");
                selectedCountries.Add("Hamburg");
                selectedCountries.Add("Cologne");
                selectedCountries.Add("Bonn");
            }
            return selectedCountries;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            picker.IsOpen = true;
        }
    }

    public class PickerCascading : INotifyPropertyChanged
    {
        #region Public Properties

        /// <summary>
        /// Area is the acutal DataSource for SfPicker control which will holds the collection of Country and State
        /// </summary>
        /// <value>The area.</value>
        public ObservableCollection<object> Area { get; set; }

        //Country is the collection of country names
        private ObservableCollection<object> Country { get; set; }

        //State is the collection of state names
        private ObservableCollection<object> State { get; set; }

        /// <summary>
        /// Headers api is holds the column name for every column in cascading picker
        /// </summary>
        /// <value>The Headers.</value>
        public ObservableCollection<string> Header { get; set; }

        private object _selectedarea;

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        //Identify the selected area using property changed method
        public object SelectedArea
        {
            get { return _selectedarea; }
            set { _selectedarea = value; RaisePropertyChanged("SelectedArea"); }
        }

        public PickerCascading()
        {
            Area = new ObservableCollection<object>();
            Header = new ObservableCollection<string>();

            Country = new ObservableCollection<object>();
            State = new ObservableCollection<object>();

            //populate Countries
            Country.Add("UK");
            Country.Add("USA");
            Country.Add("India");
            Country.Add("UAE");
            Country.Add("Germany");

            //populate states
            State.Add("London");
            State.Add("Manchester");
            State.Add("Cambridge");
            State.Add("Edinburgh");
            State.Add("Glasgow");
            State.Add("Birmingham");

            Area.Add(Country);

            Area.Add(State);

            Header.Add("Country");

            Header.Add("State");

            SelectedArea = new ObservableCollection<object>() { "UK", "London" };
        }

        //Hooked when changes occured 
        public void RaisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }

}
