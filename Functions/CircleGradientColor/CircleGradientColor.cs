#region namespace
using Windows.UI;//Colors
using System.Reflection;//PropertyInfo
using Windows.UI.Xaml.Media;//ImageBrush
using Windows.UI.Xaml.Media.Imaging;//WriteableBitmap
#endrefion

#region code
/// <summary>
/// 适用于圆周渐变的颜色停止点
/// </summary>
public class CircleGradientStop
{
    private int _angleOffset;
    private Color _stopColor;
    /// <summary>
    /// 绝对角度
    /// </summary>
    public int Angle
    {
        get { return _angleOffset; }
        set
        {
            if (value / 360 > 0 && value % 360 == 0)
            {
                _angleOffset = 360;
            }
            else _angleOffset = value % 360;
        }
    }
    /// <summary>
    /// 在对应角度下的颜色
    /// </summary>
    public Color Color
    {
        get { return _stopColor; }
        set { _stopColor = value; }
    }
    /// <summary>
    /// 初始化一个颜色停止点
    /// </summary>
    /// <param name="angle">绝对角度</param>
    /// <param name="color">在此绝对角度下的颜色</param>
    public CircleGradientStop(int angle, Color color)
    {
        if (angle / 360 > 0 && angle % 360 == 0)
        {
            _angleOffset = 360;
        }
        else _angleOffset = angle % 360;
        _stopColor = color;
    }
}

/// <summary>
/// 给对象应用圆周渐变笔刷
/// </summary>
/// <param name="obj">待操作对象</param>
/// <param name="propertyName">属性名字</param>
/// <param name="angleOffset">从正上方顺时针偏转的角度</param>
/// <param name="center">圆周渐变中心点</param>
/// <param name="circleGradientStops">颜色停止点列表</param>
/// <exception cref="NullReferenceException">
/// 1.在所提供对象中找不到相应属性
/// 2.未能识别到可应用画笔的对象类型
/// </exception>
private void SetCircleGradientBrush(object obj, string propertyName, int angleOffset, Point center, List<CircleGradientStop> circleGradientStops)
{
    PropertyInfo propertyInfo = obj.GetType().GetProperty(propertyName);
    if (propertyInfo == null)
    {
        throw new NullReferenceException("There is no property called " + propertyName);
    }

    circleGradientStops.Sort((stop1, stop2) => stop1.Angle.CompareTo(stop2.Angle));
    circleGradientStops.Insert(0, new CircleGradientStop(0, circleGradientStops[0].Color));
    circleGradientStops.Add(new CircleGradientStop(360, circleGradientStops.Last().Color));

    int width = 0;
    int height = 0;
    //目前就用了这两个，如果还有其他可以用到的再加吧
    switch (obj.GetType().BaseType.Name)
    {
        case "Control":
            width = (int)Math.Ceiling((obj as Control).Width);
            height = (int)Math.Ceiling((obj as Control).Height);
            break;
        case "Shape":
            width = (int)Math.Ceiling((obj as Shape).Width);
            height = (int)Math.Ceiling((obj as Shape).Height);
            break;
        default:
            throw new NullReferenceException("Can't identify the type: " + obj.GetType().BaseType.Name);
    }

    byte[] data = new byte[width * height * 4];

    for (int i = 0; i < data.Length; i += 4)
    {
        int x = (i / 4) % width - (int)(width * center.X);
        int y = ((i / 4) / width - (int)(height * center.Y)) * -1;
        double angle = Math.Atan2(x, y) / Math.PI * 180 - angleOffset;
        if (angle < 0)
        {
            angle += 360;
        }
        angle %= 360;
        int index = -1;
        foreach (CircleGradientStop stop in circleGradientStops)
        {
            if (angle >= stop.Angle)
                index++;
            else break;
        }
        if (index == circleGradientStops.Count - 1)
            break;
        data[i] = (byte)(circleGradientStops[index].Color.B + (double)(circleGradientStops[index + 1].Color.B - circleGradientStops[index].Color.B) / (circleGradientStops[index + 1].Angle - circleGradientStops[index].Angle) * (angle - circleGradientStops[index].Angle));        //B
        data[i + 1] = (byte)(circleGradientStops[index].Color.G + (double)(circleGradientStops[index + 1].Color.G - circleGradientStops[index].Color.G) / (circleGradientStops[index + 1].Angle - circleGradientStops[index].Angle) * (angle - circleGradientStops[index].Angle));    //G
        data[i + 2] = (byte)(circleGradientStops[index].Color.R + (double)(circleGradientStops[index + 1].Color.R - circleGradientStops[index].Color.R) / (circleGradientStops[index + 1].Angle - circleGradientStops[index].Angle) * (angle - circleGradientStops[index].Angle));  //R
        data[i + 3] = (byte)(circleGradientStops[index].Color.A + (double)(circleGradientStops[index + 1].Color.A - circleGradientStops[index].Color.A) / (circleGradientStops[index + 1].Angle - circleGradientStops[index].Angle) * (angle - circleGradientStops[index].Angle));  //A
    }

    WriteableBitmap writeableBitmap = new WriteableBitmap(width, height);
    data.CopyTo(writeableBitmap.PixelBuffer);
    propertyInfo.SetValue(obj, new ImageBrush { ImageSource = writeableBitmap });
}
#endregion
