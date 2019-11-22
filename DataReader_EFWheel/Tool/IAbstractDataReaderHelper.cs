using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataReader_EFWheel.Entity;
using MySql.Data.MySqlClient;

namespace DataReader_EFWheel.Tool
{
    public  interface IAbstractDataReaderHelper
    {
       List<T> Read<T>(int id) where T : BaseModel;

       int INSERT<T>(T t) where T : BaseModel;

        int Delete<T>(int id) where T : BaseModel;

       int Update<T>(T t) where T : BaseModel;


    }
}
