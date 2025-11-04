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

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }

    private void SaveButton_Clicked(object sender, EventArgs e)
    {
        ClearEditForm();
    }

    private void ClearEditForm()
    {
        gridFoodForm.IsVisible = false;
        gridFoodForm.IsEnabled = false;

        formFoodName.Text = string.Empty;
        formFoodGrams.Text = string.Empty;
        formFoodName.Text = string.Empty;

        foodsCollection.SelectedItem = null;
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

            formFoodName.Text = food.Name;
            formFoodGrams.Text = food.NetGrams.ToString();
            formFoodPrice.Text = food.Price.ToString();
            formFoodTypePicker.SelectedItem = _viewModel.FoodTypesList.First(f => f.Id == food.FoodTypeId);
        }
        catch (Exception ex)
        { }

    }
}