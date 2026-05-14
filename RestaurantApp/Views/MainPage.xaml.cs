using RestaurantApp.Domain.Contracts.DTOs;
using RestaurantApp.ViewModels;

namespace RestaurantApp.Views;

public partial class MainPage : ContentPage
{
    private readonly MainPageViewModel _mainPageViewModel;
    private Frame _lastSelectedMenuFrame;
    private List<(string, double, VisualElement, VisualElement)> _categorySections = new();
    private StackLayout _mainStackLayout;

    private int MenuButtonFontSize = 14;

    public MainPage(MainPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _mainPageViewModel = viewModel;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        _mainPageViewModel.ClearOrderIfEmpty();

        await _mainPageViewModel.GetMenutemsAsync();

        LoadMenuStructureAndItems();

        await Task.Delay(300);
        CaptureSectionPositions();
    }

    private void ButtonStartOrder_Clicked(object sender, EventArgs e)
    {
        gWelcomeScreen.IsEnabled = false;
        gWelcomeScreen.IsVisible = false;
        fWelcomeScreenShade.IsEnabled = false;
        fWelcomeScreenShade.IsVisible = false;
    }

    private void CaptureSectionPositions()
    {
        for (int i = 0; i < _categorySections.Count; i++)
        {
            var (categoryName, pos, element, button) = _categorySections[i];
            _categorySections[i] = (categoryName, element.Y, element, button);
        }

        // Order once so it's ready for the OnScrollViewScrolled event
        _categorySections = _categorySections.OrderBy(x => x.Item2).ToList();
        _mainStackLayout.HeightRequest = _mainStackLayout.Height + mainScrollView.Height - 160;
    }

    private void OnScrollViewScrolled(object? sender, ScrolledEventArgs e)
    {
        foreach (var (categoryName, pos, element, menuElement) in _categorySections)
        {
            if (_lastSelectedMenuFrame.Content is not Label lastMenuLabel
                || menuElement is not Frame currentMenuFrame
                || currentMenuFrame.Content is not Label currentLabel
                || lastMenuLabel.Text == categoryName
                || e.ScrollY < pos - 50)
            {
                continue;
            }

            // Transform new selected button
            currentMenuFrame.BackgroundColor = Colors.AliceBlue;
            currentMenuFrame.BorderColor = Colors.Transparent;
            currentMenuFrame.Margin = 0;
            currentLabel.FontSize = MenuButtonFontSize + 4;
            currentLabel.FontAttributes = FontAttributes.Bold;

            // Revert last selected button
            _lastSelectedMenuFrame.BackgroundColor = Colors.White;
            _lastSelectedMenuFrame.BorderColor = Colors.Black;
            _lastSelectedMenuFrame.Margin = new Thickness(0, -1);
            lastMenuLabel.FontSize = MenuButtonFontSize;
            lastMenuLabel.FontAttributes = FontAttributes.None;

            _lastSelectedMenuFrame = currentMenuFrame;
        }
    }

    private void LoadMenuStructureAndItems()
    {
        Dictionary<int, List<FoodDto>> foodsGroupedByType = _mainPageViewModel.FoodList
            .GroupBy(f => f.FoodTypeId)
            .OrderBy(x => x.Key)
            .ToDictionary(f => f.Key, f => f.ToList());

        Dictionary<int, List<DrinkDto>> drinksGroupedByType = _mainPageViewModel.DrinksList
            .GroupBy(d => d.DrinkTypeId)
            .OrderBy(x => x.Key)
            .ToDictionary(d => d.Key, d => d.ToList());

        _mainStackLayout = new() { Margin = 0, Padding = 0 };
        menuStackLayout.Clear();
        _categorySections.Clear();

        foreach (List<FoodDto> currentFoodsGroup in foodsGroupedByType.Values)
        {
            CollectionView collectionView = new() { Margin = new Thickness(0, 0, 0, 50) };

            #region FoodsDataTemplate
            collectionView.ItemTemplate = new DataTemplate(() =>
            {
                Label nameLabel = new Label { FontSize = 22 };
                nameLabel.SetBinding(Label.TextProperty, "Name");

                Label gramsLabel = new Label();
                gramsLabel.SetBinding(Label.TextProperty,
                    new Binding("NetGrams", stringFormat: "{0}гр"));

                Label typeLabel = new Label();
                typeLabel.SetBinding(Label.TextProperty, "FoodTypeName");

                Label priceLabel = new Label();
                priceLabel.SetBinding(Label.TextProperty,
                    new Binding("Price", stringFormat: "Price: {0:C2}"));

                VerticalStackLayout stack = new VerticalStackLayout
                {
                    Children =
                    {
                        nameLabel,
                        gramsLabel,
                        typeLabel,
                        priceLabel
                    }
                };

                Frame frame = new Frame
                {
                    Content = stack
                };

                TapGestureRecognizer tapGesture = new TapGestureRecognizer
                {
                    Command = ((MainPageViewModel)BindingContext).AddFoodToOrderCommand
                };

                tapGesture.SetBinding(
                    TapGestureRecognizer.CommandParameterProperty,
                    new Binding("."));

                frame.GestureRecognizers.Add(tapGesture);

                return new Grid
                {
                    Padding = 10,
                    Children = { frame }
                };
            });
            #endregion

            Frame menuFrame = new Frame()
            {
                Margin = menuStackLayout.Children.Any() ? new Thickness(0, -1) : new Thickness(0, 0, 0, -1),
                CornerRadius = 0,
                VerticalOptions = LayoutOptions.Center,
                BorderColor = Colors.Black,
                Content = new Label() { Text = currentFoodsGroup.First().FoodTypeName, FontSize = MenuButtonFontSize },
            };

            Label sectionLabel = new Label()
            {
                Text = currentFoodsGroup.First().FoodTypeName,
                FontAttributes = FontAttributes.Italic,
                FontFamily = "Cursive",
                FontSize = 26
            };

            menuFrame.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(async () =>
                {
                    await mainScrollView.ScrollToAsync(sectionLabel, ScrollToPosition.Start, animated: true);

                    _lastSelectedMenuFrame = menuFrame;
                })
            });

