using Newtonsoft.Json;
using ErkerCore.Library.Enums;
using ErkerCore.Library.Helpers;
using ErkerCore.Message.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace ErkerCore.Library
{
    public static class ExtensionUtility
    {
        public const string Ok = "✓";
        public static List<T> CloneList<T>(this IEnumerable<T> list) where T : new()
        {
            List<T> result = new List<T>();
            foreach (var item in list)
            {
                T newList = new T();
                ExtensionUtility.Clone(item, newList);
                result.Add(newList);
            }
            return result;
        }
        public static Expression<Func<T, bool>> AndAlso<T>(this Expression<Func<T, bool>> exp, Expression<Func<T, bool>> expr2)
        {
            ParameterExpression param = exp.Parameters[0];
            if (ReferenceEquals(param, expr2.Parameters[0]))
            {

                exp = Expression.Lambda<Func<T, bool>>(
                     Expression.AndAlso(exp.Body, expr2.Body), param);
                return exp;
            }
            exp = Expression.Lambda<Func<T, bool>>(Expression.AndAlso(exp.Body, Expression.Invoke(expr2, param)), param);
            return exp;
        }
        public static FeatureModule FindModule(FeatureFlexAction flexAction)
        {
            BaseDetail currEnum = FeatureFlexAction.ProductCreate.GetEnumModuleAttribute().FirstOrDefault(o => o.Id == flexAction.ToInt64());
            return currEnum != null ? (FeatureModule)currEnum.Value.ToInt64() : FeatureModule.Uncertain;
        }
        public static ListPagination<T> ToPaginationList<T>(this List<T> list)
        {
            return new ListPagination<T>(list);
        }

        public static List<H> ConvertToList<T, H>(this List<T> list) where H : new()
        {
            return list.Select(o => (H)o.Clone(new H())).ToList();
        }
        public static List<BaseDetail> GetAllFeatureEnum()
        {
            List<BaseDetail> tables = new List<BaseDetail>();
            Assembly[] a = AppDomain.CurrentDomain.GetAssemblies().Where(o => o.FullName.Contains("Library")).ToArray();

            foreach (Type item in a[0].GetTypes())
            {
                bool include = item.Name.StartsWith("Feature") && item.IsEnum == true;
                bool exclude = false;
                bool valid = include && (!exclude);
                if (valid)
                {

                    Enum en = Activator.CreateInstance(item) as Enum;
                    List<BaseDetail> descVL = en.GetEnumDescriptionAttribute();
                    foreach (BaseDetail e in descVL)
                    {
                        tables.Add(new BaseDetail { DescriptionEn= e.DescriptionEn, Description = (descVL.Count > 0 ? e.Description : ""), Name = e.Name, Value = e.Id, Id = e.Id, Parent = item.Name });
                    }

                }

            }
            return tables;
        }
        public static void SetAllPropertiesToDefaultValue(this object o)
        {
            PropertyInfo[] properties = o.GetType().GetProperties();
            foreach (PropertyInfo pro in properties)
            {
                object val = o.GetPropValue(pro.Name);
                SetDefaultValue(ref val);
                if (pro.CanWrite)
                    o.SetPropValue(pro.Name, val);
            }
        }
        private static void SetDefaultValue(ref object value)
        {
            if (value != null)
            {
                Type t = value.GetType();
                if (t == typeof(string))
                {
                    value = "".DefaultString();
                }
                else if (t == typeof(char))
                {
                    value = "".DefaultString();
                }
                else if (t == typeof(int))
                {
                    value = -1;
                }
                else if (t == typeof(long))
                {
                    value = -1;
                }
                else if (t == typeof(double))
                {
                    value = double.MinValue;
                }
                else if (t == typeof(DateTime))
                {
                    value = DateTime.Now.DefaultDateTime();
                }
                else if (t == typeof(TimeSpan))
                {
                    value = TimeSpan.MinValue;
                }
                else if (t.IsEnum)
                {
                    value = -1;
                }
            }
            else
            {
                value = null;
                return;
            }

        }

        public static List<AttributeInfo<T>> GetAttr<T>(this object entity, bool includeNullAttribute = false, string propName = "") where T : Attribute
        {
            List<AttributeInfo<T>> result = new List<AttributeInfo<T>>();
          
            if (entity != null)
            {
                PropertyInfo[] properties = entity.GetType().GetProperties();
                foreach (PropertyInfo pro in properties)
                {

                    string full = propName + (propName.IsNullOrEmpty() ? "" : ".") + pro.Name;
                    T attr = pro.GetCustomAttribute<T>();

                    if (attr != null)
                    {
                        result.Add(new AttributeInfo<T> { FullName = full, Name = pro.Name, PropertiesValue = entity.GetPropValue(pro.Name), Attribute = attr, DataType = pro.PropertyType });
                    }
                    if (pro.PropertyType.IsClass && pro.PropertyType.Namespace != "System" && pro.PropertyType.Name != "List`1")
                    {

                        result.AddRange(GetAttr<T>(entity.GetPropValue(pro.Name), includeNullAttribute, full));

                    }
                    if (includeNullAttribute && attr == null)
                    {
                        result.Add(new AttributeInfo<T> { FullName = full, Name = pro.Name, PropertiesValue = entity.GetPropValue(pro.Name), Attribute = null, DataType = pro.PropertyType });
                    }
                }
            }
            return result;
        }

        public static bool IsDefaultValue(this object value)
        {
            if (value == null)
                return true;
            Type t = value.GetType();
            if (t == typeof(string))
            {
                return value.ToString() == "".DefaultString();
            }
            else if (t == typeof(char))
            {
                return value.ToString() == "".DefaultString();
            }
            else if (t == typeof(int))
            {
                return value.ToInt32() == (-1);
            }
            else if (t == typeof(long))
            {
                return value.ToInt64() == (-1);
            }
            else if (t == typeof(double))
            {
                return value.ToDoubleTry() == double.MinValue;
            }
            else if (t == typeof(DateTime))
            {
                return value.CastTo<DateTime>() == DateTime.Now.DefaultDateTime();
            }
            else if (t == typeof(TimeSpan))
            {
                return value.CastTo<TimeSpan>() == TimeSpan.MinValue;
            }
            else if (t.IsEnum)
            {
                return value.ToInt64() == -1;
            }
            return false; // demek ki değişiklik var
        }
        public static TimeSpan ToTimeSpan(this string time)
        {


            TimeSpan t = new TimeSpan();
            TimeSpan.TryParse(time, out t);


            return t;
        }
        public static DateTime RemoveSecond(this DateTime date)
        {
            return date.Date.Add(new TimeSpan(0, date.Hour, date.Minute, 0));
        }
        public static List<T> IndentComboTreeview<T>(this List<T> list) where T : IParentChild<T>, new()
        {
            foreach (T sub in list)
            {
                string p = "";
                for (int i = 0; i < (sub.Level*2)-1; i++)
                {
                    p += " ";
                }
                p += "⮡";
              
                sub.Name = p + " "+sub.Name ;
            }
         /*   foreach (var item in sm)
            {
                Debug.WriteLine(item.Level > 0 ? item.Name : item.Name.Replace("⮡ ", ""));
            }
           */
            return list;
        }

        public static List<T> Traverse<T>(this List<T> main, TraverseResult result, int maxDepth = 1, List<T> childs = null, List<T> list = null, List<T> combotree = null, int level = -1) where T : IParentChild<T>, new()
        {
            if (combotree == null)
                combotree = new List<T>();
            if (list == null)
                list = new List<T>();
            if (childs == null)
                childs = main.Where(o => o.ParentId < 1).OrderBy(o => o.ParentId).ToList();
            level++;
            if (childs != null)
                foreach (T item in childs)
                {
                    item.Level = level;
                    List<T> c = new List<T>();
                    if (item is IParentChildCustomUnique)
                        c = main.Where(o => ((IParentChildCustomUnique)o).UniqueParentKey == ((IParentChildCustomUnique)item).UniqueChildKey).ToList();
                    else
                        c = c = main.Where(o => o.ParentId == item.Id).OrderBy(o => o.ParentId).ToList();

                    if (c.Count > 0)
                    {
                        if (item.ParentId == -1)
                        {
                            level = 0;

                            if (result == TraverseResult.Treeview)
                                list.Add(item);
                            else
                                combotree.Add(item);
                        }
                        if (item.Childs == null)
                        {
                            if (result == TraverseResult.Treeview)
                            {
                                item.Childs = new List<T>();
                                item.Childs.AddRange(c);
                            }
                            else
                                combotree.Add(c[0]);
                            if (level < maxDepth)
                            {
                                Traverse(main, result, maxDepth, c, list, combotree, level);
                            }
                        }
                    }
                    else if (c.Count == 0 && item.ParentId == -1)
                    {
                        level = 0;
                        item.Level = level;
                        if (result == TraverseResult.Treeview)
                            list.Add(item);
                        else
                            combotree.Add(item);
                    }
                    else if (c.Count == 0 && item.ParentId > 0)
                    {

                        item.Level = level;
                        if (result == TraverseResult.ComboTreeview && combotree.Exists(o=> o.Id== item.Id)==false)

                            combotree.Add(item);
                    }
                }
            return result == TraverseResult.Treeview ? list : combotree;
        }
        public static string TurkishDateTime(this DateTime dt)
        {
            return dt.ToString("dd.MM.yyyy HH:mm:ss");
        }
        public static string EnglishDateTime(this DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd HH:mm:ss");
        }
        public static string EnglishDateForLog(this DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd");
        }
        public static string ToUpperTurkish(this string o)
        {
            return o.ToUpper(System.Globalization.CultureInfo.GetCultureInfo("tr"));
        }
        public static string Trim(this string o, int len)
        {
            return o.Length > len ? o.Substring(0, len) : o;
        }
        public static List<BaseDetail> GetEnumDescriptionAttribute(this Enum E)
        {

            FieldInfo[] fi = E.GetType().GetFields();
            List<BaseDetail> l = new List<BaseDetail>();
            foreach (var item in fi)
            {
                if (item.Name == "value__") continue;
                DescriptionML[] attributes = (DescriptionML[])item.GetCustomAttributes(typeof(DescriptionML), false);
                l.Add(new BaseDetail { Id = item.GetValue(item).ToInt64(), Description = attributes != null && attributes.Length > 0 ? attributes[0].Description : "", DescriptionEn =  attributes != null && attributes.Length > 0 ? attributes[0].DescriptionEn : "", Name = item.Name, Parent = item.ReflectedType.Name });
            }
            return l;
        }
        public static List<BaseDetail> GetEnumModuleAttribute(this Enum E)
        {

            FieldInfo[] fi = E.GetType().GetFields();
            List<BaseDetail> l = new List<BaseDetail>();
            foreach (var item in fi)
            {
                if (item.Name == "value__") continue;
                ModuleAttribute[] attributes = (ModuleAttribute[])item.GetCustomAttributes(typeof(ModuleAttribute), false);
                if (attributes.Length > 0)
                {
                    l.Add(new BaseDetail { Id = item.GetValue(item).ToInt64(), Value = (attributes[0] as ModuleAttribute).FeatureModule, Parent = item.ReflectedType.Name , Name= item.Name });
                }
            }
            return l;
        }
      
        public static List<BaseDetail> DescriptionNameValue<T>()
        {
            FieldInfo[] fi = typeof(T).GetFields();
            List<BaseDetail> l = new List<BaseDetail>();
            foreach (var item in fi)
            {
                if (item.Name == "value__") continue;
                DescriptionAttribute[] attributes = (DescriptionAttribute[])item.GetCustomAttributes(typeof(DescriptionAttribute), false);
                l.Add(new BaseDetail { Id = item.GetValue(item).ToInt64(), Description = attributes != null && attributes.Length > 0 ? attributes[0].Description : "", Name = item.Name });
            }
            return l;
        }
        public static long ValueFromDesc<T>(this string desc)
        {
            List<BaseDetail> list = ExtensionUtility.DescriptionNameValue<T>();
            BaseDetail rec = list.FirstOrDefault(o => o.Description.ToUpperTurkish() == desc.Trim().ToUpperTurkish());

            return rec != null ? rec.Id : -1;
        }
        public static long ValueFromName<T>(this string name)
        {
            var list = ExtensionUtility.DescriptionNameValue<T>();
            return list.FirstOrDefault(o => o.Name == name).Id;
        }
        public static string NameFromValue<T>(this int value)
        {
            var list = ExtensionUtility.DescriptionNameValue<T>();
            return list.FirstOrDefault(o => o.Id == value).Name;
        }
        public static string NameFromValue<T>(this long value)
        {
            return NameFromValue<T>(value.ToInt32());
        }
        public static string DescFromValue<T>(this long value)
        {
            var list = ExtensionUtility.DescriptionNameValue<T>();
            return list.FirstOrDefault(o => o.Id == value)?.Description;
        }
        public static string DescFromName<T>(this string value)
        {
            var list = ExtensionUtility.DescriptionNameValue<T>();
            return list.FirstOrDefault(o => o.Name == value)?.Description;
        }
        public static string DescFromValue<T>(this int value)
        {
            return value.ToInt64().DescFromValue<T>();
        }
        public static string ToLowerTurkish(this string o)
        {
            return o.ToLower(System.Globalization.CultureInfo.GetCultureInfo("tr"));
        }
        public static string OnlyNumbersOfTelephone(this string o)
        {
            return o.Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", "");
        }
        public static string ToDoubleStringTurkish(this object o)
        {
            return o.ToString().Replace(".", ",");
        }
        public static string ToDoubleStringEnglish(this object o)
        {

            return o.ToString().Replace(",", ".");
        }
        public static string ToDoubleStringEnglish(this string o)
        {
            return o.Replace(",", ".");
        }
        public static string ToDoubleStringEnglish(this double o)
        {
            return o.ToString().Replace(",", ".");
        }
        public static int MaxValue<T>(this Enum en)
        {
            return Enum.GetValues(typeof(T)).Cast<int>().Max();
        }
        public static int MinValue<T>(this Enum en)
        {
            return Enum.GetValues(typeof(T)).Cast<int>().Min();
        }
        public static void RemoveHandler<T>(EventHandler<T> e) where T : EventArgs
        {
            EventHandler<T> dl = (EventHandler<T>)e.GetInvocationList()[e.GetInvocationList().ToList().Count - 1];
            foreach (Delegate d in e.GetInvocationList())
            {
                e -= (EventHandler<T>)d;
            }
        }
        public static DateTime DefaultDateTime(this DateTime dateTime)
        {
            return new DateTime(1980, 1, 1);
        }
        public static DateTime DayEndTime(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 23, 59, 59);
        }
        public static DateTime DayStartTime(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 00, 00, 00);
        }
        public static string ToDatetimeSapB1(this DateTime dt)
        {
            return dt.ToString("yyyyMMdd");
        }
        public static string ToDateTimeString(this DateTime dt)
        {
            return dt.ToString("yyyyMMdd");
        }
        public static string ToEnglishDate(this DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd");
        }
        public static string ToEnglishDateTime(this DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd HH:mm:ss");
        }
        public static DateTime ToDateTime(this string dt)
        {
            return dt.Length >= 10 ? Convert.ToDateTime(dt) : DefaultDateTime(DateTime.Now);
        }
        public static T GetValue<T>(this DataRow row, string column)
        {
            if (row[column] == DBNull.Value)
            {
                return default(T);
            }
            else
                return (T)row[column];
        }
        public static string GetValue(this DataRow row, string column)
        {
            return row[column].ToString();

        }
        public static string UpperCase(string Value)
        {
            return Value.ToUpper(System.Globalization.CultureInfo.CurrentCulture);
        }
        public static int ToInt32(this string v)
        {
            return Convert.ToInt32(v);

        }
        public static short ToShort(this string v)
        {
            return Convert.ToInt16(v);

        }
        public static bool IsNullOrEmpty(this string v)
        {
            return string.IsNullOrEmpty(v) || v.IsDefaultValue();

        }
        public static string ToNullString(this object value)
        {
            return value == null ? "" : value.ToString();
        }
        public static long ToInt64(this string v)
        {
            return Convert.ToInt64(v);

        }
        public static long ToInt64(this object o)
        {
            if (o is DBNull || o is null) return long.MinValue;
            if (o is bool)

                return Convert.ToBoolean(o) == true ? 1 : 0;

            if (o is Enum)
                return (int)o;

            if (o is double)
            {
                return Convert.ToInt64(o);
            }
            return o.ToString().ToInt64();

        }
        public static T CastTo<T>(this object o)
        {
            return (T)o;
        }
        public static long ToInt64Try(this object o)
        {

            long a = long.MinValue;
            if (o is DBNull || o is null) return long.MinValue;
            bool result = long.TryParse(o.ToString(), out a);

            return result ? a : long.MinValue;
        }
        public static short ToShort(this object o)
        {
            if (o is DBNull || o is null) return short.MinValue;
            if (o is bool)
                return Convert.ToBoolean(o) == true ? (short)1 : (short)0;

            if (o is Enum)
                return (short)o;
            if (o is double)
                return Convert.ToInt16(o);

            return o.ToString().ToShort();
        }
        public static int ToInt32(this object o)
        {
            if (o is DBNull || o is null) return int.MinValue;
            if (o is bool)
                return Convert.ToBoolean(o) == true ? 1 : 0;

            if (o is Enum)
                return (int)o;
            if (o is double)
                return Convert.ToInt32(o);

            return o.ToString().ToInt32();
        }
        public static int ToCeiling(this object o)
        {
            if (o is DBNull || o is null) return int.MinValue;

            if (o is double)
                return Convert.ToInt32(Math.Ceiling((double)o));
            else if (o is decimal)
                return Convert.ToInt32(Math.Ceiling((decimal)o));
            else if (o is int)
                return o.ToInt32();
            return int.MinValue;
        }
        public static int ToInt32Try(this object o)
        {
            int a = int.MinValue;
            if (o is DBNull || o is null) return int.MinValue;
            bool result = int.TryParse(o.ToString(), out a);

            return result ? a : int.MinValue;
        }
        public static double ToDoubleTry(this object o)
        {
            if (o is DBNull) return 0;
            double a = double.MinValue;
            bool result = double.TryParse(o.ToString(), out a);
            return result ? a : double.MinValue;
        }
        public static T ParseEnum<T>(this string str)
        {
            object value;
            try
            {
                value = Enum.Parse(typeof(T), str, true);
                return (T)value;

            }
            catch (Exception)
            {

                return default(T);
            }

        }
        public static List<T> ConvertToList<T>(this DataTable dt)
        {
            List<string> columnNames = dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName).ToList();
            PropertyInfo[] properties = typeof(T).GetProperties();
            return dt.Rows.OfType<DataRow>().Select(row =>
            {
                T objT = Activator.CreateInstance<T>();
                foreach (var pro in properties)
                {
                    if (columnNames.Contains(pro.Name))
                    {
                        PropertyInfo pI = objT.GetType().GetProperty(pro.Name);
                        pro.SetValue(objT, row[pro.Name] == DBNull.Value ? null : Convert.ChangeType(row[pro.Name], pI.PropertyType));
                    }
                }
                return objT;
            }).ToList();
        }
        public static TimeSpan DefaultTimeSpan(this TimeSpan dateTime)
        {
            return TimeSpan.MinValue;
        }
        public static double DefaultDouble(this double d)
        {
            return double.MinValue;
        }
        public static char DefaultChar(this char d)
        {
            return '∑';
        }
        public static string DefaultString(this string d)
        {
            return "∑";
        }
        public static string DefaultValidateDateTime(this string d)
        {
            return "DateTime.Now";
        }
        public static string Serialize(this object obj)
        {
            return JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
        }
        public static T DeSerialize<T>(this string obj)
        {
            if (!obj.IsNullOrEmpty())
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(obj);
            else
                return default(T);

        }
        public static object DeSerialize(this string obj)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject(obj);
        }
        public static object DeSerialize(this string obj,Type type)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject(obj,type);
        }
        private static byte[] GetUTF8Bytes(this string txt)
        {
            return Encoding.UTF8.GetBytes(txt);
        }
        public static string ComputeSHA256(this string password)
        {
            byte[] passwordBytes = password.GetUTF8Bytes();
            byte[] salt = "ErkerCore".GetUTF8Bytes();
            byte[] saltedPassword = passwordBytes.Concat(salt).ToArray();
            byte[] hashedPassword = new SHA256Managed().ComputeHash(saltedPassword);
            return BitConverter.ToString(hashedPassword).Replace("-", String.Empty);
        }
        public static object Clone(this object orj, object target, bool onlyNotDefaultValue = false)
        {
            PropertyInfo[] targetProps = target.GetType().GetProperties((BindingFlags.Public | BindingFlags.Instance) | BindingFlags.GetProperty);
            PropertyInfo[] properties = orj.GetType().GetProperties((BindingFlags.Public | BindingFlags.Instance) | BindingFlags.GetProperty);
            foreach (PropertyInfo propertyInfo in properties)
            {
                bool found = false;
                foreach (PropertyInfo p in targetProps)
                {
                    if (p.Name == propertyInfo.Name)
                    {
                        found = true;
                        break;
                    }
                }
                object value = GetPropValue(orj, propertyInfo.Name);
                if (onlyNotDefaultValue && value.IsDefaultValue())
                    continue;
                if (propertyInfo.CanWrite && found)
                    SetPropValue(target, propertyInfo.Name, value);

            }
            return target;
        }
        public static T CloneGeneric<T>(this object orj, T target, bool onlyNotDefaultValue = false)
        {
            Clone(orj, target, onlyNotDefaultValue);
            return target;
        }
        public static T CloneGeneric<T>(this object orj, bool onlyNotDefaultValue = false) where T : new()
        {

            T target = new T();
            Clone(orj, target, onlyNotDefaultValue);
            return target;
        }

        public static T Clone<T>(this object obj) where T : new()
        {
            return (T)obj.Clone(new T());
        }
        public static object GetPropValue(this PropertyInfo src)
        {

            return src.GetValue(src, null);
        }
        public static object GetPropValue(this object src, string propName)
        {
            if (propName.Contains("."))
            {
                string[] part = propName.Split(".", StringSplitOptions.None);
                foreach (var item in part)
                {
                    src = src.GetType().GetProperty(item).GetValue(src, null);
                }
                return src;
            }
            else

                return src.GetType().GetProperty(propName)?.GetValue(src, null);
        }
        public static void SetPropValue(this object src, string propName, object value)
        {
            if (propName.Contains("."))
            {
                string[] part = propName.Split(".", StringSplitOptions.None);
                for (int i = 0; i < part.Length - 1; i++)
                {
                    src = src.GetType().GetProperty(part[i]).GetValue(src, null);
                }
                src.SetPropValue(part[part.Length - 1], value);

            }
            else
                src.GetType().GetProperty(propName)?.SetValue(src, value, null);
        }

    }
}
