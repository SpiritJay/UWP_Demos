#region namespaces
using System;
using Windows.UI.Xaml.Media;
#endregion

#region SectorCylinder
/// <summary>
/// 为path设置圆弧柱体的Data，注意柱体厚度cylinderThickness必须小于等于半径radius，角度sectorAngle应在0到360之间
/// </summary>
/// <param name="path">需要设置的对象</param>
/// <param name="cylinderThickness">柱体厚度，内外圈的半径差</param>
/// <param name="radius">外圈半径</param>
/// <param name="sectorAngle">对应扇形角度</param>
private void SetSectorCylinder(Windows.UI.Xaml.Shapes.Path path, double cylinderThickness, double radius, double sectorAngle)
{
    double x1 = radius * Math.Cos((90 - 0.5 * sectorAngle) / 180 * Math.PI);
    double y1 = radius * Math.Sin((90 - 0.5 * sectorAngle) / 180 * Math.PI) * -1;
    double x2 = (radius - cylinderThickness) * Math.Cos((90 - 0.5 * sectorAngle) / 180 * Math.PI);
    double y2 = (radius - cylinderThickness) * Math.Sin((90 - 0.5 * sectorAngle) / 180 * Math.PI) * -1;
    PathGeometry pathGeometry = new PathGeometry();
    pathGeometry.Figures.Add(new PathFigure
    {
        StartPoint = new Point(x1 + radius, y1 + radius),
        Segments = new PathSegmentCollection
                {
                    new ArcSegment
                    {
                        Size = new Size(radius, radius),
                        Point = new Point(-x1 + radius, y1 + radius),
                        IsLargeArc = sectorAngle >= 180,
                        SweepDirection = SweepDirection.Counterclockwise
                    },
                    new LineSegment
                    {
                        Point = new Point(-x2 + radius, y2 + radius)
                    },
                    new ArcSegment
                    {
                        Size = new Size(radius - cylinderThickness, radius - cylinderThickness),
                        Point = new Point(x2 + radius, y2 + radius),
                        IsLargeArc = sectorAngle >= 180,
                        SweepDirection = SweepDirection.Clockwise
                    }
                }
    });

    path.Data = pathGeometry;
}

/// <summary>
/// 生成Path圆弧柱体的Data，注意柱体厚度cylinderThickness必须小于等于半径radius，角度sectorAngle应在0到360之间
/// </summary>
/// <param name="cylinderThickness">柱体厚度，内外圈的半径差</param>
/// <param name="radius">外圈半径</param>
/// <param name="sectorAngle">对应扇形角度</param>
/// <returns>返回Path圆弧柱体的Data</returns>
private PathGeometry SetSectorCylinderPathData(double cylinderThickness, double radius, double sectorAngle)
{
    double x1 = radius * Math.Cos((90 - 0.5 * sectorAngle) / 180 * Math.PI);
    double y1 = radius * Math.Sin((90 - 0.5 * sectorAngle) / 180 * Math.PI) * -1;
    double x2 = (radius - cylinderThickness) * Math.Cos((90 - 0.5 * sectorAngle) / 180 * Math.PI);
    double y2 = (radius - cylinderThickness) * Math.Sin((90 - 0.5 * sectorAngle) / 180 * Math.PI) * -1;
    PathGeometry pathGeometry = new PathGeometry();
    pathGeometry.Figures.Add(new PathFigure
    {
        StartPoint = new Point(x1 + radius, y1 + radius),
        Segments = new PathSegmentCollection
                {
                    new ArcSegment
                    {
                        Size = new Size(radius, radius),
                        Point = new Point(-x1 + radius, y1 + radius),
                        IsLargeArc = sectorAngle >= 180,
                        SweepDirection = SweepDirection.Counterclockwise
                    },
                    new LineSegment
                    {
                        Point = new Point(-x2 + radius, y2 + radius)
                    },
                    new ArcSegment
                    {
                        Size = new Size(radius - cylinderThickness, radius - cylinderThickness),
                        Point = new Point(x2 + radius, y2 + radius),
                        IsLargeArc = sectorAngle >= 180,
                        SweepDirection = SweepDirection.Clockwise
                    }
                }
    });

    return pathGeometry;
}
#endregion