            menuStackLayout.Children.Add(menuFrame);

            _categorySections.Add((currentFoodsGroup.First().FoodTypeName!, 0, sectionLabel, menuFrame));
            collectionView.ItemsSource = currentFoodsGroup;

            _mainStackLayout.Add(sectionLabel);
            _mainStackLayout.Add(collectionView);
        }

        foreach (List<DrinkDto> currentDrinksGroup in drinksGroupedByType.Values)
        {
            CollectionView collectionView = new() { Margin = new Thickness(0, 0, 0, 50) };

            #region DrinksDataTemplate
            collectionView.ItemTemplate = new DataTemplate(() =>
            {
                Label nameLabel = new Label { FontSize = 22 };
                nameLabel.SetBinding(Label.TextProperty, "Name");

                Label gramsLabel = new Label();
                gramsLabel.SetBinding(Label.TextProperty,
                    new Binding("Millilitres", stringFormat: "{0}мл"));

                Label typeLabel = new Label();
                typeLabel.SetBinding(Label.TextProperty, "DrinkType");

                Label priceLabel = new Label();
                priceLabel.SetBinding(Label.TextProperty,
                    new Binding("Price", stringFormat: "Price: {0:C2}"));

                Label alcoholicLabel = new Label();
                alcoholicLabel.SetBinding(Label.TextProperty,
                    new Binding("AlcoholPercentage", stringFormat: "Алк. {0:F2}%"));

                alcoholicLabel.SetBinding(VisualElement.IsVisibleProperty, "IsAlcoholic");

                VerticalStackLayout stack = new VerticalStackLayout
                {
                    Children =
                    {
                        nameLabel,
                        gramsLabel,
                        typeLabel,
                        priceLabel
                    }
                };

                Frame frame = new Frame
                {
                    Content = stack
                };

                TapGestureRecognizer tapGesture = new TapGestureRecognizer
                {
                    Command = ((MainPageViewModel)BindingContext).AddDrinkToOrderCommand
                };

                tapGesture.SetBinding(
                    TapGestureRecognizer.CommandParameterProperty,
                    new Binding("."));

                frame.GestureRecognizers.Add(tapGesture);

                return new Grid
                {
                    Padding = 10,
                    Children = { frame }
                };
            });
            #endregion

            Frame menuFrame = new Frame()
            {
                Margin = new Thickness(0, -1),
                CornerRadius = 0,
                VerticalOptions = LayoutOptions.Center,
                BorderColor = Colors.Black,
                Content = new Label() { Text = currentDrinksGroup.First().DrinkType, FontSize = MenuButtonFontSize }
            };

            Label sectionLabel = new Label()
            {
                Text = currentDrinksGroup.First().DrinkType,
                FontAttributes = FontAttributes.Italic,
                FontFamily = "Cursive",
                FontSize = 26
            };

            menuFrame.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(async () =>
                {
                    await mainScrollView.ScrollToAsync(sectionLabel, ScrollToPosition.Start, animated: true);

                    _lastSelectedMenuFrame = menuFrame;
                })
            });

            menuStackLayout.Children.Add(menuFrame);

            _categorySections.Add((currentDrinksGroup.First().DrinkType!, 0, sectionLabel, menuFrame));
            collectionView.ItemsSource = currentDrinksGroup;

            _mainStackLayout.Add(sectionLabel);
            _mainStackLayout.Add(collectionView);
        }

        if (menuStackLayout.Children.Any())
        {
            _lastSelectedMenuFrame = (Frame)menuStackLayout.Children.First();
            _lastSelectedMenuFrame.BackgroundColor = Colors.AliceBlue;
            _lastSelectedMenuFrame.BorderColor = Colors.Transparent;
            _lastSelectedMenuFrame.Margin = 0;
            ((Label)_lastSelectedMenuFrame.Content).FontSize = MenuButtonFontSize + 4;
            ((Label)_lastSelectedMenuFrame.Content).FontAttributes = FontAttributes.Bold;
        }

        mainScrollView.Content = _mainStackLayout;
    }
}
