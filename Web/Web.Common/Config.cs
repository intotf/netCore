using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Web.Common
{
    public class Config
    {
        /// <summary>
        /// 获取AppSettins 配置文件
        /// </summary>
        private IConfigurationRoot appSetBuilder;


        public static Config Instance = new Config("appsettings.json");


        private Config(string jsonPath = "appsettings.json")
        {
            appSetBuilder = new ConfigurationRoot(jsonPath).Build;
        }

        /// <summary>
        /// 根据指定键值获取配置文件值
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        public object GetAppSetting(string Key)
        {
            try
            {
                var obj = appSetBuilder[Key];
                return obj;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 获取配置项转换为指定实体
        /// </summary>
        /// <typeparam name="T">需要转换的实体</typeparam>
        /// <returns></returns>
        public T GetAppSetting<T>() where T : class, new()
        {
            try
            {
                var model = RefSetModel<T>();
                return model;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 获取指定对象配置信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private T RefSetModel<T>() where T : class, new()
        {
            var model = new T();
            var type = typeof(T);
            model = RefType(model);
            return model;
        }

        /// <summary>
        /// 反射获取配置信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model">要转换的实体</param>
        /// <param name="parentName">父级配置字符串</param>
        /// <returns></returns>
        private T RefType<T>(T model, string parentName = "") where T : class, new()
        {
            var t = model.GetType();
            var modelAttr = t.GetCustomAttribute(typeof(ConfigNameAttribute), false);
            var modelName = modelAttr == null ? t.Name : ((ConfigNameAttribute)modelAttr).Name;
            foreach (var p in t.GetProperties())
            {
                ///获取别名特性
                var configAttr = p.GetCustomAttribute(typeof(ConfigNameAttribute), false);
                var configName = p.Name;
                if (configAttr != null)
                {
                    configName = ((ConfigNameAttribute)configAttr).Name;
                }
                ///判断是否为对象
                if (p.PropertyType.IsArray ||
                    (p.PropertyType.IsClass &&
                    !p.PropertyType.IsGenericType &&
                    !p.PropertyType.Equals(typeof(String)) &&
                    !p.PropertyType.IsValueType))
                {
                    var obj = Activator.CreateInstance(p.PropertyType); //造建对象
                    //递归反射对象
                    parentName += $"{modelName}:{configName}";
                    var pModel = RefType(obj, parentName);
                    p.SetValue(model, pModel, null);
                }
                else
                {
                    var settingKey = $"{modelName}:{configName}";
                    if (!string.IsNullOrEmpty(parentName))
                    {
                        settingKey = $"{parentName}:{configName}";
                    }
                    //读取配置信息
                    var value = Convert.ChangeType(GetAppSetting(settingKey), p.PropertyType);
                    p.SetValue(model, value, null);
                }
            }
            return model;
        }

        /// <summary>
        /// 获取指定 Json 配置文件
        /// </summary>
        private class ConfigurationRoot
        {
            /// <summary>
            /// 当前配置文件信息
            /// </summary>
            public IConfigurationRoot Build { get; }

            /// <summary>
            /// 构建 ConfigurationRoot
            /// </summary>
            /// <param name="jsonPath"></param>
            public ConfigurationRoot(string jsonPath)
            {

                this.Build = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile(jsonPath, optional: true, reloadOnChange: true)
                 .Build();
            }
        }
    }


}
