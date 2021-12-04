using System;
using System.Data;
using JIgor.Projects.OracleProcedureExecutor.Services;
using Oracle.ManagedDataAccess.Client;

namespace JIgor.Projects.OracleProcedureExecutor.Samples.SampleProcedures
{
    public class MathOperations
    {
        private readonly OracleExecutor _oracleExecutor;

        public MathOperations(OracleExecutor oracleExecutor)
        {
            _oracleExecutor = oracleExecutor;
        }

        private const string ProcedureName = "JIGOR_PROJECTS_PKG.math_operations";

        public OracleParameter InNumber1 = new OracleParameter()
        {
            ParameterName = "inNumber1",
            OracleDbType = OracleDbType.Decimal,
            Direction = ParameterDirection.Input
        };

        public OracleParameter InNumber2 = new OracleParameter()
        {
            ParameterName = "inNumber2",
            OracleDbType = OracleDbType.Decimal,
            Direction = ParameterDirection.Input
        };

        public OracleParameter OutOperation1 = new OracleParameter()
        {
            ParameterName = "outOperation1",
            OracleDbType = OracleDbType.Decimal,
            Direction = ParameterDirection.Output
        };

        public OracleParameter OutOperation2 = new OracleParameter()
        {
            ParameterName = "outOperation2",
            OracleDbType = OracleDbType.Decimal,
            Direction = ParameterDirection.Output
        };

        public OracleParameter OutOperation3 = new OracleParameter()
        {
            ParameterName = "outOperation3",
            OracleDbType = OracleDbType.Decimal,
            Direction = ParameterDirection.Output
        };

        public OracleParameter OutOperation4 = new OracleParameter()
        {
            ParameterName = "outOperation4",
            OracleDbType = OracleDbType.Decimal,
            Direction = ParameterDirection.Output
        };

        public void Run(decimal number1, decimal number2)
        {
            this.InNumber1.Value = number1;
            this.InNumber2.Value = number2;

            _ = _oracleExecutor.ExecuteStoredProcedure(ProcedureName, this.InNumber1, 
                this.InNumber2, 
                this.OutOperation1, 
                this.OutOperation2, 
                this.OutOperation3, 
                this.OutOperation4);

            Console.WriteLine($"{number1} + {number2} = {OutOperation1.Value}");
            Console.WriteLine($"{number1} - {number2} = {OutOperation2.Value}");
            Console.WriteLine($"{number1} * {number2} = {OutOperation3.Value}");
            Console.WriteLine($"{number1} / {number2} = {OutOperation4.Value}");
        }
    }
}
