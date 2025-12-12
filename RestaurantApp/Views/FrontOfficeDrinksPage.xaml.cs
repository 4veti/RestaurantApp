using RestaurantApp.Domain.Contracts.DTOs;
using RestaurantApp.ViewModels;

namespace RestaurantApp.Views;

public partial class FrontOfficeDrinksPage : ContentPage
{
    FrontOfficeViewModel _viewModel;

	public FrontOfficeDrinksPage(FrontOfficeViewModel frontOfficeViewModel)
	{
		InitializeComponent();
        drinksCollection.SelectionChanged += HandleSelectionChanged;
        _viewModel = frontOfficeViewModel;
        BindingContext = frontOfficeViewModel;
	}

    private void HandleSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        try
        {
            DrinkDto? drink = e.CurrentSelection.FirstOrDefault() as DrinkDto;
            if (drink != null)
            {
                ShowDrinkEditForm(new DrinkDto() { Id = drink.Id, Name = drink.Name, Millilitres = drink.Millilitres, Price = drink.Price, DrinkTypeId = drink.DrinkTypeId });
            }
        }
        catch (Exception ex)
        { }
    }

    private void ShowDrinkEditForm(DrinkDto drink)
    {
        try
        {
            gridDrinkForm.IsVisible = true;
            gridDrinkForm.IsEnabled = true;

            backgroundDim.IsEnabled = true;
            backgroundDim.IsVisible = true;

            _viewModel.SelectedDrink = drink;
            formDrinkTypePicker.SelectedItem = _viewModel.DrinkTypesList.First(f => f.Id == drink.DrinkTypeId);
        }
        catch (Exception ex)
        { }
    }

    private void ClearEditForm()
    {
        gridDrinkForm.IsVisible = false;
        gridDrinkForm.IsEnabled = false;

        backgroundDim.IsEnabled = false;
        backgroundDim.IsVisible = false;

        drinksCollection.SelectedItem = null;
    }

    private void SaveButton_Clicked(object sender, EventArgs e)
    {
        ClearEditForm();
    }

    private void CancelButton_Clicked(object sender, EventArgs e)
    {
        ClearEditForm();
    }
}