using CsvHelper.Configuration.Attributes;
using System;

namespace AutofacMediatr.Libs.TestUtilities.Csv.Map
{
    public interface ICsvMap
    {
        [Ignore]
        public string InsertSql { get; }
    }
}
