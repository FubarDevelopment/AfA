using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.UserTypes;
using NHibernate.Type;
using System.Data;

namespace FubarDev.Afa.Entities.NHMapping.UserTypes
{
    public class AbschreibungsstatusJahr : PrimitiveType
    {
        public AbschreibungsstatusJahr()
            : base(new NHibernate.SqlTypes.AnsiStringSqlType())
        {
        }

        private string ToStringValue(object value)
        {
            if (value == null)
                return (string)DefaultValue;
            var data = (Abschreibungsstatus[])value;
            var result = new char[data.Length];
            for (int i = 0; i != data.Length; ++i)
            {
                result[i] = (char)(int)data[i];
            }
            return new string(result);
        }

        public override object DefaultValue
        {
            get { return "IIIIIIIIIIII"; }
        }

        public override string ObjectToSQLString(object value, NHibernate.Dialect.Dialect dialect)
        {
            return string.Format("'{0}'", ToStringValue(value).Replace("'", "''"));
        }

        public override Type PrimitiveClass
        {
            get { return typeof(Abschreibungsstatus[]); }
        }

        private object GetInstanceFromChar(Char c)
        {
            object instance;
            instance = Enum.ToObject(typeof(Abschreibungsstatus), c);
            if (Enum.IsDefined(typeof(Abschreibungsstatus), instance)) return instance;
            instance = Enum.ToObject(typeof(Abschreibungsstatus), Alternate(c));
            if (Enum.IsDefined(typeof(Abschreibungsstatus), instance)) return instance;
            throw new NHibernate.HibernateException(string.Format("Can't Parse {0} as {1}", c, typeof(Abschreibungsstatus).Name));
        }

        private char Alternate(char c)
        {
            return Char.IsUpper(c) ? Char.ToLower(c) : Char.ToUpper(c);
        }

        public override object FromStringValue(string xml)
        {
            var result = new Abschreibungsstatus[xml.Length];
            for (int i = 0; i != xml.Length; ++i)
                result[i] = (Abschreibungsstatus)GetInstanceFromChar(xml[i]);
            return result;
        }

        public override object Get(System.Data.IDataReader rs, string name)
        {
            return Get(rs, rs.GetOrdinal(name));
        }

        public override object Get(System.Data.IDataReader rs, int index)
        {
            return FromStringValue((string)rs[index]);
        }

        public override void Set(System.Data.IDbCommand cmd, object value, int index)
        {
            IDataParameter parm = cmd.Parameters[index] as IDataParameter;
            if (value == null)
            {
                parm.Value = DBNull.Value;
            }
            else
            {
                parm.DbType = DbType.AnsiString;
                parm.Value = ToStringValue(value);
            }
        }

        public override string Name
        {
            get { return "AbschreibungsstatusJahr"; }
        }

        public override Type ReturnedClass
        {
            get { return PrimitiveClass; }
        }
    }
}
