using RestaurantApp.Domain.Contracts.DTOs;
using RestaurantApp.ViewModels;

namespace RestaurantApp.Views;

public partial class FrontOfficeFoodsPage : ContentPage
{
    FrontOfficeViewModel _viewModel;

	public FrontOfficeFoodsPage(FrontOfficeViewModel frontOfficeViewModel)
	{
		InitializeComponent();
        foodsCollection.SelectionChanged += HandleSelectionChanged;
        _viewModel = frontOfficeViewModel;
        BindingContext = frontOfficeViewModel;
	}

    private void HandleSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        try
        {
            FoodDto? food = e.CurrentSelection.FirstOrDefault() as FoodDto;
            if (food != null)
            {
                ShowFoodEditForm(new FoodDto() { Id = food.Id, Name = food.Name, NetGrams = food.NetGrams, Price = food.Price, FoodTypeId = food.FoodTypeId });
            }
        }
        catch (Exception ex)
        { }
    }

    private void ShowFoodEditForm(FoodDto food)
    {
        try
        {
            gridFoodForm.IsVisible = true;
            gridFoodForm.IsEnabled = true;

            backgroundDim.IsEnabled = true;
            backgroundDim.IsVisible = true;

            _viewModel.SelectedFood = food;
            formFoodTypePicker.SelectedItem = _viewModel.FoodTypesList.First(f => f.Id == food.FoodTypeId);
        }
        catch (Exception ex)
        { }
    }

    private void ClearEditForm()
    {
        gridFoodForm.IsVisible = false;
        gridFoodForm.IsEnabled = false;

        backgroundDim.IsEnabled = false;
        backgroundDim.IsVisible = false;

        foodsCollection.SelectedItem = null;
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