using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace CSharpLibrary.CreateTypeLibrary
{
    // CreateTypeのインスタンス保持版
    public class CreateClass
    {
        //  設定されているプロパティを取得します
        public interface IUserData
        {
            string[] GetProperties();
        }

        private static Type Create(string className, Dictionary<String, Type> Properties)
        {
            AppDomain domain = AppDomain.CurrentDomain;
            AssemblyName assemblyName = new AssemblyName();
            assemblyName.Name = "TempAssembly.dll";
            AssemblyBuilder assemblyBuilder = domain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
            ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule(assemblyName.Name);
            TypeBuilder typeBuilder = moduleBuilder.DefineType(className, TypeAttributes.Public | TypeAttributes.Class, typeof(object), new Type[] { typeof(IUserData) });
            //  GetPropertiesを実装
            MethodBuilder getPropsMethod = typeBuilder.DefineMethod("GetProperties", MethodAttributes.Public | MethodAttributes.Virtual, typeof(string[]), Type.EmptyTypes);
            ILGenerator getPropsIL = getPropsMethod.GetILGenerator();
            getPropsIL.DeclareLocal(typeof(string[]));
            LoadInteger(getPropsIL, Properties.Count);
            getPropsIL.Emit(OpCodes.Newarr, typeof(string));
            getPropsIL.Emit(OpCodes.Stloc_0);
            for (int index = 0; index < Properties.Count; index++)
            {
                getPropsIL.Emit(OpCodes.Ldloc_0);
                LoadInteger(getPropsIL, index);
                getPropsIL.Emit(OpCodes.Ldstr, Properties.Keys.ElementAt(index));
                getPropsIL.Emit(OpCodes.Stelem_Ref);
            }

            getPropsIL.Emit(OpCodes.Ldloc_0);
            getPropsIL.Emit(OpCodes.Ret);

            MethodAttributes propAttr = MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig;
            //  プロパティを作成する
            foreach (var Propertie in Properties)
            {
                //  プライベートフィールドの作成
                FieldBuilder nameFieldBuilder = typeBuilder.DefineField(Propertie.Key + "_", Propertie.Value, FieldAttributes.Private);
                //  パブリックプロパティ
                PropertyBuilder namePropertyBuilder = typeBuilder.DefineProperty(Propertie.Key, PropertyAttributes.HasDefault, Propertie.Value, null);
                #region Getterメソッドの作成
                MethodBuilder getNameMethod = typeBuilder.DefineMethod("get_" + Propertie.Key, propAttr, Propertie.Value, Type.EmptyTypes);
                ILGenerator getNamePropIL = getNameMethod.GetILGenerator();
                getNamePropIL.Emit(OpCodes.Ldarg_0);
                getNamePropIL.Emit(OpCodes.Ldfld, nameFieldBuilder);
                getNamePropIL.Emit(OpCodes.Ret);
                #endregion
                #region Setterメソッドの作成
                MethodBuilder setNameMethod = typeBuilder.DefineMethod("set_" + Propertie.Key, propAttr, null, new Type[] { Propertie.Value });
                ILGenerator setNamePropIL = setNameMethod.GetILGenerator();
                setNamePropIL.Emit(OpCodes.Ldarg_0);
                setNamePropIL.Emit(OpCodes.Ldarg_1);
                setNamePropIL.Emit(OpCodes.Stfld, nameFieldBuilder);
                setNamePropIL.Emit(OpCodes.Ret);
                #endregion
                namePropertyBuilder.SetGetMethod(getNameMethod);
                namePropertyBuilder.SetSetMethod(setNameMethod);
            }
            Type retval = typeBuilder.CreateType();
            return retval;
        }

        private static void LoadInteger(ILGenerator il, int i)
        {
            switch (i)
            {
                case 0: il.Emit(OpCodes.Ldc_I4_0); break;
                case 1: il.Emit(OpCodes.Ldc_I4_1); break;
                case 2: il.Emit(OpCodes.Ldc_I4_2); break;
                case 3: il.Emit(OpCodes.Ldc_I4_3); break;
                case 4: il.Emit(OpCodes.Ldc_I4_4); break;
                case 5: il.Emit(OpCodes.Ldc_I4_5); break;
                case 6: il.Emit(OpCodes.Ldc_I4_6); break;
                case 7: il.Emit(OpCodes.Ldc_I4_7); break;
                case 8: il.Emit(OpCodes.Ldc_I4_8); break;
                case -1: il.Emit(OpCodes.Ldc_I4_M1); break;
                default:
                    if (-128 <= i && i <= 127)
                    {
                        il.Emit(OpCodes.Ldc_I4_S, i);
                    }
                    else
                    {
                        il.Emit(OpCodes.Ldc_I4, i);
                    }
                    break;
            }
        }

        private const string DEFALT_CLASS_NAME = "UserData";
        public readonly Type ClassType;
        public IUserData Instance { get; set; }
        #region コンストラクタ

        /// <summary>コンストラクタ</summary>
        /// <param name="Properties">プロパティ情報</param>
        public CreateClass(Dictionary<String, Type> Properties)
        : this(DEFALT_CLASS_NAME, Properties)
        {
        }

        public CreateClass(string className, Dictionary<String, Type> Properties)
        {
            ClassType = Create(className, Properties);
            Instance = NewInstance();
        }
        #endregion

        private IUserData NewInstance()
        {
            return Activator.CreateInstance(ClassType) as IUserData;
        }

        /// <summary>
        /// 指定したプロパティから値を取得する
        /// </summary>
        /// <param name="propertyName">プロパティ名</param>
        /// <param name="type">タイプ</param>
        /// <returns>取得した値</returns>
        public object GetProperty(string propertyName, Type type)
        {
            var info = Instance.GetType().GetProperty(propertyName, type);
            if (info == null || !info.CanRead)
            {
                return null;
            }
            return info.GetValue(Instance, null);
        }

        /// <summary>
        /// 指定したプロパティに値を設定する
        /// </summary>
        /// <param name="propertyName">プロパティ名</param>
        /// <param name="type">タイプ</param>
        public void SetProperty(string propertyName, object value, Type type)
        {
            var info = Instance.GetType().GetProperty(propertyName, type);
            if (info == null || !info.CanWrite)
            {
                return;
            }
            info.SetValue(Instance, value, null);
        }

    }
}
