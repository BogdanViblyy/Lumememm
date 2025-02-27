namespace Lumememm;

public class lumememm : ContentPage
{
    private readonly BoxView bucket;
    private readonly Frame head, body;
    private readonly Random random;
    private readonly Label actionLabel;
    public lumememm()
    {
        random = new Random();

        AbsoluteLayout absoluteLayout = new AbsoluteLayout();

        bucket = new BoxView { Color = Colors.Brown, WidthRequest = 60, HeightRequest = 40, BackgroundColor = Color.FromArgb("#FFFFFF"), CornerRadius = 100 };
        head = new Frame { WidthRequest = 80, HeightRequest = 80, BorderColor = Colors.Black, BackgroundColor = Color.FromArgb("#FFFFFF"), CornerRadius = 100 };
        body = new Frame { WidthRequest = 120, HeightRequest = 120, BorderColor = Colors.Black, BackgroundColor = Color.FromArgb("#FFFFFF"), CornerRadius = 100 };

        AbsoluteLayout.SetLayoutBounds(bucket, new Rect(140, 50, 60, 40));
        AbsoluteLayout.SetLayoutBounds(head, new Rect(120, 90, 80, 80));
        AbsoluteLayout.SetLayoutBounds(body, new Rect(100, 170, 120, 120));

        absoluteLayout.Children.Add(bucket);
        absoluteLayout.Children.Add(head);
        absoluteLayout.Children.Add(body);

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
        bucket.IsVisible = head.IsVisible = body.IsVisible = visible;
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
            head.BackgroundColor = body.BackgroundColor = Color.FromRgba("#000000");
            actionLabel.Text = "Värv muudetud";
        }
    }

    private async Task MeltSnowman()
    {
        for (double i = 1.0; i >= 0; i -= 0.1)
        {
            bucket.Opacity = head.Opacity = body.Opacity = i;
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