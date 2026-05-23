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

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await _viewModel.InitializeFoodsAsync();
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
        catch { }
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

            if (food.Id > 0)
            {
                formFoodTypePicker.SelectedItem = _viewModel.FoodTypesList.First(f => f.Id == food.FoodTypeId);
            }
            else
            {
                FoodTypeDto? selectedFoodType = formFoodTypePicker.SelectedItem as FoodTypeDto;

                if (selectedFoodType != null)
                {
                    food.FoodTypeId= selectedFoodType.Id;
                }
            }
        }
        catch { }
    }

    private void ClearEditForm()
    {
        gridFoodForm.IsVisible = false;
        gridFoodForm.IsEnabled = false;

        backgroundDim.IsEnabled = false;
        backgroundDim.IsVisible = false;

        foodsCollection.SelectedItem = null;
    }

    private void AddFoodButton_Clicked(object sender, EventArgs eventArgs)
    {
        ShowFoodEditForm(new FoodDto());
    }

    private void SaveButton_Clicked(object sender, EventArgs e)
    {
        ClearEditForm();
    }

    private void CancelButton_Clicked(object sender, EventArgs e)
    {
        ClearEditForm();
    }
    private void formFoodTypePicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (formFoodTypePicker.SelectedItem is FoodTypeDto selectedFoodType)
        {
            _viewModel.SelectedFood.FoodTypeId = selectedFoodType.Id;
        }
    }
}