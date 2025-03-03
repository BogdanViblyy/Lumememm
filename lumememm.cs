namespace Lumememm;

public class lumememm : ContentPage
{
    private readonly Image tophat, carrot;
    private readonly Frame head, body;
    private readonly Random random;
    private readonly Label actionLabel;
    private bool isMelted = false;

    public lumememm()
    {
        random = new Random();

        AbsoluteLayout absoluteLayout = new AbsoluteLayout();

        tophat = new Image { Source = "tophat.png", WidthRequest = 60, HeightRequest = 40 };
        carrot = new Image { Source = "dotnet_bot.png", WidthRequest = 30, HeightRequest = 15 };

        head = new Frame { WidthRequest = 80, HeightRequest = 80, BorderColor = Colors.Black, BackgroundColor = Colors.White, CornerRadius = 100 };
        body = new Frame { WidthRequest = 120, HeightRequest = 120, BorderColor = Colors.Black, BackgroundColor = Colors.White, CornerRadius = 100 };

        AbsoluteLayout.SetLayoutBounds(tophat, new Rect(140, 50, 60, 40));
        AbsoluteLayout.SetLayoutBounds(head, new Rect(120, 90, 80, 80));
        AbsoluteLayout.SetLayoutBounds(body, new Rect(100, 170, 120, 120));
        AbsoluteLayout.SetLayoutBounds(carrot, new Rect(150, 120, 30, 15));

        absoluteLayout.Children.Add(tophat);
        absoluteLayout.Children.Add(head);
        absoluteLayout.Children.Add(body);
        absoluteLayout.Children.Add(carrot);

        actionLabel = new Label { Text = "Vali tegevus", FontSize = 18, HorizontalOptions = LayoutOptions.Center };

        Button hideButton = new Button { Text = "Peida lumememm" };
        hideButton.Clicked += (s, e) => ToggleVisibility(false);

        Button showButton = new Button { Text = "Näita lumememme" };
        showButton.Clicked += (s, e) => ToggleVisibility(true);

        Button colorButton = new Button { Text = "Muuda värvi" };
        colorButton.Clicked += async (s, e) => await ChangeColor();

        Button meltButton = new Button { Text = "Sulata lumememm" };
        meltButton.Clicked += async (s, e) => await MeltSnowman();

        Slider sizeSlider = new Slider { Minimum = 0.5, Maximum = 2, Value = 1 };
        sizeSlider.ValueChanged += (s, e) => ResizeSnowman(e.NewValue);

        StackLayout buttonLayout = new StackLayout
        {
            Children = { actionLabel, hideButton, showButton, colorButton, meltButton, sizeSlider },
            Spacing = 10,
            Padding = new Thickness(10),
            VerticalOptions = LayoutOptions.End
        };

        Content = new StackLayout
        {
            Children = { absoluteLayout, buttonLayout }
        };
    }

    private void ToggleVisibility(bool visible)
    {
        if (isMelted && visible)
        {
            isMelted = false;
            tophat.TranslationY = 0;
            carrot.TranslationY = 0;
            tophat.Opacity = 1;
            carrot.Opacity = 1;
            head.Opacity = 1;
            body.Opacity = 1;
        }
        tophat.IsVisible = carrot.IsVisible = head.IsVisible = body.IsVisible = visible;
        actionLabel.Text = visible ? "Lumememm nähtaval" : "Lumememm peidetud";
    }

    private async Task ChangeColor()
    {
        int r = random.Next(0, 255);
        int g = random.Next(0, 255);
        int b = random.Next(0, 255);

        bool confirm = await DisplayAlert("Värvi muutus", $"Kas soovid muuta värvi? Uus värv: RGB({r}, {g}, {b})", "Jah", "Ei");
        if (confirm)
        {
            head.BackgroundColor = body.BackgroundColor = Color.FromRgb(r, g, b);
            actionLabel.Text = "Värv muudetud";
        }
    }

    private async Task MeltSnowman()
    {
        isMelted = true;
        for (double i = 1.0; i >= 0; i -= 0.1)
        {
            tophat.Opacity = carrot.Opacity = head.Opacity = body.Opacity = i;
            tophat.TranslationY += 10;
            carrot.TranslationY += 10;
            await Task.Delay(200);
        }
        actionLabel.Text = "Lumememm sulas ära!";
    }

    private void ResizeSnowman(double scale)
    {
        head.WidthRequest = head.HeightRequest = 80 * scale;
        body.WidthRequest = body.HeightRequest = 120 * scale;
        actionLabel.Text = "Lumememme suurus muudetud";
    }
}
