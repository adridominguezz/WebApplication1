﻿using ClassLibrary1.PostgreDataStruct;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static SkiaSharp.HarfBuzz.SKShaper;

namespace ClassLibrary1
{
    public class PostgreeCon
    {
        public static NpgsqlDataSource? dataSource;
        public void IniciarCon()
        {
            var connectionString = "Host=localhost;Port=5432;Username=postgres;Password=Granada13;Database=postgres;Pooling=true;";
            dataSource = NpgsqlDataSource.Create(connectionString);
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString);
            dataSourceBuilder.MapComposite<imagen>();
            dataSourceBuilder.MapComposite<proveedor>();
            dataSource = dataSourceBuilder.Build();
        }
        public List<T> ConsultaTest<T>(string consulta, object parameters = null) where T : new()
        {
            using var command = dataSource.CreateCommand(consulta);

            // Agregar parámetros si se proporcionan
            if (parameters != null)
            {
                foreach (var property in parameters.GetType().GetProperties())
                {
                    command.Parameters.AddWithValue("@" + property.Name, property.GetValue(parameters, null));
                }
            }

            using var reader = command.ExecuteReader();
            List<T> result = new List<T>();

            while (reader.Read())
            {
                T DatoInterno = new T();
                Type t = DatoInterno.GetType();
                PropertyInfo[] prop = t.GetProperties();
                int count = 0;
                foreach (var itemprop in prop)
                {
                    var tipon = reader.GetPostgresType(count);
                    Type o = itemprop.PropertyType;
                    MethodInfo method = reader.GetType().GetMethod("GetFieldValue")
                            .MakeGenericMethod(new Type[] { o });
                    object? r = method.Invoke(reader, new object[] { count });

                    itemprop.SetValue(DatoInterno, r);
                    count++;
                }
                result.Add(DatoInterno);
            }
            return result;
        }

        public List<T> ConsultaTest<T>(string tabla, T datofiltro) where T : new()
        {

            string consulta = string.Format("Select * from {0}", tabla);

            Type tin = datofiltro.GetType(); 
            PropertyInfo[] propcons = tin.GetProperties();


            string wherecondition = string.Format(" where ");
            foreach (var itemprop in propcons)
            {
                
                var objreaded = itemprop.GetValue(datofiltro);
                if (objreaded != null)
                {
                    if (itemprop.PropertyType == Type.GetType("System.String"))
                    {
                        wherecondition = string.Format("{0} {1} = '{2}'", wherecondition, itemprop.Name, objreaded);
                    }
                    else
                    {
                        wherecondition = string.Format("{0} {1} = '{2}'", wherecondition, itemprop.Name, objreaded);
                    }
                }
            }
           


            using var command = dataSource.CreateCommand(consulta + wherecondition);
            using var reader = command.ExecuteReader();
            List<T> result = new List<T>();

            while (reader.Read())
            {
                T DatoInterno = new T();
                Type t = DatoInterno.GetType();
                PropertyInfo[] prop = t.GetProperties();
                int count = 0;
                foreach (var itemprop in prop)
                {

                    var tipon = reader.GetPostgresType(count);
                    Type o = itemprop.PropertyType;
                    MethodInfo method = reader.GetType().GetMethod("GetFieldValue")
                             .MakeGenericMethod(new Type[] { o });
                    object? r = method.Invoke(reader, new object[] { count });


                    itemprop.SetValue(DatoInterno, r);


                    count++;
                }
                result.Add(DatoInterno);
            }
            return result;
        }



        public void InsertTest<T>(string tabla, T DatosEscritura) where T : new()
        {
            string columnas = string.Empty;
            string values = string.Empty;

            List<NpgsqlParameter> Parameter = new List<NpgsqlParameter>();
            Type t = DatosEscritura.GetType();
            PropertyInfo[] prop = t.GetProperties();
            int count = 1;
            foreach (var itemprop in prop)
            {
                if (itemprop.GetValue(DatosEscritura) != null)
                {
                    columnas += itemprop.Name + ",";
                    values += $"${count}" + ",";
                    var val = itemprop.GetValue(DatosEscritura);
                    Parameter.Add(new() { Value = itemprop.GetValue(DatosEscritura) });
                    count++;
                }

            }
            columnas = columnas.Trim(',');
            values = values.Trim(',');
            var conn = dataSource.OpenConnection();
            conn.ReloadTypes();
            string consulta = $"INSERT INTO {tabla}({columnas}) VALUES ({values})";
            var cmd = new NpgsqlCommand(consulta, conn);
            foreach (var p in Parameter)
                cmd.Parameters.Add(p);
            int rows = cmd.ExecuteNonQuery();

        }

    }
}
