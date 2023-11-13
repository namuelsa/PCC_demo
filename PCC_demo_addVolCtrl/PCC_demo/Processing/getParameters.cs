using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using Accord.Math; 

namespace PCC_demo.Processing
{
    internal class getParameters
    {
        public static readonly Dictionary<int, (Dictionary<int, double>, double)> formulaDict = new Dictionary<int, (Dictionary<int, double>, double)>();  //OMP预测公式合集，通过sensor编号，查询相关的电压组合和预测公式
        public static readonly Dictionary<Tuple<int, int>, double[]> PCAMatrix, PCAMean;

        public static Dictionary<Tuple<int, int>, double> afterPCA_dict = new Dictionary<Tuple<int, int>, double>();
        public static double[,] afterPCA_array = new double[6, 101];

        static getParameters()
        {
            //string filePath = @"./Resources/OMP_dict.xlsx";
            //string PCAPath = @"D:\230718test\230718六通道持续采集\230718六通道持续采集_UIupdated\PCC_demo\PCC_demo\Resources\PCAtxt";

            string filePath = Path.Combine(systemSetting.projectPath, "Resources", "OMP_dict.xlsx");
            string PCAPath = Path.Combine(systemSetting.projectPath, "Resources", "PCAtxt");

            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial; //使用OfficeOpenXml必须先定义lincense级别（EPPlus库）

            formulaDict = ReadOMPExcel(filePath, 1); //第二个参数为读取第几行的数据
            // 调用方法-->  getOMPparameters.PredictValues(testArray, getOMPparameters.formulaDict);

            PCAMatrix = ReadMatrix(PCAPath);
            PCAMean = ReadMean(PCAPath);
        }

        //  读取记录了OMP算法得到的预测公式的excel文件
        static Dictionary<int, (Dictionary<int, double>, double)> ReadOMPExcel(string filePath, int selectedRow)
        {
            Dictionary<int, (Dictionary<int, double>, double)> result = new Dictionary<int, (Dictionary<int, double>, double)>();
            using (ExcelPackage package = new ExcelPackage(new FileInfo(filePath)))
            {
                for (int i = 0; i < package.Workbook.Worksheets.Count; i++)
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[i];
                    string sheetName = worksheet.Name;
                    int sheetNum = int.Parse(sheetName.Substring(3));
                    ExcelRange coeffCell = worksheet.Cells[selectedRow + 1, 7];
                    ExcelRange constCell = worksheet.Cells[selectedRow + 1, 8];
                    string coeffStr = coeffCell.Value.ToString().Trim('[', ']');
                    double constVal = double.Parse(constCell.Value.ToString());
                    Dictionary<int, double> coeffDict = new Dictionary<int, double>();
                    string[] tuples = coeffStr.Split(',');
                    for (int j = 0; j < tuples.Length; j += 2)
                    {
                        string tuple = tuples[j].Replace(" ", "").Replace("(", "") + tuples[j + 1].Trim(')');
                        string[] values = tuple.Split(' ');
                        double coeff = double.Parse(values[0]);
                        int voltage = int.Parse(values[1]);
                        coeffDict.Add(voltage, coeff);
                    }
                    result.Add(sheetNum, (coeffDict, constVal));
                }
            }
            return result;
        }

        // 计算预测值的函数
        public static double[] PredictValues(double[,] inputArray, Dictionary<int, (Dictionary<int, double>, double)> formuladict)
        {
            double[] outputArray = new double[6];
            for (int i = 0; i < inputArray.GetLength(0); i++)
            {
                int boxNum = i + 1;
                var formula = formuladict[boxNum];
                var coeffDict = formula.Item1;
                var constVal = formula.Item2;
                double predictVal = constVal;
                foreach (var item in coeffDict)
                {
                    int voltage = item.Key;
                    double coeff = item.Value;
                    double voltageVal = inputArray[i, voltage];
                    predictVal += voltageVal * coeff;
                }
                outputArray[i] = predictVal;
            }
            return outputArray;
        }

        // 读取PCA Matrix
        public static Dictionary<Tuple<int, int>, double[]> ReadMatrix(string path)
        {
            var dict = new Dictionary<Tuple<int, int>, double[]>();
            foreach (var fileName in Directory.GetFiles(path, "*_matrix.txt"))
            {
                var a = Int32.Parse(fileName.Substring(fileName.IndexOf("(") + 1, 1));
                var b = Int32.Parse(fileName.Substring(fileName.IndexOf(",") + 2, fileName.IndexOf(")") - fileName.IndexOf(",") - 2));
                var key = Tuple.Create(a, b);

                var content = File.ReadAllText(fileName);
                var columns = content.Split(' ');
                var array = Array.ConvertAll<string, double>(columns, double.Parse);
                dict.Add(key, array);
            }
            return dict;
        }

        public static Dictionary<Tuple<int, int>, double[]> ReadMean(string path)
        {
            var dict = new Dictionary<Tuple<int, int>, double[]>();
            foreach (var fileName in Directory.GetFiles(path, "*_mean.txt"))
            {
                var a = Int32.Parse(fileName.Substring(fileName.IndexOf("(") + 1, 1));
                var b = Int32.Parse(fileName.Substring(fileName.IndexOf(",") + 2, fileName.IndexOf(")") - fileName.IndexOf(",") - 2));
                var key = Tuple.Create(a, b);

                var content = File.ReadAllLines(fileName);
                var value = Array.ConvertAll<string, double>(content, double.Parse);
                dict.Add(key, value);
            }
            return dict;
        }

        // 定义一个函数，输入一个测量数组，一个vol值，和一个PCA降维字典，把降维后的值加进afterPCA_dict
        public static void PCAprocess(double[,] inputArray, int VolNo, Dictionary<Tuple<int, int>, double[]> pcaMatrix, Dictionary<Tuple<int, int>, double[]> pcaMean)
        {
            for (int sensorNo = 1; sensorNo <= 6; sensorNo++)
            {
                var key = Tuple.Create(sensorNo, VolNo);
                var value = new double[240];
                if (pcaMatrix.TryGetValue(key, out value))
                {
                    double[] inputVector = inputArray.GetRow(sensorNo - 1);
                    double[] meanValue = pcaMean[key];
                    double[] inputVectorCentered = new double[inputVector.Length];
                    for (int i = 0; i < inputVector.Length; i++)
                    {
                        inputVectorCentered[i] = inputVector[i] - meanValue[i];
                    }
                    double result = Matrix.Dot(inputVectorCentered, value);
                    if(afterPCA_dict.ContainsKey(key))
                    {
                        afterPCA_dict[key] = result;
                    }
                    else
                    {
                        afterPCA_dict.Add(key, result);
                    }
                }
            }
        }

        public static void transPCAdict_toArray(Dictionary<Tuple<int, int>, double> dict)
        {
            foreach(var pair in dict) 
            {
                var sensorNo = pair.Key.Item1;
                var volNo = pair.Key.Item2;

                afterPCA_array[sensorNo - 1, volNo] = pair.Value;
            }
        }
    }
}