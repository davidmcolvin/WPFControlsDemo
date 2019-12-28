using Caliburn.Micro;
using DemoLibrary;
using DemoLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFDemoUI.ViewModels
{
  public class ShellViewModel : Screen
  {
    public BindableCollection<PersonModel> People { get; set; }

    private PersonModel _selectedPerson;

    public ShellViewModel()
    {
      DataAccess da = new DataAccess();
      People = new BindableCollection<PersonModel>(da.GetPeople());
    }

    public PersonModel SelectedPerson
    {
      get { return _selectedPerson; }
      set 
      { 
        _selectedPerson = value;
        SelectedAddress = value?.PrimaryAddress;
        NotifyOfPropertyChange(() => SelectedPerson);
      }
    }

    private AddressModel _selectedAddress;

    public AddressModel SelectedAddress
    {
      get { return _selectedAddress; }
      set 
      { 
        _selectedAddress = value;
        if (SelectedPerson != null)
        {
          SelectedPerson.PrimaryAddress = value;
        }
        NotifyOfPropertyChange(() => SelectedAddress);
      }
    }

    public void AddPerson()
    {
      int maxId = 0;
      DataAccess dataAccess = new DataAccess();


      if (People.Count > 0)
      {
        maxId = People.Max(x => x.PersonId); 
      }

      People.Add(dataAccess.GetPerson(maxId + 1));

    }

    public void RemoveRandomPerson()
    {
      if (People.Count == 0)
      {
        return;
      }

      DataAccess dataAccess = new DataAccess();

      PersonModel randomPerson = dataAccess.GetRandomItem(People.ToArray());

      People.Remove(randomPerson);

    }




  }
}
