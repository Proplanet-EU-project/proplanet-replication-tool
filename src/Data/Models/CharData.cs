using Blazorise.Charts;
using System.Text.Json;

public class CharData
{
    public string[] Labels { get; private set; }
    public List<string> BackgroundColors { get; private set; }
    public List<string> BorderColors { get; private set; }

    public CharData()
    {
        Labels = new string[] { };
        BackgroundColors = new List<string>();
        BorderColors = new List<string>();
    }

    public CharData(string[] labels)
    {
        Labels = labels;
        GenerateColors(labels.Length);
    }

    private void GenerateColors(int count)
    {
        BackgroundColors = new List<string>();
        BorderColors = new List<string>();
        var colorList = new List<string>
    {
        "rgba(255, 0, 0, 0.2)",   // Intense Red
        "rgba(0, 255, 0, 0.2)",   // Bright Green
        "rgba(0, 0, 255, 0.2)",   // Pure Blue
        "rgba(255, 255, 0, 0.2)", // Vibrant Yellow
        "rgba(0, 255, 255, 0.2)", // Bright Cyan
        "rgba(255, 0, 255, 0.2)", // Intense Magenta
        "rgba(255, 128, 0, 0.2)", // Vivid Orange
        "rgba(128, 0, 128, 0.2)", // Dark Purple
        "rgba(128, 128, 0, 0.2)", // Olive Green
        "rgba(64, 224, 208, 0.2)", // Light Turquoise
        "rgba(255, 20, 147, 0.2)", // Strong Pink
        "rgba(0, 0, 128, 0.2)"    // Navy Blue
    };
        for (int i = 0; i < count; i++)
        {
            int colorIndex = i % 12;

            string backgroundColor = colorList[colorIndex];
            string borderColor = backgroundColor.Replace("0.2", "1");

            BackgroundColors.Add(backgroundColor);
            BorderColors.Add(borderColor);
        }
    }
}