#region namespace
using Windows.UI;//Colors
#endregion

#region code
SetCircleGradientBrush(
    testPath,
    "Fill",
    -90,
    new Point(0.5, 1), 
    new List<CircleGradientStop>
    {
        new CircleGradientStop(10, Colors.Lime),
        new CircleGradientStop(85, Colors.Yellow),
        new CircleGradientStop(95, Colors.Yellow),
        new CircleGradientStop(170, Colors.Red)
    });
SetCircleGradientBrush(
    testEllipse,
    "Fill",
    90,
    new Point(0.25, 0.75),
    new List<CircleGradientStop>
    {
        new CircleGradientStop(0, Colors.Red),
        new CircleGradientStop(60, Colors.Yellow),
        new CircleGradientStop(120, Colors.Lime),
        new CircleGradientStop(180, Colors.Cyan),
        new CircleGradientStop(240, Colors.Blue),
        new CircleGradientStop(300, Colors.Magenta),
        new CircleGradientStop(360, Colors.Red)
    });
#endregion
