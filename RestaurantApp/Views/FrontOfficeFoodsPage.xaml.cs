using RestaurantApp.Domain.Contracts.DTOs;
using RestaurantApp.Domain.Entities;
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

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }

    private void SaveButton_Clicked(object sender, EventArgs e)
    {
        ClearEditForm();
    }

    private void CancelButton_Clicked(object sender, EventArgs e)
    {
        ClearEditForm();
    }

    private void HandleSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        try
        {
            FoodDto? food = e.CurrentSelection.FirstOrDefault() as FoodDto;
            if (food != null)
            {
                ShowFoodEditForm(food);
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

        foodsCollection.SelectedItem = null;
    }
}