using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace WebApi.Utils
{

    public class Utils
    {
        public string DatatableToJson(DataTable dt)
        {
            var response = new List<Dictionary<string, object>>();

            foreach (DataRow row in dt.Rows)
            {
                var elements = new List<Dictionary<string, object>>();
                foreach (DataColumn column in dt.Columns)
                {
                    var element = new Dictionary<string, object>
                    {
                        { "column", column.ColumnName },
                        { "value", row[column].ToString().Replace(',','.') }
                    };
                    elements.Add(element);
                }

                var entry = new Dictionary<string, object>
                {
                    { "elements", elements }
                };

                response.Add(entry);
            }

            return System.Text.Json.JsonSerializer.Serialize(response);
        }

        public DataTable RoundValues(DataTable dt, string[] excludeColumns)
        {
            DataTable roundedTable = dt.Clone();

            foreach (DataRow row in dt.Rows)
            {
                DataRow newRow = roundedTable.NewRow();

                foreach (DataColumn column in dt.Columns)
                {
                    if (!excludeColumns.Contains(column.ColumnName))
                    {
                        object value = row[column.ColumnName];
                        if (value != null && value != DBNull.Value && double.TryParse(value.ToString(), out double doubleValue))
                        {
                            double roundedValue = Math.Round(doubleValue, 2);
                            newRow[column.ColumnName] = roundedValue;
                        }
                    }
                    else
                    {
                        newRow[column.ColumnName] = row[column.ColumnName];
                    }
                }

                roundedTable.Rows.Add(newRow);
            }

            return roundedTable;
        }

        public DataTable InsertRowsturnoTotal(DataTable dt)
        {
            try
            {
                if (dt.Rows.Count > 0)
                {
                    List<string> dtTurno = dt.AsEnumerable()
                                                            .GroupBy(r => new { Turno = r["Turno"] })
                                                            .Select(g => g.Key.Turno.ToString()).ToList();

                    List<DataRow> lstRowTurno = new List<DataRow>();

                    dtTurno.ForEach(x =>
                    {
                        var k = dt.NewRow();
                        k[0] = x;
                        k[1] = x;
                        lstRowTurno.Add(k);
                    });

                    DataRow rowTotal = dt.NewRow();

                    DataTable dtHandle = dt.Clone();

                    dtHandle = dt.Copy();

                    foreach (DataRow item in dtHandle.Rows)
                    {
                        if (item[0].ToString().Contains("Material"))
                        {
                            item.Delete();
                        }
                    }

                    dtHandle.AcceptChanges();

                    foreach (var column in dtHandle.Columns.Cast<DataColumn>().ToArray())
                    {
                        if (dtHandle.AsEnumerable().All(dr => dr.IsNull(column)))
                            dtHandle.Columns.Remove(column);

                    }

                    foreach (DataColumn col in dtHandle.Columns)
                    {
                        if (col.ColumnName == "Hora" || col.ColumnName == "Turno") continue;


                        rowTotal[col.ColumnName] = dtHandle.AsEnumerable().Sum(x => { Decimal.TryParse(x[col.ColumnName].ToString(), out decimal result); return result; });

                        for (var i = 0; i < dtTurno.Count; i++)
                        {
                            lstRowTurno[i][col.ColumnName] = dtHandle.AsEnumerable().Where(x => x["Turno"].ToString() == dtTurno[i].ToString()).Sum(x => { Decimal.TryParse(x[col.ColumnName].ToString(), out decimal result); return result; });
                        }
                    }

                    rowTotal[0] = "Total Dia:";

                    foreach (var row1 in lstRowTurno)
                    {
                        if (row1.ItemArray[0].ToString() != "")
                            dt.Rows.Add(row1);
                    }

                    dt.Rows.Add(rowTotal);

                    dt.Columns.Remove("Turno");

                    return dt;
                }
                return new DataTable();
            }
            catch (Exception ex)
            {
                return dt;
            }
        }

        public DataTable CalculateVariability(DataTable dt)
        {
            DataTable dtHandle = dt.Clone();

            dtHandle = dt.Copy();

            DataRow rowAvg = dtHandle.NewRow();

            DataRow rowDesvPad = dtHandle.NewRow();

            foreach (DataColumn col in dtHandle.Columns)
            {
                if (col.ColumnName == "Data" || col.ColumnName == "Dia") continue;

                rowAvg[col.ColumnName] = dtHandle.AsEnumerable().Average(x => { Decimal.TryParse(x[col.ColumnName].ToString(), out decimal result); return result; });
                rowDesvPad[col.ColumnName] = Math.Sqrt(dtHandle.AsEnumerable().Sum(x => { Decimal.TryParse(x[col.ColumnName].ToString(), out decimal result); return Math.Pow((double)(result - (decimal)rowAvg[col.ColumnName]), 2); }) / (dt.Rows.Count - 1));
            }
            rowAvg[0] = "Média:";
            rowDesvPad[0] = "Desvio padrão:";
            dtHandle.Rows.Add(rowAvg);
            dtHandle.Rows.Add(rowDesvPad);

            return dtHandle;
        }

        public DataTable RemoveEmptyRows(DataTable dataTable)
        {
            DataTable dtHandle = dataTable.Clone();

            dtHandle = dataTable.Copy();

            foreach (DataRow row in dtHandle.Rows)
            {
                if (row.ItemArray[1].ToString().Equals(""))

                    row.Delete();

            }
            dtHandle.AcceptChanges();

            return dtHandle;
        }

        public DataTable ReplaceNullsWithZero(DataTable dataTable)
        {
            DataTable updatedTable = dataTable.Clone();

            foreach (DataRow row in dataTable.Rows)
            {
                DataRow updatedRow = updatedTable.NewRow();

                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    object value = row[i];

                    if (value == null || value == DBNull.Value)
                    {
                        updatedRow[i] = 0;
                    }
                    else
                    {
                        updatedRow[i] = value;
                    }
                }

                updatedTable.Rows.Add(updatedRow);
            }

            return updatedTable;
        }

        public DataTable RemoveEmptyColumns(DataTable dataTable)
        {
            DataTable dtHandle = dataTable.Clone();

            dtHandle = dataTable.Copy();

            var columnsToRemove = dtHandle.Columns.Cast<DataColumn>()
                        .Where(column => dtHandle.AsEnumerable().Any(row => string.IsNullOrEmpty(row[column].ToString())))
                        .ToList();

            foreach (var column in columnsToRemove)
            {
                dtHandle.Columns.Remove(column);
            }

            return dtHandle;
        }

        public DataTable DataPoolingGrid(DataTable dataSet)
        {
            DataTable dtCloned = dataSet.Clone();
            dtCloned.Columns["Hora"].DataType = typeof(string);
            foreach (DataRow row in dataSet.Rows)
            {
                dtCloned.ImportRow(row);
            }

            foreach (DataRow row in dtCloned.Rows)
            {
                if (row["Hora"].ToString() == "1000")
                {
                    row["Hora"] = "Total Dia:";
                }
                else if (row["Hora"].ToString() == "100")
                {
                    row["Hora"] = "Turno 1";
                }
                else if (row["Hora"].ToString() == "200")
                {
                    row["Hora"] = "Turno 2";
                }
                else if (row["Hora"].ToString() == "300")
                {
                    row["Hora"] = "Turno 3";
                }
                else
                {
                    row["Hora"] = Convert.ToInt32(row["Hora"]).ToString("00") + ":00:00";
                }
            }
            dtCloned.DefaultView.Sort = "union_order ASC";
            dtCloned.Columns.Remove("Timestamp");
            dtCloned.Columns.Remove("union_order");

            return RoundValues(dtCloned, new string[] { "Hora" });
        }

        public DataTable DataPooling(DataTable dataSet)
        {

            if (dataSet.Columns.Contains("Hora"))
                dataSet.Columns.Remove("Hora");
            dataSet.Columns["Timestamp"].ColumnName = "Hora";
            DataTable dtCloned = dataSet.Clone();
            if (dataSet.Columns.Contains("Turno"))
                dtCloned.Columns["Turno"].DataType = typeof(string);

            dtCloned.Columns["Hora"].DataType = typeof(string);

            foreach (DataRow row in dataSet.Rows)
            {
                dtCloned.ImportRow(row);
            }

            dtCloned.DefaultView.Sort = "Hora asc";

            foreach (DataRow row in dtCloned.Rows)
            {
                if (row["Turno"].ToString() == "1")
                {
                    row["Turno"] = "Turno 1";
                }
                if (row["Turno"].ToString() == "2")
                {
                    row["Turno"] = "Turno 2";
                }
                if (row["Turno"].ToString() == "3")
                {
                    row["Turno"] = "Turno 3";
                }
                if (row["Hora"].ToString() != "")
                {
                    row["Hora"] = row["Hora"].ToString().Substring(11);
                }
            }


            return InsertRowsturnoTotal(dtCloned);
        }

        public DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                // Get the display name of the property
                var displayName = prop.GetCustomAttributes(typeof(DisplayNameAttribute), true)
                    .OfType<DisplayNameAttribute>()
                    .FirstOrDefault()?.DisplayName;

                // Use the display name if available, otherwise use the property name
                var columnName = string.IsNullOrWhiteSpace(displayName) ? prop.Name : displayName;

                // Setting column names as Property names or display names
                dataTable.Columns.Add(columnName);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        public string DataTableToChartJson(DataTable dataTable)
        {
            DataTable dtHandle = dataTable.Clone();

            dtHandle = dataTable.Copy();

            var jsonData = new Dictionary<string, object>();
            var seriesData = new List<Dictionary<string, object>>();

            foreach (DataColumn column in dtHandle.Columns)
            {
                var series = new Dictionary<string, object>
                {
                    ["name"] = column.ColumnName,
                    ["data"] = dtHandle.AsEnumerable()
                        .Select(row => row.Field<double>(column))
                        .Reverse()
                        .ToList()
                };

                seriesData.Add(series);
            }


            jsonData["series"] = seriesData;

            return JsonConvert.SerializeObject(jsonData);
        }

        public DataTable GetSelectedColumns(DataTable sourceTable, string[] columnNames, string filterColumn, string filterValue)
        {
            // Create a new DataTable with the selected columns
            DataTable newTable = new DataTable();
            foreach (string columnName in columnNames)
            {
                if (sourceTable.Columns.Contains(columnName))
                {
                    DataColumn column = sourceTable.Columns[columnName];
                    newTable.Columns.Add(column.ColumnName, columnName == "Timestamp" ? typeof(DateTime) : column.DataType);
                }
                else
                {
                    newTable.Columns.Add(columnName, typeof(decimal));
                }

            }

            // Apply the filter and add the selected rows to the new DataTable
            foreach (DataRow row in sourceTable.Rows)
            {
                if (string.IsNullOrEmpty(filterColumn) || row[filterColumn].ToString() == filterValue)
                {
                    object[] selectedValues = row.ItemArray.Where((_, index) => columnNames.Contains(sourceTable.Columns[index].ColumnName)).ToArray();
                    newTable.Rows.Add(selectedValues);
                }
            }

            return newTable;
        }


        //public Parameter GetParameterByName(string _name, string sinter)
        //{
        //    Parameter _param = new();
        //    ParameterDataAccess parameterDataAccess = new();
        //    try
        //    {
        //        DataSet dataSet = parameterDataAccess.GetParameter(_name, sinter);

        //        foreach (DataTable table in dataSet.Tables)
        //        {

        //            foreach (DataRow dr in table.Rows)
        //            {
        //                _param.ParameterId = Convert.ToInt32(dr["ParametroID"]);
        //                _param.Name = Convert.ToString(dr["Nome"]);
        //                _param.Value = Convert.ToDecimal(dr["Valor"]);
        //                _param.User = 1;

        //            }
        //        }

        //        return _param;
        //    }
        //    catch (Exception exception)
        //    {
        //        return _param;
        //    }
        //}


        //internal DataTable GetEspecificConsume(DataTable dataSource, string sinter)
        //{
        //    decimal sum = 0;
        //    List<string> exCludeColumns = new() { "TOTAL", "MINERIO", "Timestamp", "Hora", "Turno" };

        //    dataSource = ReplaceNullsWithZero(dataSource);

        //    var listColumnDate = (from DataRow dr in dataSource.Rows
        //                          select new
        //                          {
        //                              Date = Convert.ToDateTime(dr["Timestamp"]),
        //                          }).ToList();

        //    DateTime targetDate = new(2023, 06, 28);

        //    bool containsDate = listColumnDate.Any(date => date.Date == targetDate.Date || date.Date < targetDate.Date);

        //    if (dataSource.Columns.Contains("prodMes") && !containsDate)
        //    {
        //        sum = Convert.ToDecimal(dataSource.Compute($"sum(prodMes)", ""));
        //    }
        //    else
        //    {
        //        foreach (DataColumn col in dataSource.Columns)
        //        {
        //            if (!exCludeColumns.Contains(col.ColumnName) && !col.ColumnName.Contains('/'))
        //            {
        //                try
        //                {
        //                    var parametro = GetParameterByName("Fator de Perda", sinter);
        //                    decimal _fatorPerda = Convert.ToDecimal(parametro.Value);

        //                    ParameterDataAccess parameterDataAccess = new();
        //                    parametro = parameterDataAccess.GetPPC(parameterDataAccess.GetMaterialId(col.ColumnName, sinter), sinter);
        //                    decimal _ppc = Convert.ToDecimal(parametro.Value);

        //                    decimal sumColumn = Convert.ToDecimal(dataSource.Compute($"sum({col.ColumnName})", ""));
        //                    if (col.ColumnName != "RETORNO" && col.ColumnName != "RETORNO_FRIO")
        //                    {
        //                        sum += sumColumn * (1 - _ppc / 100) * (1 - _fatorPerda / 100);
        //                    }
        //                }
        //                catch (Exception e)
        //                {
        //                    Console.WriteLine("Erro: " + e);
        //                }
        //            }
        //        }
        //    }

        //    DataTable dt = GetSelectedColumns(
        //        dataSource,
        //        new string[] { "MINERIO", "RESIDUO", "FSD" }, "", "");

        //    DataTable dtHandle = new();

        //    foreach (DataColumn col in dt.Columns)
        //    {
        //        dtHandle.Columns.Add(col.ColumnName);

        //    }
        //    DataRow sumRow = dtHandle.NewRow();
        //    foreach (DataColumn col in dt.Columns)
        //    {
        //        if (dataSource.Columns.Contains(col.ColumnName))
        //        {
        //            decimal sumColumn = Convert.ToDecimal(dataSource.Compute($"sum({col.ColumnName})", ""));

        //            sumColumn = sumColumn < 1 ? 1 : sumColumn;
        //            sum = sum < 1 ? 1 : sum;

        //            decimal consumoEspecifico = (sumColumn / sum) * 1000;
        //            sumRow[col.ColumnName] = consumoEspecifico;
        //        }
        //        else
        //        {
        //            sumRow[col.ColumnName] = 0;
        //        }
        //    }

        //    dtHandle.Rows.Add(sumRow);

        //    return dtHandle;
        //}


        public DataTable CalculateSumColumns(DataTable dataTable)
        {
            DataTable sumTable = new DataTable();
            foreach (DataColumn column in dataTable.Columns)
            {
                sumTable.Columns.Add(column.ColumnName, column.DataType);
            }

            DataRow sumRow = sumTable.NewRow();
            foreach (DataColumn column in sumTable.Columns)
            {
                object sum = dataTable.AsEnumerable()
                    .Sum(row => row.Field<decimal>(column.ColumnName));
                sumRow[column.ColumnName] = sum;
            }

            sumTable.Rows.Add(sumRow);
            return sumTable;
        }


        public DataTable AddColumnDataTables(DataTable dt1, DataTable dt2)
        {

            DataTable mergedTable = dt1.Copy();

            if (dt2.Rows.Count < 1)
            {
                return mergedTable;
            }

            foreach (DataColumn col in dt2.Columns)
            {
                if (!mergedTable.Columns.Contains(col.ColumnName))
                {
                    mergedTable.Columns.Add(col.ColumnName, col.DataType);
                }
            }

            for (int i = 0; i < mergedTable.Rows.Count; i++)
            {
                DataRow dt2Row = dt2.Rows[i % dt2.Rows.Count];
                DataRow mergedRow = mergedTable.Rows[i];

                foreach (DataColumn col in dt2.Columns)
                {
                    if (!mergedTable.Columns.Contains(col.ColumnName))
                    {
                        mergedTable.Columns.Add(col.ColumnName, col.DataType);
                    }

                    mergedRow[col.ColumnName] = dt2Row[col.ColumnName];
                }
            }

            return mergedTable;
        }

    }
}
