using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BenqOA.Helper
{
    public class GridHelper
    {
        //用来存放计算后获取的列
        public class column:IComparable
        {
            public string Value = "";
            public string Text = "";
            public int CompareTo(object obj)
            {
                var c = obj as column;
                var intCValue = Convert.ToInt32(c.Value);
                var intThisValue = Convert.ToInt32(this.Value);
                if (intCValue == intThisValue)
                    return 0;
                else if (intCValue < intThisValue)
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
            }
        }

        public List<column> GetColumns(string type, DateTime beginDate, DateTime endDate)
        {
            var cols = new List<column>();
            if (beginDate <= endDate)
            {
                switch (type)
                {
                    case "month":
                        var differMonth = (endDate.Year - beginDate.Year) * 12 + (endDate.Month - beginDate.Month + 1);
                        for (var i = 0; i < differMonth; i++)
                        {
                            var c = new column
                            {
                                Value = beginDate.ToString("yyyyMM"),
                                Text = beginDate.ToString("yyyy/MM")
                            };
                            cols.Add(c);
                            beginDate = beginDate.AddMonths(1);
                        }
                        break;
                    case "season":
                        var differSeason = (endDate.Year - beginDate.Year) * 4 + (GetSeason(endDate) - GetSeason(beginDate) + 1);
                        for (var i = 0; i < differSeason; i++)
                        {
                            var season = "0" + GetSeason(beginDate);
                            var beginYear = beginDate.ToString("yyyy");
                            var c = new column
                            {
                                Value = beginYear + season,
                                Text = beginYear + "/" + season
                            };
                            cols.Add(c);
                            beginDate = beginDate.AddMonths(3);
                        }
                        break;
                    case "halfYear":
                        var differHalfYear = (endDate.Year - beginDate.Year) * 2 + ((endDate.Month > 6 ? 2 : 1) - (beginDate.Month > 6 ? 2 : 1) + 1);
                        for (var i = 0; i < differHalfYear; i++)
                        {
                            var halfYear = (beginDate.Month > 6 ? 2 : 1);
                            var beginYear = beginDate.ToString("yyyy");
                            var c = new column
                            {
                                Value = beginYear + "0" + halfYear,
                                Text = beginYear + (halfYear == 2 ? "下半年" : "上半年")
                            };
                            cols.Add(c);
                            beginDate = beginDate.AddMonths(6);
                        }
                        break;
                    case "year":
                        var differYear = endDate.Year - beginDate.Year + 1;
                        for (var i = 0; i < differYear; i++)
                        {
                            var beginYear = beginDate.ToString("yyyy");
                            var c = new column
                            {
                                Value = beginYear,
                                Text = beginYear
                            };
                            cols.Add(c);
                            beginDate = beginDate.AddYears(1);
                        }
                        break;
                    default:
                        break;
                }
            }
            return cols;
        }

        public int GetSeason(DateTime dt)
        {
            var m = dt.Month;
            if (m > 0 && m < 4)
                return 1;
            else if (m > 3 && m < 7)
            {
                return 2;
            }
            else if (m > 6 && m < 10)
            {
                return 3;
            }
            else if (m > 9 && m < 13)
            {
                return 4;
            }
            else
            {
                return -1;
            }
        }
    }
}