namespace Lumememm;

public partial class MainPage : ContentPage
{
    public List<KeyValuePair<ContentPage, string>> Lehed =
    [
        new KeyValuePair<ContentPage, string>(new lumememm(), "Lumememm")
    ];
    private readonly ScrollView sv = new();
    private readonly VerticalStackLayout vsl = new()
    {
        BackgroundColor = Color.FromArgb("#000000")
    };

    public MainPage()
    {
        Title = "Avaleht";

        for (int i = 0; i < Lehed.Count; i++)
        {
            Button nupp = new()
            {
                Text = Lehed[i].Value,
                BackgroundColor = Color.FromArgb("#000000"),
                TextColor = Color.FromArgb("#FFFFFF"),
                BorderWidth = 10,
                ZIndex = i
            };
            vsl.Add(nupp);
            nupp.Clicked += Lehte_avamine;
        }
        sv.Content = vsl;
        Content = sv;
    }

    private async void Lehte_avamine(object? sender, EventArgs e)
    {
        Button? btn = sender as Button;
        await Navigation.PushAsync(Lehed[btn.ZIndex].Key);
    }
}