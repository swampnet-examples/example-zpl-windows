using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleDoc.Labels
{
    /// <summary>
    /// Ok, this requires some thought.
    /// 
    /// We want to specify stuff in points (pt) so stuff is the same size no matter what the DPI of the printer is.
    /// 
    /// ZPL generally works with absolute positions, defined in dots. We know a labels width / height in *dots*
    /// .. but I guess not in actual mm? Do we know how big a dot is?
    /// 
    /// Going to assume:
    ///  - We know the DPI of the printer
    ///  - 
    /// </summary>
    public class PrintInfo
    {
        public string Name { get; set; }

        /// <summary>
        /// Printer DPI
        /// </summary>
        public double DPI { get; set; }
        public double LabelWidthInches { get; set; }
        public double LabelHeightInches { get; set; }

        public double LabelWidthDots => LabelWidthInches * DPI;
        public double LabelHeightDots => LabelHeightInches * DPI;

        public int PointToDot(double point)
        {
            return (int)((point / 72.0) * DPI);
        }

        /// <summary>
        /// Format:
        /// 
        /// 10%
        /// 10pct
        /// 10pt
        /// 10
        /// </summary>
        /// <param name="marginLeft"></param>
        /// <returns></returns>
        public int ToDotX(string value)
        {
            int dot = 0;
            if (!string.IsNullOrEmpty(value))
            {
                // pct
                if (value.EndsWith("%"))
                {
                    double pct = Convert.ToDouble(value.Replace("%", ""));
                    dot = (int)(LabelWidthDots * (pct / 100.0));
                }
                else if (value.EndsWith("pct"))
                {
                    double pct = Convert.ToDouble(value.Replace("pct", ""));
                    dot = (int)(LabelWidthDots * (pct / 100.0));
                }
                else if (value.EndsWith("pt"))
                {
                    double pt = Convert.ToDouble(value.Replace("pt", ""));
                    dot = PointToDot(pt);
                }
                else if (value.EndsWith("d"))
                {
                    return Convert.ToInt32(value.Replace("d", ""));
                }
            }

            return dot;
        }


        public int ToDotY(string value)
        {
            int dot = 0;

            if (!string.IsNullOrEmpty(value))
            {
                // pct
                if (value.EndsWith("%"))
                {
                    double pct = Convert.ToDouble(value.Replace("%", ""));
                    dot = (int)(LabelHeightDots * (pct / 100.0));
                }
                else if (value.EndsWith("pct"))
                {
                    double pct = Convert.ToDouble(value.Replace("pct", ""));
                    dot = (int)(LabelHeightDots * (pct / 100.0));
                }
                else if (value.EndsWith("pt"))
                {
                    double pt = Convert.ToDouble(value.Replace("pt", ""));
                    dot = PointToDot(pt);
                }
                else if (value.EndsWith("d"))
                {
                    return Convert.ToInt32(value.Replace("d", ""));
                }
            }
            return dot;
        }
    }
}
