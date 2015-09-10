namespace XamlBrewer.Uwp.LexDbSample.ViewModels
{
    using Mvvm;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using DataAccessLayer;
    using Models;

    internal class MainPageViewModel : ViewModelBase
    {
        private DelegateCommand cancelCommand;
        private DelegateCommand createCommand;
        private DelegateCommand deleteCommand;
        private bool hasSelection = false;
        private DelegateCommand newCommand;
        private ObservableCollection<VintageMuscleCarViewModel> cars = new ObservableCollection<VintageMuscleCarViewModel>();
        private DelegateCommand saveCommand;
        private DelegateCommand selectCommand;
        private VintageMuscleCarViewModel selectedCar = null;
        private bool isDatabaseCreated = false;
        public MainPageViewModel()
        {
            if (this.IsInDesignMode)
            {
                return;
            }

            this.createCommand = new DelegateCommand(this.Create_Executed);
            this.selectCommand = new DelegateCommand(this.Select_Executed);
            this.newCommand = new DelegateCommand(this.New_Executed, this.New_CanExecute);
            this.deleteCommand = new DelegateCommand(this.Delete_Executed, this.Edit_CanExecute);
            this.saveCommand = new DelegateCommand(this.Save_Executed, this.Save_CanExecute);
            this.cancelCommand = new DelegateCommand(this.Cancel_Executed, this.Save_CanExecute);
        }

        public ICommand CancelCommand
        {
            get { return this.cancelCommand; }
        }

        public ICommand CreateCommand
        {
            get { return this.createCommand; }
        }

        public ICommand DeleteCommand
        {
            get { return this.deleteCommand; }
        }

        public bool HasSelection
        {
            get { return this.hasSelection; }
            private set { this.SetProperty(ref this.hasSelection, value); }
        }

        public ICommand NewCommand
        {
            get { return this.newCommand; }
        }

        public ObservableCollection<VintageMuscleCarViewModel> Cars
        {
            get { return this.cars; }
            set { this.SetProperty(ref this.cars, value); }
        }

        public ICommand SaveCommand
        {
            get { return this.saveCommand; }
        }

        public ICommand SelectCommand
        {
            get { return this.selectCommand; }
        }
        public VintageMuscleCarViewModel SelectedCar
        {
            get { return this.selectedCar; }
            set
            {
                this.SetProperty(ref this.selectedCar, value);
                this.HasSelection = this.selectedCar != null;
                this.deleteCommand.RaiseCanExecuteChanged();
                this.editCommand.RaiseCanExecuteChanged();
            }
        }
        protected override bool Edit_CanExecute()
        {
            return this.selectedCar != null && base.Edit_CanExecute();
        }

        protected override void Edit_Executed()
        {
            base.Edit_Executed();
            this.selectedCar.IsInEditMode = true;
            this.saveCommand.RaiseCanExecuteChanged();
            this.cancelCommand.RaiseCanExecuteChanged();
        }
        private void Cancel_Executed()
        {
            if (this.selectedCar.Id == 0)
            {
                this.cars.Remove(this.selectedCar);
                this.SelectedCar = null;

                // Select last car. 
                if (this.cars.Count > 0)
                {
                    this.SelectedCar = this.Cars.Last();
                }
            }
            else
            {
                // Get old one back from db
                this.selectedCar.Model = Dal.GetCarById(this.selectedCar.Id);
                this.selectedCar.IsInEditMode = false;
            }

            this.IsInEditMode = false;
        }

        private async void Create_Executed()
        {
            await Dal.ResetCars();

            // Select. Otherwise the displayed list may be out of sync with the db.
            this.selectCommand.Execute(null);
        }

        private void Delete_Executed()
        {
            // Remove from db
            Dal.DeleteCars(new List<VintageMuscleCar>() { this.selectedCar.Model });

            // Remove from list
            this.Cars.Remove(this.selectedCar);

            // Clear UI
            this.SelectedCar = null;
        }

        private bool New_CanExecute()
        {
            return !this.IsInEditMode && this.isDatabaseCreated;
        }

        private void New_Executed()
        {
            this.Cars.Add(new VintageMuscleCarViewModel(new VintageMuscleCar()));
            this.SelectedCar = this.cars.Last();
            this.editCommand.Execute(null);
        }

        private bool Save_CanExecute()
        {
            return this.IsInEditMode;
        }

        private void Save_Executed()
        {
            // Store new one in db
            Dal.SaveCar(this.selectedCar.Model);

            // Force a property change notification on the ViewModel:
            this.selectedCar.Model = this.selectedCar.Model;

            // Leave edit mode
            this.IsInEditMode = false;
            this.selectedCar.IsInEditMode = false;
        }

        private void Select_Executed()
        {
            IEnumerable<VintageMuscleCar> models = Dal.GetCars();

            this.cars.Clear();
            foreach (var m in models)
            {
                this.cars.Add(new VintageMuscleCarViewModel(m));
            }

            this.isDatabaseCreated = true;
            this.newCommand.RaiseCanExecuteChanged();
        }
    }
}